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
        OperationResult IProductCategoryApplication.Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            //چک کنیم که با این نام وجود داره یا خیر اگه وجود دااشت خطا بده که وجود داره
            if (_productCategoryRepository.Exist(x => x.Name.Equals(command.Name)))
            {
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد لطفا مجداد تلاش کنید");
            }
            var slug = command.Slug.Slugify();
            var productCatecory = new ProductCategory(
                command.Name, command.Description,
                command.Picture, command.PictureAlt,
                command.PictureTitle, command.MetaKeyword,
                command.MetaDescription, slug);
            _productCategoryRepository.Create(productCatecory);
            _productCategoryRepository.SaveChanges();
            return operation.Succesdead();
        }

        OperationResult IProductCategoryApplication.Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
            {
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد لطفا مجداد تلاش کنید");
            }
            if (_productCategoryRepository.Exist(x => x.Name.Equals(command.Name) && x.Id.Equals(command.Id)))
            {
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد لطفا مجداد تلاش کنید");
            }
            var slug = command.Slug.Slugify();
            productCategory.Edit
                (command.Name, command.Description,
                command.Picture, command.PictureAlt,
                command.PictureTitle, command.MetaKeyword,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succesdead();
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