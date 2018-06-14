import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Game } from './game';

@Component({
    templateUrl: './product-list.component.html',
    providers: [DataService]
})
export class GameListComponent implements OnInit {

    games: Game[];
    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getGames().subscribe((data: Game[]) => this.games = data);
    }
}