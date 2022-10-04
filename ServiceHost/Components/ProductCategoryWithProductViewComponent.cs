using LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class ProductCategoryWithProductViewComponent:ViewComponent
{
    private readonly IProductCategoryQueryModel _productCategoryQueryModel;

    public ProductCategoryWithProductViewComponent(IProductCategoryQueryModel productCategoryQueryModel)
    {
        _productCategoryQueryModel = productCategoryQueryModel;
    }
    public IViewComponentResult Invoke()
    {
        var products = _productCategoryQueryModel.GetProductCategoryWithProduct();
        return View(products);
    }
}