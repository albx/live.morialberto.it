using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class ArchiveViewModel
{
    public IEnumerable<StreamingList.StreamingListItem> Streamings { get; set; } = [];

    public bool HasNextPage { get; set; }
}
