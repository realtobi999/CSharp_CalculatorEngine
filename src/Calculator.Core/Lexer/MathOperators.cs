namespace Calculator.Core.Lexer;

public enum MathOperators
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Exponentiation,
}

public static class MathOperatorsUtils
{
    public static bool TryParse(string s, out MathOperators mathOperator)
    {
        switch (s)
        {
            case "+":
                mathOperator = MathOperators.Addition;
                return true;
            case "-":
                mathOperator = MathOperators.Subtraction;
                return true;
            case "*":
                mathOperator = MathOperators.Multiplication;
                return true;
            case "/":
                mathOperator = MathOperators.Division;
                return true;
            case "^":
                mathOperator = MathOperators.Exponentiation;
                return true;
            default:
                mathOperator = default;
                return false;
        }
    }

    public static bool TryParse(char c, out MathOperators mathOperator)
    {
        return TryParse(c.ToString(), out mathOperator);
    }

}