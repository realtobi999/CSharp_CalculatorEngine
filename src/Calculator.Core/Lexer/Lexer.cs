namespace Calculator.Core.Lexer;

public class Lexer : ILexer
{
    public IEnumerable<Token> Tokenize(string expression)
    {
        expression = expression.Replace(" ", ""); // remove all spaces
        var tokens = new List<Token>();

        // validate
        var (valid, exception) = Validate(expression);
        if (!valid && exception is not null) throw exception;

        var number = "";
        for (int i = 0; i < expression.Length; i++)
        {
            if (char.IsDigit(expression[i]))
            {
                number += expression[i]; // collect the digits into a number
                continue;
            }
            else if (expression[i] == '.' && number.Length >= 1 && !number.Contains('.'))
            {
                number += expression[i]; // handle decimal points
                continue;
            }

            // add the number token when encountering a non-digit
            if (number.Length > 0)
            {
                tokens.Add(new Token(TokenType.Number, number));
                number = ""; // reset for the next number
            }

            // handle operator or parenthesis
            switch (expression[i])
            {
                case '+':
                    tokens.Add(new Token(TokenType.Plus, "+"));
                    break;
                case '-':
                    tokens.Add(new Token(TokenType.Minus, "-"));
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
                default:
                    throw new SyntaxException($"Unexpected character: {expression[i]}");
            }
        }

        // add the last number, if any
        if (number.Length > 0)
        {
            tokens.Add(new Token(TokenType.Number, number));
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
