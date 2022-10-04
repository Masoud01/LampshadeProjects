using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.Product;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    [Range(1,100000,ErrorMessage = VlidateMessage.IsRequired)]
    public int ProductId { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = VlidateMessage.IsRequired)]
    public double UnitPrice { get; set; }
    public bool InStuck { get; set; }
    public List<ProductViewModel> Product { get; set; }
}