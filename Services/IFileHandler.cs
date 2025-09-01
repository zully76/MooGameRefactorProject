using System.Collections.Generic;

namespace MooGameRefactorProject.Services
{
    public interface IFileHandler
    {
        bool Exists(string path);
        string[] ReadAllLines(string path);
        void WriteAllLines(string path, IEnumerable<string> lines);
        void AppendLine(string path, string line);
    }
}
