using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping;

public class ArticleMapping:IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Tbl_Article");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ShortDescription).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(500);
        builder.Property(x => x.PictureAlt).HasMaxLength(500);
        builder.Property(x => x.PictureTitle).HasMaxLength(500);
        builder.Property(x => x.Slug).HasMaxLength(600).IsRequired();
        builder.Property(x => x.Keyword).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MetaDescription).IsRequired();
        builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);
        builder.HasOne(x => x.ArticleCategory).WithMany(x => x.Articles)
            .HasForeignKey(x => x.CategoryId);
    }
}