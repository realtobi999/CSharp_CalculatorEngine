using Calculator.Core.Exceptions;

namespace Calculator.Core.Lexer;

public class Lexer : ILexer
{
    public IEnumerable<Token> Tokenize(string expression)
    {
        // Remove all spaces.
        expression = expression.Replace(" ", "");
        var tokens = new List<Token>();

        // Validate.
        var (valid, exception) = Validate(expression);
        if (!valid && exception is not null) throw exception;

        var number = "";
        for (int i = 0; i < expression.Length; i++)
        {
            // Collect the digits into a number.
            if (char.IsDigit(expression[i]))
            {
                number += expression[i];
                continue;
            }
            // Handle decimal points.
            else if (expression[i] == '.' && number.Length >= 1 && !number.Contains('.'))
            {
                number += expression[i];
                continue;
            }

            // Add the number token when encountering a non-digit.
            if (number.Length > 0)
            {
                tokens.Add(new Token(TokenType.Number, number));
                number = ""; // Reset for the next number.
            }

            // Handle operator or parenthesis.
            switch (expression[i])
            {
                case '+':
                    tokens.Add(new Token(TokenType.Plus, "+"));
                    break;
                case '-':
                    number += "-";

                    // If the last token is a number or a right parenthesis, add a plus operator.
                    if (tokens.Count > 0 && (tokens.Last().Type == TokenType.Number || tokens.Last().Type == TokenType.RightParen))
                    {
                        tokens.Add(new Token(TokenType.Plus, "+"));
                    }
                    // If the next character is an opening parenthesis, treat it as a negative number.
                    if (i + 1 < expression.Length && expression[i + 1] == '(')
                    {
                        tokens.Add(new Token(TokenType.Number, "-1"));
                        number = ""; // Reset the number, because we set it above.
                        tokens.Add(new Token(TokenType.Multiply, "*"));
                    }
                    break;
                case '*':
                    tokens.Add(new Token(TokenType.Multiply, "*"));
                    break;
                case '/':
                    tokens.Add(new Token(TokenType.Divide, "/"));
                    break;
                case '(':
                    tokens.Add(new Token(TokenType.LeftParen, "("));
                    break;
                case ')':
                    tokens.Add(new Token(TokenType.RightParen, ")"));
                    break;
                case '^':
                    tokens.Add(new Token(TokenType.Exponentiation, "^"));
                    break;
                default:
                    throw new SyntaxException($"Unexpected character: {expression[i]}");
            }
        }

        // Add the last number, if any.
        if (number.Length > 0)
        {
            tokens.Add(new Token(TokenType.Number, number));
        }

        return tokens;
    }


    private static (bool, Exception?) Validate(string expression)
    {
        // Check if the last character is a valid operator.
        if (MathOperatorsUtils.TryParse(expression.Last(), out _))
        {
            return (false, new SyntaxException("Expression cannot end with an operator."));
        }

        // Check if the first character is a valid operator and exclude subtraction.
        if (MathOperatorsUtils.TryParse(expression.First(), out MathOperators oprt) && oprt != MathOperators.Subtraction)
        {
            return (false, new SyntaxException("Expression cannot start with an operator except subtraction."));
        }

        // If all checks pass, return true with no exception.
        return (true, null);
    }
}
