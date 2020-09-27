namespace Crypts_And_Coders.Models.DTOs
{
    public class EnemyLootDTO
    {
        public int EnemyId { get; set; }
        public int ItemId { get; set; }
        public ItemDTO Item { get; set; }
    }
}