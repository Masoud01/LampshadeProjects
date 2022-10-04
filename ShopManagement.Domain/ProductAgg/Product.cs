using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string? Name { get; private set; }
        public string? Code { get; private set; }
        public string? ShortDescription { get; private set; }
        public string? Description { get; private set; }
        public string? Picture { get; private set; }
        public string? PictureAlt { get; private set; }
        public string? PictureTitle { get; private set; }
        public string? Slug { get; private set; }
        public string? Keyword { get; private set; }
        public string? MetaDescription { get; private set; }
        public int CategoryId { get; private set; }
        public ProductCategory ? ProductCategory { get; private set; }
        public List<ProductPicture>? ProductPictures { get; private set; }
        public  Product(
            string name, string code, string shortDescription,
            string description, string picture, 
            string pictureAlt, string pictureTitle,
            string slug, string keyword, 
            string metaDescription, int categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keyword = keyword;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }
        public void EditProduct(
            string name, string code, string shortDescription,
            string description, string picture,
            string pictureAlt, string pictureTitle,
            string slug, string keyword,
            string metaDescription, int categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keyword = keyword;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }
      
    }
}
