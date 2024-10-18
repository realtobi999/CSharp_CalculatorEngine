namespace Calculator.Core.Lexer;

public interface ILexer
{
    IEnumerable<Token> Tokenize(string expression);
}
