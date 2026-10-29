using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using WcfInventorySample.Contracts;

namespace WcfInventorySample.Service
{
    /// <summary>
    /// WCF service implementation. All inventory logic and database work lives
    /// here — the WPF and web clients never touch the database directly.
    /// WCF creates one service instance per call, so one context per instance
    /// is the right lifetime.
    /// </summary>
    public class InventoryService : IInventoryService, IDisposable
    {
        private readonly InventoryContext _context;
        private readonly bool _contextOwned;

        public InventoryService()
            : this(new InventoryContext(), contextOwned: true)
        {
        }

        public InventoryService(InventoryContext context)
            : this(context, contextOwned: false)
        {
        }

        private InventoryService(InventoryContext context, bool contextOwned)
        {
            _context = context;
            _contextOwned = contextOwned;
        }

        public List<InventoryItemDto> GetItems()
        {
            // Materialize before mapping: EF cannot translate ToDto into SQL.
            return _context.Items
                .OrderBy(i => i.Name)
                .ToList()
                .Select(ToDto)
                .ToList();
        }

        public InventoryItemDto GetItem(int id)
        {
            return ToDto(FindItemOrFault(id));
        }

        public InventoryItemDto AddItem(InventoryItemDto item)
        {
            var entity = new Item
            {
                Sku = item.Sku,
                Name = item.Name,
                Category = item.Category,
                QuantityOnHand = item.QuantityOnHand,
                ReorderLevel = item.ReorderLevel,
                Location = item.Location,
                UnitPrice = item.UnitPrice
            };
            _context.Items.Add(entity);
            _context.SaveChanges();
            return ToDto(entity);
        }

        public InventoryItemDto UpdateItem(InventoryItemDto item)
        {
            var entity = FindItemOrFault(item.Id);
            entity.Sku = item.Sku;
            entity.Name = item.Name;
            entity.Category = item.Category;
            entity.QuantityOnHand = item.QuantityOnHand;
            entity.ReorderLevel = item.ReorderLevel;
            entity.Location = item.Location;
            entity.UnitPrice = item.UnitPrice;
            _context.SaveChanges();
            return ToDto(entity);
        }

        public InventoryItemDto AdjustStock(int id, int delta, string reason)
        {
            var entity = FindItemOrFault(id);
            if (delta < 0 && entity.QuantityOnHand + delta < 0)
            {
                throw new FaultException<InsufficientStockFault>(
                    new InsufficientStockFault
                    {
                        ItemId = id,
                        QuantityOnHand = entity.QuantityOnHand,
                        RequestedDelta = delta,
                        Message = $"Cannot remove {-delta} units of item {id}: only {entity.QuantityOnHand} on hand."
                    },
                    new FaultReason("Insufficient stock for the requested adjustment."));
            }

            entity.QuantityOnHand += delta;
            _context.StockTransactions.Add(new StockTransaction
            {
                ItemId = id,
                Delta = delta,
                Reason = reason,
                Timestamp = DateTime.UtcNow
            });
            _context.SaveChanges();
            return ToDto(entity);
        }

        public void Dispose()
        {
            if (_contextOwned)
            {
                _context.Dispose();
            }
        }

        private Item FindItemOrFault(int id)
        {
            var entity = _context.Items.Find(id);
            if (entity == null)
            {
                throw new FaultException<NotFoundFault>(
                    new NotFoundFault
                    {
                        ItemId = id,
                        Message = $"Item {id} was not found."
                    },
                    new FaultReason($"Item {id} was not found."));
            }
            return entity;
        }

        private static InventoryItemDto ToDto(Item item)
        {
            return new InventoryItemDto
            {
                Id = item.Id,
                Sku = item.Sku,
                Name = item.Name,
                Category = item.Category,
                QuantityOnHand = item.QuantityOnHand,
                ReorderLevel = item.ReorderLevel,
                Location = item.Location,
                UnitPrice = item.UnitPrice
            };
        }
    }
}
