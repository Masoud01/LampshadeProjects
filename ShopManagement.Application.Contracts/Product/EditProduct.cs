﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product
{
    public class EditProduct:CreateProduct
    {
        public int Id { get; set; }
    }
}
