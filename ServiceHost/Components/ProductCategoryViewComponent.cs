using LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class ProductCategoryViewComponent:ViewComponent
{
    private readonly IProductCategoryQueryModel _categoryQuery;

    public ProductCategoryViewComponent(IProductCategoryQueryModel categoryQuery)
    {
        _categoryQuery = categoryQuery;
    }
    public IViewComponentResult Invoke()
    {
        var productCategory = _categoryQuery.GetProductCategory();
        return View(productCategory);
    }
}