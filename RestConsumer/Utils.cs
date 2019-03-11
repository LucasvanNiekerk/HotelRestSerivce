using System.Net.Http;

namespace RestConsumer
{
    class Utils
    {
        private static HttpClient _client;

        public static HttpClient Client()
        {
            return _client == null ? new HttpClient() : _client;
        }
    }
}
