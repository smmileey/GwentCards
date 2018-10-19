using System;
using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
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
            List<CardInfoDto> cardInfoDtos = retriever.GetCardsInfo(new MaxPageProvider(new DocumentWrapper(baseDocument)), 
                new CardDetailsFetcher(new PaginationItemProvider(new PageContentProvider()), new CardInfoProvider()), 
                new PageRangeDto());

            Console.WriteLine(JsonConvert.SerializeObject(cardInfoDtos, Formatting.Indented));
            Console.ReadLine();
        }
    }
}
