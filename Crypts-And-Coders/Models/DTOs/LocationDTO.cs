using System.Collections.Generic;

namespace Crypts_And_Coders.Models.DTOs
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LocationEnemyInfoDTO> Enemies { get; set; }
    }
}