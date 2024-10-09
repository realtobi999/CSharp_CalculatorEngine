using Calculator.Core.Lexer;

var lexer = new Lexer();
var tokens = lexer.Tokenize("*2 + 2*3");

foreach (var token in tokens)
{
    Console.WriteLine(token);
}
