using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.ArticleCategory;

public class CreateArticleCategory
{
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public string? Name { get;  set; }
    public IFormFile? Picture { get;  set; }
    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public string? ShortDescription { get;  set; }
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public int ShowOrder { get;  set; }
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public string? Slug { get;  set; }
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public string? KeyWord { get;  set; }
    [Required(ErrorMessage = VlidateMessage.IsRequired)]
    public string? MetaDescription { get;  set; }
    public string? CanonicalAddress { get;  set; }
}