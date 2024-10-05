using Calculator.Core.Interfaces;
using Calculator.Core.Models;

namespace Calculator.Core;

public class QueryLexer : IQueryLexer
{
    private readonly ITokenizer _tokenizer;

    public QueryLexer(ITokenizer tokenizer)
    {
        _tokenizer = tokenizer;
    }

    public MathQuery ToQuery(string expression)
    {
        var query = new MathQuery() { Numbers = [], Operators = [] };
        var tokens = _tokenizer.Tokenize(expression).ToList();

        for (int i = 0; i < tokens.Count; i++)
        {
            if (double.TryParse(tokens[i], out double num))
            {
                query.Numbers[i] = num;
            }
            else if (MathOperatorsUtils.TryParse(tokens[i], out MathOperators oprt))
            {
                if (i == 0 && oprt != MathOperators.Subtraction)
                {
                    throw new Exception("Invalid Input. Expression cannot start with a operator.");
                }
                if (i == tokens.Count - 1)
                {
                    throw new Exception("Invalid Input. Expression cannot end with a operator.");
                }

                // multiple a number by -1 after the subtraction operator and assign it as addition
                if (oprt == MathOperators.Subtraction)
                {
                    tokens[i + 1] = "-" + tokens[i+1];

                    if (i == 0)
                    {
                        continue;
                    }

                    oprt = MathOperators.Addition;
                }

                query.Operators[i - 1] = oprt;
            }
            else
            {
                throw new Exception($"Invalid query. Symbol '{tokens[i]}' is not valid.");
            }
        }

        return query;
    }
}
