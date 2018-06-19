import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { RoundPlayer } from '../models/roundPlayer';


@Injectable()
export class RoundPlayerDataService {
    private url = "http://localhost:57060/api/RoundPlayerValues";

    constructor(private http: Http) {
    }

    getRoundPlayers() {
        return this.http.get(this.url);
    }
    getRoundPlayer(id: string) {
        return this.http.get(this.url + '/' + id);
    }
    createRoundPlayer(roundPlayer: RoundPlayer) {
        return this.http.post(this.url, roundPlayer);
    }
    updateRoundPlayer(roundPlayer: RoundPlayer) {

        return this.http.put(this.url + '/' + roundPlayer.Id, roundPlayer);
    }
    deleteRoundPlayer(id: string) {
        return this.http.delete(this.url + '/' + id);
    }
}
