using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverTests.ExtensionMethods
{
    internal static class StringExtensionMethods
    {
        public static IEnumerable<string> GetLines(this string s)
        {
            return s.Split(Environment.NewLine);
        }
    }
}
