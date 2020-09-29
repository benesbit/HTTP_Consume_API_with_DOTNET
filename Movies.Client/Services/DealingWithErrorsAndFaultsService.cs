using Marvin.StreamExtensions;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class DealingWithErrorsAndFaultsService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public DealingWithErrorsAndFaultsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            await GetMovieAndDealWithInvalidResponses(_cancellationTokenSource.Token);
        }

        private async Task GetMovieAndDealWithInvalidResponses(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/movies/030a43b0-f9a5-405a-811c-bf342524b2be");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var resposne = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                resposne.EnsureSuccessStatusCode();
                var stream = await resposne.Content.ReadAsStreamAsync();

                var movie = stream.ReadAndDeserializeFromJson<Movie>();
            }
        }
    }
}
