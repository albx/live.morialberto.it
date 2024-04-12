namespace MoriAlberto.Live.WebSite.Model;

public class ArchiveViewModel
{
    public IEnumerable<StreamingListItem> Streamings { get; set; } = [];

    public bool HasNextPage { get; set; }
}
