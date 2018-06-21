import { Component, OnInit, Input, OnChanges, SimpleChanges, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { PlayerLogic } from '../../../models/playerLogic';
import { CardLogic } from '../../../models/cardLogic';
import { CardValues, Card } from '../../../utils/cardValues';

@Component({
    selector: 'card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css'],
    changeDetection: ChangeDetectionStrategy.Default
})
export class CardComponent implements OnInit {

    _card: CardLogic = new CardLogic('0', 'null');
    @Output() notify: EventEmitter<boolean> = new EventEmitter<boolean>();

    @Input()
    set card(c: CardLogic) {
        this._card = c;
        //console.log('card ' + this._card.value + ' ' + this._card.suit + ' got value');
        this.notify.emit(true);
    }

    ngOnInit(): void {
        //console.log('card ' + this._card.value + ' ' + this._card.suit + ' init');
    }
 
}
