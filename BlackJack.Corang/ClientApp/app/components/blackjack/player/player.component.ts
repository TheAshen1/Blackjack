import { Component, OnInit, Input, OnChanges, SimpleChanges, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { PlayerLogic } from '../../../models/playerLogic';
import { CardLogic } from '../../../models/cardLogic';
import { CardValues, Card } from '../../../utils/cardValues';

@Component({
    selector: 'player',
    templateUrl: './player.component.html',
    styleUrls: ['./player.component.css'],
    changeDetection: ChangeDetectionStrategy.Default
})
export class PlayerComponent implements OnInit, OnChanges {

    _player: PlayerLogic = new PlayerLogic();
    _score: number = 0;
    playerFinishedRound: boolean = false;

    @Output() forbidDraw: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Output() draw: EventEmitter<PlayerLogic> = new EventEmitter<PlayerLogic>();

    @Input()
    set player(p: PlayerLogic) {
        this._player = p;
        //console.log('player ' + this._player.name + ' set');
    }



    ngOnChanges(changes: SimpleChanges): void {
        //console.log("changed");
        this.countScore()

        //console.log('player ' + this._player.name + ' _score: ' + this._score);
    }
    ngOnInit(): void {
        if (this._player.cards == null) {
            this._player.cards = [];
        }
        //console.log('player ' + this._player.name + ' init');
    } 

    public countScore() {
        var score = 0;
        var aces = 0;

        this._player.cards.forEach(
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


        if (this._player.isBot && this._player.cards.length >= 2 && this._score < 17) {
            this.askForCard();
        }


        if (!this._player.isBot && this._score >= 21) {
            this.stand();
        } 
    }

    askForCard() {
        this.playerFinishedRound = true;
        this.draw.emit(this._player);
    }

    stand() {
        this.playerFinishedRound = true;
        this.forbidDraw.emit(true);
    }
}
