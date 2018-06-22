import { Component, OnInit } from '@angular/core';
import { RoundDataService } from '../../services/roundData.service';
import { Round } from '../../models/round';
import { CardValues, Card } from '../../utils/cardValues';
import { CardLogic } from '../../models/cardLogic';

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

    toggleViewMode(round: Round) {
        round.showFullDeck = !round.showFullDeck;
    }


    loadRounds() {
        this.roundDataService.getRounds()
            .subscribe((data) => {
                var parsedObject = data.json() as Round[];

                parsedObject.forEach((round: Round) => {
                    
                    var tempDeck = JSON.parse(round.deck.toString()) as CardLogic[];

                    tempDeck.forEach((card: CardLogic) => {
                        if (CardValues.values[card.value as Card] != 1) {
                            card.uri = 'assets/images/' + CardValues.values[card.value as Card] + card.suit.charAt(0) + '.png';
                        } else {
                            card.uri = 'assets/images/' + card.value.charAt(0) + card.suit.charAt(0) + '.png';
                        }
                    });

                    round.deck = tempDeck;
                    round.showFullDeck = false;
                });

                this.rounds = parsedObject;
            });
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
