import { CardLogic } from "./cardLogic";

export interface Round {
    id: string;
    gameId: string;
    deck: CardLogic[]; 

    showFullDeck: boolean;
}
