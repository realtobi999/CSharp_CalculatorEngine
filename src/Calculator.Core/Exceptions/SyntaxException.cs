namespace Calculator.Core.Exceptions;

public class SyntaxException(string message) : Exception($"SYNTAX ERROR! {message}")
{

}
