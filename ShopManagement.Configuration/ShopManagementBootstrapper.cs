using LampshadeQuery.Contract.Product;
using LampshadeQuery.Contract.ProductCategory;
using LampshadeQuery.Contract.Slides;
using LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategoryContract;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void  Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            //Product Services
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();
            //ProductPicture Services
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            //Slider Services
            services.AddTransient<ISLideApplication, SlideApplication>();
            services.AddTransient<ISlideRepositroy, SliderRepository>();
            //commentRepository
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();
            //LampShadeQuery
            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQueryModel,ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();
            //ConnectionString
            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}