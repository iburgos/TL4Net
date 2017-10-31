using System.Threading.Tasks;
using Telegram4Net.SchemaTools.Helpers;
using Telegram4Net.SchemaTools.Models;
using Telegram4Net.SchemaTools.Processors;

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
        private readonly IConstructorsProcessor _constructorProcessor;

        public SchemaBuilder(ISchemaRetriever schemaRetriever, 
            IJsonConverterHelper jsonConverterHelper,
            IConstructorsProcessor constructorProcessor)
        {
            _schemaRetriever = schemaRetriever;
            _jsonConverterHelper = jsonConverterHelper;
            _constructorProcessor = constructorProcessor;
        }

        public async Task<bool> Build()
        {
            string schemaJson = await _schemaRetriever.Retrieve();

            var schema = _jsonConverterHelper.Deserialize<Schema>(schemaJson);

            FileHelper.CleanFolder();
            _constructorProcessor.Process(schema.Constructors);

            return true;
        }
    }
}