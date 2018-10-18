using System;
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
        public void MaxPageProviderNotProvided_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CardsInfoRetriever().GetCardsInfo(null, Mock.Of<ICardDetailsFetcher>(), new PageRangeDto()));
        }

        [Test]
        public void CardDetailsFetcherNotProvided_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CardsInfoRetriever().GetCardsInfo(Mock.Of<IMaxPageProvider>(), null, new PageRangeDto()));
        }

        [Test]
        public void PageRangeNotProvided_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CardsInfoRetriever().GetCardsInfo(Mock.Of<IMaxPageProvider>(), null, new PageRangeDto()));
        }

        [Test]
        public void WhenTwoPages_WithThreeCards_AllAreReturned()
        {
            var maxPageProvider = Mock.Of<IMaxPageProvider>(provider => provider.GetMaxPage() == 2);
            var cardDetailsFetcher = Mock.Of<ICardDetailsFetcher>(fetcher => fetcher.GetCardDetails(1) == new List<CardInfoDto> { new CardInfoDto(), new CardInfoDto() }
                                                                             && fetcher.GetCardDetails(2) == new List<CardInfoDto> { new CardInfoDto() });
            var sut = new CardsInfoRetriever();

            Assert.AreEqual(3, sut.GetCardsInfo(maxPageProvider, cardDetailsFetcher, new PageRangeDto()).Count);
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(int.MinValue)]
        public void WhenIncorrectMinRange_ItIsSetToOne(int minRange)
        {
            var sut = new CardsInfoRetriever();

            var maxPageProvider = Mock.Of<IMaxPageProvider>(provider => provider.GetMaxPage() == 2);
            var cardDetailsFetcher = Mock.Of<ICardDetailsFetcher>(fetcher => fetcher.GetCardDetails(1) == new List<CardInfoDto> { new CardInfoDto(), new CardInfoDto() }
                                                                             && fetcher.GetCardDetails(2) == new List<CardInfoDto> { new CardInfoDto() });
            Assert.AreEqual(3, sut.GetCardsInfo(maxPageProvider, cardDetailsFetcher, new PageRangeDto { Min = minRange }).Count);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void WhenIncorrectMaxRange_ItIsSetToTheOneProvidedByMaxRangeProvider(int maxRange)
        {
            var sut = new CardsInfoRetriever();

            var maxPageProvider = Mock.Of<IMaxPageProvider>(provider => provider.GetMaxPage() == 2);
            var cardDetailsFetcher = Mock.Of<ICardDetailsFetcher>(fetcher => fetcher.GetCardDetails(1) == new List<CardInfoDto> { new CardInfoDto(), new CardInfoDto() }
                                                                             && fetcher.GetCardDetails(2) == new List<CardInfoDto> { new CardInfoDto() });

            Assert.AreEqual(3, sut.GetCardsInfo(maxPageProvider, cardDetailsFetcher, new PageRangeDto { Max = maxRange }).Count);
        }

        [Test]
        public void WhenNoCards_EmptyListIsReturned()
        {
            var sut = new CardsInfoRetriever();

            var maxPageProvider = Mock.Of<IMaxPageProvider>(provider => provider.GetMaxPage() == 0);
            var cardDetailsFetcher = Mock.Of<ICardDetailsFetcher>(fetcher => fetcher.GetCardDetails(1) == new List<CardInfoDto> { new CardInfoDto(), new CardInfoDto() }
                                                                             && fetcher.GetCardDetails(2) == new List<CardInfoDto> { new CardInfoDto() });

            Assert.AreEqual(0, sut.GetCardsInfo(maxPageProvider, cardDetailsFetcher, new PageRangeDto()).Count);
        }
    }
}
