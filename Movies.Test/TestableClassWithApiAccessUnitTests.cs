using System;
using System.Net.Http;
using Xunit;
using Movies.Client;
using System.Threading;

namespace Movies.Test
{
    public class TestableClassWithApiAccessUnitTests
    {
        [Fact]
        public void GetMovie_On401Response_MustThrowUnauthorizedApiAccessException()
        {
            var httpClient = new HttpClient(new Return401UnauthorizedResponseHandler());
            var testableClass = new TestableClassWithApiAccess(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            Assert.ThrowsAsync<UnauthorizedApiAccessException>(
                () => testableClass.GetMovie(cancellationTokenSource.Token));
        }
    }
}
