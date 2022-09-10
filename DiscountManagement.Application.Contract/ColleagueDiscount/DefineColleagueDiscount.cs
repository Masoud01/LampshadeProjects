using Microsoft.AspNetCore.Authentication;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount;

public class DefineColleagueDiscount
{
    public int ProductId { get; set; }
    public int DiscountRate { get;  set; }
    public List<ProductViewModel> Product { get; set; }
}