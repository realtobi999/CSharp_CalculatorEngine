using Calculator.Core.Evaluator;
using Calculator.Core.Lexer;
using Calculator.Core.Parser;
using FluentAssertions;

namespace Calculator.Tests.Unit;

public class ParserEvaluatorCsvTests
{
    [Theory]
    [MemberData(nameof(LoadSimpleData))]
    public void Parse_Calculate_SimpleInputData(string expression, double expectedValue)
    {
        var tokens = new Lexer().Tokenize(expression);
        var ast = new Parser(tokens.GetEnumerator()).Parse();
        var result = new Evaluator().Calculate(ast);

        Math.Round(result, 2).Should().Be(Math.Round(expectedValue, 2));
    }

    [Theory]
    [MemberData(nameof(LoadComplexData))]
    public void Parse_Calculate_SimpleComplexData(string expression, double expectedValue)
    {
        var tokens = new Lexer().Tokenize(expression);
        var ast = new Parser(tokens.GetEnumerator()).Parse();
        var result = new Evaluator().Calculate(ast);

        Math.Round(result, 2).Should().Be(Math.Round(expectedValue, 2));
    }

    public static IEnumerable<object[]> LoadSimpleData()
    {
        var lines = File.ReadAllLines("./../../../Data/simple_parser_evaluator_test_data.csv");

        return ExtractCsv(lines);
    }

    public static IEnumerable<object[]> LoadComplexData()
    {
        var lines = File.ReadAllLines("./../../../Data/complex_parser_evaluator_test_data.csv");

        return ExtractCsv(lines);
    }

    private static IEnumerable<object[]> ExtractCsv(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            // skip empty lines to avoid processing them
            if (string.IsNullOrWhiteSpace(line)) continue;

            // split by the first occurrence of ", " (to avoid issues if expression contains a comma)
            var values = line.Split([","], 2, StringSplitOptions.None);

            var expression = values[0];
            var result = double.Parse(values[1]);

            yield return new object[] { expression, result };
        }
    }
}
