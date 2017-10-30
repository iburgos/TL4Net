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

        public static string FormatFileName(string type)
        {
            const string CLASS_PREFIX = "TL";

            if (String.IsNullOrEmpty(type))
                throw new ArgumentException("ARGH!");

            string domain = GetDomainFromType(type);
            Console.WriteLine($"type => {type} - - - domain => {domain}");
            return CLASS_PREFIX + Capitalize(domain) + Capitalize(type);
        }

        public static string GetNameofClass(string type)
        {
            bool containsDot = ContainsDot(type);
            bool containsQuestionmark = ContainsQuestionmark(type);

            string formatedName;
            if (containsDot && containsQuestionmark == false)
                formatedName = FormatFileName(type.Split(Constants.DotChar)[1]);
            else if (containsDot)
                formatedName = FormatFileName(type.Split(Constants.QuestionmarkChar)[1]);
            else
                formatedName = FormatFileName(type);

            return formatedName;
        }

        public static string GetDomainFromType(string type)
        {
            string[] typeArray = type.Split(Constants.DotChar);
            return typeArray.Length == 1 ? string.Empty : typeArray[0];
        }

        public static string Capitalize(string word)
        {
            return 
                word == string.Empty ? string.Empty : 
                word.First().ToString().ToUpper() + word.Substring(1);
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