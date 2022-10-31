using _0_Framework.Domain;
using CommentManagement.Application.Contract.ProductComment;

namespace CommentManagement.Domain.ProductCommentAgg;

public interface IProductCommentRepository:IRepository<int,ProductComment>
{
    List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel);
}