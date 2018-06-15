import { Component, OnInit, Output} from '@angular/core';
import { GameLogicService } from '../../../services/gameLogic.service';
import { GameLogic } from '../../../models/gameLogic';

@Component({
  selector: 'game-body',
  templateUrl: './gameBody.component.html',
  styleUrls: ['./gameBody.component.css'],
  providers: [GameLogicService]
})
export class GameBody {

  constructor(private gameLogicService: GameLogicService) { }

}
