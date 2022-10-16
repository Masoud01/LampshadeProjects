using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<int,ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetail(int Id);
        string GetCategorySlugById(int id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
