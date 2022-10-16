using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Slider
{
    public class CreateSlide
    {
        
        [MaxFileSize(2*1024*1024)]
        public IFormFile? Picture { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? PictureAlt { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? PictureTitle { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? Heading { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? Title { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? Text { get;  set; }
        
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? BtnText { get;  set; }
       
        [Required(ErrorMessage = VlidateMessage.IsRequired)]
        public string? Link { get; set; }
    }
}
