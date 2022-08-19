using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        EditProductCategory GetDetail(long Id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
