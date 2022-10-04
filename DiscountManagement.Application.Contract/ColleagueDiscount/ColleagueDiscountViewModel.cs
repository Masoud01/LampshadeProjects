namespace DiscountManagement.Application.Contract.ColleagueDiscount;

public class ColleagueDiscountViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Product { get; set; }
    public int DiscountRate { get; set; }
    public bool IsActive { get; set; }
    public string? CreateDate { get; set; }
}