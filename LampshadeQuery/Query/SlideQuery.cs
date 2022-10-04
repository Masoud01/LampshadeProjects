using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LampshadeQuery.Contract.Slides;
using ShopManagement.Infrastructure.EFCore;

namespace LampshadeQuery.Query
{
    public class SlideQuery:ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }
        public List<SlideQueryModel> GetSlides()
        {
            var query = _context!.Slides?.Where(x => x.IsRemoved.Equals(false))
                .Select(x => new SlideQueryModel()
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    BtnText = x.BtnText,
                    Heading = x.Heading,
                    Title = x.Title,
                    Text = x.Text,
                    Link = x.Link
                });
            return query!.ToList();
        }
    }
}
