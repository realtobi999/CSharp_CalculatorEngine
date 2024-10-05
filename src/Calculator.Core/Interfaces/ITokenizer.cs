namespace Calculator.Core.Interfaces;

public interface ITokenizer
{
    IEnumerable<string> Tokenize(string expression);
}
