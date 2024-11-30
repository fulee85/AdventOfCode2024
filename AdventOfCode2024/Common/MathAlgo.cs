namespace AdventOfCode2024.Common;

public static class MathAlgo
{
    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Function to find the least common multiple (LCM) of two numbers
    public static long LCM(long a, long b)
    {
        return (a * b) / GCD(a, b);
    }

    // Function to find the LCM of a list of numbers
    public static long FindLCM(List<long> numbers)
    {
        if (numbers.Count < 2)
        {
            throw new ArgumentException("At least two numbers are required to find the LCM.");
        }

        long lcm = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }

        return lcm;
    }
}
