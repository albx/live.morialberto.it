using MoriAlberto.Live.Models;
using MoriAlberto.Live.WebSite.Model;
using System.Net.Http.Json;

namespace MoriAlberto.Live.WebSite.Services;

public class StreamingsService
{
    public HttpClient Client { get; }

    public StreamingsService(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IndexViewModel> GetStreamingsForHomePageAsync()
    {
        var model = new IndexViewModel();

        var streamings = await Client.GetFromJsonAsync<IEnumerable<StreamingList.StreamingListItem>>("api/streamings/scheduled");
        model.Streamings = streamings ?? Array.Empty<StreamingList.StreamingListItem>();

        return model;
    }

    public async Task<ArchiveViewModel> GetStreamingsArchiveAsync(StreamingsSearchParameters? search = null)
    {
        var url = "api/streamings";
        if (search is not null)
        {
            url = $"{url}?sort={search.Sort}&p={search.Page}";
            if (!string.IsNullOrWhiteSpace(search.Query))
            {
                url = $"{url}&q={System.Web.HttpUtility.UrlEncode(search.Query)}";
            }
        }

        var streamingList = await Client.GetFromJsonAsync<StreamingList>(url);
        var model = new ArchiveViewModel
        {
            Streamings = streamingList?.Streamings ?? Array.Empty<StreamingList.StreamingListItem>(),
            NumberOfPages = streamingList?.NumberOfPages ?? 0
        };

        return model;
    }
}
