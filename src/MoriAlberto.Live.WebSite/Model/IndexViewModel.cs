namespace MoriAlberto.Live.WebSite.Model;

public class IndexViewModel
{
    public StreamingListItem? NextStreaming { get; set; }

    public IEnumerable<StreamingListItem> Streamings { get; set; } = [];
}
