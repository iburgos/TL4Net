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
        public async Task<string> Retrieve()
        {
            string result = string.Empty;

            IRestClient restClient = new RestClient("https://core.telegram.org/schema/json");

            IRestRequest restRequest = new RestRequest(Method.GET);

            IRestResponse restResponse = await restClient.ExecuteGetTaskAsync(restRequest);

            if (restResponse.ResponseStatus == ResponseStatus.Completed)
            {
                result = restResponse.Content;
            }

            return result;
        }
    }
}