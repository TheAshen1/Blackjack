import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from './game';

@Injectable()
export class DataService {

    private url = "http://localhost:57060/api/GameValues";

    constructor(private http: HttpClient) {
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