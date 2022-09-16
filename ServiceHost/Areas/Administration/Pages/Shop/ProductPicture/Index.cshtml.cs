using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string? Mesagee { get; set; }
        public List<ProductPictureViewModel>? productViewModels;
        public ProductPictureViewModel? SearchModel;
        public SelectList? Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductPictureApplication productPictureApplication,IProductApplication productApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");

            productViewModels = _productPictureApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetProducts()
            }; 
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(int Id)
        {
            var product = _productPictureApplication.Detail(Id);
            product.Products = _productApplication.GetProducts();
            return Partial("./Edit", product);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(int Id)
        {
            var result = _productPictureApplication.Remove(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGeRestore(int Id)
        {
            var result = _productPictureApplication.Restore(Id);
            if (result.IsSuccedead)
            {
                return RedirectToPage("./Index");
            }
            Mesagee = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
