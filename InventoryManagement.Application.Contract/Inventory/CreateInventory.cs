namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    public int ProductId { get; set; }
    public double UnitPrice { get; set; }
    public bool InStuck { get; set; }
}