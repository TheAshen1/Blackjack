import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from '@angular/router';

import { GridModule } from '@progress/kendo-angular-grid';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { MenuModule } from '@progress/kendo-angular-menu';

import { AppComponent } from './app.component';
import { Home } from './components/home/home.component';
import { GameStart } from './components/home/gameStart/gameStart.component';
import { GameBody } from './components/home/gameBody/gameBody.component';
import { GameList } from './components/gameList/gameList.component';
import { PlayerList } from './components/playerList/playerList.component';
import { RoundList } from './components/roundList/roundList.component';
import { RoundPlayerList } from './components/roundPlayerList/roundPlayerList.component';


const appRoutes: Routes = [
  { path: '', component: Home },
  { path: 'games', component: GameList },
  { path: 'players', component: PlayerList },
  { path: 'rounds', component: RoundList },
  { path: 'roundPlayers', component: RoundPlayerList },
  { path: '**', redirectTo: '' }
];

@NgModule({
  declarations: [
    AppComponent,
    GameList,
    PlayerList,
    RoundList,
    RoundPlayerList,
    Home,
    GameStart,
    GameBody
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    GridModule,
    ButtonsModule,
    MenuModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
