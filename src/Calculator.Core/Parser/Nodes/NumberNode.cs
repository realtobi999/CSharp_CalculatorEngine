using Calculator.Core.Parser;

public class NumberNode : Node
{
    public NumberNode(double value)
    {
        Value = value;
    }

    public double Value { get; set; }
}
