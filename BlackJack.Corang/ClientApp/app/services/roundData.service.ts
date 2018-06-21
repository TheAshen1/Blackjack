import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Round } from '../models/round';


@Injectable()
export class RoundDataService {
    private url = "api/RoundValues/";

    constructor(private http: Http) {
    }

    getRounds() {
        return this.http.get(this.url + 'RetrieveAllRounds');
    }
    getRound(id: string) {
        return this.http.get(this.url + 'RetrieveRound/' + id);
    }
    createRound(round: Round) {
        return this.http.post(this.url + 'CreateRound', round);
    }
    updateRound(round: Round) {

        return this.http.put(this.url + 'UpdateRound/' + round.id, round);
    }
    deleteRound(id: string) {
        return this.http.delete(this.url + 'DeleteRound/' + id);
    }
}
