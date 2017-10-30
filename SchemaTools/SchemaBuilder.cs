using System.Threading.Tasks;
using Telegram4Net.SchemaTools.Helpers;
using Telegram4Net.SchemaTools.Models;

namespace Telegram4Net.SchemaTools
{
    public interface ISchemaBuilder
    {
        Task<bool> Build();
    }

    public class SchemaBuilder : ISchemaBuilder
    {
        private readonly ISchemaRetriever _schemaRetriever;
        private readonly IJsonConverterHelper _jsonConverterHelper;

        public SchemaBuilder(ISchemaRetriever schemaRetriever, IJsonConverterHelper jsonConverterHelper)
        {
            _schemaRetriever = schemaRetriever;
            _jsonConverterHelper = jsonConverterHelper;
        }

        public async Task<bool> Build()
        {
            string schemaJson = await _schemaRetriever.Retrieve();

            var schema = _jsonConverterHelper.Deserialize<Schema>(schemaJson);



            return true;
        }
    }
}