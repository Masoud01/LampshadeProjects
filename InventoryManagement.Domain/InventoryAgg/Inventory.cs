using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg;

public class Inventory : EntityBase
{
    public int ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public bool InStuck { get; private set; }
    public List<InventoryOperation> Operations { get; private set; }

    public Inventory(
        int productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStuck = false;

    }

    public void Edit(
        int productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
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