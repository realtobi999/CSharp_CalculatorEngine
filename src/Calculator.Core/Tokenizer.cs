using System;
using Calculator.Core.Interfaces;

namespace Calculator.Core;

public class Tokenizer : ITokenizer
{
    public IEnumerable<string> Tokenize(string expression)
    {
        var currentNumber = "";

        foreach (var ch in expression)
        {
            if (char.IsDigit(ch) || ch == '.')
            {
                currentNumber += ch;
            }
            else
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    yield return currentNumber; 
                    currentNumber = ""; 
                }
                yield return ch.ToString(); 
            }
        }

        // yield the last number if it exists
        if (!string.IsNullOrEmpty(currentNumber))
        {
            yield return currentNumber;
        }
    }
}
