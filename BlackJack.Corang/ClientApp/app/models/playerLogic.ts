import { CardLogic } from "./cardLogic";

export interface PlayerLogic {
    id: string;
    currentRoundPlayerId: string;
    name: string;
    isBot: boolean; 
    chips: number;
    bet: number;

    cards: CardLogic[];
    score: number;
    x: number;
    y: number;
}
