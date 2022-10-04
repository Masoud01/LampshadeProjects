namespace LampshadeQuery.Contract.ProductCategory;

public interface IProductCategoryQueryModel
{
    ProductCategoryViewQueryModel GetCategoryWithProductBy(string slug);
    List<ProductCategoryViewQueryModel> GetProductCategory();
    List<ProductCategoryViewQueryModel> GetProductCategoryWithProduct();
}