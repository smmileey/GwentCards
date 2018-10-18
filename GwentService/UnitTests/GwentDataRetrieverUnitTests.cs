using System.Collections.Generic;
using GwentDataRetriever;
using Models;
using Models.Enumerations;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class CardsInfoRetrieverUnitTests
    {
        [Test]
        public void WhenTwoPages_WithThreeCards_AllAreReturned()
        {
            var maxPageProvider = Mock.Of<IMaxPageProvider>(provider => provider.GetMaxPage() == 2);
            var cardDetailsFetcher = Mock.Of<ICardDetailsFetcher>(fetcher => fetcher.GetCardDetails(1) == new List<CardInfoDto> { new CardInfoDto(), new CardInfoDto() }
                                                                             && fetcher.GetCardDetails(2) == new List<CardInfoDto> { new CardInfoDto() });
            var sut = new CardsInfoRetriever();

            Assert.AreEqual(3, sut.GetCardsInfo(maxPageProvider, cardDetailsFetcher, new PageRangeDto()).Count);
        }
    }
}
