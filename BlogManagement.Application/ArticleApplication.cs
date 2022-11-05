using _0_Framework.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application;

public class ArticleApplication:IArticleApplication
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFileUploader _fileUploader;
    private readonly IArticleCategoryRepository _articleCategoryRepository;
    public ArticleApplication(IArticleRepository articleRepository,IFileUploader fileUploader,IArticleCategoryRepository articleCategoryRepository)
    {
        _articleRepository = articleRepository;
        _fileUploader = fileUploader;
        _articleCategoryRepository = articleCategoryRepository;
    }
    public OperationResult Create(CreateArticle command)
    {
        var operation = new OperationResult();
        if (_articleRepository!.Exist(x => x.Title == command.Title))
        {
            operation.Failed(ApplicationMessages.DuplicatedRecord);
        }

        var slug = command.Slug!.Slugify();
        var categorySLug = _articleCategoryRepository.GetSlugById(command.CategoryId);
        var path = $"{categorySLug}//{slug}";
        var picturePath = _fileUploader.Upload(file: command.Picture!, picturePath: path);
        var publishDate = command.PublishDate!.ToGeorgianDateTime();
        var article = new Article(
            title:command.Title!,
            categoryId:command.CategoryId,
            shortDescription:command.ShortDescription,
            description:command.Description,
            picture:picturePath,
            pictureAlt:command.PictureAlt,
            pictureTitle:command.PictureTitle,
            slug:slug,
            metaDescription:command.MetaDescription,
            keyword:command.Keyword,
            canonicalAddress:command.CanonicalAddress,
            publishDate:publishDate
        );
        _articleRepository.Create(article);
        _articleRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditArticle command)
    {
        var operation = new OperationResult();
        var editArticle = _articleRepository.GetWithCategory(command.Id);
        if (editArticle == null)
        {
            operation.Failed(ApplicationMessages.RecordNotFound);
        }
        var slug = command.Slug!.Slugify();
        var categorySLug = _articleCategoryRepository.GetSlugById(command.CategoryId);
        var path = $"{categorySLug}//{slug}";
        var picturePath = _fileUploader.Upload(file: command.Picture!, picturePath: path);
        var publishDate = command.PublishDate!.ToGeorgianDateTime();
        editArticle!.EditArticle(
            title: command.Title!,
            categoryId: command.CategoryId,
            shortDescription: command.ShortDescription,
            description: command.Description,
            picture: picturePath,
            pictureAlt: command.PictureAlt,
            pictureTitle: command.PictureTitle,
            slug: slug,
            metaDescription: command.MetaDescription,
            keyword: command.Keyword,
            canonicalAddress: command.CanonicalAddress,
            publishDate: publishDate
        );
        _articleRepository.SaveChanges();
        return operation.Succedded();
    }

    public EditArticle GetDetail(int Id)
    {
        return _articleRepository.GetDetail(Id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        return _articleRepository.Search(searchModel);
    }
}