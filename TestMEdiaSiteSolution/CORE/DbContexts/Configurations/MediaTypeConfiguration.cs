using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORE.DbContexts.Configurations;

public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
{
    public void Configure(EntityTypeBuilder<MediaType> builder)
    {
        builder.ToTable("MediaTypes", "dbo");
        builder.HasKey(e => e.MediaTypeId).HasName("PK_MediaTypeId");

        builder.Property(p => p.MediaTypeId).IsRequired();

        builder.Property(p => p.MediaTypeTitle).IsRequired().HasMaxLength(100).IsConcurrencyToken();
        builder.HasIndex(e => e.MediaTypeTitle, "UQ_MediaTypeTitle").IsUnique();
    }
}