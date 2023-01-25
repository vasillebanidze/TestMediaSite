using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORE.DbContexts.Configurations;

public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
{
    public void Configure(EntityTypeBuilder<WatchList> builder)
    {
        builder.ToTable("WatchLists", "dbo");
        builder.HasKey(e => new {e.UserId, e.MediaId}).HasName("PK_UserId_MediaId");

        builder.Property(p => p.UserId).IsRequired();

        builder.Property(p => p.MediaId).IsRequired();
        builder.HasOne(d => d.Media).WithMany(p => p.WatchLists)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}