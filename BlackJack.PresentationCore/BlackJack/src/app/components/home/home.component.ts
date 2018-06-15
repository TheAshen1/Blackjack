import { Component, OnInit } from '@angular/core';
import { GameLogicService } from '../../services/gameLogic.service';
import { GameLogic } from '../../models/gameLogic';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [GameLogicService]
})
export class Home{

  gameData: GameLogic;

  gameIsGoingOn: boolean = false;

  onBegin(data: GameLogic) {
    console.log('its alive!');
    this.gameData = data;
    this.gameIsGoingOn = true;
  }
}
