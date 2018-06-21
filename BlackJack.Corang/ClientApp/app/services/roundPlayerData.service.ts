import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { RoundPlayer } from '../models/roundPlayer';


@Injectable()
export class RoundPlayerDataService {
    private url = "api/RoundPlayerValues/";

    constructor(private http: Http) {
    }

    getRoundPlayers() {
        return this.http.get(this.url + 'RetrieveAllRoundPlayers');
    }
    getRoundPlayer(id: string) {
        return this.http.get(this.url + 'RetrieveRoundPlayer/' + id);
    }
    createRoundPlayer(roundPlayer: RoundPlayer) {
        return this.http.post(this.url + 'CreateRoundPlayer', roundPlayer);
    }
    updateRoundPlayer(roundPlayer: RoundPlayer) {
        return this.http.put(this.url + 'UpdateRoundPlayer/' + roundPlayer.id, roundPlayer);
    }
    deleteRoundPlayer(id: string) {
        return this.http.delete(this.url + 'DeleteRoundPlayer/' + id);
    }
}
