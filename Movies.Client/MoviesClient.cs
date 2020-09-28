using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Movies.Client
{
    public class MoviesClient
    {
        public HttpClient Client { get; }
    }

    public MoviesClient(HttpClient client)
    {
        Client = client;
    }
}
