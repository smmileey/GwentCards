using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Enumerations;
using Newtonsoft.Json;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using Type = Models.Enumerations.Type;

namespace GwentDataRetriever
{
    public class CardsInfoRetriever
    {

        public void GetCardsInfo()
        {
            const string url = "https://www.gwentdb.com/cards?filter-display=1";
            Document document = NSoupClient.Connect(url).Get();
            Elements elements = document.Select(".card-row");
            List<CardInfoDto> cards = new List<CardInfoDto>();

            foreach (var element in elements)
            {
                cards.Add(new CardInfoDto
                {
                    Name = element.GetElementsByClass("col-name")?.Select("a")?.First()?.OwnText(),
                    Faction = Enum.Parse<Faction>(element.GetElementsByClass("col-faction")?.Text?.Replace(" ", string.Empty).Replace("'", string.Empty)),
                    Power = int.Parse(element.GetElementsByClass("col-power")?.Text),
                    Rows = GetRows(element.GetElementsByClass("col-row")?.First()).ToList(),
                    Type = Enum.Parse<Type>(element.GetElementsByClass("col-type")?.Text),
                    Abilities = GetAbilities(element.GetElementsByClass("col-abilities"))
                });    
            }

            Console.Write(JsonConvert.SerializeObject(cards, Formatting.Indented));
            Console.ReadLine();
        }

        private string GetAbilities(Elements abilities)
        {
            if (abilities == null) return null;

            Elements elements = abilities.Select("span");
            return !string.IsNullOrEmpty(elements?.Text) ? elements.Text : elements?.Attr("title");
        }

        private IEnumerable<Row> GetRows(Element element)
        {
            if (element == null) yield break;

            foreach (var span in element.Select("span"))
            {
                yield return Enum.Parse<Row>(span.OwnText());
            }
        }
    }
}
