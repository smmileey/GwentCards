using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using Models.Enumerations;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using Utilities;
using Utilities.Enumerations;
using Type = Models.Enumerations.Type;

namespace GwentDataRetriever
{
    public class CardDetailsFetcher : ICardDetailsFetcher
    {
        private const string BaseUrl = "https://www.gwentdb.com/cards?filter-display=1";

        public IEnumerable<CardInfoDto> GetCardDetails(int pageNumber)
        {
            string requestUrl = $"{BaseUrl}&page={pageNumber}";
            Document cardRow = NSoupClient.Connect(requestUrl)?.Get();

            Elements elements = cardRow?.Select(CssSelectors.Selector.Get(CardSelectors.Row));
            if (elements == null) yield break;

            foreach (var element in elements)
            {
                yield return new CardInfoDto
                {
                    Name = element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Name))?.Select(CssSelectors.Selector.Get(HtmlElementSelectors.A))?.First()?.OwnText(),
                    Faction = GetFaction(element),
                    Power = GetPower(element),
                    Rows = GetRows(element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Placement))?.First()).ToList(),
                    Type = GetType(element),
                    Abilities = GetAbilities(element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Abilities)))
                };
            }
        }

        private Faction? GetFaction(Element element)
        {
            return element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Faction))?
                .Text?
                .Replace(" ", string.Empty)?
                .Replace("'", string.Empty)?
                .TryParse<Faction>(Enum.TryParse);
        }

        private static int? GetPower(Element element)
        {
            return element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Power))?
                .Text?
                .TryParse<int>(int.TryParse);
        }

        private IEnumerable<Row> GetRows(Element element)
        {
            return element?.Select(CssSelectors.Selector.Get(HtmlElementSelectors.Span))
                .Select(span => span.OwnText().TryParse<Row>(Enum.TryParse));
        }

        private static Type? GetType(Element element)
        {
            return element.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Type))?.Text?.TryParse<Type>(Enum.TryParse);
        }

        private string GetAbilities(Elements abilities)
        {
            Elements elements = abilities.Select(CssSelectors.Selector.Get(HtmlElementSelectors.Span));
            return !string.IsNullOrEmpty(elements?.Text) ? elements.Text : elements?.Attr(CssSelectors.Selector.Get(AttributeSelectors.Title));
        }
    }
}