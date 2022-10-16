using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }
        
        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exist(x => x.Name!.Equals(command.Name)))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
            }


            var slug = command.Slug!.Slugify();
            var categorySlug = _productCategoryRepository.GetCategorySlugById(command.CategoryId);
            var path = $"{categorySlug}//{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture!, path);
            var product =  new Product(
                command.Name!,command.Code!,
                command.ShortDescription!,
                command.Description!,pictureName!,
                command.PictureAlt!,command.PictureTitle!,
                slug,command.Keyword!,
                command.MetaDescription!,command.CategoryId
                );
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product.Equals(null))
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_productRepository.Exist(x=>x.Name!.Equals(command.Name) && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug = command.Slug!.Slugify();
            var path = $"{product.ProductCategory!.Slug}//{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture!, path);
            product.EditProduct(
                command.Name!, command.Code!,
                 command.ShortDescription!,
                command.Description!, pictureName!,
                command.PictureAlt!, command.PictureTitle!,
                slug, command.Keyword!, 
                command.MetaDescription!,command.CategoryId
                );
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProduct GetDetial(int Id)
        {
            return _productRepository.GetDetail(Id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProduct();
        }
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
