using System.Collections.Generic;
using System.ServiceModel;

namespace WcfInventorySample.Contracts
{
    /// <summary>
    /// Inventory operations exposed by the WCF service. Both the WPF client
    /// (NetTcp) and the ASP.NET web client (BasicHttp) talk to this contract.
    /// </summary>
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        List<InventoryItemDto> GetItems();

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        InventoryItemDto GetItem(int id);

        [OperationContract]
        InventoryItemDto AddItem(InventoryItemDto item);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        InventoryItemDto UpdateItem(InventoryItemDto item);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(InsufficientStockFault))]
        InventoryItemDto AdjustStock(int id, int delta, string reason);
    }
}
