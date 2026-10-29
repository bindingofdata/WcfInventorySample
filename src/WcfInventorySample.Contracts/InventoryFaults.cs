using System.Runtime.Serialization;

namespace WcfInventorySample.Contracts
{
    /// <summary>Returned when an operation references an item id that does not exist.</summary>
    [DataContract]
    public class NotFoundFault
    {
        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    /// <summary>Returned when a negative stock adjustment exceeds the quantity on hand.</summary>
    [DataContract]
    public class InsufficientStockFault
    {
        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public int QuantityOnHand { get; set; }

        [DataMember]
        public int RequestedDelta { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
