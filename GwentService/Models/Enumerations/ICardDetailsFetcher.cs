using System.Collections.Generic;

namespace Models.Enumerations
{
    public interface ICardDetailsFetcher
    {
        IEnumerable<CardInfoDto> GetCardDetails(int pageNumber);
    }
}