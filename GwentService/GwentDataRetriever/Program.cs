using Models;
using NSoup;

namespace GwentDataRetriever
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string url = $"https://www.gwentdb.com/cards?filter-display=1";
            var baseDocument = NSoupClient.Connect(url)?.Get();

            CardsInfoRetriever retriever = new CardsInfoRetriever();
            retriever.GetCardsInfo(new MaxPageProvider(baseDocument), new CardDetailsFetcher(), new PageRangeDto());
        }
    }
}
