using GwentDataRetriever;
using Moq;
using NSoup.Select;
using NUnit.Framework;

namespace UnitTests
{
    public class PaginationItemProviderTests
    {
        [Test]
        public void Test()
        {
            var elements = new Elements();
            var document = Mock.Of<IDocument>(doc => doc.Select(It.IsAny<string>()).GetValue() == elements);
            var pageContentProvider = Mock.Of<IPageContentProvider>(provider => provider.Provide(It.IsAny<string>()) == document);

            Assert.AreSame(elements, new PaginationItemProvider(pageContentProvider).Provide(2));
        }
    }
}