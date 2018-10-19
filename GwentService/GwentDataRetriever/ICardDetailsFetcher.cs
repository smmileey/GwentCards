using System.Collections.Generic;
using Models;

namespace GwentDataRetriever
{
    public interface ICardDetailsFetcher
    {
        IEnumerable<CardInfoDto> GetCardDetails(int pageNumber);
    }
}