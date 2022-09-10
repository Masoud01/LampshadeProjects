using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository;

public class CustomerDiscountRepository : RepositoryBase<int, CustomerDiscount>, ICustomerDiscountRepository
{
    private readonly DiscountContext _context;
    private readonly ShopContext _shopContext;
    public CustomerDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
    {
        _context = context;
        _shopContext = shopContext;
    }

    public EditCustomerDiscount GetDetails(int id)
    {
        return _context.CustomerDiscounts.Select(
            x => new EditCustomerDiscount()
            {
                Id = x.Id,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                Reason = x.Reason
            }).SingleOrDefault(x => x.Id == id);
    }

    public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
    {
        var products = _shopContext.Products.Select(x => new { Id = x.Id, Name = x.Name });
        var query = _context.CustomerDiscounts.Select(
            x => new CustomerDiscountViewModel()
            {
                ID = x.Id,
                Reason = x.Reason,
                DiscountRate = x.DiscountRate,
                StartDateGr = x.StartDate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                EndDateGr = x.EndDate,
                CreateDate = x.CreateDate.ToFarsi()
            });
        if (!string.IsNullOrWhiteSpace(search.StartDate))
        {
            //var startDate = DateTime.Now;
            query = query.Where(x => x.StartDateGr > search.StartDate.ToGeorgianDateTime());
        }
        if (!string.IsNullOrWhiteSpace(search.EndDate))
        {
           // var endDate = DateTime.Now;
            query = query.Where(x => x.EndDateGr > search.EndDate.ToGeorgianDateTime());
        }

        var discount= query.OrderByDescending(
            x => x.ID).ToList();
        discount.ForEach(x=>
            x.ProductName=
                products.First(c=>c.Id==x.ProductId).Name);
        return discount;
    }
}