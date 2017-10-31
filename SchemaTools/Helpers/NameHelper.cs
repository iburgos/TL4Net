using System;
using System.Linq;

namespace Telegram4Net.SchemaTools.Helpers
{
    public class NameHelper
    {
        public static string GetNameSpace(string type)
        {
            if (ContainsDot(type))
                return $"{Constants.FullDomainNameFolder}.{FormatNamespaceName(GetDomainFromType(type))}";

            return Constants.FullDomainNameFolder;
        }

        public static string FormatNamespaceName(string domain)
        {
            if (String.IsNullOrEmpty(domain))
                throw new ArgumentException("ARGH!");

            if (ContainsDot(domain))
            {
                domain = domain.Replace(Constants.DotString, Constants.EmptyString);
                string temp = string.Empty;
                foreach (string s in domain.Split(Constants.EmptyChar))
                {
                    temp += FormatNamespaceName(s) + Constants.EmptyString;
                }
                domain = temp.Trim();
            }

            return Capitalize(domain);
        }

        public static string FormatFileName(string type, string domain)
        {
            if (String.IsNullOrEmpty(type))
                throw new ArgumentException("ARGH!");

            return Constants.DomainNameFolder + Capitalize(domain) + Capitalize(type);
        }

        public static string GetClassName(string type, bool isBaseClass = false)
        {
            bool containsDot = ContainsDot(type);
            bool containsQuestionmark = ContainsQuestionmark(type);

            string formatedName;
            if (containsDot && containsQuestionmark == false)
                formatedName = FormatFileName(type.Split(Constants.DotChar)[1], GetDomainFromType(type));
            else if (containsDot)
                formatedName = FormatFileName(type.Split(Constants.QuestionmarkChar)[1], GetDomainFromType(type));
            else
                formatedName = FormatFileName(type, GetDomainFromType(type));

            return isBaseClass ? $"{formatedName}Base" : formatedName;
        }

        public static string GetDomainFromType(string type)
        {
            Console.WriteLine($"Type => {type}");
            string[] typeArray = type.Split(Constants.DotChar);
            return typeArray.Length == 1 ? string.Empty : typeArray[0];
        }

        public static string Capitalize(string word)
        {
            return 
                word == string.Empty ? string.Empty : 
                word.First().ToString().ToUpper() + word.Substring(1);
        }

        public static string CheckForFlagBase(string type, string result)
        {
            if (!ContainsQuestionmark(type))
                return result;

            string innerType = type.Split(Constants.QuestionmarkChar)[1];

            if (innerType == Constants.TrueString)
                return result;

            if (new[] { "bool", "int", "uint", "long", "double" }.Contains(result))
                return result + Constants.QuestionmarkString;

            return result;
        }

        public static string GetTypeName(string type)
        {
            switch (type.ToLower())
            {
                case "#":
                case "int":
                    return "int";
                case "uint":
                    return "uint";
                case "long":
                    return "long";
                case "double":
                    return "double";
                case "string":
                    return "string";
                case "bytes":
                    return "byte[]";
                case "true":
                case "bool":
                    return "bool";
                case "!x":
                    return "TLObject";
                case "x":
                    return "TLObject";
            }

            if (type.StartsWith("Vector"))
                return "TLVector<" + GetTypeName(type.Replace("Vector<", "").Replace(">", "")) + ">";

            if (type.ToLower().Contains("inputcontact"))
                return "TLInputPhoneContact";


            //bool containsDot = ContainsDot(type);
            //bool containsQuestionmark = ContainsQuestionmark(type);

            //string formatedName;
            //if (containsDot && containsQuestionmark == false)
            //{

            //    if (interfacesList.Any(x => x.ToLower() == (type).ToLower()))
            //        formatedName = FormatFileName(type.Split('.')[0], GetDomainFromType(type)) + "." + "TLAbs" + type.Split('.')[1];
            //    if (classesList.Any(x => x.ToLower() == (type).ToLower()))
            //        return FormatName(type.Split('.')[0]) + "." + "TL" + type.Split('.')[1];
            //    return FormatName(type.Split('.')[1]);
            //}
            //if (containsQuestionmark == false)
            //{
            //    if (interfacesList.Any(x => x.ToLower() == type.ToLower()))
            //        return "TLAbs" + type;
            //    if (classesList.Any(x => x.ToLower() == type.ToLower()))
            //        return "TL" + type;
            //    return type;
            //}

            return GetTypeName(type.Split(Constants.QuestionmarkChar)[1]);
        }

        private static bool ContainsDot(string type)
        {
            return type.IndexOf(Constants.DotChar) != -1;
        }

        private static bool ContainsQuestionmark(string type)
        {
            return type.IndexOf(Constants.QuestionmarkChar) != -1;
        }
    }
}