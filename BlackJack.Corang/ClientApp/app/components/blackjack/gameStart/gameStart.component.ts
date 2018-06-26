import { Component, OnInit, Output, EventEmitter, Injectable, Inject } from '@angular/core';
import { GameLogicService } from '../../../services/gameLogic.service';
import { GameLogic } from '../../../models/gameLogic';
import { CookieService } from 'ngx-cookie-service/cookie-service/cookie.service';

@Component({
    selector: 'game-start',
    templateUrl: './gameStart.component.html',
    styleUrls: ['./gameStart.component.css'],
    providers: [GameLogicService, CookieService]
})
export class GameStart implements OnInit {

    playerName: string = "";
    numberOfBots: number = 0;
    userIsNew: boolean = true;
    cookieValueUserName: string = "unknown";
    private cookieKeyUserId: string = "_UserId";
    private cookieKeyUserName: string = "_UserName";

    @Output() begin = new EventEmitter<GameLogic>();

    constructor(private gameLogicService: GameLogicService, private cookieService: CookieService) { }

    ngOnInit(): void {
        this.cookieValueUserName = this.cookieService.get(this.cookieKeyUserName);
        if (this.cookieValueUserName != undefined && this.cookieValueUserName != "") {
            this.userIsNew = false;
        }
        else {
            this.userIsNew = true;
        }
    }

    start() {
        this.gameLogicService.startNewGame(this.playerName, this.numberOfBots)
            .subscribe((data) => {
                var parsedData = data.json()as GameLogic;
                var thePlayerId = "";
                var thePlayerName = "";
                parsedData.players.forEach((player) => {
                    if (!player.isBot) {
                        thePlayerId = player.id;
                        thePlayerName = player.name;
                    }
                });

                if (thePlayerId == "") {
                    console.log('thePlayer was not found');
                }
                else {
                    this.cookieService.set(this.cookieKeyUserId, thePlayerId);
                    this.cookieService.set(this.cookieKeyUserName, thePlayerName);
                }
                this.begin.emit(parsedData);
            });
    }

    startAuthentificated() {
        this.gameLogicService.startNewGameAuthenificated(this.playerName, this.numberOfBots, this.cookieService.get(this.cookieKeyUserId))
            .subscribe((data) => {
                var parsedData = data.json() as GameLogic;
                this.begin.emit(parsedData);
            });
    }
}
