using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Remove(int Id);
        OperationResult Restore(int Id);
        EditProductPicture Detail(int Id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
