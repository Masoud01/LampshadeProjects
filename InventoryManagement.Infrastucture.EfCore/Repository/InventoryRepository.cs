using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository;

public class InventoryRepository : RepositoryBase<int, Inventory>, IInventoryRepository
{
    private readonly InventoryContext? _inventoryContext;
    private readonly ShopContext? _shopContext;
    public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
    {
        _inventoryContext = context;
        _shopContext = shopContext;
    }

    public EditInventory GetDetail(int Id)
    {
        return _inventoryContext?.Inventory!.Select(x => new EditInventory()
        {
            Id = x.Id,
            InStuck = x.InStuck,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice

        }).FirstOrDefault(x => x.Id == Id);
    }

    public Inventory GetByIntProductId(int Id)
    {
        return _inventoryContext?.Inventory!.FirstOrDefault(x => x.ProductId == Id);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        var products = _shopContext?.Products!
            .Select(x => new { x.Id, x.Name }).ToList();
        var query = _inventoryContext?.Inventory!.Select(x => new InventoryViewModel()
        {
            Id = x.Id,
            InStuck = x.InStuck,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice,
            CurrentCount = x.CalculateCurrent(),
            CreateDate = x.CreateDate.ToFarsi()
        });
        if (searchModel.ProductId > 0)
        {
            query = query!.Where(x => x.ProductId == searchModel.ProductId);
        }

        if (searchModel.InStuck == true)
        {
            query = query!.Where(x => x.InStuck == false);
        }

        var invetoryList = query!.OrderByDescending(x => x.Id).ToList();
        invetoryList.ForEach(item =>
        {
            item.ProductName = products?.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            
        });
        return invetoryList;
    }

    public List<InventoryOperationViewModel> GetOperationLog(int InventoryId)
    {
        var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == InventoryId);
        return inventory.Operations.Select(x => new InventoryOperationViewModel
        {
            Id = x.Id,
            Count = x.Count,
            CurrentCount = x.CurrentCount,
            Description = x.Description,
            Operation = x.Operation,
            OperationDate = x.OperationDate.ToFarsi(),
            Operator = "مدیر سیستم",
            OperatorId = x.OperatorId,
            OrderId = x.OrderId,
        }).OrderByDescending(x=>x.Id).ToList();
    }
}