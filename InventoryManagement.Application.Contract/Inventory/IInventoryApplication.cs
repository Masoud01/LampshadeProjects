using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);
    OperationResult Edit(EditInventory command);
    OperationResult InCrease(InCreaseInventory command);
    OperationResult Reduce(List<ReduceInventory> command);
    OperationResult Reduce(ReduceInventory command);
    EditInventory GetDetail(int Id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(int InventoryId);
}