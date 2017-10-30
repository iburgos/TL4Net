using System;
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
        private string AbsTemplate => File.ReadAllText("../Templates/ConstructorAbs.tmp");

        public void Process(List<Constructor> constructorList)
        {
            foreach (Constructor constructor in constructorList)
            {
                List<Constructor> sameTypeConstructors = GetElementsByType(constructorList, constructor.Type);
                if (sameTypeConstructors.Count > 1)
                {
                    string path =
                    (NameHelper.GetNameSpace(constructor.Type).Replace("TeleSharp.TL", "TL\\").Replace(".", "") + "\\" +
                     NameHelper.GetNameofClass(constructor.Type, true) + ".cs").Replace("\\\\", "\\");

                    FileStream classFile = FileHelpers.CreateFile(path);

                    using (StreamWriter writer = new StreamWriter(classFile))
                    {
                        string nspace = (NameHelper.GetNameSpace(constructor.Type).Replace("TeleSharp.TL", "TL\\").Replace(".", "")).Replace("\\\\", "\\").Replace("\\", ".");
                        if (nspace.EndsWith("."))
                        {
                            nspace = nspace.Remove(nspace.Length - 1, 1);
                        }
                        
                        string temp = AbsTemplate.Replace(Constants.NamespaceSection, "TeleSharp." + nspace);
                        temp = temp.Replace(Constants.NameSection, NameHelper.GetNameofClass(constructor.Type, true));
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