using Calculator.Core.Models;

namespace Calculator.Core.Interfaces;

public interface IQueryLexer
{
    MathQuery ToQuery(string expression);
}
