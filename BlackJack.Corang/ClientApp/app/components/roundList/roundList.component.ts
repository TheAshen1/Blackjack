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

    round: Round | undefined;
    rounds: Round[] = [];
    tableMode: boolean = true;

    constructor(private roundDataService: RoundDataService) { }

    ngOnInit(): void {
        this.loadRounds();
    }

    loadRounds() {
        this.roundDataService.getRounds()
            .subscribe((data) => this.rounds = data.json() as Round[]);
    }

    save() {
        if (this.round == undefined) {
            console.log('round is undefined');
            return;
        }

        if (this.round.id == null) {
            this.roundDataService.createRound(this.round)
                .subscribe((data) => this.loadRounds());
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
        this.round = undefined;
        this.tableMode = true;
    }
    delete(r: Round) {
        if (r != null && r.id != null) {
            this.roundDataService.deleteRound(r.id)
                .subscribe(data => this.loadRounds());
        }
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}
