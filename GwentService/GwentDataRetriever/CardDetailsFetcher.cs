using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using Models.Enumerations;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;
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

            Elements elements = cardRow?.Select(".card-row");
            if (elements == null) yield break;

            foreach (var element in elements)
            {
                yield return new CardInfoDto
                {
                    Name = element.GetElementsByClass(CardSelectors.CardName.Selector)?.Select(HtmlSelectors.Href.Selector)?.First()?.OwnText(),
                    Faction = GetFaction(element),
                    Power = GetPower(element),
                    Rows = GetRows(element.GetElementsByClass(CardSelectors.CardRow.Selector)?.First()).ToList(),
                    Type = GetType(element),
                    Abilities = GetAbilities(element.GetElementsByClass(CardSelectors.CardAbilities.Selector))
                };
            }
        }

        private Faction? GetFaction(Element element)
        {
            return element.GetElementsByClass(CardSelectors.CardFaction.Selector)?
                .Text?
                .Replace(" ", string.Empty)?
                .Replace("'", string.Empty)?
                .TryParse<Faction>(Enum.TryParse);
        }

        private static int? GetPower(Element element)
        {
            return element.GetElementsByClass(CardSelectors.CardPower.Selector)?
                .Text?
                .TryParse<int>(int.TryParse);
        }

        private IEnumerable<Row> GetRows(Element element)
        {
            return element?.Select(HtmlSelectors.Span.Selector)
                .Select(span => span.OwnText().TryParse<Row>(Enum.TryParse));
        }

        private static Type? GetType(Element element)
        {
            return element.GetElementsByClass(CardSelectors.CardType.Selector)?.Text?.TryParse<Type>(Enum.TryParse);
        }

        private string GetAbilities(Elements abilities)
        {
            Elements elements = abilities.Select(HtmlSelectors.Span.Selector);
            return !string.IsNullOrEmpty(elements?.Text) ? elements.Text : elements?.Attr(HtmlSelectors.Title.Selector);
        }
    }
}