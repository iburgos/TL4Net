using SimpleInjector;

namespace Telegram4Net.SchemaTools.Bootstrappers
{
    public class Bootstrapper
    {
        public static void Initialize(Container container)
        {
            container.Register<ISchemaRetriever, SchemaRetriever>();
        }
    }
}