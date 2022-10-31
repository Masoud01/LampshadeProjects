using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampshadeQuery.Contract.Product
{
    public class ProductPictureQueryModel
    {
        public int ProductId { get; set; }
        public string? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
    }
}
