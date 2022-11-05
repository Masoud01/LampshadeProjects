using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategory;
        private readonly IFileUploader _fileUploader;
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategory, IFileUploader fileUploader)
        {
            _articleCategory = articleCategory;
            fileUploader = _fileUploader;
        }
        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategory.Exist(x => x.Name!.Equals(command.Name)))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug!.Slugify();
            var path = $"{slug}";
            var articlePicture = _fileUploader.Upload(command.Picture!, path);
            var articleCategory = new ArticleCategory(
                name: command.Name,
                picture: articlePicture,
                pictureAlt:command.PictureAlt!,
                pictureTitle:command.PictureTitle!,
                shortDescription: command.ShortDescription,
                showOrder: command.ShowOrder,
                slug: slug,
                keyWord: command.KeyWord,
                metaDescription: command.MetaDescription,
                canonicalAddress: command.CanonicalAddress);
            _articleCategory.Create(articleCategory);
            _articleCategory.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategory.Get(command.Id);
            if (articleCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var slug = command.Slug!.Slugify();
            var path = $"{slug}";
            var articlePicture = _fileUploader.Upload(command.Picture!, path);
            articleCategory.EditArticleCategory(
                name: command.Name,
                picture: articlePicture,
                pictureAlt: command.PictureAlt!,
                pictureTitle: command.PictureTitle!,
                shortDescription: command.ShortDescription,
                showOrder: command.ShowOrder,
                slug: slug,
                keyWord: command.KeyWord,
                metaDescription: command.MetaDescription,
                canonicalAddress: command.CanonicalAddress);
            _articleCategory.SaveChanges();
            return operation.Succedded();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategory.Search(searchModel: searchModel);
        }

        public EditArticleCategory GetDetail(int Id)
        {
            return _articleCategory.GetDetail(Id);
        }
    }
}