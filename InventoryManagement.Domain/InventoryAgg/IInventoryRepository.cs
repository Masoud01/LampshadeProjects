using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg;

public interface IInventoryRepository:IRepository<int,Inventory>
{
    EditInventory GetDetail(int Id);
    Inventory GetByIntProductId(int Id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(int InventoryId);

}