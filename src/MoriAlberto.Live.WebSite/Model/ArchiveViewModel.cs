using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class ArchiveViewModel
{
    public IEnumerable<StreamingList.StreamingListItem> Streamings { get; set; } = Enumerable.Empty<StreamingList.StreamingListItem>();

    public int NumberOfPages { get; set; }
}
