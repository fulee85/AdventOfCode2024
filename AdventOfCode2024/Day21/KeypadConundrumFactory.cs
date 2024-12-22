namespace AdventOfCode2024.Day21;
public class KeypadConundrumFactory
{
    private readonly DirectionalKeypadFactory directionalKeypadFactory;
    private readonly NumericKeypadFactory numericKeypadFactory;

    public KeypadConundrumFactory(DirectionalKeypadFactory directionalKeypadFactory, NumericKeypadFactory numericKeypadFactory)
    {
        this.directionalKeypadFactory = directionalKeypadFactory;
        this.numericKeypadFactory = numericKeypadFactory;
    }

    public KeypadConundrum Create(int directionalKeypadCount)
    {
        Keypad keypad = new MostOuterPad();
        for (int i = 0; i < directionalKeypadCount; i++)
        {
            keypad = directionalKeypadFactory.Create(keypad);
        }
        keypad = numericKeypadFactory.Create(keypad);

        return new KeypadConundrum(keypad);
    }
}
