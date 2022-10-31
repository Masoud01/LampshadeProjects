using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace CommentManagement.Domain.ProductCommentAgg;

public class ProductComment:EntityBase
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public bool IsConfirm { get; private set; }
    public bool IsCancel { get; private set; }
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    public ProductComment(
        string name, string email, 
        string description, int productId)
    {
        Name = name;
        Email = email;
        Description = description;
        ProductId = productId;
    }

    public void Confirm() => IsConfirm = true;
    public void Cancel() => IsCancel = true;
}