import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player } from '../models/player';


@Injectable()
export class PlayerDataService {
  private url = "http://localhost:57060/api/PlayerValues";

  constructor(private http: HttpClient) {
  }

  getPlayers() {
    return this.http.get(this.url);
  }
  getPlayer(id: string) {
    return this.http.get(this.url + '/' + id);
  }
  createPlayer(player: Player) {
    return this.http.post(this.url, player);
  }
  updatePlayer(player: Player) {

    return this.http.put(this.url + '/' + player.Id, player);
  }
  deletePlayer(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
