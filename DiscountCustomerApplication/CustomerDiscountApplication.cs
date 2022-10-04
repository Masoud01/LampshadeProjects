using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication:ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

     
        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }
        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            var startDate = command.StartDate!.ToGeorgianDateTime();
            var endTime = command.EndDate!.ToGeorgianDateTime();
            var discount = new CustomerDiscount(
                command.ProductId, command.DiscountRate,
                startDate,endTime, command.Reason);
            _customerDiscountRepository.Create(discount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_customerDiscountRepository.Exist(x => x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var startDate = command.StartDate!.ToGeorgianDateTime();
            var endTime = command.EndDate!.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId,command.DiscountRate,
                startDate,endTime,command.Reason
                );
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditCustomerDiscount GetDetails(int id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
        {
            return _customerDiscountRepository.Search(search);
        }
    }
}