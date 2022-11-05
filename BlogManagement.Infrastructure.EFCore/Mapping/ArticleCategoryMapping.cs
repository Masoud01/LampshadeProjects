using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping;

public class ArticleCategoryMapping:IEntityTypeConfiguration<ArticleCategory>
{
    
    public void Configure(EntityTypeBuilder<ArticleCategory> builder)
    {
        builder.ToTable("Tbl_ArticleCategory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired().IsUnicode();
        builder.Property(x => x.ShortDescription).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.Picture).HasMaxLength(255);
        builder.Property(x => x.PictureAlt).HasMaxLength(150);
        builder.Property(x => x.PictureTitle).HasMaxLength(150);
        builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();
        builder.Property(x => x.KeyWord).HasMaxLength(80).IsRequired();
        builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
        builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);
        builder.HasMany(x => x.Articles).WithOne(x => x.ArticleCategory)
            .HasForeignKey(x => x.CategoryId);

    }
}