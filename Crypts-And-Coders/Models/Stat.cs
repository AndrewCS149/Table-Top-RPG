using System.Collections.Generic;

namespace Crypts_And_Coders.Models
{
    public class Stat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CharacterStat> StatSheet { get; set; }
    }
}