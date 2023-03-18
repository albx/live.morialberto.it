using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoriAlberto.Live.ReadModel.Model;

namespace MoriAlberto.Live.ReadModel.Persistence.Mapping;

internal class StreamingMapper : IEntityTypeConfiguration<Streaming>
{
    public void Configure(EntityTypeBuilder<Streaming> builder)
    {
        builder.ToTable("KITT_Streamings");

        builder.Property(s => s.HostingChannelUrl).HasMaxLength(255).IsRequired();
        builder.Property(s => s.YouTubeVideoUrl).HasMaxLength(255);
    }
}
