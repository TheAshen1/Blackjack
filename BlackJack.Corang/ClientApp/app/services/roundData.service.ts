import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Round } from '../models/round';


@Injectable()
export class RoundDataService {
    private url = "http://localhost:57060/api/RoundValues";

    constructor(private http: Http) {
    }

    getRounds() {
        return this.http.get(this.url);
    }
    getRound(id: string) {
        return this.http.get(this.url + '/' + id);
    }
    createRound(round: Round) {
        return this.http.post(this.url, round);
    }
    updateRound(round: Round) {

        return this.http.put(this.url + '/' + round.Id, round);
    }
    deleteRound(id: string) {
        return this.http.delete(this.url + '/' + id);
    }
}
