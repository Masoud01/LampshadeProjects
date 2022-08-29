using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;


namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<int,Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetail(int id);
        List<ProductViewModel> GetProduct();

    }
}
