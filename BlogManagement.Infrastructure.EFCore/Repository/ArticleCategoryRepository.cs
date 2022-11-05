using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository;

public class ArticleCategoryRepository : RepositoryBase<int, ArticleCategory>, IArticleCategoryRepository
{
    private readonly BlogContext _context;
    public ArticleCategoryRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        var query =
            _context.ArticleCategories!
            .Select(
            x => new ArticleCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreateDate = x.CreateDate.ToFarsi(),
                ShowOrder = x.ShowOrder,
                ShortDescription = x.ShortDescription
            });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name!.Equals(searchModel.Name));
        }

        return query.OrderByDescending(x => x.Id).AsNoTracking().ToList();
    }

    public EditArticleCategory GetDetail(int Id)
    {
        return _context.ArticleCategories
            !.Select(x => new EditArticleCategory()
            {
                Id = x.Id,
                Name = x.Name,
                CanonicalAddress = x.CanonicalAddress,
                ShortDescription = x.ShortDescription,
                MetaDescription = x.MetaDescription,
                KeyWord = x.KeyWord,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                
            }).FirstOrDefault(x => x.Id == Id);
    }

    public string GetSlugById(int Id)
    {
        return _context!.ArticleCategories?
            .Select(x => new { x.Id, x.Slug })
            .FirstOrDefault(x => x.Id.Equals(Id))
            ?.Slug;
    }
}