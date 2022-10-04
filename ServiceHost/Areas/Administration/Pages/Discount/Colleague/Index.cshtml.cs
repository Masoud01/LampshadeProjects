using DiscountManagement.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.Colleague
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Mesagee { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscount;
        public ColleagueDiscountSearchModel SearchModel;

        public SelectList Product;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication,IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }
        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Product = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscount = _colleagueDiscountApplication.search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Product = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(int Id)
        {
            var customerDiscount = _colleagueDiscountApplication.GetDetails(Id);
            customerDiscount.Product = _productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetNotActive(int Id)
        {
            var result = _colleagueDiscountApplication.DeActive(Id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGeActive(int Id)
        {
            var result = _colleagueDiscountApplication.Active(Id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
