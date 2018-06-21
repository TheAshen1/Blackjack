import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Player } from '../models/player';


@Injectable()
export class PlayerDataService {
    private url = 'api/PlayerValues/';

    constructor(private http: Http) {
    }

    getPlayers() {
        return this.http.get(this.url + 'RetrieveAllPlayers');
    }
    getPlayer(id: string) {
        return this.http.get(this.url + 'RetrievePlayer/' + id);
    }
    createPlayer(player: Player) {
        return this.http.post(this.url + 'CreatePlayer', player);
    }
    updatePlayer(player: Player) {

        return this.http.put(this.url + 'UpdatePlayer/' + player.id, player);
    }
    deletePlayer(id: string) {
        return this.http.delete(this.url + 'DeletePlayer/' + id);
    }
}
