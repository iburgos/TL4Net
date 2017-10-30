using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace Telegram4Net.SchemaTools
{
    public interface ISchemaRetriever
    {
        Task<string> Retrieve();
    }

    public class SchemaRetriever : ISchemaRetriever
    {
        private readonly IRestClient _restClient;

        public SchemaRetriever(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> Retrieve()
        {
            string result = string.Empty;

            _restClient.BaseUrl = new Uri("https://core.telegram.org/schema/json");

            IRestRequest restRequest = new RestRequest(Method.GET);

            IRestResponse restResponse = await _restClient.ExecuteGetTaskAsync(restRequest);

            if (restResponse.ResponseStatus == ResponseStatus.Completed)
            {
                result = restResponse.Content;
            }

            return result;
        }
    }
}