using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Axinom.LiveStreamValidation
{
    sealed class HttpHeadTimeSource : ITimeSource
    {
        public string Name => "http-head";
        public bool IsStaticValue => false;

        public HttpHeadTimeSource(Uri url)
        {
            _url = url;
        }

        private Uri _url;

        public async Task<DateTimeOffset> GetTimeAsync()
        {
            using (var client = new HttpClient
            {
                Timeout = Constants.HttpRequestTimeout
            })
            {
                var request = new HttpRequestMessage(HttpMethod.Head, _url);
                var response = await client.SendAsync(request);

                return response.Headers.Date.Value;
            }
        }
    }
}
