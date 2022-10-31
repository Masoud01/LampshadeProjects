using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.CommentAgg;

public class Comment:EntityBase
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Message { get; private set; }
    public bool IsConfirm { get; private set; }
    public bool IsCancel { get; private set; }
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public Comment(string name, string email, string message,int productId)
    {
        Name = name;
        Email = email;
        Message = message;
        ProductId = productId;
    }

    public void Confirm() => IsConfirm = true;
    public void Cancel() => IsCancel = true;
}