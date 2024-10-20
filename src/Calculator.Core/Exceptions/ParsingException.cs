namespace Calculator.Core.Exceptions;

public class ParsingException(string message) : Exception($"ERROR WHILE PARSING! {message}")
{

}
