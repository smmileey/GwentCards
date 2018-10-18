namespace Models
{
    public class CardSelectors
    {
        public string Selector { get; set; }

        private CardSelectors(string selector)
        {
            Selector = selector;
        }

        public static CardSelectors CardName = new CardSelectors("col-name");
        public static CardSelectors CardFaction = new CardSelectors("col-faction");
        public static CardSelectors CardPower = new CardSelectors("col-power");
        public static CardSelectors CardRow = new CardSelectors("col-row");
        public static CardSelectors CardType = new CardSelectors("col-type");
        public static CardSelectors CardAbilities = new CardSelectors("col-abilities");
    }
}