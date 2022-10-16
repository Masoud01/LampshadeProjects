using LampshadeQuery.Contract.ProductCategory;
using LampshadeQuery.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryViewQueryModel ProductCategory;
        private readonly IProductCategoryQueryModel _productCategoryQuery;

        public ProductCategoryModel(IProductCategoryQueryModel productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string slug)
        {
            ProductCategory = _productCategoryQuery.GetCategoryWithProductBy(slug);
        }
    }
}
