using System;
using System.ComponentModel.DataAnnotations;

namespace WcfInventorySample.Service
{
    /// <summary>A record of one stock adjustment, written by AdjustStock.</summary>
    public class StockTransaction
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int Delta { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
