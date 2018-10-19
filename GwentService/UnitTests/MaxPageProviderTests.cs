using GwentDataRetriever;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class MaxPageProviderTests
    {
        [Test]
        public void FourElementsOnPage_FourIsReturned()
        {
            var paginationItems = Mock.Of<IElements>(elemProxy => elemProxy.Select(It.IsAny<string>()).Last.OwnText() == "4");
            var document = Mock.Of<IDocument>(doc => doc.GetAllElements() == paginationItems);
            var sut = new MaxPageProvider(document);

            Assert.AreEqual(4, sut.GetMaxPage());
        }

        [Test]
        public void NoElementsOnPage_ZeroIsReturned()
        {          
            var sut = new MaxPageProvider(Mock.Of<IDocument>());

            Assert.AreEqual(0, sut.GetMaxPage());
        }

        [Test]
        public void IncorrectValueReturnedFromPagination_ZeroIsReturned()
        {
            var paginationItems = Mock.Of<IElements>(elemProxy => elemProxy.Select(It.IsAny<string>()).Last.OwnText() == "wrong");
            var document = Mock.Of<IDocument>(doc => doc.GetAllElements() == paginationItems);
            var sut = new MaxPageProvider(document);

            Assert.AreEqual(0, sut.GetMaxPage());
        }
    }
}