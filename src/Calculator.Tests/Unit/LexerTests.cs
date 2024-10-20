using Calculator.Core.Exceptions;
using Calculator.Core.Lexer;

namespace Calculator.Tests.Unit;

public class LexerTests
{
    [Fact]
    public void Tokenize_ExpressionCannotStartWithAOperator()
    {
        // prepare
        var lexer = new Lexer();

        // act & assert
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("*2+3"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("+2+3"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("/2+3"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("*2+3"));
    }

    [Fact]
    public void Tokenize_ExpressionCannotEndWithAOperator()
    {
        // prepare
        var lexer = new Lexer();

        // act & assert
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("2+3/"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("2+3*"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("2+3+"));
        Assert.Throws<SyntaxException>(() => lexer.Tokenize("2+3-"));
    }
}
