using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class FooterViewComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}