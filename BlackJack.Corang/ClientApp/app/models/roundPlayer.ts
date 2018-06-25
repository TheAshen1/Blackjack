import { CardLogic } from "./cardLogic";

export interface RoundPlayer {
    id: string;
    roundId: string;
    playerId: string;
    cards: CardLogic[];
    bet: number;
}
