using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactorProject.Services
{
    public interface IFileHandler

    {
        bool Exists(string Path);
        string[] ReadAllLines(string path);
        void WriteAllLines(string Path, IEnumerable<string> lines);
        void AppendLine(string path, string line);

    }
}
