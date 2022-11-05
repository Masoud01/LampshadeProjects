using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }
        public List<ArticleCategoryViewModel> ArticleCategoryView;
        public ArticleCategorySearchModel SearchModel;
        public void OnGet(ArticleCategorySearchModel searchModel)
        {
           ArticleCategoryView= _articleCategoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate(){
            return Partial("./Create",new CreateArticleCategory());
        }
        public JsonResult OnPostCreate(CreateArticleCategory command){
            var result=_articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(int Id)
        {
            var articleCategory = _articleCategoryApplication.GetDetail(Id);
            return Partial("./Edit", articleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }


    }
}
