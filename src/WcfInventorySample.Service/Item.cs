using System.ComponentModel.DataAnnotations;

namespace WcfInventorySample.Service
{
    /// <summary>
    /// EF entity for an inventory item. Entities stay inside the service —
    /// clients only ever see InventoryItemDto.
    /// </summary>
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sku { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderLevel { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
