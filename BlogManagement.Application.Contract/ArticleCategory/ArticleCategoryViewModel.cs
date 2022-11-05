using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.ArticleCategory;

public class ArticleCategoryViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public string? ShortDescription { get; set; }
    public int ShowOrder { get; set; }
    public string? CreateDate { get; set; }
}