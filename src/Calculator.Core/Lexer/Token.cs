namespace Calculator.Core.Lexer;

public class Token
{
    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public TokenType Type { get; init; }
    public string Value { get; init; }
    
}

public enum TokenType {
    Number,
    Plus,
    Minus,
    Multiply,
    Divide,
    LeftParen,
    RightParen,
}

