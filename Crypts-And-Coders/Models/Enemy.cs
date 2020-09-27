using System.Collections.Generic;
using static Crypts_And_Coders.Models.SpeciesAndClass;

namespace Crypts_And_Coders.Models
{
    public class Enemy
    {
        /// <summary>
        /// Id, Ability, and Type properties
        /// Enemy Species specifically for enemies
        /// </summary>
        public int Id { get; set; }

        public string Abilities { get; set; }
        public string Type { get; set; }

        public List<EnemyInLocation> EnemyInLocation { get; set; }
        public List<EnemyLoot> Loot { get; set; }
        public Species Species { get; set; }
    }
}