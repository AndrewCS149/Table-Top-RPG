namespace Crypts_And_Coders.Models
{
    public class EnemyInLocation
    {
        public int LocationId { get; set; }
        public int EnemyId { get; set; }

        public Enemy Enemy { get; set; }
        public Location Location { get; set; }
    }
}