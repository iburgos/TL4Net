using System;
using System.Threading.Tasks;
using SimpleInjector;
using Telegram4Net.SchemaTools.Bootstrappers;

namespace Telegram4Net.SchemaTools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new Container();

            Bootstrapper.Initialize(container);

            var startClass = container.GetInstance<ISchemaRetriever>();

            var myTask = Task.Run(() => startClass.Retrieve());
            var myresult = myTask.Result;
            Console.Write(myresult);
            Console.ReadKey();
        }
    }
}