import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { Game } from './game';

@Component({
    templateUrl: './product-detail.component.html',
    providers: [DataService]
})
export class GameDetailComponent implements OnInit {

    id: string;
    game: Game;
    loaded: boolean = false;

    constructor(private dataService: DataService, activeRoute: ActivatedRoute) {
        this.id = activeRoute.snapshot.params["id"];
    }

    ngOnInit() {
        if (this.id)
            this.dataService.getGame(this.id)
                .subscribe((data: Game) => { this.game = data; this.loaded = true; });
    }
}