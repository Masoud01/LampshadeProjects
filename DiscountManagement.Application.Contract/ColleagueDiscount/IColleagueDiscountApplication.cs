using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.ColleagueDiscount;

public interface IColleagueDiscountApplication
{
    OperationResult Define(DefineColleagueDiscount command);
    OperationResult Edit(EditColleagueDiscount command);
    EditColleagueDiscount GetDetails(int Id);
    List<ColleagueDiscountViewModel> search(ColleagueDiscountSearchModel searchModel);
    OperationResult Active(int Id);
    OperationResult DeActive(int Id);
}