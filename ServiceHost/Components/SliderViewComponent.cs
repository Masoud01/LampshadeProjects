using LampshadeQuery.Contract.Slides;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components;

public class SliderViewComponent:ViewComponent
{
    private readonly ISlideQuery _slideQuery;

    public SliderViewComponent(ISlideQuery slideQuery)
    {
        _slideQuery = slideQuery;
    }
    public IViewComponentResult Invoke()
    {
        var slides = _slideQuery.GetSlides();
        return View(slides);
    }
    
}