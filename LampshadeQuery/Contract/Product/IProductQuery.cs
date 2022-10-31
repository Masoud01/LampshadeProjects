using LampshadeQuery.Query;

namespace LampshadeQuery.Contract.Product;

public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
    List<ProductQueryModel> Search(string value);
    ProductQueryModel GetDetails(string slug);
}