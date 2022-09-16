namespace InventoryManagement.Application.Contract.Inventory;

public class InventoryViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CurrentCount { get; set; }
    public string? ProductName { get; set; }
    public double UnitPrice { get; set; }
    public bool InStuck { get; set; }
    public string? CreateDate { get; set; }
}