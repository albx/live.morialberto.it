using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class IndexViewModel
{
    public IEnumerable<StreamingList.StreamingListItem> Streamings { get; set; } = Enumerable.Empty<StreamingList.StreamingListItem>();
}
