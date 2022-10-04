using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using _0_Framework.Application;
namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Name { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Code { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? ShortDescription { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Description { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Picture { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? PictureAlt { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? PictureTitle { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Slug { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string ? Keyword { get;  set; }
        public string ? MetaDescription { get;  set; }
        public int CategoryId { get;  set; }
        public List<ProductCategoryViewModel>? CategoryViewModels { get; set; }
    }
}
