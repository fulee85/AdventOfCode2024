using System.Numerics;

namespace AdventOfCode2024.Day17;

public class ClosedIntervall(BigInteger from, BigInteger length)
{
    public BigInteger From { get; } = from;

    public BigInteger To { get; } = from + length - 1;

    public bool Contains(BigInteger bigInteger) => From <= bigInteger && bigInteger <= To;

    public override string ToString() => $"{From} - {To}";
}
