﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISLideApplication
    {
        private readonly ISlideRepositroy _slideRepositroy;
        public SlideApplication(ISlideRepositroy slideRepositroy)
        {
            _slideRepositroy = slideRepositroy;
        }
        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            if (_slideRepositroy.Exist(x => x.Picture.Equals(command.Picture)))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slider = new Slider(
                command.Picture, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text,
                command.BtnText,command.Link);
            _slideRepositroy.Create(slider);
            _slideRepositroy.SaveChanges();
            return operation.Succesdead();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepositroy.Get(command.Id);
            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_slideRepositroy.Exist(x=>x.Picture.Equals(command.Picture) && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            slide.Edit(
                command.Picture, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepositroy.SaveChanges();
            return operation.Succesdead();
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
           return operation.Succesdead();
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
            return operation.Succesdead();
        }
    }
}