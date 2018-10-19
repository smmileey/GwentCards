using System;
using System.Collections.Generic;
using Models;
using NSoup.Select;

namespace GwentDataRetriever
{
    public class CardDetailsFetcher : ICardDetailsFetcher
    {
        private readonly IPaginationItemProvider _paginationItemProvider;
        private readonly ICardInfoProvider _cardInfoProvider;

        public CardDetailsFetcher(IPaginationItemProvider paginationItemProvider, ICardInfoProvider cardInfoProvider)
        {
            _paginationItemProvider = paginationItemProvider ?? throw new ArgumentNullException(nameof(paginationItemProvider));
            _cardInfoProvider = cardInfoProvider ?? throw new ArgumentNullException(nameof(cardInfoProvider));
        }

        public IEnumerable<CardInfoDto> GetCardDetails(int pageNumber)
        {
            Elements paginationItems = _paginationItemProvider.Provide(pageNumber);
            if (paginationItems == null) yield break;

            foreach (var paginationItem in paginationItems)
            {
                yield return _cardInfoProvider.GetCardInfo(paginationItem);
            }
        }     
    }
}