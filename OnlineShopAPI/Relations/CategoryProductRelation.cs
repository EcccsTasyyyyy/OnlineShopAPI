using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopAPI.Data;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Relations;

public class CategoryProductRelation : IEntityTypeConfiguration<CategoryModel>
{
    public void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId).IsRequired();
    }
}