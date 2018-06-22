import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent 
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'blackjack', pathMatch: 'full' },
            {
                path: 'blackjack',
                loadChildren: './modules/blackjack.module#BlackJackModule'
            },
            {
                path: 'games',
                loadChildren: './modules/gameList.module#GameListModule'
            },
            {
                path: 'players',
                loadChildren: './modules/playerList.module#PlayerListModule'
            },
            {
                path: 'rounds',
                loadChildren: './modules/roundList.module#RoundListModule'
            },
            {
                path: 'roundPlayers',
                loadChildren: './modules/roundPlayerList.module#RoundPlayerListModule'
            },
            { path: '**', redirectTo: 'blackjack' }
        ])
    ]
})
export class AppModuleShared {
}
