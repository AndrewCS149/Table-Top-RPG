using System.Collections.Generic;

namespace Crypts_And_Coders.Models.DTOs
{
    public class EnemyDTO
    {
        public int Id { get; set; }
        public string Abilities { get; set; }
        public string Type { get; set; }
        public string Species { get; set; }
        public List<EnemyLootDTO> Loot { get; set; }
    }
}