using System;
using System.Collections.Generic;
using System.IO;

namespace MooGameRefactorProject.Services
{
    public class FileHandler : IFileHandler
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string[] ReadAllLines(string path)
        {
            if (!Exists(path))
            {
                return Array.Empty<string>();
            }
            return File.ReadAllLines(path);
        }

        public void WriteAllLines(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        public void AppendLine(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}
