import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';




import { RoundPlayerList } from './components/roundPlayerList/roundPlayerList.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,     
        HomeComponent,


        RoundPlayerList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            {
                path: 'blackjack',
                loadChildren: './components/blackjack/blackjack.module#BlackJackModule'
            },
            {
                path: 'games',
                loadChildren: './components/gameList/gameList.module#GameListModule'
            },
            {
                path: 'players',
                loadChildren: './components/playerList/playerList.module#PlayerListModule'
            },
            {
                path: 'rounds',
                loadChildren: './components/roundList/roundList.module#RoundListModule'
            },
            {
                path: 'roundPlayers',
                loadChildren: './components/roundPlayerList/roundPlayerList.module#RoundPlayerListModule'
            },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
