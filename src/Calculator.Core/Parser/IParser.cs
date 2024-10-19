using Calculator.Core.Lexer;

namespace Calculator.Core.Parser;

public interface IParser
{
    Node Parse();
}
