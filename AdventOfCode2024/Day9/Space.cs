using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day9;

internal class Space
{
    private int index = 0;
    public Space(int index, int length)
    {
        if (index % 2 == 0)
        {
            IsFile = true;
            FileId = index / 2;
        }
        else
        {
            IsFile = false;
        }
        Length = length;
    }

    public int FileId { get; }

    public int Length { get; }

    public bool IsFile { get; set; }

    public bool GetNext(out int next)
    {
        if (index < Length)
        {
            next = FileId;
            index++;
            return true;
        }
        else
        {
            next = -1;
            return false;
        }
    }

    public bool IsEnumerated => index == Length;
}
