using _0_Framework.Application;

namespace BlogManagement.Application.Contract.Article;

public interface IArticleApplication
{
    OperationResult Create(CreateArticle command);
    OperationResult Edit(EditArticle command);
    EditArticle GetDetail(int Id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);

}