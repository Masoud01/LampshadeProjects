using LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class MenuViewComponent:ViewComponent
{
    private readonly IProductCategoryQueryModel _productCategoryQueryModel;

    public MenuViewComponent(IProductCategoryQueryModel productCategoryQueryModel)
    {
        _productCategoryQueryModel = productCategoryQueryModel;
    }
    public IViewComponentResult Invoke()
    {
        //var productCategorys=
        return View();
    }
}