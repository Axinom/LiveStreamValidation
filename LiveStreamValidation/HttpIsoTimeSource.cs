using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Axinom.LiveStreamValidation
{
    internal sealed class HttpIsoTimeSource : ITimeSource
    {
        public string Name => "http-iso";
        public bool IsStaticValue => false;

        public HttpIsoTimeSource(Uri url)
        {
            _url = url;
        }

        private readonly Uri _url;

        public async Task<DateTimeOffset> GetTimeAsync()
        {
            using (var client = new HttpClient
            {
                Timeout = Constants.HttpRequestTimeout
            })
            {
                var value = await client.GetStringAsync(_url);
                return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
            }
        }
    }
}
