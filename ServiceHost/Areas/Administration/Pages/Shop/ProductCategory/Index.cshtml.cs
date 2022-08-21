using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContract;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> productCategories;
        public ProductCategorySearchModel SearchModel;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }
        public void OnGet(ProductCategorySearchModel searchModel)
        {
           productCategories= _productCategoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate(){
            return Partial("./Create",new CreateProductCategory());
        }
        public JsonResult OnPostCreate(CreateProductCategory command){
            var result=_productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

    }
}
