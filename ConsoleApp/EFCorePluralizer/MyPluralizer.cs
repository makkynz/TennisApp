using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp.EFCoreTools
{
    public class MyPluralizer : IPluralizer
    {
        public string Pluralize(string name)
        {
            return Inflector.Pluralize(name) ?? name;
        }

        public string Singularize(string name)
        {
            return Inflector.Singularize(name) ?? name;
        }
    }
}
