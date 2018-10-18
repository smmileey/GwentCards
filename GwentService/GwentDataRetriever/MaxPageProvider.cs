using System;
using Extensions;
using NSoup.Nodes;
using Utilities;
using Utilities.Enumerations;

namespace GwentDataRetriever
{
    public class MaxPageProvider : IMaxPageProvider
    {
        private readonly Document _document;

        public MaxPageProvider(Document document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public int GetMaxPage()
        {
            var elements = _document.GetAllElements();
            if (elements == null) return 0;

            string paginationItems = elements.Select(CssSelectors.Selector.Get(CardSelectors.CardContainer))?.Last?.OwnText();
            return paginationItems.TryParse<int>(int.TryParse);

        }
    }
}