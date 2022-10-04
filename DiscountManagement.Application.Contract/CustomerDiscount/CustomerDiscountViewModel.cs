namespace DiscountManagement.Application.Contract.CustomerDiscount;

public class CustomerDiscountViewModel
{
    public int ID { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int DiscountRate { get; set; }
    public string? StartDate { get; set; }
    public DateTime StartDateGr { get; set; }
    public string? EndDate { get; set; }
    public DateTime EndDateGr { get; set; }
    public string? Reason { get; set; }
    public string? CreateDate { get; set; }
}