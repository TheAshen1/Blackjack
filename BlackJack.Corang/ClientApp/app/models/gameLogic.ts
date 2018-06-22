import { PlayerLogic } from "./playerLogic";

export interface GameLogic {
    gameId: string;
    currentRoundId: string;
    numberOfBots: number;
    players: PlayerLogic[];

}
