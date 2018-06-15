import { Component, OnInit } from '@angular/core';
import { RoundDataService } from '../../services/roundData.service';
import { Round } from '../../models/round';

@Component({
  selector: 'round-list',
  templateUrl: './roundList.component.html',
  styleUrls: ['./roundList.component.css'],
  providers: [RoundDataService]
})
export class RoundList implements OnInit {

  round: Round = new Round();
  rounds: Round[];
  tableMode: boolean = true;

  constructor(private roundDataService: RoundDataService) { }

  ngOnInit(): void {
    this.loadRounds();
  }

  loadRounds() {
    this.roundDataService.getRounds()
      .subscribe((data: Round[]) => this.rounds = data);
  }

  save() {
    if (this.round.Id == null) {
      this.roundDataService.createRound(this.round)
        .subscribe((data: Round) => this.rounds.push(data));
    } else {
      this.roundDataService.updateRound(this.round)
        .subscribe(data => this.loadRounds());
    }
    this.cancel();
  }
  editRound(r: Round) {
    this.round = r;
  }
  cancel() {
    this.round = new Round();
    this.tableMode = true;
  }
  delete(r: Round) {
    this.roundDataService.deleteRound(r.Id)
      .subscribe(data => this.loadRounds());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}
