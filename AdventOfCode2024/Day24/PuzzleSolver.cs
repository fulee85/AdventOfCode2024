using AdventOfCode2024.Common;
using System.Collections.ObjectModel;
using Gate = (string Wire1, string Wire2, System.Func<bool, bool, bool> Operation);
using GateWiring = (string Wire1, string Wire2, string Operation);

namespace AdventOfCode2024.Day24;

public class PuzzleSolver : PuzzleSolverBase
{
    const string AND = "AND";
    const string XOR = "XOR";
    const string OR = "OR";
    public PuzzleSolver(IInput input) : base(input)
    {
        inputWires = input
            .TakeWhile(l => l != string.Empty)
            .Select(l => l.Split(": "))
            .ToDictionary(prts => prts[0], prts => prts[1] == "1");

        gates = input.SkipWhile(l => l != string.Empty)
            .Skip(1)
            .Select(l => l.Split(" -> "))
            .ToDictionary(prts => prts[1], prts =>
            {
                var p = prts[0].Split();
                Func<bool, bool, bool> op = p[1] switch
                {
                    AND => (a, b) => a && b,
                    OR => (a, b) => a || b,
                    XOR => (a, b) => a ^ b,
                    _ => throw new NotImplementedException(),
                };

                return (p[0], p[2], op);
            });
    }

    private readonly Dictionary<string, bool> inputWires;
    private readonly Dictionary<string, Gate> gates;
    public override string GetFirstSolution()
    {
        List<string> outputWares = gates.Keys
            .Where(k => k.StartsWith('z'))
            .ToList();

        outputWares.Sort();

        List<bool> result = outputWares.Select(GetOutput).ToList();

        var value = ToInt(result);

        return value.ToString();
    }

    private long ToInt(List<bool> resultList)
    {
        long ex = 1;
        long result = 0;
        foreach (var item in resultList)
        {
            if (item)
            {
                result += ex;
            }
            ex *= 2;
        }

        return result;
    }

    private bool GetOutput(string label)
    {
        if (inputWires.ContainsKey(label))
        {
            return inputWires[label];
        }
        var gate = gates[label];
        bool outputValue = gate.Operation(GetOutput(gate.Wire1), GetOutput(gate.Wire2));

        inputWires[label] = outputValue;

        return outputValue;
    }

    public override string GetSecondSolution()
    {
        int inputBitsCount = inputWires.Count(w => w.Key.StartsWith('x'));
        List<string> outputWires = gates.Keys
            .Where(k => k.StartsWith('z'))
            .Order()
            .ToList();

        Dictionary<GateWiring,string> gateOutputStrings = input.SkipWhile(l => l != string.Empty)
            .Skip(1)
            .Select(l => l.Split(" -> "))
            .ToDictionary(prts =>
            {
                var p = prts[0].Split();
                var wire1 = p[0].CompareTo(p[2]) < 0 ? p[0] : p[2];
                var wire2 = p[0].CompareTo(p[2]) > 0 ? p[0] : p[2];
                return (wire1, wire2, p[1]);
            }, prts => prts[1]);

        Dictionary<string, GateWiring> gateInputStrings = gateOutputStrings.ToDictionary(kv => kv.Value, kv => kv.Key);

        Dictionary<string, string> reLabelDictionaryReverseXOR = new();
        Dictionary<string, string> reLabelDictionaryReverseAND = new();

        for (int i = 0; i < inputBitsCount; i++)
        {
            string iStr = i.ToString().PadLeft(2, '0');
            var xXX = "x" + iStr;
            var yXX = "y" + iStr;

            var xXORy = xXX + XOR + yXX;
            var originalOutputLabel = gateOutputStrings[(xXX, yXX, XOR)];
            reLabelDictionaryReverseXOR[xXORy] = originalOutputLabel;

            var xANDy = xXX + AND + yXX; 
            originalOutputLabel = gateOutputStrings[(xXX, yXX, AND)];
            reLabelDictionaryReverseAND[xANDy] = originalOutputLabel;
        }

        List<string> misConnectedWires = new();
        if (reLabelDictionaryReverseXOR["x00XORy00"] != "z00")
        {
            misConnectedWires.Add("z00");
        }

        reLabelDictionaryReverseAND["c00"] = reLabelDictionaryReverseAND["x00ANDy00"];
        reLabelDictionaryReverseAND.Remove("x00ANDy00");

        for (int i = 1; i < inputBitsCount; i++)
        {
            string iStr = i.ToString().PadLeft(2, '0');
            var zK = "z" + iStr;
            var xK = "x" + iStr;
            var yK = "y" + iStr;
            var cK = "c" + (i-1).ToString().PadLeft(2, '0');
            var gateToZ = gateInputStrings[zK];
            if (gateToZ.Operation != XOR)
            {
                misConnectedWires.Add(zK);
                var missconnectedWire = gateOutputStrings.FirstOrDefault(k => k.Key.Operation == XOR &&
                (k.Key.Wire1 == reLabelDictionaryReverseXOR[xK + XOR + yK] || k.Key.Wire2 == reLabelDictionaryReverseXOR[xK + XOR + yK]));
                misConnectedWires.Add(missconnectedWire.Value);
                gateInputStrings[zK] = missconnectedWire.Key;
                gateOutputStrings[missconnectedWire.Key] = zK;
                gateOutputStrings[gateToZ] = missconnectedWire.Value;
                gateInputStrings[missconnectedWire.Value] = gateToZ;
                if (reLabelDictionaryReverseAND.ContainsValue(zK))
                {
                    reLabelDictionaryReverseAND[reLabelDictionaryReverseAND.First(kv => kv.Value == zK).Key] = missconnectedWire.Value;
                }
            }
            //if (gateToZ.Wire1 != reLabelDictionaryReverse[cK] && gateToZ.Wire2 != reLabelDictionaryReverse[cK])
            //{
            //    misConnectedWires.Add(reLabelDictionaryReverse[cK]);
            //}
            //if (gateToZ.Wire1 != reLabelDictionaryReverse[xK + XOR + yK] && gateToZ.Wire2 != reLabelDictionaryReverse[xK + XOR + yK])
            //{
            //    misConnectedWires.Add(reLabelDictionaryReverse[xK + XOR + yK]);
            //}

            var orGate = gateInputStrings.Values
                .FirstOrDefault(gw => gw.Wire1 == reLabelDictionaryReverseAND[xK + AND + yK] || gw.Wire2 == reLabelDictionaryReverseAND[xK + AND + yK]);
            if (orGate.Operation != OR)
            {
                misConnectedWires.Add(reLabelDictionaryReverseAND[xK + AND + yK]);
            }
        }

        foreach (var orGate in gateOutputStrings.Where(gos => gos.Key.Operation == OR))
        {
            if (!reLabelDictionaryReverseAND.ContainsValue(orGate.Key.Wire1) && !reLabelDictionaryReverseAND.ContainsValue(orGate.Key.Wire2))
            {
                Console.WriteLine(orGate.Value);
                misConnectedWires.Add("khg");
            }
        }

        return string.Join(',',misConnectedWires.Order());
    }
}
