using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application;

public class CommentApplication:ICommentApplication
{
    private readonly ICommentRepository _commentRepository;

    public CommentApplication(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public OperationResult Add(AddComment comment)
    {
        var operation = new OperationResult();
        var comments = new Comment(
            name: comment.Name!, 
            email: comment.Email!,
            message: comment.Message!,
            productId:comment.ProductId
            );
        _commentRepository.Create(comments);
        _commentRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Confirm(int Id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.Get(Id);
        if (comment == null)
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        comment.Confirm();
        _commentRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Cancel(int Id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.Get(Id);
        if (comment == null)
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        comment.Cancel();
        _commentRepository.SaveChanges();
        return operation.Succedded();
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        return _commentRepository.Search(searchModel);
    }
}