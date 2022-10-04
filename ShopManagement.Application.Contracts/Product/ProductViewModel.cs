using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int CategoryId { get; set; }
        public string? CategpryName { get; set; }
        public string? Picture { get; set; }
        public string? CreateDate { get; set; }
    }
}
