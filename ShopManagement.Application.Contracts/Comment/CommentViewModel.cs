namespace ShopManagement.Application.Contracts.Comment;

public class CommentViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Message { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public bool IsCancel { get; set; }
    public bool IsConfirm { get; set; }
    public string CommentDate { get; set; }
}