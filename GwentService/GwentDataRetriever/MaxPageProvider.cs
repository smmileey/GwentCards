using System;
using Extensions;
using Utilities;
using Utilities.Enumerations;

namespace GwentDataRetriever
{
    public class MaxPageProvider : IMaxPageProvider
    {
        private readonly IDocument _document;

        public MaxPageProvider(IDocument document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public int GetMaxPage()
        {
            IElements elements = _document.GetAllElements();
            if (elements == null) return 0;

            string paginationItems = elements.Select(CssSelectors.Selector.Get(CardSelectors.CardContainer))?.Last?.OwnText();
            return paginationItems.TryParse<int>(int.TryParse);
        }
    }
}