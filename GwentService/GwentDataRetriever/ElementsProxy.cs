using System;
using NSoup.Select;

namespace GwentDataRetriever
{
    public class ElementsProxy : IElements
    {
        private Elements _elements;

        public ElementsProxy(Elements elements)
        {
            _elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public virtual IElement Last => new ElementWrapper(_elements.Last);

        public virtual IElements Select(string query)
        {
            _elements = _elements.Select(query);
            return this;
        }

        public Elements GetValue()
        {
            return _elements;
        }
    }
}