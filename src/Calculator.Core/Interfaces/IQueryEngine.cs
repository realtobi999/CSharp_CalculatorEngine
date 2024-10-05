using Calculator.Core.Models;

namespace Calculator.Core.Interfaces;

public interface IQueryEngine
{
    double Calculate(MathQuery query);
}
