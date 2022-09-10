using _0_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg;

public interface IColleagueDiscountRepository:IRepository<int,ColleagueDiscount>
{
    EditColleagueDiscount GetDetails(int Id);
    List<ColleagueDiscountViewModel> search(ColleagueDiscountSearchModel searchModel);
}