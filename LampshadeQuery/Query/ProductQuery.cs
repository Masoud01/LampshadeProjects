using _0_Framework.Application;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using LampshadeQuery.Contract.Product;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace LampshadeQuery.Query;

public class ProductQuery : IProductQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;

    public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }
    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventory = _inventoryContext?.Inventory!
            .Select(
                x => new { x.ProductId, x.UnitPrice }
            );
        var discounts = _discountContext?.CustomerDiscounts
            ?.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            ?.Select
            (
                x => new { x.DiscountRate, x.ProductId }
            );

        var products = _context.Products.Include(x => x.ProductCategory)
            .Select(x => new ProductQueryModel()
            {
                Id = x.Id,
                Category = x.ProductCategory!.Name,
                ProductName = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();
  
        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(
                x => x.ProductId == product.Id
            );
            var price = productInventory.UnitPrice;

            if (productInventory != null)
            {
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault
                    (x => x.ProductId == product.Id);
                if (discount != null)
                {
                    int discountRate = discounts.FirstOrDefault
                    (x => x.ProductId == product.Id
                    ).DiscountRate;
                    product.DiscountRate = Convert.ToInt32(discountRate);
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((double)((price * discountRate) / 100));
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }
        return products;
    }
}
