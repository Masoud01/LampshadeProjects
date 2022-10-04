namespace LampshadeQuery.Contract.Product;

public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
}