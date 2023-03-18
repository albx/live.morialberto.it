using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class ArchiveViewModel
{
    public IEnumerable<StreamingListItem> Streamings { get; set; } = Enumerable.Empty<StreamingListItem>();
}
