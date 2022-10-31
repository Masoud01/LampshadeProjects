using _0_Framework.Application;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using LampshadeQuery.Contract.Product;
using LampshadeQuery.Contract.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
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

        var products = _context.Products!.Include(x => x.ProductCategory)
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
            var productInventory = inventory!.FirstOrDefault(
                x => x.ProductId == product.Id
            );
            var price = productInventory!.UnitPrice;

            if (productInventory != null)
            {
                product.Price = price.ToMoney();
                var discount = discounts!.FirstOrDefault
                    (x => x.ProductId == product.Id);
                if (discount != null)
                {
                    int discountRate = discounts!.FirstOrDefault
                    (x => x.ProductId == product.Id
                    )!.DiscountRate;
                    product.DiscountRate = Convert.ToInt32(discountRate);
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((double)((price * discountRate) / 100));
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }
        return products;
    }

    public List<ProductQueryModel> Search(string value)
    {
        var inventory = _inventoryContext?.Inventory!
          .Select(
              x => new { x.ProductId, x.UnitPrice }
          );
        var discounts = _discountContext?.CustomerDiscounts
            ?.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            ?.Select
            (
                x => new { x.DiscountRate, x.ProductId, x.EndDate }
            );
        var query = _context?.Products
            !.Include(x => x.ProductCategory)
            !.Select(x => new
                ProductQueryModel()
            {
                Id = x.Id,
                Category = x.ProductCategory!.Name,
                CategorySlug = x.ProductCategory.Slug,
                ProductName = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            }).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(value))
        {
            query = query!.Where(x => x.ProductName!.Contains(value));
        }

        var products = query!.OrderByDescending(x => x.Id).ToList();
        foreach (var product in products)
        {
            var productInventory = inventory?.FirstOrDefault(
                x => x.ProductId == product.Id
            );
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts!.FirstOrDefault
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

        return products;
    }

    public ProductQueryModel GetDetails(string slug)
    {

        var inventory = _inventoryContext?.Inventory!
            .Select(
                x => new { x.ProductId, x.UnitPrice, x.InStuck }
            );
        var discounts = _discountContext?.CustomerDiscounts
            ?.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            ?.Select
            (
                x => new { x.DiscountRate, x.ProductId, x.EndDate }
            );

        var product = _context?
            .Products!
            .Include(x => x.ProductCategory)
            .Include(X => X.ProductPictures)
            .Include(x=>x.Comments)
            .Select(x => new ProductQueryModel()
            {
                Id = x.Id,
                Category = x.ProductCategory!.Name,
                CategorySlug = x.ProductCategory.Slug,
                ProductName = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Code = x.Code!,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Keywords = x.Keyword,
                MetaDescription = x.MetaDescription,
                Pictures = MapProductPictures(x.ProductPictures!),
                Comments = MapGetComments(x.Comments)
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
        if (product == null)
        {
            return new ProductQueryModel();
        }

        var productInventory = inventory!.FirstOrDefault(
            x => x.ProductId == product.Id
        );
        var price = productInventory!.UnitPrice;

        if (productInventory != null)
        {
            product.Price = price.ToMoney();
            var discount = discounts!.FirstOrDefault
                (x => x.ProductId == product.Id);
            if (discount != null)
            {
                int discountRate = discounts!.FirstOrDefault
                (x => x.ProductId == product.Id
                )!.DiscountRate;
                product.DiscountRate = Convert.ToInt32(discountRate);
                product.HasDiscount = discountRate > 0;
                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                var discountAmount = Math.Round((double)((price * discountRate) / 100));
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
                product.InStuck = productInventory.InStuck;
            }

        }
        return product;
    }

    private static List<CommentProductQueryModel> MapGetComments(List<Comment> xComments)
    {
        return xComments.Select(
            cx=>new CommentProductQueryModel()
            {
                Message = cx.Message,
                Name = cx.Name,
            })
            .Where(x=>x.IsCancel.Equals(false))
            .Where(x=>x.IsConfirm.Equals(true))
            .ToList();
    }


    private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> xProductPictures)
    {
        return xProductPictures
            .Where(x => x.IsRemoved == false)
            .Select(x => new ProductPictureQueryModel()
            {
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                IsRemoved = (bool)x.IsRemoved!
            }).ToList();
    }
}
