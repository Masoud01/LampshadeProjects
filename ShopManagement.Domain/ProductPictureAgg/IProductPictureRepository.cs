using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<int,ProductPicture>
    {
        EditProductPicture Detail(int Id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        ProductPicture GetWithProductAndCategoryId(int id);
    }
}
