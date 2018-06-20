import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from '../models/game';


@Injectable()
export class GameDataService {
    private url = "api/GameValues";

  constructor(private http: HttpClient) {
    }

    getGames() {
        return this.http.get(this.url + '/GetAll');
    }
    getGame(id: string) {
        return this.http.get(this.url + '/' + id);
    }
    createGame(game: Game) {
        return this.http.post(this.url, game);
    }
    updateGame(game: Game) {

        return this.http.put(this.url + '/' + game.Id, game);
    }
    deleteGame(id: string) {
        return this.http.delete(this.url + '/' + id);
    }
}
