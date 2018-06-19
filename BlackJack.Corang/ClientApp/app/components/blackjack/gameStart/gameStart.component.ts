import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { GameLogicService } from '../../../services/gameLogic.service';
import { GameLogic } from '../../../models/gameLogic';

@Component({
  selector: 'game-start',
  templateUrl: './gameStart.component.html',
  styleUrls: ['./gameStart.component.css'],
  providers: [GameLogicService]
})
export class GameStart {

  playerName: string = "";
  numberOfBots: number = 0;

  @Output() begin = new EventEmitter<GameLogic>();

  constructor(private gameLogicService: GameLogicService) { }

  start() {
    this.gameLogicService.startNewGame(this.playerName, this.numberOfBots)
      .subscribe((data: GameLogic) => this.begin.emit(data));
  }
}
