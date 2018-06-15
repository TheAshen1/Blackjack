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

  roundPlayer: RoundPlayer = new RoundPlayer();
  roundPlayers: RoundPlayer[];
  tableMode: boolean = true;

  constructor(private roundPlayerDataService: RoundPlayerDataService) { }

  ngOnInit(): void {
    this.loadRoundPlayers();
  }

  loadRoundPlayers() {
    this.roundPlayerDataService.getRoundPlayers()
      .subscribe((data: RoundPlayer[]) => this.roundPlayers = data);
  }

  save() {
    if (this.roundPlayer.Id == null) {
      this.roundPlayerDataService.createRoundPlayer(this.roundPlayer)
        .subscribe((data: RoundPlayer) => this.roundPlayers.push(data));
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
    this.roundPlayer = new RoundPlayer();
    this.tableMode = true;
  }
  delete(rp: RoundPlayer) {
    this.roundPlayerDataService.deleteRoundPlayer(rp.Id)
      .subscribe(data => this.loadRoundPlayers());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}
