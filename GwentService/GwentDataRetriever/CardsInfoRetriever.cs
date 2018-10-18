using System;
using System.Collections.Generic;
using Models;
using Models.Enumerations;
using Newtonsoft.Json;
using NSoup.Nodes;
using NSoup.Select;

namespace GwentDataRetriever
{
    public class CardsInfoRetriever
    {
        public const int MinPage = 1;

        public void GetCardsInfo(Document document, PageRangeDto pageRange, ICardDetailsFetcher cardDetailsFetcher)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (pageRange == null) throw new ArgumentNullException(nameof(pageRange));

            int actualMaxPage = GetMaxPage(document.GetAllElements());
            int minRange = pageRange.Min.HasValue && pageRange.Min.Value >= MinPage ? pageRange.Min.Value : MinPage;
            int maxRange = pageRange.Max.HasValue && pageRange.Max.Value <= actualMaxPage ? pageRange.Max.Value : actualMaxPage;

            List<CardInfoDto> cards = new List<CardInfoDto>();
            for (int pageNumber = minRange; pageNumber < maxRange; pageNumber++)
            {
                cards.AddRange(cardDetailsFetcher.GetCardDetails(pageNumber));
            }

            Console.Write(JsonConvert.SerializeObject(cards, Formatting.Indented));
            Console.ReadLine();
        }

        public int GetMaxPage(Elements elements)
        {
            if (elements == null) throw new ArgumentNullException(nameof(elements));

            string paginationItems = elements.Select("li .b-pagination-item").Last.OwnText();
            return int.Parse(paginationItems);
        }

    }
}
