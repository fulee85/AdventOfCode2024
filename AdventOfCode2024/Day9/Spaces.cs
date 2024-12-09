using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day9;

internal class Spaces : IEnumerable<int>
{
    private List<Space> spaces = new List<Space>();

    public Spaces(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            spaces.Add(new Space(i, numbers[i]));
        }
    }

    public IEnumerator<int> GetEnumerator() => EnumerateFileIds().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private IEnumerable<int> EnumerateFileIds()
    {
        int actualSpaceId = 0;
        int lastFileSpaceId = spaces.Count - 1;
        bool allEnumerated = false;
        while (!allEnumerated)
        {
            if (spaces[actualSpaceId].IsEnumerated)
            {
                break;
            }

            while (spaces[actualSpaceId].GetNext(out var fileId))
            {
                yield return fileId;
            }
            actualSpaceId++;

            for (int i = 0; i < spaces[actualSpaceId].Length; i++)
            {
                if (spaces[lastFileSpaceId].GetNext(out var fileId))
                {
                    yield return fileId;
                }
                else
                {
                    lastFileSpaceId = lastFileSpaceId - 2;
                    if (spaces[lastFileSpaceId].GetNext(out fileId))
                    {
                        yield return fileId;
                    }
                    else
                    {
                        allEnumerated = true;
                        break;
                    }
                }
            }
            actualSpaceId++;
        }
    }
}
