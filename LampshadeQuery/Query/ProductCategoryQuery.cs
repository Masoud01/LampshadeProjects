using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using LampshadeQuery.Contract.Product;
using LampshadeQuery.Contract.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using _0_Framework.Application;
namespace LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQueryModel
{
    private readonly ShopContext? _context;
    private readonly InventoryContext? _inventoryContext;
    private readonly DiscountContext? _discountContext;

    public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }

    public ProductCategoryViewQueryModel GetCategoryWithProductBy(string slug)
    {
        var inventory = _inventoryContext?.Inventory!
            .Select(
                x => new { x.ProductId, x.UnitPrice }
            );
        var discounts = _discountContext?.CustomerDiscounts
            ?.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            ?.Select
            (
                x => new { x.DiscountRate, x.ProductId,x.EndDate }
            );
        var category = _context?.ProductCategories
            !.Include(x => x.Products)
            !.ThenInclude(x => x.ProductCategory)
            !.Select(x => new
                ProductCategoryViewQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products!),
                    Desacription = x.Description!,
                    MetaDescription = x.MetaDescription!,
                    Keyword = x.MetaKeyword!,
                    Slug = x.Slug
                }).FirstOrDefault(x=>x.Slug==slug);
        
            foreach (var product in category?.Products!)
            {
                var productInventory = inventory?.FirstOrDefault(
                    x => x.ProductId == product.Id
                );
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault
                        (x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        var discountRate = discounts?.FirstOrDefault
                        (x => x.ProductId == product.Id
                        )?.DiscountRate;
                        product.DiscountRate = Convert.ToInt32(discountRate);
                        product.HasDiscount = discountRate > 0;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((double)((price * discountRate) / 100)!);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }


            }
        
        return category;
    }


public List<ProductCategoryViewQueryModel> GetProductCategory()
    {
        return _context!.ProductCategories?.Select(x =>
            new ProductCategoryViewQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }
        ).ToList();
    }

    public List<ProductCategoryViewQueryModel> GetProductCategoryWithProduct()
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
        var categorys = _context?.ProductCategories
            !.Include(x => x.Products)
            !.ThenInclude(x => x.ProductCategory)
            !.Select(x => new
                ProductCategoryViewQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)

            }).ToList();
        foreach (var item in categorys)
        {
            foreach (var product in item.Products)
            {
                var productInventory = inventory?.FirstOrDefault(
                    x => x.ProductId == product.Id
                );
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
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
        }
        return categorys;
    }

    private static List<ProductQueryModel> MapProducts(List<Product> xProducts)
    {
        return xProducts.Select(x => new ProductQueryModel()
        {
            Id = x.Id,
            Category = x.ProductCategory?.Name,
            ProductName = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
        }).OrderByDescending(x => x.Id).ToList();
    }
}