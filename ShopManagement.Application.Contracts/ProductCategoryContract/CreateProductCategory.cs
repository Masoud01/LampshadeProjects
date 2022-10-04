using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategoryContract
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = VlidateMessage.IsRequired)]

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? MetaKeyword { get; set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? MetaDescription { get; set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? Slug { get; set; }

    }
}
