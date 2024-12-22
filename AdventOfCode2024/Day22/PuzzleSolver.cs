using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day22;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        List<long> secretNumbers = input.Select(long.Parse).ToList();
        for (int i = 0; i < 2000; i++)
        {
            for (int j = 0; j < secretNumbers.Count; j++)
            {
                secretNumbers[j] = EvolveSecretNumber(secretNumbers[j]);
            }
        }

        return secretNumbers.Sum().ToString();
    }
    public override string GetSecondSolution()
    {
        List<long> secretNumbers = input.Select(long.Parse).ToList();
        List<List<int>> offeredBananasOfMonkeys = new List<List<int>>();
        for (int j = 0; j < secretNumbers.Count; j++)
        {
            var monkeyOffers = new List<int>() { (int)secretNumbers[j] % 10 };
            for (int i = 0; i < 2000; i++)
            {
                secretNumbers[j] = EvolveSecretNumber(secretNumbers[j]);
                monkeyOffers.Add((int)secretNumbers[j] % 10);
            }
            offeredBananasOfMonkeys.Add(monkeyOffers);
        }

        List<List<int>> changesOfOfferedBananasByMonkeys = new();
        List<Dictionary<SequenceOfChanges, int>> monkeysSequencesOfChanges = new();
        for (int i = 0; i < offeredBananasOfMonkeys.Count; i++)
        {
            var monkeyOfferChanges = new List<int>();
            var sequencesOfChanges = new Dictionary<SequenceOfChanges,int>();
            for (int j = 0; j < offeredBananasOfMonkeys[0].Count - 1; j++)
            {
                monkeyOfferChanges.Add(offeredBananasOfMonkeys[i][j + 1] - offeredBananasOfMonkeys[i][j]);
                if (monkeyOfferChanges.Count >= 4)
                {
                    var sequence = new SequenceOfChanges(monkeyOfferChanges[^4], monkeyOfferChanges[^3], monkeyOfferChanges[^2], monkeyOfferChanges[^1]);
                    if (!sequencesOfChanges.ContainsKey(sequence))
                    {
                        sequencesOfChanges[sequence] = offeredBananasOfMonkeys[i][j + 1];
                    }
                }
            }
            changesOfOfferedBananasByMonkeys.Add(monkeyOfferChanges);
            monkeysSequencesOfChanges.Add(sequencesOfChanges);
        }

        HashSet<SequenceOfChanges> AllSequenceOfChanges = new HashSet<SequenceOfChanges>();
        foreach (var monkeySequencesOfChange in monkeysSequencesOfChanges)
        {
            AllSequenceOfChanges.UnionWith(monkeySequencesOfChange.Keys);
        }

        int maxBanana = int.MinValue;
        SequenceOfChanges? maxSequence = null;
        foreach (var sequence in AllSequenceOfChanges)
        {
            var sumBanana = 0;
            for (int i = 0; i < monkeysSequencesOfChanges.Count; i++)
            {
                sumBanana += monkeysSequencesOfChanges[i].TryGetValue(sequence, out var value) ? value : 0;
            }

            if (sumBanana > maxBanana)
            {
                maxBanana = sumBanana;
                maxSequence = sequence;
            }
        }


        return maxBanana.ToString();
    }

    public long EvolveSecretNumber(long secretNumber)
    {
        var step1Result = Prune(Mix(secretNumber, secretNumber * 64));
        var step2Result = Prune(Mix(step1Result, step1Result / 32));
        var step3Result = Prune(Mix(step2Result, step2Result * 2048));

        return step3Result;
    }

    public static long Mix(long secret, long givenValue) => secret ^ givenValue;

    public static long Prune(long secret) => secret % 16777216;
}
