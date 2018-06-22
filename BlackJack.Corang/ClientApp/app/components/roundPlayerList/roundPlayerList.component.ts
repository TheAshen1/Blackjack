import { Component, OnInit } from '@angular/core';
import { RoundPlayerDataService } from '../../services/roundPlayerData.service';
import { RoundPlayer } from '../../models/roundPlayer';
import { CardValues, Card } from '../../utils/cardValues';
import { CardLogic } from '../../models/cardLogic';

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
            .subscribe((data) => {
                var parsedData = data.json() as RoundPlayer[]; 
                //JSON.parse()
                parsedData.forEach((roundPlayer: RoundPlayer) => {
                    var tempCards = JSON.parse(roundPlayer.cards.toString());

                     tempCards.forEach((card: CardLogic) => {
                        if (CardValues.values[card.value as Card] != 1) {
                            card.uri = 'assets/images/' + CardValues.values[card.value as Card] + card.suit.charAt(0) + '.png';
                        } else {
                            card.uri = 'assets/images/' + card.value.charAt(0) + card.suit.charAt(0) + '.png';
                        }
                    });

                    roundPlayer.cards = tempCards;
                });

                this.roundPlayers = parsedData;
            });
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
