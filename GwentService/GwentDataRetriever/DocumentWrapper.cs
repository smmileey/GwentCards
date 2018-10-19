using System;
using NSoup.Nodes;

namespace GwentDataRetriever
{
    public class DocumentWrapper : IDocument
    {
        private readonly Document _document;

        public DocumentWrapper(Document document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public IElements GetAllElements()
        {
            return new ElementsProxy(_document.GetAllElements());
        }

        public IElements Select(string query)
        {
            return new ElementsProxy(_document.Select(query));
        }
    }
}