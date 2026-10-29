using System.Runtime.Serialization;

namespace WcfInventorySample.Contracts
{
    /// <summary>
    /// Data transfer object for an inventory item. EF entities are never
    /// leaked across the service boundary — only DTOs cross the wire.
    /// </summary>
    [DataContract]
    public class InventoryItemDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Sku { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public int QuantityOnHand { get; set; }

        [DataMember]
        public int ReorderLevel { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}
