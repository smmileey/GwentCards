using Models;
using NSoup;

namespace GwentDataRetriever
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string url = $"https://www.gwentdb.com/cards?filter-display=1";

            CardsInfoRetriever retriever = new CardsInfoRetriever();
            retriever.GetCardsInfo(NSoupClient.Connect(url)?.Get(), new PageRangeDto(), new CardDetailsFetcher());
        }
    }
}
