using LampshadeQuery.Contract.Product;

namespace LampshadeQuery.Contract.ProductCategory;

public class ProductCategoryViewQueryModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }
    public string? Slug { get; set; }
    public string Desacription { get; set; }
    public string Keyword { get; set; }
    public string MetaDescription { get; set; }
    public List<ProductQueryModel>? Products { get; set; }
}