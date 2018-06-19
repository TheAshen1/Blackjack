import { PlayerLogic } from "./playerLogic";

export class GameLogic {
  constructor(
    public GameId?: string,
    public CurrentRoundId?: string,
    public NumberOfBots?: number,
    public Players?: PlayerLogic[]
  ) { }
}
