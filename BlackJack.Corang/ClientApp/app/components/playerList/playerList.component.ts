import { Component, OnInit } from '@angular/core';
import { PlayerDataService } from '../../services/playerData.service';
import { Player } from '../../models/player';

@Component({
    selector: 'player-list',
    templateUrl: './playerList.component.html',
    styleUrls: ['./playerList.component.css'],
    providers: [PlayerDataService]
})
export class PlayerList implements OnInit {

    player: Player = new Player();
    players: Player[] = [];
    tableMode: boolean = true;

    constructor(private playerDataService: PlayerDataService) { }

    ngOnInit(): void {
        this.loadPlayers();
    }

    loadPlayers() {
        this.playerDataService.getPlayers()
            .subscribe((data) => this.players = data.json() as Player[]);
    }

    save() {
        if (this.player.id == null) {
            this.playerDataService.createPlayer(this.player)
                .subscribe((data) => this.loadPlayers());
        } else {
            this.playerDataService.updatePlayer(this.player)
                .subscribe(data => this.loadPlayers());
        }
        this.cancel();
    }
    editPlayer(p: Player) {
        this.player = p;
    }
    cancel() {
        this.player = new Player();
        this.tableMode = true;
    }
    delete(p: Player) {
        if (p != null && p.id != null) {
            this.playerDataService.deletePlayer(p.id)
                .subscribe(data => this.loadPlayers());
        }
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}
