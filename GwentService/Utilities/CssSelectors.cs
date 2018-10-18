using System.Collections.Generic;
using Utilities.Enumerations;

namespace Utilities
{
    public class CssSelectors
    {
        private static ISelector _selector;
        private static readonly Dictionary<AttributeSelectors, CssSelectors> AttributeSelectors =
            new Dictionary<AttributeSelectors, CssSelectors>
            {
                {Enumerations.AttributeSelectors.Title, new CssSelectors("title")  }
            };
        private static readonly Dictionary<CardSelectors, CssSelectors> CardSelectors =
            new Dictionary<CardSelectors, CssSelectors>
            {
                {Enumerations.CardSelectors.Row, new CssSelectors(".card-row")  },
                {Enumerations.CardSelectors.Name, new CssSelectors("col-name")  },
                {Enumerations.CardSelectors.Faction, new CssSelectors("col-faction")  },
                {Enumerations.CardSelectors.Power, new CssSelectors("col-power")  },
                {Enumerations.CardSelectors.Placement, new CssSelectors("col-row")  },
                {Enumerations.CardSelectors.Type, new CssSelectors("col-type")  },
                {Enumerations.CardSelectors.Abilities, new CssSelectors("col-abilities")},
                {Enumerations.CardSelectors.CardContainer, new CssSelectors("li .b-pagination-item")},
            };
        private static readonly Dictionary<HtmlElementSelectors, CssSelectors> HtmlElementsSelectors =
            new Dictionary<HtmlElementSelectors, CssSelectors>
            {
                {Enumerations.HtmlElementSelectors.A, new CssSelectors("a")  },
                {Enumerations.HtmlElementSelectors.Span, new CssSelectors("span")  },
            };

        public static ISelector Selector => _selector ?? (_selector = new Selector(AttributeSelectors, CardSelectors, HtmlElementsSelectors));
        public string Value { get; set; }

        private CssSelectors(string value)
        {
            Value = value;
        }
    }

}