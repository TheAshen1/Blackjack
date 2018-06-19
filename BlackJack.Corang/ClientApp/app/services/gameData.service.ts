import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Game } from '../models/game';


@Injectable()
export class GameDataService {
    private url = "http://localhost:57060/api/GameValues";

    constructor(private http: Http) {
    }

    getGames() {
        return this.http.get(this.url);
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
