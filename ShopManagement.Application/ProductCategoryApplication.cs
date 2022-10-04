using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public List<ProductCategoryViewModel> ProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        OperationResult IProductCategoryApplication.Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            //چک کنیم که با این نام وجود داره یا خیر اگه وجود دااشت خطا بده که وجود داره
            if (_productCategoryRepository.Exist(x => x.Name!.Equals(command.Name)))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug = command.Slug!.Slugify();
            var productCategory = new ProductCategory(
                command.Name!, command.Description!,
                command.Picture!, command.PictureAlt!,
                command.PictureTitle!, command.MetaKeyword!,
                command.MetaDescription!, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        OperationResult IProductCategoryApplication.Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug = command.Slug!.Slugify();
            productCategory.Edit
                (command.Name!, command.Description!,
                command.Picture!, command.PictureAlt!,
                command.PictureTitle!, command.MetaKeyword!,
                command.MetaDescription!, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        EditProductCategory IProductCategoryApplication.GetDetail(int Id)
        {
            return _productCategoryRepository.GetDetail(Id);
        }

        List<ProductCategoryViewModel> IProductCategoryApplication.Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}