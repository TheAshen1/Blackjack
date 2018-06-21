import { PlayerLogic } from "./playerLogic";

export class GameLogic {
  constructor(
    public gameId?: string,
    public currentRoundId?: string,
    public numberOfBots?: number,
    public players?: PlayerLogic[]
  ) { }
}
