import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { BlackJack } from '../components/blackjack/blackjack.component';
import { GameStart } from '../components/blackjack/gameStart/gameStart.component';

@NgModule({
    declarations: [
        BlackJack,
        GameStart,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: '',
                component: BlackJack
            }
        ])
    ]
})
export class BlackJackModule {
}
