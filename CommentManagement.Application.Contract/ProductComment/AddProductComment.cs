namespace CommentManagement.Application.Contract.ProductComment;

public class AddProductComment
{
    public string Name { get;  set; }
    public string Email { get;  set; }
    public string Description { get;  set; }
    public int ProductId { get;  set; }
}