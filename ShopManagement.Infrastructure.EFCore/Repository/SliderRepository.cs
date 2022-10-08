using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;
namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SliderRepository : RepositoryBase<int, Slider>, ISlideRepositroy
    {
        private readonly ShopContext _context;
        public SliderRepository(ShopContext context):base(context)
        {
            _context = context;
        }
        public EditSlide GetDetail(int id)
        {
            return _context!.Slides?.Select(x => new EditSlide()
            {
                Id = id,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Heading=x.Heading,
                Title=x.Title,
                Text=x.Text,
                BtnText=x.BtnText,
                Link = x.Link,
            }).AsNoTracking().SingleOrDefault(x => x.Id.Equals(id));
        }

        public List<SlideVIewModel> GetList()
        {
            return _context!.Slides?.Select(x => new SlideVIewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Picture = x.Picture,
                Heading = x.Heading,
                IsRemoved = x.IsRemoved,
                CreateDate = x.CreateDate.ToFarsi()
            }).AsNoTracking().OrderByDescending(x => x.Id).ToList();
        }
    }
}
