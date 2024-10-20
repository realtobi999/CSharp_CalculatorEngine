namespace Calculator.Core.Exceptions;

public class EvaluatingException(string message) : Exception($"ERROR WHILE EVALUATING: {message}")
{

}
