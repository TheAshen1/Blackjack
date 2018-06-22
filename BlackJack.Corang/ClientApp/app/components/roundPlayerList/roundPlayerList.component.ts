import { Component, OnInit } from '@angular/core';
import { RoundPlayerDataService } from '../../services/roundPlayerData.service';
import { RoundPlayer } from '../../models/roundPlayer';

@Component({
    selector: 'roundPlayer-list',
    templateUrl: './roundPlayerList.component.html',
    styleUrls: ['./roundPlayerList.component.css'],
    providers: [RoundPlayerDataService]
})
export class RoundPlayerList implements OnInit {

    roundPlayer: RoundPlayer | undefined;
    roundPlayers: RoundPlayer[] = [];
    tableMode: boolean = true;

    constructor(private roundPlayerDataService: RoundPlayerDataService) { }

    ngOnInit(): void {
        this.loadRoundPlayers();
    }

    loadRoundPlayers() {
        this.roundPlayerDataService.getRoundPlayers()
            .subscribe((data) => this.roundPlayers = data.json() as RoundPlayer[]);
    }

    save() {
        if (this.roundPlayer == undefined) {
            console.log('roundPlayer is undefined');
            return;
        }

        if (this.roundPlayer.id == null) {
            this.roundPlayerDataService.createRoundPlayer(this.roundPlayer)
                .subscribe((data) => this.loadRoundPlayers());
        } else {
            this.roundPlayerDataService.updateRoundPlayer(this.roundPlayer)
                .subscribe(data => this.loadRoundPlayers());
        }
        this.cancel();
    }
    editRoundPlayer(rp: RoundPlayer) {
        this.roundPlayer = rp;
    }
    cancel() {
        this.roundPlayer = undefined;
        this.tableMode = true;
    }
    delete(rp: RoundPlayer) {
        if (rp != null && rp.id != null) {
            this.roundPlayerDataService.deleteRoundPlayer(rp.id)
                .subscribe(data => this.loadRoundPlayers());
        }
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}
