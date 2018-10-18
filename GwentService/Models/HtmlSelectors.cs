namespace Models
{
    public class HtmlSelectors
    {
        public string Selector { get; set; }

        private HtmlSelectors(string selector)
        {
            Selector = selector;
        }

        public static HtmlSelectors Href = new HtmlSelectors("a");
        public static HtmlSelectors Span = new HtmlSelectors("span");
        public static HtmlSelectors Title = new HtmlSelectors("title");
    }
}