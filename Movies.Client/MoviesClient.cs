using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Movies.Client
{
    public class MoviesClient
    {
        public HttpClient Client { get; }

        public MoviesClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("http://localhost:57863");
            Client.Timeout = new TimeSpan(0, 0, 30);
            Client.DefaultRequestHeaders.Clear();
        }
    }
}
