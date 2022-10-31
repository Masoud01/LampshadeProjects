namespace LampshadeQuery.Contract.Product;

public class CommentProductQueryModel
{
    public string? Name { get; set; }
    public string? Message { get; set; }
    public int CommentId { get; set; }
    public bool IsConfirm { get; set; }
    public bool IsCancel { get; set; }
}