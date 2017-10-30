using System;
using System.Linq;

namespace Telegram4Net.SchemaTools.Helpers
{
    public class NameHelper
    {
        public static string GetNameSpace(string type)
        {
            if (ContainsDot(type))
                return $"{Constants.FullDomainNameFolder}.{FormatName(GetDomainFromType(type))}";

            return Constants.FullDomainNameFolder;
        }

        public static string FormatName(string domain)
        {
            if (String.IsNullOrEmpty(domain))
                throw new ArgumentException("ARGH!");

            if (ContainsDot(domain))
            {
                domain = domain.Replace(Constants.DotString, Constants.EmptyString);
                string temp = string.Empty;
                foreach (string s in domain.Split(Constants.EmptyChar))
                {
                    temp += FormatName(s) + Constants.EmptyString;
                }
                domain = temp.Trim();
            }

            return Capitalize(domain);
        }

        public static string GetNameofClass(string type, bool isInterface = false, bool isMethod = false)
        {
            bool containsDot = ContainsDot(type);
            bool containsQuestionmark = ContainsQuestionmark(type);

            string formatedName;
            if (containsDot && containsQuestionmark == false)
                formatedName = FormatName(type.Split(Constants.DotChar)[1]);
            else if (containsDot)
                formatedName = FormatName(type.Split(Constants.QuestionmarkChar)[1]);
            else
                formatedName = FormatName(type);

            return isInterface ? $"I{formatedName}" : formatedName;
        }

        private static bool ContainsDot(string type)
        {
            return type.IndexOf(Constants.DotChar) != -1;
        }

        private static bool ContainsQuestionmark(string type)
        {
            return type.IndexOf(Constants.QuestionmarkChar) != -1;
        }

        private static string GetDomainFromType(string type)
        {
            return type.Split(Constants.DotChar)[0];
        }

        private static string Capitalize(string word)
        {
            return word.First().ToString().ToUpper() + word.Substring(1);
        }
    }
}