using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategoryContract;

namespace ServiceHost.Areas.Administration.Pages.Discount.Customer
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Mesagee { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscountView;
        public CustomerDiscountSearchModel SearchModel;

        public SelectList Product;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Product = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscountView = _customerDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Product = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(int Id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(Id);
            customerDiscount.Product = _productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
