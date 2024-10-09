
namespace Calculator.Core.Lexer;

public class Lexer : ILexer
{
    public IEnumerable<string> Tokenize(string expression)
    {
        expression = expression.Replace(" ", "");
        var tokens = new List<string>();

        // validate
        var (valid, exception) = Validate(expression);
        if (!valid && exception is not null) throw exception;

        var number = "";
        for (int i = 0; i < expression.Length; i++)
        {
            if (char.IsDigit(expression[i]))
            {
                number += expression[i];
                continue;
            }
            else if (expression[i] == '.' && number.Length >= 1 && !number.Contains('.'))
            {
                number += expression[i];
                continue;
            }

            if (number.Length > 0) // we do this check to avoid accidentally adding ""
            {
                // add the number since the current character is not a digit => end of the number
                tokens.Add(number);
                number = "";
            }

            if (MathOperatorsUtils.TryParse(expression[i], out MathOperators oprt))
            {
                tokens.Add(expression[i].ToString());
            }
            else if (expression[i] == '(' || expression[i] == ')')
            {
                tokens.Add(expression[i].ToString());
            }
            else
            {
                throw new SyntaxException("Invalid symbol");
            }
        }

        // after the loop at the last stored number
        if (number.Length > 0)
        {
            tokens.Add(number);
        }

        return tokens;
    }

    private static (bool, Exception?) Validate(string expression)
    {
        // check if the last character is a valid operator
        if (MathOperatorsUtils.TryParse(expression.Last(), out _))
        {
            return (false, new SyntaxException("Expression cannot end with an operator."));
        }

        // check if the first character is a valid operator and exclude subtraction
        if (MathOperatorsUtils.TryParse(expression.First(), out MathOperators oprt) && oprt != MathOperators.Subtraction)
        {
            return (false, new SyntaxException("Expression cannot start with an operator except subtraction."));
        }

        // If all checks pass, return true with no exception
        return (true, null);
    }
}
