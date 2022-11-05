using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository;

public class ArticleRepository:RepositoryBase<int,Article>,IArticleRepository
{
    private readonly BlogContext _blogContext;
    public ArticleRepository(BlogContext context) : base(context)
    {
        _blogContext = context;
    }

    public EditArticle GetDetail(int Id)
    {
        return _blogContext.Articles.Select(x => new EditArticle()
        {
            CanonicalAddress = x.CanonicalAddress,
            CategoryId = x.CategoryId,
            Id = x.Id,
            //Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
            ShortDescription = x.ShortDescription,
            Description = x.Description,
            Keyword = x.Keyword,
            MetaDescription = x.MetaDescription,
            PublishDate = x.PublishDate.ToFarsi(),
            Title = x.Title
        }).FirstOrDefault(x => x.Id.Equals(Id));
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = _blogContext.Articles!
            .Include(x=>x.ArticleCategory)
            .Select(x => new ArticleViewModel()
        {
            Id = x.Id,
            CategoryId = x.ArticleCategory!.Id,
            CategoryName = x.ArticleCategory!.Name,
            Title = x.Title,
            CreateDate = x.CreateDate.ToFarsi(),
            PublishDate = x.PublishDate.ToFarsi(),
            ShortDescription = x.ShortDescription
        });
        if (searchModel.CategoryId > 0)
        {
            query = query.Where(x => x.CategoryId.Equals(searchModel.CategoryId));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
        {
            query = query.Where(x => x.Title!.Equals(searchModel.Title));
        }

        return query.AsNoTracking().OrderByDescending(x => x.Id).ToList();
    }

    public Article GetWithCategory(int Id)
    {
        return _blogContext.Articles!
            .Include(x => x.ArticleCategory)
            .FirstOrDefault(x => x.Id == Id);
    }
}