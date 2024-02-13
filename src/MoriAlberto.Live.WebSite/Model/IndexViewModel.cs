using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class IndexViewModel
{
    public StreamingList.StreamingListItem? NextStreaming { get; set; }

    public IEnumerable<StreamingList.StreamingListItem> Streamings { get; set; } = [];
}
