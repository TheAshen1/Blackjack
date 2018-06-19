import { CardLogic } from "./cardLogic";

export class PlayerLogic {
    public Cards: CardLogic[] = [];
    constructor(
        public Id?: string,
        public Name?: string,
        public IsBot?: boolean,
        public CurrentRoundPlayerId?: string
    ) { }
}
