﻿using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.CustomerDiscount;

public interface ICustomerDiscountApplication
{
    OperationResult Define(DefineCustomerDiscount command);
    OperationResult Edit(EditCustomerDiscount command);
    EditCustomerDiscount GetDetails(int id);
    List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);
}