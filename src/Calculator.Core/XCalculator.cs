using System;
using Calculator.Core.Interfaces;

namespace Calculator.Core;

public class XCalculator
{
    private readonly IQueryLexer _lexer;
    private readonly IQueryEngine _engine;

    public XCalculator(IQueryLexer lexer, IQueryEngine engine)
    {
        _lexer = lexer;
        _engine = engine;
    }

    public double Solve(string expression)
    {
        var query = _lexer.ToQuery(expression);

        return _engine.Calculate(query);
    }
}
