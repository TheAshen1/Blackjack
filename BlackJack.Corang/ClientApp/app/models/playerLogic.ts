import { CardLogic } from "./cardLogic";

export class PlayerLogic {
    public cards: CardLogic[] = [];
    public score: number = 0;
    constructor(
        public id?: string,
        public currentRoundPlayerId?: string,
        public name?: string,
        public isBot?: boolean      
    ) { }
}
