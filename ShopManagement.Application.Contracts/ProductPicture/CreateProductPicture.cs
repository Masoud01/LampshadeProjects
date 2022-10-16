using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1,1000,ErrorMessage = VlidateMessage.IsRequired)]
        public int ProductId { get;  set; }
        [MaxFileSize(1*1024*1024,ErrorMessage = VlidateMessage.MaxFileSize)]
        public IFormFile? Picture { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? PictureAlt { get;  set; }
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? PictureTitle { get;  set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}
