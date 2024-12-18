using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17;

public class IntervalFinder
{
    private readonly int index;
    private readonly int repetitionLength;
    private readonly List<int> valueOccurrencesInRepetition;
    private readonly BigInteger firstIntervalStart;
    private readonly BigInteger intervalsLength;

    public IntervalFinder(int index, int value, List<int>[] repetitionDictionary, int repetitionLength)
    {
        if (value > 7) throw new Exception();
        this.index = index;
        this.repetitionLength = repetitionLength;
        valueOccurrencesInRepetition = repetitionDictionary[value];
        firstIntervalStart = BigInteger.Pow(8, index);
        intervalsLength = firstIntervalStart;
    }

    public ClosedIntervall GetClosestInterval(BigInteger bigInteger)
    {
        if (bigInteger < firstIntervalStart)
        {
            BigInteger intervalStart = valueOccurrencesInRepetition.First() * intervalsLength;
            return new ClosedIntervall(intervalStart, intervalsLength);
        }
        else
        {
            BigInteger index = (bigInteger / intervalsLength);
            BigInteger shift = index / repetitionLength;
            BigInteger indexInRepetition = index % repetitionLength;
            if (!valueOccurrencesInRepetition.Contains((int)indexInRepetition))
            {
                var indexes = valueOccurrencesInRepetition.SkipWhile(i => i < (int)indexInRepetition).ToList();
                if (indexes.Count == 0)
                {
                    shift++;
                    indexInRepetition = valueOccurrencesInRepetition.First();
                }
                else
                {
                    indexInRepetition = indexes.First();
                }
            }

            return new ClosedIntervall((indexInRepetition + (repetitionLength * shift)) * intervalsLength, intervalsLength);
        }
    }
}
