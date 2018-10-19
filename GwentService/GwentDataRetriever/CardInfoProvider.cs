using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using Models.Enumerations;
using NSoup.Nodes;
using NSoup.Select;
using Utilities;
using Utilities.Enumerations;
using Type = Models.Enumerations.Type;


namespace GwentDataRetriever
{
    public class CardInfoProvider : ICardInfoProvider
    {
        public CardInfoDto GetCardInfo(Element paginationItem)
        {
            if (paginationItem == null) throw new ArgumentNullException(nameof(paginationItem));

            return new CardInfoDto
            {
                Faction = GetFaction(paginationItem),
                Type = GetCardType(paginationItem),
                Name = GetName(paginationItem),
                Abilities = GetAbilities(paginationItem),
                Power = GetPower(paginationItem),
                Rows = GetRows(paginationItem)
            };
        }
        private Faction? GetFaction(Element paginationItem)
        {
            return paginationItem.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Faction))?
                .Text?
                .Replace(" ", string.Empty)?
                .Replace("'", string.Empty)?
                .TryParse<Faction>(Enum.TryParse);
        }

        private Type? GetCardType(Element paginationItem)
        {
            return paginationItem.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Type))?
                .Text?
                .TryParse<Type>(Enum.TryParse);
        }

        private string GetName(Element paginationItem)
        {
            return paginationItem.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Name))
                 ?.Select(CssSelectors.Selector.Get(HtmlElementSelectors.A))?
                .First()?
                .OwnText();
        }

        private string GetAbilities(Element paginationItem)
        {
            Elements spans = paginationItem.Select(CssSelectors.Selector.Get(HtmlElementSelectors.Span));
            return !string.IsNullOrEmpty(spans?.Text)
                ? spans.Text
                : spans?.Attr(CssSelectors.Selector.Get(AttributeSelectors.Title));
        }

        private int? GetPower(Element paginationItem)
        {
            return paginationItem.GetElementsByClass(CssSelectors.Selector.Get(CardSelectors.Power))?
                .Text?
                .TryParse<int>(int.TryParse);
        }

        private List<Row> GetRows(Element paginationItem)
        {
            return paginationItem?.Select(CssSelectors.Selector.Get(HtmlElementSelectors.Span))
                .Select(span => span.OwnText().TryParse<Row>(Enum.TryParse))
                .ToList();
        }
    }
}