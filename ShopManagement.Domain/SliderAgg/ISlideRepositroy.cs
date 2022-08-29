using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slider;


namespace ShopManagement.Domain.SliderAgg
{
    public interface ISlideRepositroy:IRepository<int,Slider>
    {
        List<SlideVIewModel> GetList();
        EditSlide GetDetail(int id);
    }
}
