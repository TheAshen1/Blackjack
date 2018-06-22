import { CardLogic } from "./cardLogic";

export interface PlayerLogic {
    id: string;
    currentRoundPlayerId: string;
    name: string;
    isBot: boolean;  

    cards: CardLogic[];
    score: number;
    x: number;
    y: number;
}
