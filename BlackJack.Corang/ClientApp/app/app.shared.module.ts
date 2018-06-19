import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { BlackJack } from './components/blackjack/blackjack.component';
import { GameStart } from './components/blackjack/gameStart/gameStart.component';
import { PlayerComponent } from './components/blackjack/player/player.component';
import { GameList } from './components/gameList/gamelist.component';
import { PlayerList } from './components/playerList/playerList.component';
import { RoundList } from './components/roundList/roundList.component';
import { RoundPlayerList } from './components/roundPlayerList/roundPlayerList.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'blackjack', component: BlackJack },
            { path: 'games', component: GameList },
            { path: 'players', component: PlayerList },
            { path: 'rounds', component: RoundList },
            { path: 'roundPlayers', component: RoundPlayerList },

            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
