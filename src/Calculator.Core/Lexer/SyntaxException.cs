namespace Calculator.Core.Lexer;

public class SyntaxException(string message): Exception($"SYNTAX ERROR! {message}")
{

}
