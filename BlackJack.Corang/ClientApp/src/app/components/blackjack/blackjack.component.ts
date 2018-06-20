import { Component, OnInit } from '@angular/core';
import { GameLogicService } from '../../services/gameLogic.service';
import { CardLogic } from '../../models/cardLogic';
import { GameLogic } from '../../models/gameLogic';
import { PlayerLogic } from '../../models/playerLogic';
import { CardValues, Card } from '../../utils/cardValues';


@Component({
    selector: 'blackjack',
    templateUrl: './blackjack.component.html',
    styleUrls: ['./blackjack.component.css'],
    providers: [GameLogicService]
})
export class BlackJack {


    constructor(private gameLogicService: GameLogicService) { }

    gameData: GameLogic = new GameLogic();

    gameIsGoingOn: boolean = false;
    roundFinished: boolean = false;
    message: string = "";

    onBegin(data: GameLogic) {
        this.roundFinished = false;
        this.gameData = data;
        if (this.gameData.Players == null)
            this.gameData.Players = [];
        this.gameData.Players.forEach(function (player) { player.Cards = []; });
        this.gameIsGoingOn = true;

        setTimeout(this.startTheGame(), 50000);

    }


    startTheGame() {

        if (this.gameData.Players == null) {
            console.log('players is null');
            return;
        }
        var timeOut = 0;
        this.gameData.Players.forEach((player) => {
            setTimeout(this.drawTwoCards(player), timeOut);
            timeOut += 10000;
        });

        //this.gameData.Players.forEach( (player) => {
        //  if (!player.IsBot) {
        //    this.countScore(player);
        //    return;
        //  }
        //  while (this.countScore(player) < 17) {
        //    this.drawCard(player);
        //  }
        //});
    }

    drawCard(player: PlayerLogic) {
        console.log("drawing card");
        if (player.CurrentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return;}
        this.gameLogicService.giveCard(player.CurrentRoundPlayerId)
            .subscribe((data: string) => { this.handleCard(data, player); this.countScore(player); }
            );
    }
    drawTwoCards(player: PlayerLogic) {
        console.log("drawing two cards");
        if (player.CurrentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return; }
        this.gameLogicService.giveCard(player.CurrentRoundPlayerId)
            .subscribe((data: string) => { this.handleCard(data, player); this.drawCard(player); });
    }

    handleCard(data: string, player: PlayerLogic) {    
        var cardData = data.split(' ');
        var newCard = new CardLogic(cardData[0], cardData[2]);

        if (CardValues.values.hasOwnProperty(cardData[0])) {
            newCard.uri = 'assets/images/' + CardValues.values[newCard.value as Card] + newCard.suit.charAt(0) + '.png';
            //newCard.uri = 'http://localhost:4200/' + cardValues[cardData[0]] + cardData[2].charAt(0) + '.png';
        } else {
            newCard.uri = 'assets/images/' + newCard.value.charAt(0) + newCard.suit.charAt(0) + '.png';
            //newCard.uri = 'http://localhost:4200/' + cardData[0].charAt(0) + cardData[2].charAt(0)+ '.png';
        }
        console.log(newCard);
        player.Cards.push(newCard);
    }

    countScore(player: PlayerLogic) {
        var score = 0;
        var aces = 0;

        player.Cards.forEach(
            function (card: CardLogic) {
                var value = CardValues.values[card.value as Card];
                console.log(value);
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
        console.log("player:" + player.Name + " score: " + score);
        return score;
    }

    finishRound(data: boolean): void {
        this.roundFinished = data;
        console.log("player pressed 'stand'");
        if (this.gameData.Players == null) { console.log('gameData.Players is null'); return;}
        var winners: string[] = [];
        var maxScore: number = 0;
        this.gameData.Players.forEach((player) => {
            let playerScore = this.countScore(player);

            if (playerScore > maxScore && playerScore <= 21) {
                winners = [];
                winners.push(player.Name || "anonymous");
                maxScore = playerScore;
            }
            else if (playerScore === maxScore && playerScore <= 21) {
                winners.push(player.Name || "anonymous");
            }
        });

        this.message = "";
        if (winners.length === 1)
            this.message = "The winner is " + winners;
        else
            this.message = "Winners are: " + winners;
        alert(this.message);
    }


    moveToTheNextRound() {
        if (this.gameData.GameId == null) { console.log('gameData.GameId is null'); return;}
        this.gameLogicService.startNewGameRound(this.gameData.GameId)
            .subscribe((data: GameLogic) => { this.gameData = data; });
    }

    finishGame() {

    }


}

