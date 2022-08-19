using RestSharp;
using System.Text.Json;

namespace SpecFlowAPI
{
    public class ServiceBase
    {
        private readonly RestClient _client;
        private readonly AccessTokenProvider? _accessTokenProvider;
        private readonly string _baseUrl;

        public ServiceBase(string baseUrl, AccessTokenProvider? accessTokenProvider)
        {
            _baseUrl = baseUrl;
            _accessTokenProvider = accessTokenProvider;
            _client = new(_baseUrl)
            {
                Authenticator = _accessTokenProvider
            };
        }

        public IRestResponse ExecuteGet(string methoPath)
        {
            var request = new RestRequest(methoPath, DataFormat.Json);

            return _client.Execute(request, Method.GET);
        }

        public IRestResponse ExecutePost(string methodPath, dynamic body)
        {
            var request = new RestRequest(methodPath, DataFormat.Json);
            var jsonBody = body is not string ? JsonSerializer.Serialize(body) : body;

            request.AddJsonBody(jsonBody);

            return _client.Execute(request, Method.POST);
        }

        public IRestResponse ExecutePut(string methodPath, dynamic body)
        {
            var request = new RestRequest(methodPath, DataFormat.Json);
            var jsonBody = body is not string ? JsonSerializer.Serialize(body) : body;

            request.AddJsonBody(jsonBody);

            return _client.Execute(request, Method.PUT);
        }

        public IRestResponse ExecuteDelete(string methoPath)
        {
            var request = new RestRequest(methoPath, DataFormat.Json);

            return _client.Execute(request, Method.DELETE);
        }

    }
}
