using _0_Framework.Application;


namespace ShopManagement.Application.Contracts.Slider
{
    public interface ISLideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        EditSlide GetDetail(int id);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        List<SlideVIewModel> GetList();
    }
}
