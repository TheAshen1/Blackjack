import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { PlayerList } from '../components/playerList/playerList.component';

@NgModule({
    declarations: [
        PlayerList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: '',
                component: PlayerList
            }
        ])
    ]
})
export class PlayerListModule {
}
