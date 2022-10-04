using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application;

public class InventoryApplication: IInventoryApplication
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryApplication(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
    public OperationResult Create(CreateInventory command)
    {
        var operation = new OperationResult();
        if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
        {
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        }

        var inventory = new Inventory(command.ProductId, command.ProductId);
        _inventoryRepository.Create(inventory);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.Id);
        if (inventory.Equals(null))
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }
        if (_inventoryRepository.Exist(x => 
                x.ProductId==command.ProductId && x.Id!=command.Id))
        {
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        }
        
        inventory.Edit(command.ProductId,command.UnitPrice);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult InCrease(InCreaseInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory.Equals(null))
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        int operatorId = 1;
        inventory.InCrease(command.Count, operatorId, command.Description);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Reduce(List<ReduceInventory> command)
    {
        var operation = new OperationResult();
        const int operatorId=1;
        foreach (var item in command)
        {
            var inventory = _inventoryRepository.Get(item.ProductId);
            inventory.Reduce(item.Count,operatorId,item.Description,item.OrderId);
            _inventoryRepository.SaveChanges();
        
        }
        return operation.Succedded();
    }

    public OperationResult Reduce(ReduceInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory.Equals(null))
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        int operatorId = 1;
        inventory.Reduce(command.Count, operatorId, command.Description,0);
        _inventoryRepository.SaveChanges();
        return operation.Succedded();
    }


    public EditInventory GetDetail(int Id)
    {
        return _inventoryRepository.GetDetail(Id);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        return _inventoryRepository.Search(searchModel);
    }

    public List<InventoryOperationViewModel> GetOperationLog(int InventoryId)
    {
        return _inventoryRepository.GetOperationLog(InventoryId);
    }
}