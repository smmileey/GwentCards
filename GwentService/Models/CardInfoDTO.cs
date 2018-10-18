using System.Collections.Generic;
using Models.Enumerations;
using Type = Models.Enumerations.Type;

namespace Models
{
    public class CardInfoDto
    {
        public string Name { get; set; }

        public Faction? Faction { get; set; }

        public int? Power { get; set; }

        public List<Row> Rows { get; set; }

        public Type? Type { get; set; }

        public string Abilities { get; set; }
    }
}
