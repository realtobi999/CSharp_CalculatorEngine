using Calculator.Core.Lexer;

namespace Calculator.Tests.Unit;

public partial class LexerCsvTests
{
    private readonly Lexer _lexer;

    public LexerCsvTests()
    {
        _lexer = new Lexer();
    }

    [Theory]
    [MemberData(nameof(LoadDataSimpleLexerData))]
    public void Tokenize_SimpleInputData(string expression, IEnumerable<string> expectedTokens)
    {
        Assert.Equal(expectedTokens, _lexer.Tokenize(expression));
    }

    [Theory]
    [MemberData(nameof(LoadDataComplexLexerData))]
    public void Tokenize_ComplexInputData(string expression, IEnumerable<string> expectedTokens)
    {
        Assert.Equal(expectedTokens, _lexer.Tokenize(expression));
    }

    public static IEnumerable<object[]> LoadDataSimpleLexerData()
    {
        var lines = File.ReadAllLines("./../../../Data/simple_lexer_test_data.csv");

        return ExtractCsv(lines);
    }

    public static IEnumerable<object[]> LoadDataComplexLexerData()
    {
        var lines = File.ReadAllLines("./../../../Data/complex_lexer_test_data.csv");

        return ExtractCsv(lines);
    }

    private static IEnumerable<object[]> ExtractCsv(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string? line = lines[i];

            // skip empty lines to avoid processing them
            if (string.IsNullOrWhiteSpace(line)) continue;

            // split by the first occurrence of ", " (to avoid issues if expression contains a comma)
            var values = line.Split([", "], 2, StringSplitOptions.None);

            // Split the tokens by commas and trim any whitespace
            var expectedTokens = values[1].Trim('[', ']')
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) // split by commas
                .Select(token => token.Trim()) // trim spaces from each token
                .ToList();

            yield return new object[] { values[0], expectedTokens };
        }
    }
}