using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategoryContract;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Mesagee { get; set; }
        public List<ProductViewModel> productViewModels;
        public ProductSearchModel SearchModel;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList ProductCateories;
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }
        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCateories = new SelectList(
                _productCategoryApplication.ProductCategories(),"Id","Name"
                );
           productViewModels = _productApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct();
            command.categoryViewModels = _productCategoryApplication.ProductCategories();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(int Id)
        {
            var product = _productApplication.GetDetial(Id);
            product.categoryViewModels = _productCategoryApplication.ProductCategories();
            return Partial("./Edit", product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(command);
        }
        public IActionResult OnGetNotInStuck(int Id)
        {
            var result=_productApplication.NotInStuck(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGeIsInStuck(int Id)
        {
            var result=_productApplication.InStuck(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
