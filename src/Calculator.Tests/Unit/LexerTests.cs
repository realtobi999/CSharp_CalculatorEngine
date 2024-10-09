using System.Text.RegularExpressions;
using Calculator.Core.Lexer;
using FluentAssertions;

namespace Calculator.Tests.Unit;

public partial class LexerTests
{
    private readonly Lexer _lexer;

    public LexerTests()
    {
        _lexer = new Lexer();
    }

    [Theory]
    [MemberData(nameof(LoadDataSimpleLexerData))]
    public void Tokenize_SimpleInputData(string expression, IEnumerable<string> expectedTokens)
    {
        Assert.Equal(expectedTokens, _lexer.Tokenize(expression));
    }

    public static IEnumerable<object[]> LoadDataSimpleLexerData()
    {
        var lines = File.ReadAllLines("./../../../Data/simple_lexer_test_data.csv");

        for (int i = 1; i < lines.Length; i++)
        {
            string? line = lines[i];

            // skip empty lines to avoid processing them
            if (string.IsNullOrWhiteSpace(line)) continue;

            // split by the first occurrence of ", " (to avoid issues if expression contains a comma)
            var values = line.Split([", " ], 2, StringSplitOptions.None);

            // Split the tokens by commas and trim any whitespace
            var expectedTokens = values[1].Trim('[', ']')
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) // split by commas
                .Select(token => token.Trim()) // trim spaces from each token
                .ToList();

            yield return new object[] { values[0], expectedTokens };
        }
    }

}