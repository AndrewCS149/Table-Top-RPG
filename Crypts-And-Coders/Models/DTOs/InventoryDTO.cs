namespace Crypts_And_Coders.Models.DTOs
{
    public class InventoryDTO
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }

        public ItemDTO Item { get; set; }
    }
}