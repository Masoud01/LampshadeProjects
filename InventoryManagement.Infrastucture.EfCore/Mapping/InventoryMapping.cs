using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EfCore.Mapping;

public class InventoryMapping:IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");
        builder.HasKey(x => x.Id);
        builder.OwnsMany(x=>x.Operations,modelBuilder=>
        {
            modelBuilder.ToTable("Operation");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
            modelBuilder.Property(x => x.Description).HasMaxLength(1000);
        });
    }
}