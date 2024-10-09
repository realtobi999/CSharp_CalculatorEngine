namespace Calculator.Core.Lexer;

public interface ILexer
{
    IEnumerable<string> Tokenize(string expression);
}
