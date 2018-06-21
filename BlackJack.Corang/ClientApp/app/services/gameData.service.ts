import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Game } from '../models/game';


@Injectable()
export class GameDataService {
    private url = 'api/GameValues/';

    constructor(private http: Http) {
    }

    getGames() {
        return this.http.get(this.url + 'RetrieveAllGames');
    }
    getGame(id: string) {
        return this.http.get(this.url + 'RetrieveGame/' + id);
    }
    createGame(game: Game) {
        return this.http.post(this.url + 'CreateGame', game);
    }
    updateGame(game: Game) {

        return this.http.put(this.url + 'UpdateGame/' + game.id, game);
    }
    deleteGame(id: string) {
        return this.http.delete(this.url + 'DeleteGame/' + id);
    }
}
