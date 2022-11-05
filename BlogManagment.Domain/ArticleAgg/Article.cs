using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg;

public class Article : EntityBase
{
    public string Title { get; private set; }
    public int CategoryId { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? Description { get; private set; }
    public string? Picture { get; private set; }
    public string? PictureAlt { get; private set; }
    public string? PictureTitle { get; private set; }
    public string? Slug { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? Keyword { get; private set; }
    public string? CanonicalAddress { get; private set; }
    public DateTime? PublishDate { get; private set; }
    public ArticleCategory? ArticleCategory { get; private set; }

    public Article(
        string title, int categoryId,
        string? shortDescription, string? description,
        string? picture, string? pictureAlt,
        string? pictureTitle, string? slug, string? metaDescription,
        string? keyword, string? canonicalAddress, DateTime? publishDate)
    {
        Title = title;
        CategoryId = categoryId;
        ShortDescription = shortDescription;
        Description = description;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        MetaDescription = metaDescription;
        Keyword = keyword;
        CanonicalAddress = canonicalAddress;
        PublishDate = publishDate;
    }
    public void EditArticle(
        string title, int categoryId,
        string? shortDescription, string? description,
        string? picture, string? pictureAlt,
        string? pictureTitle, string? slug, string? metaDescription,
        string? keyword, string? canonicalAddress, DateTime? publishDate)
    {
        Title = title;
        CategoryId = categoryId;
        ShortDescription = shortDescription;
        Description = description;
        if (!string.IsNullOrWhiteSpace(picture))
        {
            Picture = picture;
        }
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        MetaDescription = metaDescription;
        Keyword = keyword;
        CanonicalAddress = canonicalAddress;
        PublishDate = publishDate;
    }
}