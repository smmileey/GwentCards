using System;
using NSoup.Select;
using Utilities;
using Utilities.Enumerations;

namespace GwentDataRetriever
{
    public class PaginationItemProvider : IPaginationItemProvider
    {
        private readonly IPageContentProvider _pageContentProvider;
        private const string BaseUrl = "https://www.gwentdb.com/cards?filter-display=1";

        public PaginationItemProvider(IPageContentProvider pageContentProvider)
        {
            _pageContentProvider = pageContentProvider ?? throw new ArgumentNullException(nameof(pageContentProvider));
        }

        public Elements Provide(int pageNumber)
        {
            string requestUrl = $"{BaseUrl}&page={pageNumber}";
            IDocument pageContent = _pageContentProvider.Provide(requestUrl);
            return pageContent?.Select(CssSelectors.Selector.Get(CardSelectors.Row)).GetValue();
        }
    }
}