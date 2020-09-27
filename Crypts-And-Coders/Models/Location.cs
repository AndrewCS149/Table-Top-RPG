using System.Collections.Generic;

namespace Crypts_And_Coders.Models
{
    public class Location
    {
        /// <summary>
        /// Id, Name, Description Properties
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<EnemyInLocation> Enemies { get; set; }
    }
}