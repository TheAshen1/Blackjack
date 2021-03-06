import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, AfterViewChecked } from '@angular/core';
import { GameLogicService } from '../../services/gameLogic.service';
import { CardLogic } from '../../models/cardLogic';
import { GameLogic } from '../../models/gameLogic';
import { PlayerLogic } from '../../models/playerLogic';
import { CardValues, Card } from '../../utils/cardValues';
import * as $ from 'jquery';

@Component({
    selector: 'blackjack',
    templateUrl: './blackjack.component.html',
    styleUrls: ['./blackjack.component.css'],
    providers: [GameLogicService]
})
export class BlackJack {

    constructor(private gameLogicService: GameLogicService) { }

    gameData: GameLogic | undefined;

    gameIsGoingOn: boolean = false;
    canPlaceBets: boolean = false;
    roundFinished: boolean = true;
    message: string = "";
    positions: Array<[number, number]> = [];


    onBegin(data: GameLogic) {
        this.gameData = data;
        if (this.gameData.players == undefined)
            this.gameData.players = [];
        this.gameData.players.forEach(function (player) { player.cards = []; });
        this.gameIsGoingOn = true;

        this.startTheGame();
    }


    startTheGame() {
        if (this.gameData == undefined || this.gameData.gameId == "00000000-0000-0000-0000-000000000000") {
            console.log('gameData is undefined');
            this.gameIsGoingOn = false;
            return;
        }
        this.calculatePositions()
        this.roundFinished = false;
        this.message = "";

        this.canPlaceBets = true;
        this.gameData.players.forEach((player, index) => {
            player.x = this.positions[index][0];
            player.y = this.positions[index][1];
            player.bet = player.chips > 10 ? 10 : player.chips;
            player.score = 0;
        });

    }

    continueGame() {
        this.canPlaceBets = false;
        if (this.gameData == undefined || this.gameData.gameId == "00000000-0000-0000-0000-000000000000") {
            console.log('gameData is undefined');
            this.gameIsGoingOn = false;
            return;
        }

        var thePlayer: PlayerLogic | undefined;
        this.gameData.players.forEach((player, index) => {

            if (player.isBot)
                this.drawTwoCards(player);
            else
                thePlayer = player;
        });
        if (thePlayer == undefined) {
            console.log('cannot find the player');
            return;
        }
        this.drawTwoCards(thePlayer);
    }

    calculatePositions(): any {
        var maxNumberOfPlayers = 7;
        var minPlayerHeight = 200;
        var maxPlayerWidth = 200;
        var containerWidth = 800;
        var containerHeight = 600;
        var radius = 250;
        var angle = 0;
        var step = 2 * Math.PI / maxNumberOfPlayers;

        for (var i = 0; i < maxNumberOfPlayers; i++) {

            var temp: [number, number] = [
                Math.round(containerWidth / 2 + radius * Math.cos(angle) - maxPlayerWidth / 2),
                Math.round(containerHeight / 2 + radius * Math.sin(angle) - minPlayerHeight / 2)
            ];

            this.positions.push(temp);

            angle += step;
        }
    }

    drawCard(player: PlayerLogic) {
        if (player.currentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return; }
        this.gameLogicService.giveCard(player.currentRoundPlayerId)
            .subscribe((data) => {
                this.handleCard(data.json() as CardLogic, player);
                this.countScore(player);
            });
    }
    drawTwoCards(player: PlayerLogic) {
        if (player.currentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return; }
        this.gameLogicService.giveCard(player.currentRoundPlayerId)
            .subscribe((data) => {
                this.handleCard(data.json() as CardLogic, player);
                this.drawCard(player);
            });
    }
    handleCard(card: CardLogic, player: PlayerLogic) {

        if (CardValues.values[card.value as Card] != 1) {
            card.uri = 'assets/images/' + CardValues.values[card.value as Card] + card.suit.charAt(0) + '.png';
        } else {
            card.uri = 'assets/images/' + card.value.charAt(0) + card.suit.charAt(0) + '.png';
        }
        player.cards.push(card);
    }

    countScore(player: PlayerLogic) {
        var score = 0;
        var aces = 0;

        player.cards.forEach(
            function (card: CardLogic) {
                var value = CardValues.values[card.value as Card];
                if (CardValues.values[card.value as Card] != 1) {
                    score += value;
                }
                else {
                    aces++;
                }
            }
        );

        for (var i = 0; i < aces; i++) {
            if (score <= 21 - (aces + score)) {
                score += 11;
            }
            else {
                score++;
            }
        }
        //console.log("player:" + player.name + " score: " + score);

        player.score = score;


        if (!player.isBot && player.score >= 21) {
            this.finishRound();
        }
        if (player.isBot && player.cards.length >= 2 && player.score < 17) {
            this.drawCard(player);
        }

    }


    finishRound() {
        if (this.gameData == undefined) {
            console.log('gameData is undefined');
            return;
        }
        if (this.gameData.players == undefined) {
            console.log('players are undefined');
            return;
        }

        this.gameData.players.forEach((player) => {
            if (player.name == 'Dealer') {
                return;
            }
            this.gameLogicService.placeBet(player.currentRoundPlayerId, player.bet)
                .subscribe((answer) => {
                    if (answer.text() == 'false') {
                        console.log('Could not place bet');
                    }
                });
        });


        this.roundFinished = true;
        var winners: string[] = [];
        var maxScore: number = 0;
        this.gameData.players.forEach((player) => {

            if (player.score > maxScore && player.score <= 21) {
                winners = [];
                winners.push(player.name || "anonymous");
                maxScore = player.score;
            }
            else if (player.score === maxScore && player.score <= 21) {
                winners.push(player.name || "anonymous");
            }
        });

        this.message = "";
        if (winners.length === 1)
            this.message = "The winner is " + winners;
        else
            this.message = "Winners are: " + winners;
        alert(this.message);


        this.gameData.players.forEach((player) => {
            if (!player.isBot && player.bet == player.chips && winners.findIndex((w) => w == player.name) < 0) {
                alert('Game Over');
                this.finishGame();
            }
        });
    }

    /////// after round is finished
    moveToTheNextRound() {

        if (this.gameData == undefined) {
            console.log('gameData is undefined');
            return;
        }

        this.gameLogicService.startNewGameRound(this.gameData.gameId)
            .subscribe((data) => {
                var tempData = data.json() as GameLogic;

                this.gameData = tempData;
                if (this.gameData.players == undefined) {
                    console.log('new round data is undefined');
                    this.finishGame();
                    return;
                }

                this.gameData.players.forEach((player, index) => {
                    player.cards = [];
                });
                this.startTheGame()
            });
    }

    finishGame() {

        if (this.gameData == undefined) {
            console.log('gameData is undefined');
            return;
        }

        this.gameLogicService.finishTheGame(this.gameData.gameId)
            .subscribe((data) => {
                this.gameIsGoingOn = false;
            });
    }

}

