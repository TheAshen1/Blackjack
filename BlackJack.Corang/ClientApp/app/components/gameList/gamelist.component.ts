import { Component, OnInit, Inject } from '@angular/core';
import { GameDataService } from '../../services/gameData.service';
import { Game } from '../../models/game';

@Component({
    selector: 'game-list',
    templateUrl: './gameList.component.html',
    styleUrls: ['./gameList.component.css'],
    providers: [GameDataService]
})
export class GameList implements OnInit {

    game: Game = new Game();
    games: Game[] = [];
    tableMode: boolean = true;

    constructor(private gameDataService: GameDataService) { }

    ngOnInit(): void {
        this.loadGames();
    }

    loadGames() {
        this.gameDataService.getGames()
            .subscribe((data) => { this.games = data.json() as Game[] }, error => console.error(error));
    }

    save() {
        if (this.game.Id == null) {
            this.gameDataService.createGame(this.game)
                .subscribe((data) => this.games.push(data.json() as Game));
        } else {
            this.gameDataService.updateGame(this.game)
                .subscribe((data) => this.loadGames());
        }
        this.cancel();
    }
    editGame(g: Game) {
        this.game = g;
    }
    cancel() {
        this.game = new Game();
        this.tableMode = true;
    }
    delete(g: Game) {
        if (g != null && g.Id != null) {
            this.gameDataService.deleteGame(g.Id)
                .subscribe((data) => this.loadGames());
        }
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}
