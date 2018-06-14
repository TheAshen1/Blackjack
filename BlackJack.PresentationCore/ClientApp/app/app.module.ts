//import { NgModule } from '@angular/core';
//import { BrowserModule } from '@angular/platform-browser';
//import { FormsModule } from '@angular/forms';
//import { HttpClientModule } from '@angular/common/http';
//import { AppComponent } from './app.component';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { GameListComponent } from './game-list.component';
import { GameDetailComponent } from './game-detail.component';

// определение маршрутов
const appRoutes: Routes = [
    { path: '', component: GameListComponent },
    { path: 'game/:id', component: GameDetailComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, GameListComponent, GameDetailComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }