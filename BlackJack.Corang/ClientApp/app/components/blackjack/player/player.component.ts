import { Component, OnInit, Input, OnChanges, SimpleChanges, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { GameLogicService } from '../../../services/gameLogic.service';
import { PlayerLogic } from '../../../models/playerLogic';
import { CardLogic } from '../../../models/cardLogic';
import { CardValues, Card } from '../../../utils/cardValues';

@Component({
    selector: 'player',
    templateUrl: './player.component.html',
    styleUrls: ['./player.component.css'],
    providers: [GameLogicService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlayerComponent implements OnInit, OnChanges {

    @Input() _player: PlayerLogic = new PlayerLogic();
    @Input() _cards: CardLogic[] = [];


    set player(p: PlayerLogic) {
        this._player = p;
        console.log('player ' + this._player.Name + ' set');
    }


    set cards(c: CardLogic[]) {
        this._cards = c;
        console.log('player ' + this._player.Name + ' got his cards');
    }

    @Output() standing = new EventEmitter<boolean>();

    _score: number = 0;

    constructor(private gameLogicService: GameLogicService) { }

    ngOnChanges(changes: SimpleChanges): void {
        console.log("updated");
        this.countScore()
        console.log('player ' + this._player.Name + ' _score: ' + this._score);
    }
    ngOnInit(): void {
        console.log('player ' + this._player.Name + ' init');
        //this._score = 0;
        //this.cards = [];
        //this.drawCard();
        //this.drawCard();
        //console.log(this.countScore());
        //if (this.player.IsBot) {
        //  while (this.countScore() < 17) {
        //    this.drawCard();
        //  }
        //}

    }

    public drawCard() {
        console.log("drawing card");
        if (this._player.CurrentRoundPlayerId == null) { console.log('CurrentRoundPlayerId is null'); return;}
        this.gameLogicService.giveCard(this._player.CurrentRoundPlayerId)
            .subscribe((data: string) => { this.handleCard(data); this.countScore(); }
            );
    }
    private handleCard(data: string) {

        var cardData: string[] = data.split(' ');
        var newCard = new CardLogic(cardData[0], cardData[2]);

        if (CardValues.values.hasOwnProperty(cardData[0])) {
            //newCard.uri = 'assets/images/' + cardValues[cardData[0]] + cardData[2].charAt(0) + '.png';
            newCard.uri = './' + CardValues.values[newCard.value as Card] + newCard.suit.charAt(0) + '.png';
        } else {
            //newCard.uri = 'assets/images/' + cardData[0].charAt(0) + cardData[2].charAt(0) + '.png';
            newCard.uri = './' + newCard.value.charAt(0) + newCard.suit.charAt(0) + '.png';
        }
        console.log(newCard);
        this._cards.push(newCard);
    }

    public countScore() {
        var score = 0;
        var aces = 0;

        this._cards.forEach(
            function (card) {
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


        this._score = score;

        return score;
    }

    public stand() {
        this.standing.emit(true);
    }
}
