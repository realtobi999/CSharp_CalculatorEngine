using Calculator.Core.Evaluator;
using Calculator.Core.Lexer;
using Calculator.Core.Parser;

var tokens = new Lexer().Tokenize("-(2)^2");
var ast = new Parser(tokens.GetEnumerator()).Parse();
var result = new Evaluator().Calculate(ast);

Console.WriteLine($"[*] The Result Is: {result}");