using RestSharp;
using SimpleInjector;
using Telegram4Net.SchemaTools.Helpers;
using Telegram4Net.SchemaTools.Processors;

namespace Telegram4Net.SchemaTools.Bootstrappers
{
    public class Bootstrapper
    {
        public static void Initialize(Container container)
        {
            container.Register<IJsonConverterHelper, JsonConverterHelper>();

            container.Register<IRestClient>(() => new RestClient());
            container.Register<ISchemaRetriever, SchemaRetriever>();
            container.Register<ISchemaBuilder, SchemaBuilder>();
            container.Register<IConstructorsProcessor, ConstructorsProcessor>();

            container.Verify();
        }
    }
}