import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
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
export class BlackJack implements AfterViewInit {

    constructor(private gameLogicService: GameLogicService) { }

    gameData: GameLogic = new GameLogic();

    gameIsGoingOn: boolean = false;
    roundFinished: boolean = true;
    message: string = "";

    @ViewChild("roundBody")
    private roundBody: ElementRef | undefined;


    ngAfterViewInit(): void {
        if (this.gameIsGoingOn)
            this.positionPlayer();
    }

    onBegin(data: GameLogic) {
        this.gameData = data;
        if (this.gameData.players == null)
            this.gameData.players = [];
        this.gameData.players.forEach(function (player) { player.cards = []; });
        this.gameIsGoingOn = true;

        setTimeout(this.startTheGame(), 50000);

    }


    startTheGame() {

     

        this.roundFinished = false;
        this.message = "";
        if (this.gameData.players == null) {
            console.log('players is null');
            return;
        }

        var timeOut = 0;
        this.gameData.players.forEach((player) => {
            setTimeout(this.drawTwoCards(player), timeOut);
            timeOut += 10000;
        });
    }

    positionPlayer(): any {
        console.log('positioning players');
        if (this.gameData.players == null) {
            console.log('no players found');
            return;
        }
        if (this.roundBody == undefined) {
            console.log('roundBody is undefined');
            return;
        }

        var radius = 250;
        var width = this.roundBody.nativeElement.clientWidth;
        var height = this.roundBody.nativeElement.clientHeight;
        var angle = 0;
        var step = 2 * Math.PI / this.gameData.players.length;

        var minPlayerHeight = 100;
        var maxPlayerWidth = 60;

        this.gameData.players.forEach(function (player, index) {
            player.x = Math.round(width / 2 + radius * Math.cos(angle) - maxPlayerWidth / 2);
            player.y = Math.round(height / 2 + radius * Math.sin(angle) - minPlayerHeight / 2);
            angle += step;
        });


        //var radius = 250;
        //var fields = $('.player'),
        //    container = $('#roundBody'),
        //    width = container.width(),
        //    height = container.height(),
        //    angle = 0,
        //    step = 2 * Math.PI / fields.length;

        //if (fields == null) {
        //    console.log('cannot get players');
        //    return;
        //}
        //fields.each(function () {

        //    if (width == undefined || height == undefined) {
        //        console.log('height or width is undefined');
        //        return;
        //    }
        //    var player = $(this);

        //    if (player == undefined) {
        //        console.log('player is undefined');
        //        return;
        //    }
        //    var x = Math.round(width / 2 + radius * Math.cos(angle) - (player.width() as number)/ 2);
        //    var y = Math.round(height / 2 + radius * Math.sin(angle) - (player.height() as number) / 2);
        //    player.css({
        //        left: x + 'px',
        //        top: y - this.clientHeight + 'px'
        //    });
        //    angle += step;
        //});
    }

    drawCard(player: PlayerLogic) {
        //console.log("drawing card");
        if (player.currentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return; }
        this.gameLogicService.giveCard(player.currentRoundPlayerId)
            .subscribe((data) => {
                this.handleCard(data.text(), player); this.countScore(player);
            }
            );
    }
    drawTwoCards(player: PlayerLogic) {
        //console.log("drawing two cards");
        if (player.currentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return; }
        this.gameLogicService.giveCard(player.currentRoundPlayerId)
            .subscribe((data) => { this.handleCard(data.text(), player); this.drawCard(player); });
    }
    handleCard(data: string, player: PlayerLogic) {
        var cardData = data.split(' ');
        var newCard = new CardLogic(cardData[0], cardData[2]);

        if (CardValues.values[newCard.value as Card] != 1) {
            newCard.uri = 'assets/images/' + CardValues.values[newCard.value as Card] + newCard.suit.charAt(0) + '.png';
            //newCard.uri = 'http://localhost:4200/' + cardValues[cardData[0]] + cardData[2].charAt(0) + '.png';
        } else {
            newCard.uri = 'assets/images/' + newCard.value.charAt(0) + newCard.suit.charAt(0) + '.png';
            //newCard.uri = 'http://localhost:4200/' + cardData[0].charAt(0) + cardData[2].charAt(0)+ '.png';
        }
        //console.log(newCard);
        player.cards.push(newCard);
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
        console.log("player:" + player.name + " score: " + score);

        player.score = score;


        if (!player.isBot && player.score >= 21) {
            setTimeout(this.finishRound(), 10000);
        }
        if (player.isBot && player.cards.length >= 2 && player.score < 17) {
            setTimeout(this.drawCard(player), 1000);
        }
    }


    finishRound(): void {
        this.roundFinished = true;

        if (this.gameData.players == null) { console.log('gameData.Players is null'); return; }
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
    }


    playerDrawCard() {
        if (this.gameData.players == null) {
            console.log('players is null');
            return;
        }
        var player = this.gameData.players.find((player) => !player.isBot);
        if (player == null) {
            console.log('cannot find the player');
            return;
        }
        this.drawCard(player);

    }


    /////// anfter round is finished
    moveToTheNextRound() {
        if (this.gameData.gameId == null || this.gameData.players == null) { console.log('gameData is null'); return; }

        this.gameLogicService.startNewGameRound(this.gameData.gameId)
            .subscribe((data) => {
                var tempData = data.json() as GameLogic;
                //var playersData = tempData.players as PlayerLogic[];
                //if (tempData == null || tempData.players == null) {
                //    console.log('new round data is null');
                //    return;
                //}

                //this.gameData.currentRoundId = tempData.currentRoundId;
                //playersData.forEach((newPlayerData, index) => {
                //    if (this.gameData.players == null) { console.log('players is null'); return; }

                //    this.gameData.players[index].currentRoundPlayerId = newPlayerData.currentRoundPlayerId;
                //    this.gameData.players[index].cards = []; 
                //});

                //this.roundFinished = false;


                this.gameData = tempData;
                if (this.gameData.players == null) {
                    console.log('new round data is null');
                    return;
                }

                this.gameData.players.forEach((player, index) => {
                    player.cards = [];
                });
                this.startTheGame()
            });
    }

    finishGame() {
        if (this.gameData.gameId == null) { console.log('gameData.GameId is null'); return; }
        this.gameLogicService.finishTheGame(this.gameData.gameId)
            .subscribe((data) => {
                this.gameIsGoingOn = false;
            });
    }

}

