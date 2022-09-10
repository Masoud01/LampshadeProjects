using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;


namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<int, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _Context;
        public ProductPictureRepository(ShopContext context):base(context)
        {
            _Context = context;
        }
        public EditProductPicture Detail(int Id)
        {
           return _Context.ProductPictures.Select(x => new EditProductPicture()
            {
                Id=x.Id,
                ProductId=x.ProductId,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle
            }).SingleOrDefault(x => x.Id == Id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _Context.ProductPictures
                .Include(x=>x.Product)
                .Select(x => new ProductPictureViewModel()
            {
                Id=x.Id,
                Picture=x.Picture,
                ProductId=x.ProductId,
                CreateDate=x.CreateDate.ToFarsi(),
                ProductName=x.Product.Name
            });
            if (!string.IsNullOrWhiteSpace(searchModel.ProductName))
            {
                query = query.Where(x => x.ProductName.Contains(searchModel.ProductName));
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
