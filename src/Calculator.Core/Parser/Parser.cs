using Calculator.Core.Exceptions;
using Calculator.Core.Lexer;

namespace Calculator.Core.Parser;

public class Parser : IParser
{
    private IEnumerator<Token> Tokens { get; set; }
    private Token CurrentToken { get; set; }

    public Parser(IEnumerator<Token> tokens)
    {
        Tokens = tokens;
        CurrentToken = GetNextToken();
    }

    public Node Parse()
    {
        return ParseAddSubtract();
    }

    private Node ParseAddSubtract()
    {
        var left = ParseMultiplyDivide();

        while (CurrentToken.Type == TokenType.Plus || CurrentToken.Type == TokenType.Minus)
        {
            var op = CurrentToken.Type;
            CurrentToken = GetNextToken();
            var right = ParseMultiplyDivide();
            left = new BinaryOpNode(left, op, right);
        }

        return left;
    }

    private Node ParseMultiplyDivide()
    {
        var left = ParsePrimary();

        while (CurrentToken.Type == TokenType.Multiply || CurrentToken.Type == TokenType.Divide)
        {
            var op = CurrentToken.Type;
            CurrentToken = GetNextToken();
            var right = ParsePrimary();
            left = new BinaryOpNode(left, op, right);
        }

        return left;
    }

    private Node ParsePrimary()
    {
        Node node;

        // handle parentheses
        if (CurrentToken.Type == TokenType.LeftParen)
        {
            CurrentToken = GetNextToken(); // skip the '('
            node = ParseAddSubtract();

            if (CurrentToken.Type != TokenType.RightParen)
            {
                throw new ParsingException("Unexpected token during primary parsing. Expected closing parenthesis");
            }

            CurrentToken = GetNextToken(); // skip the ')'
            return node;
        }

        if (CurrentToken.Type != TokenType.Number)
        {
            throw new ParsingException("Unexpected token during primary parsing.");
        }

        // handle numbers
        node = new NumberNode(double.Parse(CurrentToken.Value));
        CurrentToken = GetNextToken();

        return node;
    }


    private Token GetNextToken()
    {
        if (Tokens.MoveNext())
        {
            return Tokens.Current;
        }

        return new Token(TokenType.EndOfExpression, "END");
    }
}
