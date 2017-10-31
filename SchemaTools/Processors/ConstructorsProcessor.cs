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
        private string Template => File.ReadAllText($"{FileHelper.AssemblyFolder}/Templates/Constructor.tmp");

        public void Process(List<Constructor> constructorList)
        {
            //int processedCounter = 0;

            foreach (var constructor in constructorList)
            {
                string nameSpace = NameHelper.GetNameSpace(constructor.Type);
                string className = NameHelper.GetClassName(constructor.Type);
                string directory = FileHelper.GetFolderName(constructor.Type);
                string path = $"{directory}\\{className}{Constants.CSharpFileExtension}";

                FileStream classFile = FileHelper.CreateFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    string template = Template;

                    template = ReplaceAboutClass(template, constructor, nameSpace, className);
                    template = ReplaceParams(constructor, template);

                    #region ComputeFlagFunc
                    if (constructor.Params.All(x => x.Name != "flags")) template = template.Replace(Constants.ComputeSection, string.Empty);
                    else
                    {
                        var compute = "flags = 0;" + Environment.NewLine;
                        //foreach (var param in constructor.Params.Where(x => IsFlagBase(x.Type)))
                        //{
                        //    if (IsTrueFlag(param.Type))
                        //    {
                        //        compute += $"flags = {CheckForKeyword(param.Name)} ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                        //    }
                        //    else
                        //    {
                        //        compute += $"flags = {CheckForKeyword(param.Name)} != null ? (flags | {GetBitMask(param.Type)}) : (flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                        //    }
                        //}
                        template = template.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    var serialize = "";

                    if (constructor.Params.Any(x => x.Name == "flags")) serialize += "ComputeFlags();" + Environment.NewLine + "bw.Write(flags);" + Environment.NewLine;
                    foreach (var p in constructor.Params.Where(x => x.Name != "flags"))
                    {
                        //serialize += WriteWriteCode(p) + Environment.NewLine;
                    }
                    template = template.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    var deserialize = "";

                    foreach (var p in constructor.Params)
                    {
                        //deserialize += WriteReadCode(p) + Environment.NewLine;
                    }
                    template = template.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    writer.Write(template);
                    writer.Close();
                    classFile.Close();
                }
            }


            //Console.WriteLine($"Total => {constructorList.Count} - Processed => {processedCounter}");
        }

        private static string ReplaceParams(Constructor constructor, string template)
        {
            string fields = "";
            foreach (var tmp in constructor.Params)
            {
                fields +=
                    $"public {NameHelper.CheckForFlagBase(tmp.Type, NameHelper.GetTypeName(tmp.Type))} {KeywordChecker.Check(tmp.Name)} " +
                    "{get;set;}" +
                    Environment.NewLine;
            }
            template = template.Replace(Constants.ParamsSection, fields);
            return template;
        }

        private static string ReplaceAboutClass(string template, Constructor constructor, string nameSpace, string className)
        {
            template = template.Replace(Constants.NamespaceSection, nameSpace);
            template = template.Replace(Constants.ConstructorSection, constructor.Id.ToString());
            template = template.Replace(Constants.NameSection, className);
            return template;
        }

        private List<Constructor> GetElementsByType(IEnumerable<Constructor> constructorList, string type)
        {
            return constructorList.Where(x => x.Type == type).ToList();
        }
    }
}