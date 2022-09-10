using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application;

public class ColleagueDiscountApplication : IColleagueDiscountApplication
{
    private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

    public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = colleagueDiscountRepository;
    }
    public OperationResult Define(DefineColleagueDiscount command)
    {
        var operation = new OperationResult();
        if (_colleagueDiscountRepository.Exist(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
        {
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        }

        var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
        _colleagueDiscountRepository.Create(discount);
        _colleagueDiscountRepository.SaveChanges();
        return operation.Succesdead();
    }

    public OperationResult Edit(EditColleagueDiscount command)
    {
        var operation = new OperationResult();
        var detail = _colleagueDiscountRepository.Get(command.Id);
        if (detail == null)
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }
        if (_colleagueDiscountRepository.Exist(x =>
                x.ProductId == command.ProductId &&
                x.DiscountRate == command.DiscountRate && 
                x.Id!=command.Id))
        {
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        }

        detail.Edit(command.ProductId, command.DiscountRate);
        _colleagueDiscountRepository.SaveChanges();
        return operation.Succesdead();
    }

    public EditColleagueDiscount GetDetails(int Id)
    {
        return _colleagueDiscountRepository.GetDetails(Id);
    }

    public List<ColleagueDiscountViewModel> search(ColleagueDiscountSearchModel searchModel)
    {
        return _colleagueDiscountRepository.search(searchModel);
    }

    public OperationResult Active(int Id)
    {
        var operation = new OperationResult();
        var detail = _colleagueDiscountRepository.Get(Id);
        if (detail == null)
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        detail.IsActive();
        _colleagueDiscountRepository.SaveChanges();
        return operation.Succesdead();
    }

    public OperationResult DeActive(int Id)
    {
        var operation = new OperationResult();
        var detail = _colleagueDiscountRepository.Get(Id);
        if (detail == null)
        {
            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        detail.DeActive();
        _colleagueDiscountRepository.SaveChanges();
        return operation.Succesdead();
    }
}