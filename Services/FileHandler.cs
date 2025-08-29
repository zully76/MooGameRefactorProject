using System;
using System.IO;
using System.Collections.Generic;

namespace MooGameRefactorProject.Services
{
    // Implementación de IFileHandler
    public class FileHandler : IFileHandler
    {
        // Verifica si el archivo existe
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        // Lee todas las líneas del archivo
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        // Escribe todas las líneas en el archivo
        public void WriteAllLines(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        // Agrega una línea al archivo
        public void AppendLine(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}
