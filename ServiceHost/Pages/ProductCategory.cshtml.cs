using LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQueryModel _categoryQueryModel;

        public ProductCategoryModel(IProductCategoryQueryModel categoryQueryModel)
        {
            _categoryQueryModel = categoryQueryModel;
        }

        public ProductCategoryViewQueryModel ProductCategory;
        public void OnGet(string slug)
        {
            ProductCategory = _categoryQueryModel.GetCategoryWithProductBy(slug); 
        }
    }
}
