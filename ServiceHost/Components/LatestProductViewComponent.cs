using LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class LatestProductViewComponent:ViewComponent
{
    private readonly IProductQuery _ProductQuery;

    public LatestProductViewComponent(IProductQuery productQuery)
    {
        _ProductQuery = productQuery;
    }
    public IViewComponentResult Invoke()
    {
        var query = _ProductQuery.GetLatestArrivals();
        return View(query);
    }
    
}