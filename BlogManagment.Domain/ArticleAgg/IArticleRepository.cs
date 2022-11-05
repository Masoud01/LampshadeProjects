using _0_Framework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg;

public interface IArticleRepository:IRepository<int,Article>
{
    EditArticle GetDetail(int Id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    Article GetWithCategory(int Id);
}