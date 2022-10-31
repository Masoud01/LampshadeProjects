namespace CommentManagement.Application.Contract.ProductComment;

public class ProductCommentViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public bool IsCancel { get; set; }
    public bool IsConfrom { get; set; }
}