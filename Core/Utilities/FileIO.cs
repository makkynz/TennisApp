using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities
{
    public class FileIO
    {
        public static void Save( string path, string value)
        {
            File.WriteAllText(path, value);
        }

        public static string Read(string path)
        {
            return !File.Exists(path) ? null : File.ReadAllText(path);
        }
    }
}
