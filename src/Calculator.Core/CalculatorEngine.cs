using System.Net.Security;

namespace Calculator.Core;

public class CalculatorEngine
{
    public int Solve(string expression)
    {
        var tokens = new List<string>();

        // convert the string like "33+33" into a list like ["33", "+", "33"]
        var num1 = "";
        foreach (var ch in expression)
        {
            if (char.IsDigit(ch))
            {
                num1 += ch;
            }
            else
            {
                if (num1 != "")
                {
                    tokens.Add(num1);
                    num1 = "";
                }
                tokens.Add(ch.ToString());
            }
        }
        // add the last number if it exists
        if (num1 != "") tokens.Add(num1);

        // adjust the tokens for easier usage => attach the "-" symbol directly to the following number
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "-" && int.TryParse(tokens[i + 1], out _))
            {
                tokens[i + 1] = "-" + tokens[i + 1];

                if (i == 0)
                {
                    tokens.RemoveAt(i);
                }
                else
                {
                    tokens[i] = "+";
                }
            }
        }

        // iterate through the tokens and apply operators until only one number remains
        while (true)
        {
            if (tokens.Count == 1) break;

            var token = tokens[0];

            if (int.TryParse(token, out int num2))
            {
                if (tokens[1] == "+" && int.TryParse(tokens[2], out int num3))
                {
                    tokens[0] = (num2 + num3).ToString();

                    tokens.RemoveAt(2);
                    tokens.RemoveAt(1);
                }
            }
        }

        return int.Parse(tokens.First());
    }

}
