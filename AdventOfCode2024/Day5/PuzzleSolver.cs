using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day5;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly List<Rule> _rules = [];
    private readonly List<List<int>> _sections = [];
    private readonly PageComparer pageComparer;

    public PuzzleSolver(IInput input) : base(input)
    {
        var enumerator = input.GetEnumerator();
        while (enumerator.MoveNext() && enumerator.Current != string.Empty)
        {
            _rules.Add(new Rule(enumerator.Current));
        }

        while (enumerator.MoveNext())
        {
            _sections.Add(enumerator.Current.Split(',').Select(int.Parse).ToList());
        }

        pageComparer = new PageComparer(_rules);
    }

    public override string GetFirstSolution()
    {
        int middlePageNumberSum = 0;
        foreach (var section in _sections)
        {
            bool isInRightOrder = true;
            foreach (var rule in _rules)
            {
                isInRightOrder &= rule.IsRuleSatisfiedBySection(section);
                if (!isInRightOrder)
                {
                    break;
                }
            }

            if (isInRightOrder)
            {
                middlePageNumberSum += section[section.Count / 2];
            }
        }
        return middlePageNumberSum.ToString();
    }
    public override string GetSecondSolution()
    {
        int middlePageNumberSum = 0;
        foreach (var section in _sections)
        {
            bool isInRightOrder = true;
            foreach (var rule in _rules)
            {
                isInRightOrder &= rule.IsRuleSatisfiedBySection(section);
                if (!isInRightOrder)
                {
                    break;
                }
            }

            if (!isInRightOrder)
            {
                section.Sort(pageComparer);
                middlePageNumberSum += section[section.Count / 2];
            }
        }
        return middlePageNumberSum.ToString();
    }
}
