using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                    string nameSpace = NameHelper.GetNameSpace(constructor.Type);
                    string className = NameHelper.GetNameofClass(constructor.Type, true);
                    var dir = RootFolder + $"\\Domain\\TL\\{className}.cs";

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

        public static string RootFolder
        {
            get
            {
                return GetParent(Assembly.GetExecutingAssembly().Location, "TL4Net");
            }
        }

        public static string GetParent(string path, string parentName)
        {
            var dir = new DirectoryInfo(path);

            if (dir.Parent == null)
            {
                return null;
            }

            if (dir.Parent.Name == parentName)
            {
                return dir.Parent.FullName;
            }

            return GetParent(dir.Parent.FullName, parentName);
        }
    }
}