import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { GameList } from '../components/gameList/gamelist.component';

@NgModule({
    declarations: [
        GameList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: '',
                component: GameList
            }
        ])
    ]
})
export class GameListModule {
}
