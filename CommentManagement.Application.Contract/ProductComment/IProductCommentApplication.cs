using _0_Framework.Application;

namespace CommentManagement.Application.Contract.ProductComment;

public interface IProductCommentApplication
{
    OperationResult AddComment(AddProductComment comment);
    OperationResult Confirm(int id);
    OperationResult Cancel(int id);
    List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel);
}