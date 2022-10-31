using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;
namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Mesagee { get; set; }
        public List<CommentViewModel> CommentView;
        public CommentSearchModel SearchModel;
        private readonly ICommentApplication _comment;
        public IndexModel(ICommentApplication commentApplication)
        {
            _comment =commentApplication;
          
        }
        //
        public void OnGet(CommentSearchModel searchModel)=> 
            CommentView = _comment.Search(searchModel);
        //

        public IActionResult OnGetCancel(int Id)
        {
            var result = _comment.Cancel(Id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(int Id)
        {
            var result = _comment.Confirm(Id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }
    }
}
