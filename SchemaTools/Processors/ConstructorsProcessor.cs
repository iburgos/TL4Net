using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telegram4Net.SchemaTools.Helpers;
using Telegram4Net.SchemaTools.Models;

namespace Telegram4Net.SchemaTools.Processors
{
    public interface IConstructorsProcessor
    {
        void Process(List<Constructor> constructorList);
    }

    public class ConstructorsProcessor : IConstructorsProcessor
    {
        private string AbsTemplate => File.ReadAllText("../Debug/Templates/ConstructorAbs.tmp");

        public void Process(List<Constructor> constructorList)
        {
            foreach (Constructor constructor in constructorList)
            {
                List<Constructor> sameTypeConstructors = GetElementsByType(constructorList, constructor.Type);
                if (sameTypeConstructors.Count > 1)
                {
                    string nameSpace = NameHelper.GetNameSpace(constructor.Type);
                    string className = NameHelper.GetNameofClass(constructor.Type);
                    string directory = FileHelper.GetFolderName(constructor.Type);
                    string path = $"{directory}\\{className}.cs";

                    FileStream classFile = FileHelper.CreateFile(path);

                    using (StreamWriter writer = new StreamWriter(classFile))
                    {                      
                        string temp = AbsTemplate.Replace(Constants.NamespaceSection, nameSpace);
                        temp = temp.Replace(Constants.NameSection, NameHelper.GetNameofClass(constructor.Type));
                        writer.Write(temp);
                        writer.Close();
                        classFile.Close();
                    }
                }
                else
                {
                    //interfacesList.Remove(list.First().Type);
                    //list.First().Type = "himself";
                }
            }
        }

        private List<Constructor> GetElementsByType(IEnumerable<Constructor> constructorList, string type)
        {
            return constructorList.Where(x => x.Type == type).ToList();
        }

        
    }
}