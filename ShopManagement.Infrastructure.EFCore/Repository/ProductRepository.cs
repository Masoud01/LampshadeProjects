using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<int, Product>, IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;

        }
        public EditProduct GetDetail(int id)
        {
            var detail = _context?.Products!.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keyword = x.Keyword,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
            }).FirstOrDefault(x => x.Id.Equals(id));
            return detail;
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context?.Products!
                .Include(x => x.ProductCategory)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    CategoryId = x.CategoryId,
                    Picture = x.Picture,
                    CreateDate = x.CreateDate.ToFarsi(),
                    CategpryName = x.ProductCategory!.Name,
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query!.Where(x => x.Name!.Contains(searchModel.Name));
            }
            if(!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query!.Where(x => x.Code!.Contains(searchModel.Code));
            }
            if(searchModel.CategoryId != 0)
            {
                query = query!.Where(x => x.CategoryId.Equals(searchModel.CategoryId));
            }
            return query!.OrderByDescending(x => x.Id).ToList();
        }

        List<ProductViewModel> IProductRepository.GetProduct()
        {
            return _context?.Products!.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
