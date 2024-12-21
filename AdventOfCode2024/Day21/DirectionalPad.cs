namespace AdventOfCode2024.Day21;

public class DirectionalPad : Pad
{
    private static Dictionary<string, string>? staticDictionary;

    private static readonly Dictionary<(char, char), char> directionalPadEdgesMap = new()
        {
            { ('p', 'u'), 'l' },
            { ('p', 'r'), 'd' },
            { ('u', 'p'), 'r' },
            { ('u', 'd'), 'd' },
            { ('r', 'p'), 'u' },
            { ('r', 'd'), 'l' },
            { ('d', 'u'), 'u' },
            { ('d', 'r'), 'r' },
            { ('d', 'l'), 'l' },
            { ('l', 'd'), 'r' },
        };

    private static readonly Dictionary<char, char[]> directionalPadEdges = new()
    {
        {'p', ['u','r']},
        {'u', ['p','d']},
        {'r', ['p','d']},
        {'d', ['u','r','l']},
        {'l', ['d']}
    };

    private static readonly List<char> directionalPadChars = ['p', 'u', 'r', 'd', 'l'];

    public DirectionalPad(Pad pad): base(pad)
    {
        StartChar = 'p';
        if (staticDictionary is null)
        {
            base.chars = directionalPadChars;
            base.edgesMap = directionalPadEdgesMap;
            base.edges = directionalPadEdges;
            PopulateDictionary();
            dictionary["pp"] = "";
            dictionary["uu"] = "";
            dictionary["rr"] = "";
            dictionary["dd"] = "";
            dictionary["ll"] = "";
            staticDictionary = dictionary;
        }
        else
        {
            dictionary = staticDictionary;
        }
    }

    public void AddNewNextPad()
    {
        next = new DirectionalPad(this);
    }
}
