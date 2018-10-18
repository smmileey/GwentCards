using System;
using System.Collections.Generic;
using Models;
using Models.Enumerations;

namespace GwentDataRetriever
{
    public class CardsInfoRetriever
    {
        public const int MinPage = 1;

        public List<CardInfoDto> GetCardsInfo(IMaxPageProvider maxPageProvider, ICardDetailsFetcher cardDetailsFetcher, PageRangeDto pageRange)
        {
            if (maxPageProvider == null) throw new ArgumentNullException(nameof(maxPageProvider));
            if (cardDetailsFetcher == null) throw new ArgumentNullException(nameof(cardDetailsFetcher));
            if (pageRange == null) throw new ArgumentNullException(nameof(pageRange));

            int actualMaxPage = maxPageProvider.GetMaxPage();
            int minRange = pageRange.Min.HasValue && pageRange.Min.Value >= MinPage ? pageRange.Min.Value : MinPage;
            int maxRange = pageRange.Max.HasValue && pageRange.Max.Value <= actualMaxPage ? pageRange.Max.Value : actualMaxPage;

            List<CardInfoDto> cards = new List<CardInfoDto>();
            for (int pageNumber = minRange; pageNumber <= maxRange; pageNumber++)
            {
                cards.AddRange(cardDetailsFetcher.GetCardDetails(pageNumber));
            }

            return cards;
        }
    }
}
