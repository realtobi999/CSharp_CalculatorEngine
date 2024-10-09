
namespace Calculator.Core.Lexer;

public class Lexer : ILexer
{
    public IEnumerable<string> Tokenize(string expression)
    {
        expression = expression.Replace(" ", "");
        var tokens = new List<string>();

        var number = "";
        for (int i = 0; i < expression.Length; i++)
        {
            if (char.IsDigit(expression[i]))
            {
                number += expression[i];
            }
            if (expression[i] == '.' && number.Length >= 1 && !number.Contains('.'))
            {
                number += expression[i];
            }
            if (MathOperatorsUtils.TryParse(expression[i], out MathOperators oprt))
            {
                // make sure that the number isn't empty beforehand (exception for subtraction) 
                if (number.Length <= 0 && oprt != MathOperators.Subtraction)
                {
                    throw new SyntaxException("Unexpected empty number in expression.");
                }

                // add the number to the tokens and then the operator
                if (number.Length > 0)
                {
                    tokens.Add(number);
                }
                tokens.Add(expression[i].ToString());

                number = "";
            }
        }

        // after the loop at the last stored number
        if (number.Length > 0)
        {
            tokens.Add(number);
        }

        return tokens;
    }
}
