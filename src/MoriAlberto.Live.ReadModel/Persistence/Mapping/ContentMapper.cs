using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoriAlberto.Live.ReadModel.Model;

namespace MoriAlberto.Live.ReadModel.Persistence.Mapping;

internal class ContentMapper : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("KITT_Contents");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.Title).HasMaxLength(255).IsRequired();
        builder.HasIndex(c => c.Title);

        builder.Property(c => c.Slug).HasMaxLength(255).IsRequired();
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.OwnsOne(c => c.Seo);
    }
}
