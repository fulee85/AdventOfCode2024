namespace AdventOfCode2024.Day7;
internal class Equation
{
    private readonly long testValue;
    private readonly long[] numbers;
    private Func<long, long, long>[] operations;

    public Equation(string input, Func<long, long, long>[] operations)
    {
        var parts = input.Split(':');
        testValue = long.Parse(parts[0].Trim());
        numbers = parts[1].Trim().Split().Select(long.Parse).ToArray();
        this.operations = operations;
    }

    public bool CanBeSolved()
    {
        Queue<Vertex> queue = new Queue<Vertex>();
        queue.Enqueue(new Vertex(numbers[0], 1));
        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            if (vertex.Level == numbers.Length)
            {
                if (vertex.Value == testValue)
                {
                    return true;
                }
            }
            else
            {
                foreach (var operation in operations)
                {
                    var nextValue = operation(vertex.Value, numbers[vertex.Level]);
                    if (nextValue <= testValue)
                    {
                        queue.Enqueue(new Vertex(nextValue, vertex.Level + 1));
                    }
                }
            }
        }
        return false;
    }

    public long TestValue => testValue;
}
