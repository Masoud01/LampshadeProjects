using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg;

public interface IArticleCategoryRepository : IRepository<int, ArticleCategory>
{
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    EditArticleCategory GetDetail(int Id);
    string GetSlugById(int Id);
}
