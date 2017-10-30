using RestSharp;
using SimpleInjector;
using Telegram4Net.SchemaTools.Helpers;

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
        }
    }
}