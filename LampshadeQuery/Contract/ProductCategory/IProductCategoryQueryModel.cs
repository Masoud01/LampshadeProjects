namespace LampshadeQuery.Contract.ProductCategory;

public interface IProductCategoryQueryModel
{
    List<ProductCategoryViewQueryModel> GetProductCategory();
}