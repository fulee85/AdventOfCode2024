using System.Collections;

namespace AdventOfCode2024.Day9;

internal class SpacesWithoutFragmentation : IEnumerable<int>
{
    private List<Space> spaces = new List<Space>();

    public SpacesWithoutFragmentation(List<int> numbers)
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
        while (actualSpaceId < spaces.Count)
        {
            if (spaces[actualSpaceId].IsFile)
            {
                while (spaces[actualSpaceId].GetNext(out var fileId))
                {
                    yield return fileId;
                }
                actualSpaceId++;
            }
            else
            {
                var emptySpaceLength = spaces[actualSpaceId].Length;
                while (emptySpaceLength > 0)
                {
                    while (lastFileSpaceId >= 0 && (spaces[lastFileSpaceId].IsEnumerated || emptySpaceLength < spaces[lastFileSpaceId].Length))
                    {
                        lastFileSpaceId -= 2;
                    }

                    if (lastFileSpaceId < 0)
                    {
                        for (int i = 0; i < emptySpaceLength; i++)
                        {
                            emptySpaceLength--;
                            yield return 0;
                        }
                    }
                    else
                    {
                        while (spaces[lastFileSpaceId].GetNext(out var fileId))
                        {
                            emptySpaceLength--;
                            yield return fileId;
                        }
                        spaces[lastFileSpaceId].IsFile = false;
                    }
                }
                actualSpaceId++;
                lastFileSpaceId = spaces.Count - 1;
            }
        }
    }
}