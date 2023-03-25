using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MoriAlberto.Live.Api.Services;
using MoriAlberto.Live.Models;
using System.Net;

namespace MoriAlberto.Live.Api
{
    public class StreamingsFunction
    {
        private readonly ILogger _logger;

        public StreamingsService Service { get; }

        public StreamingsFunction(ILoggerFactory loggerFactory, StreamingsService service)
        {
            _logger = loggerFactory.CreateLogger<StreamingsFunction>();
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Function(nameof(GetScheduledStreamings))]
        public async Task<HttpResponseData> GetScheduledStreamings(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "streamings/scheduled")] HttpRequestData request)
        {
            var scheduledStreamings = await Service.GetScheduledStreamingsAsync();

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(scheduledStreamings);

            return response;
        }

        [Function(nameof(GetStreamings))]
        public async Task<HttpResponseData> GetStreamings(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "streamings")] HttpRequestData request)
        {
            var search = ParseQueryString(request);
            var streamings = await Service.GetAllStreamingsAsync(search);

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(streamings);

            return response;
        }

        private StreamingsSearchParameters ParseQueryString(HttpRequestData request)
        {
            if (string.IsNullOrWhiteSpace(request.Url.Query))
            {
                return new StreamingsSearchParameters();
            }

            var query = QueryHelpers.ParseQuery(request.Url.Query);
            var search = new StreamingsSearchParameters();
            if (query.ContainsKey("q") && !string.IsNullOrWhiteSpace(query["q"]))
            {
                search.Query = query["q"].ToString() ?? string.Empty;
            }
            if (query.ContainsKey("sort") && Enum.TryParse<StreamingsSearchParameters.SortDirection>(query["sort"], out var sort))
            {
                search.Sort = sort;
            }
            if (query.ContainsKey("p") && int.TryParse(query["p"], out var page))
            {
                search.Page = page;
            }

            return search;
        }

        [Function(nameof(GetStreamingDetail))]
        public async Task<HttpResponseData> GetStreamingDetail(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "streamings/{slug}")] HttpRequestData request,
            string slug)
        {
            var streaming = await Service.GetStreamingDetailAsync(slug);
            if (streaming is null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(streaming);

            return response;
        }
    }
}
