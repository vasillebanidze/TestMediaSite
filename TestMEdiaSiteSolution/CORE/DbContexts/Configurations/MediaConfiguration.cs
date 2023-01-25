using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORE.DbContexts.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("Medias", "dbo");
        builder.HasKey(e => e.MediaId).HasName("PK_MediaId");

        builder.Property(p => p.MediaId).IsRequired();
        builder.Property(p => p.MediaTitle).IsRequired().HasMaxLength(100).IsConcurrencyToken();
        builder.Property(p => p.PictureUrl).IsRequired().HasMaxLength(100);

        builder.Property(p => p.MediaTypeId).IsRequired();
        builder.HasOne(d => d.MediaType).WithMany(p => p.Medias)
            .HasForeignKey(d => d.MediaTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}