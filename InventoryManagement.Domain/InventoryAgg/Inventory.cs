using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg;

public class Inventory : EntityBase
{
    public int ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public bool InStuck { get; private set; }
    public List<InventoryOperation>? Operations { get; private set; }

    public Inventory(
        int productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStuck = false;

    }

    public int CalculateCurrent()
    {
        var plus = this.Operations?.Where(x => x.Operation).Sum(x => x.Count);
        var minus = this.Operations?.Where(x => x.Operation == false).Sum(x => x.Count);
        var result = plus - minus;
        return (int)result!;
    }

    public void InCrease(int count, int operatorId, string description)
    {
        var currentCount = CalculateCurrent() + count;
        var operation = new InventoryOperation(
            true, count,
            operatorId, currentCount,
            description, 0, Id);
        this.Operations?.Add(operation);
        this.InStuck = currentCount > 0;
    }

    public void Reduce(int count, int operatorId, string description,int orderId)
    {
        var currentCount = CalculateCurrent() - count;
        var operation = new InventoryOperation(
            true, count,
            operatorId, currentCount,
            description, orderId, Id);
        this.Operations?.Add(operation);
        this.InStuck = currentCount > 0;

    }
}
public class InventoryOperation
{

    public int Id { get; private set; }
    public bool Operation { get; private set; }
    public int Count { get; private set; }
    public int OperatorId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public int CurrentCount { get; private set; }
    public string? Description { get; private set; }
    public int OrderId { get; private set; }
    public int InventoryId { get; private set; }
    public Inventory? Inventory { get; private set; }

    public InventoryOperation(
        bool operation, int count,
        int operatorId, int currentCount,
        string? description, int orderId,
        int inventoryId)
    {
        Operation = operation;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = inventoryId;
    }
}
