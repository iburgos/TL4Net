using System.IO;

namespace Telegram4Net.SchemaTools.Helpers
{
    internal class FileHelpers
    {
        public static FileStream CreateFile(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (File.Exists(path))
                File.Delete(path);

            return File.OpenWrite(path);
        }
    }
}