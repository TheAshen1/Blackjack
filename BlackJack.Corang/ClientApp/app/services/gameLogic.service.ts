import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { GameLogic } from '../models/gameLogic';


@Injectable()
export class GameLogicService {
    private url = 'api/GameLogic/';

    constructor(private http: Http) {
    }

    giveCard(roundPlayerId: string) {
        return this.http.get(this.url + 'GiveCard/', {
            params: {
                roundPlayerId
            }
        });
    }
    placeBet(roundPlayerId: string, bet: number) {
        return this.http.get(this.url + 'PlaceBet/', {
            params: {
                roundPlayerId,
                bet
            }
        });
    }
    startNewGame(playerName: string, numberOfBots: number) {
        return this.http.get(this.url + 'StartNewGame/', {
            params: {
                playerName,
                numberOfBots
            }
        });
    }
    startNewGameAuthenificated(playerName: string, numberOfBots: number, userId: string) {
        return this.http.get(this.url + 'StartNewGameAuthentificated/', {
            params: {
                playerName,
                numberOfBots,
                userId
            }
        });
    }
    startNewGameRound(gameId: string) {
        return this.http.get(this.url + 'StartNewGameRound/', {
            params: {
                gameId
            }
        });
    }
    finishTheGame(gameId: string) {
        return this.http.get(this.url + 'FinishTheGame/', {
            params: {
                gameId
            }
        });
    }

}
