import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Round } from '../models/round';


@Injectable()
export class RoundDataService {
    private url = "http://localhost:57060/api/RoundValues";

  constructor(private http: HttpClient) {
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
