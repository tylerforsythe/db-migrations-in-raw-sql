using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace DB_Migrations_in_Raw_SQL
{
    public static class Resources
    {
        public static string ReadResource(string resourceName) {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            StreamReader textStream = new StreamReader(_assembly.GetManifestResourceStream("DB_Migrations_in_Raw_SQL.Scripts." + resourceName));//"MyNamespace.MyImage.bmp"
            return textStream.ReadToEnd();
        }
    }
}
