using MoriAlberto.Live.WebSite.Model;

namespace MoriAlberto.Live.WebSite.Services;

public class StreamingsService
{
    public HttpClient Client { get; }

    public StreamingsService(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<IndexViewModel> GetStreamingsForHomePageAsync()
    {
        var model = new IndexViewModel();
        model.Streamings = Enumerable.Range(0, 6).Select(i => new IndexViewModel.ScheduledStreaming
        {
            Title = $"Test {i}",
            ScheduleDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(18, 0),
            Slug = $"test-{i}"
        });

        return Task.FromResult(model);
    }
}
