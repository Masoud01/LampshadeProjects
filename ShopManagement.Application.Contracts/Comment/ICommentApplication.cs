using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Comment;

public interface ICommentApplication
{
    OperationResult Add(AddComment comment);
    OperationResult Confirm(int Id);
    OperationResult Cancel(int Id);
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}