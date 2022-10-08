using LampshadeQuery.Contract.Product;
using LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public List<ProductQueryModel> Product;
        public string Value;
        public void OnGet(string value)
        {
            Value = value;
            Product = _productQuery.Search(value); 
        }
    }
}
