using CommentManagement.Domain.ProductCommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cm.Infrastucture.EfCore.Mapping
{
    public class CommentProductMapping:IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            
        }
    }
}
