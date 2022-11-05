using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg;

public class ArticleCategory:EntityBase
{
    public string? Name { get; private set; }
    public string? Picture { get; private set; }
    public string? PictureAlt { get; private set; }
    public string? PictureTitle { get; private set; }
    public string? ShortDescription { get; private set; }
    public int ShowOrder { get; private set; }
    public string? Slug { get; private set; }
    public string? KeyWord { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? CanonicalAddress { get; private set; }
    public List<Article>? Articles { get;private set; }
    public ArticleCategory(
        string? name, string? picture,
        string pictureAlt,string pictureTitle,
        string? shortDescription, 
        int showOrder, string? slug,
        string? keyWord, string? 
            metaDescription, string? canonicalAddress)
    {
        Name = name;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        ShortDescription = shortDescription;
        ShowOrder = showOrder;
        Slug = slug;
        KeyWord = keyWord;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
    }
    public void EditArticleCategory(
        string? name, string? picture,
        string? pictureAlt,string? pictureTitle,
        string? shortDescription,
        int showOrder, string? slug,
        string? keyWord, string?
            metaDescription, string? canonicalAddress)
    {
        Name = name;
        if (!string.IsNullOrWhiteSpace(picture))
        {
            Picture = picture;
        }
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        ShortDescription = shortDescription;
        ShowOrder = showOrder;
        Slug = slug;
        KeyWord = keyWord;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
    }
}