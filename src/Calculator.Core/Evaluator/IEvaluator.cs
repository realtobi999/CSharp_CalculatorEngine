using Calculator.Core.Parser;

namespace Calculator.Core.Evaluator;

public interface IEvaluator
{
    double Calculate(Node node);
}
