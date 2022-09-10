using LampshadeQuery.Contract.ProductCategory;
using ShopManagement.Infrastructure.EFCore;

namespace LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQueryModel
{
    private readonly ShopContext _context;

    public ProductCategoryQuery(ShopContext context)
    {
        _context = context;
    }
    public List<ProductCategoryViewQueryModel> GetProductCategory()
    {
        return _context.ProductCategories.Select(x =>
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
}