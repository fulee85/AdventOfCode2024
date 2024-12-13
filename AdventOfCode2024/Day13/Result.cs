namespace AdventOfCode2024.Day13;

public class Result
{
    public Result(long A, long B, bool Success = true)
    {
        this.A = A;
        this.B = B;
        this.Success = Success;
    }

    public static Result FailedResult = new Result(0, 0, false);

    public long A { get; }
    public long B { get; }
    public bool Success { get; }
}
