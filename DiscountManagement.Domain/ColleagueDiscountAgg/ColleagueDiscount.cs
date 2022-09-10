using _0_Framework.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg;

public class ColleagueDiscount:EntityBase
{
    public int ProductId { get;private set; }
    public int DiscountRate { get;private set; }
    public bool Active { get;private set; }

    public ColleagueDiscount(int productId, int discountRate)
    {
        ProductId = productId;
        DiscountRate = discountRate;
        Active=false;
    }
    public void Edit(int productId, int discountRate)
    {
        ProductId = productId;
        DiscountRate = discountRate;
    }

    public void IsActive()
    {
        Active = false;
    }

    public void DeActive()
    {
        Active=true;
    }
}