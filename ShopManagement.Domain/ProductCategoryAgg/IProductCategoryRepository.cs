using ShopManagement.Application.Contracts.ProductCategoryContract;
using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory productCategory);
        ProductCategory Get(long Id);
        List<ProductCategory> GetAll();
        bool Exist(Expression<Func<ProductCategory,bool>> expression);
        EditProductCategory GetDetail(long Id);
        List<ProductCategoryViewModel> search(ProductCategorySearchModel searchModel);
        void SaveChanges();

    }
}
