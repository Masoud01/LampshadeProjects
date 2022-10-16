using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;


namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<int, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext? _context;
        public ProductPictureRepository(ShopContext context):base(context)
        {
            _context = context;
        }
        public EditProductPicture Detail(int Id)
        {
           return _context?.ProductPictures!.Select(x => new EditProductPicture()
            {
                Id=x.Id,
                ProductId=x.ProductId,
              //  Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle
            }).SingleOrDefault(x => x.Id == Id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context?.ProductPictures!
                .Include(x=>x.Product)
                .Select(x => new ProductPictureViewModel()
            {
                Id=x.Id,
                Picture=x.Picture,
                ProductId=x.ProductId,
                CreateDate=x.CreateDate.ToFarsi(),
                ProductName=x.Product!.Name
            });
            if (!string.IsNullOrWhiteSpace(searchModel.ProductName))
            {
                query = query!.Where(x => x.ProductName!.Contains(searchModel.ProductName));
            }
            return query!.OrderByDescending(x => x.Id).ToList();
        }

        public ProductPicture GetWithProductAndCategoryId(int id)
        {
            return _context?
                .ProductPictures!
                .Include(
                    x => x.Product
                ).ThenInclude(x => x.ProductCategory)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
