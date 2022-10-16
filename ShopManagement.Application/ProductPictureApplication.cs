using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var picturePath = $"{product.ProductCategory!.Slug}/{product.Slug}";
            var data = new ProductPicture(
                command.ProductId,
                _fileUploader.Upload(command.Picture!,picturePath), 
                command.PictureAlt, 
                command.PictureTitle);
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
            var productPicture = _productPictureRepository.GetWithProductAndCategoryId(command.Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var picturePath = $"{productPicture!.Product!.ProductCategory!.Slug}/{productPicture.Product!.Slug}";
            productPicture.Edit(
                command.ProductId,
                _fileUploader.Upload(command.Picture,picturePath), 
                command.PictureAlt,
                command.PictureTitle);
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
