using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17;

public class IntervalFinder
{
    private readonly int repetitionLength;
    private readonly List<int> valueOccurrencesInRepetition;
    private readonly BigInteger firstIntervalStart;
    private readonly BigInteger intervalsLength;

    public IntervalFinder(int index, int value, List<int>[] repetitionDictionary, int repetitionLength)
    {
        this.repetitionLength = repetitionLength;
        valueOccurrencesInRepetition = repetitionDictionary[value];
        firstIntervalStart = BigInteger.Pow(8, index);
        intervalsLength = firstIntervalStart;
    }

    private ClosedIntervall? previousIntervall;

    public ClosedIntervall GetClosestInterval(BigInteger bigInteger)
    {
        if (previousIntervall != null && previousIntervall.Contains(bigInteger))
        {
            return previousIntervall;
        }

        if (bigInteger < firstIntervalStart)
        {
            BigInteger intervalStart = valueOccurrencesInRepetition[0] * intervalsLength;
            previousIntervall = new ClosedIntervall(intervalStart, intervalsLength);
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
                    indexInRepetition = valueOccurrencesInRepetition[0];
                }
                else
                {
                    indexInRepetition = indexes[0];
                }
            }

            previousIntervall = new ClosedIntervall((indexInRepetition + (repetitionLength * shift)) * intervalsLength, intervalsLength);
        }

        return previousIntervall;
    }
}
