namespace Calculator.Core.Parser;

public class ParsingException(string message) : Exception($"PARSING ERROR! {message}")
{

}
