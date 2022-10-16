using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISLideApplication
    {
        private readonly ISlideRepositroy _slideRepositroy;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepositroy slideRepositroy, IFileUploader fileUploader)
        {
            _slideRepositroy = slideRepositroy;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            if (_slideRepositroy.Exist(x => x.Picture != null && x.Picture.Equals(command.Picture)))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = "slides";
            var picturePath = _fileUploader.Upload(command.Picture!, path);
            var slider = new Slider(
                picturePath, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text,
                command.BtnText,command.Link);
            _slideRepositroy.Create(slider);
            _slideRepositroy.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepositroy.Get(command.Id);
            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_slideRepositroy.Exist(x=>x.Picture != null && x.Picture.Equals(command.Picture) && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var path = "slides";
            var picturePath = _fileUploader.Upload(command.Picture!, path);
            slide.Edit(
                picturePath, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepositroy.SaveChanges();
            return operation.Succedded();
        }

        public EditSlide GetDetail(int id)
        {
            return _slideRepositroy.GetDetail(id);
        }

        public List<SlideVIewModel> GetList()
        {
            return _slideRepositroy.GetList();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var slide = _slideRepositroy.Get(id);
            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            slide.Remove();
            _slideRepositroy.SaveChanges();
           return operation.Succedded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var slide = _slideRepositroy.Get(id);
            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            slide.Restore();
            _slideRepositroy.SaveChanges();
            return operation.Succedded();
        }
    }
}
