import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Game } from '../models/game';


@Injectable()
export class GameDataService {
    private url = "api/GameValues";

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }

    getGames() {
        return this.http.get(this.baseUrl + this.url + '/RetrieveAllGames');
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
