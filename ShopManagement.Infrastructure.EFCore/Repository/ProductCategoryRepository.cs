using _0_Framework.Inrastuchture;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Linq.Expressions;


namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<int, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductCategory GetDetail(int Id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                MetaKeyword = x.MetaKeyword,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == Id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreateDate = x.CreateDate.ToString(),

            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
