using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Common
{
    public static class LineParser
    {
        public static (string startNode, string leftNode, string rightNode) ParseDay8Input(string line)
        {
            string startNode = line[0..3];
            string leftNode = line[7..10];
            string rightNode = line[12..15];

            return (startNode, leftNode, rightNode);
        }
    }
}
