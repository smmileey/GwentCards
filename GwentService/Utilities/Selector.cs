using System;
using System.Collections.Generic;
using Utilities.Enumerations;

namespace Utilities
{
    internal class Selector : ISelector
    {

        private readonly Dictionary<AttributeSelectors, CssSelectors> _attributeSelectors;
        private readonly Dictionary<CardSelectors, CssSelectors> _cardsSelectors;
        private readonly Dictionary<HtmlElementSelectors, CssSelectors> _htmlElementsSelectors;

        public Selector(Dictionary<AttributeSelectors, CssSelectors> attributeSelectors, Dictionary<CardSelectors, CssSelectors> cardsSelectors, 
            Dictionary<HtmlElementSelectors, CssSelectors> htmlElementsSelectors)
        {
            _attributeSelectors = attributeSelectors ?? throw new ArgumentNullException(nameof(cardsSelectors));
            _cardsSelectors = cardsSelectors ?? throw new ArgumentNullException(nameof(cardsSelectors));
            _htmlElementsSelectors = htmlElementsSelectors ?? throw new ArgumentNullException(nameof(htmlElementsSelectors));
        }

        public string Get(AttributeSelectors attributeSelector)
        {
            return _attributeSelectors.TryGetValue(attributeSelector, out var value)
                ? value.Value
                : throw new ArgumentOutOfRangeException(nameof(attributeSelector));
        }

        public string Get(CardSelectors cardSelector)
        {
            return _cardsSelectors.TryGetValue(cardSelector, out var value)
                ? value.Value
                : throw new ArgumentOutOfRangeException(nameof(cardSelector));
        }

        public string Get(HtmlElementSelectors htmlElementSelector)
        {
            return _htmlElementsSelectors.TryGetValue(htmlElementSelector, out var value)
                ? value.Value
                : throw new ArgumentOutOfRangeException(nameof(htmlElementSelector));
        }
    }
}