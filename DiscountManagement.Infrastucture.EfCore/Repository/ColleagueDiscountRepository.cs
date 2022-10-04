using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository;

public class ColleagueDiscountRepository:RepositoryBase<int, ColleagueDiscount>,IColleagueDiscountRepository
{
    private readonly DiscountContext _context;
    private readonly ShopContext _shopContext;
    public ColleagueDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
    {
        _context = context;
        _shopContext = shopContext;
    }

    public EditColleagueDiscount GetDetails(int Id)
    {
        return _context?.ColleagueDiscounts!.Select(x => new EditColleagueDiscount()
        {
            Id = x.Id,
            DiscountRate = x.DiscountRate,
            ProductId = x.ProductId
        }).FirstOrDefault(x => x.Id.Equals(Id));
    }

    public List<ColleagueDiscountViewModel> search(ColleagueDiscountSearchModel searchModel)
    {
        var products = _shopContext?.Products!.Select(x => new {x.Id,x.Name }).ToList();
        var query=_context?.ColleagueDiscounts!.Select(x=>new ColleagueDiscountViewModel()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            DiscountRate = x.DiscountRate,
            IsActive = x.Active,
            CreateDate = x.CreateDate.ToFarsi()
        });
        if (searchModel.ProductId > 0)
        {
            query = query!.Where(x => x.ProductId.Equals(searchModel.ProductId));
        }

        var discounts = query!.OrderByDescending(x => x.Id).ToList();
        discounts.ForEach(discount=>discount.Product=
            products?.FirstOrDefault(x=>x.Id.Equals(discount.ProductId))?.Name);
        return discounts;
    }
}