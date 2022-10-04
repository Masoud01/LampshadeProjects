using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var data = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(data);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductPicture Detail(int Id)
        {
            return _productPictureRepository.Detail(Id);
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_productPictureRepository.Exist(
                x => x.Picture == command.Picture &&
                x.ProductId == command.ProductId
                && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Remove(int Id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(int Id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
