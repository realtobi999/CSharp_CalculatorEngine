using Calculator.Core;
using Calculator.Core.Models;

namespace Calculator.Tests.Unit;

public class QueryLexerTests
{
    [Fact]
    public void Tokenize_ShouldPerformSimpleTokenization()
    {
        // prepare
        var lexer = new QueryLexer(new Tokenizer());

        // act  & assert 
        var expected = new MathQuery
        {
            Numbers = new Dictionary<int, double>
                {
                    { 0, 6 },
                    { 2, 4 },
                    { 4, 12 },
                    { 6, 72 },
                    { 8, 8 },
                    { 10, -9 },
                    { 12, 3 },
                    { 14, 2 },

                },
            Operators = new Dictionary<int, MathOperators>
                {
                    { 0, MathOperators.Multiplication },
                    { 2, MathOperators.Division },
                    { 4, MathOperators.Addition},
                    { 6, MathOperators.Division},
                    { 8, MathOperators.Addition},
                    { 10, MathOperators.Addition},
                    { 12, MathOperators.Exponentiation}
                }
        };

        // use xUnit because for some weird reason FluentAssertions doesnt work for complex objects
        Assert.Equivalent(expected, lexer.ToQuery("6*4/12+72/8-9+3^2"));
    }
}
