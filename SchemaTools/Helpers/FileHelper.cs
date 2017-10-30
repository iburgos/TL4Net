using System.IO;
using System.Reflection;

namespace Telegram4Net.SchemaTools.Helpers
{
    public class FileHelper
    {
        public static FileStream CreateFile(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (File.Exists(path))
                File.Delete(path);

            return File.OpenWrite(path);
        }

        public static string GetFolderName(string type)
        {
            const string DOMAIN_FOLDER = "Domain\\TL";
            string domain = NameHelper.GetDomainFromType(type);
            return domain == string.Empty ? $"{RootFolder}\\{DOMAIN_FOLDER}" : $"{RootFolder}\\{DOMAIN_FOLDER}\\{NameHelper.Capitalize(domain)}";
        }

        public static string RootFolder =>
            GetParent(Assembly.GetExecutingAssembly().Location, Constants.SolutionFolderName);

        private static string GetParent(string path, string parentName)
        {
            var dir = new DirectoryInfo(path);

            if (dir.Parent == null)
                return null;

            if (dir.Parent.Name == parentName)
                return dir.Parent.FullName;

            return GetParent(dir.Parent.FullName, parentName);
        }
    }
}