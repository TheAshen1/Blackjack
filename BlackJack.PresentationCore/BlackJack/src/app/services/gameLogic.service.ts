import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameLogic } from '../models/gameLogic';


@Injectable()
export class GameLogicService {
  private url = "http://localhost:57060/api/GameLogic";

  constructor(private http: HttpClient) {
  }

  giveCard(roundPlayerId: string) {
    return this.http.get(this.url, {
      params: {
        roundPlayerId 
      }
    });
  }
  startNewGame(playerName: string, numberOfBots) {
    return this.http.get(this.url, {
      params: {
        playerName,
        numberOfBots
      }
    });
  }
  startNewGameRound(gameId: string) {
    return this.http.get(this.url, {
      params: {
        gameId
      }
    });
  }
  finishTheGame(gameId: string) {
    return this.http.get(this.url, {
      params: {
        gameId
      }
    });
  }
  
}
