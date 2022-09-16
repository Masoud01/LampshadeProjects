using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slider;


namespace ServiceHost.Areas.Administration.Pages.Shop.Slider
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string? Mesagee { get; set; }
        public List<SlideVIewModel>? SlideVIewModels;
        private readonly ISLideApplication _sLideApplication;
        public IndexModel(ISLideApplication sLideApplication)
        {
            _sLideApplication = sLideApplication;
          
        }
        public void OnGet()
        {
            SlideVIewModels = _sLideApplication.GetList();
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _sLideApplication.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(int Id)
        {
            var slider = _sLideApplication.GetDetail(Id);
            return Partial("./Edit", slider);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _sLideApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(int Id)
        {
            var result = _sLideApplication.Remove(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGeRestore(int Id)
        {
            var result = _sLideApplication.Restore(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
