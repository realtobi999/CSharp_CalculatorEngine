namespace Calculator.Core;

public class CalculatorEngine
{
    public int Solve(string expression)
    {
        var tokens = new List<string>();

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
                    // add the number and reset
                    tokens.Add(num1);
                    num1 = "";
                }
                // add operator
                tokens.Add(ch.ToString());
            }
        }

        // add the last number if exists
        if (num1 != "") tokens.Add(num1); 

        // iterate through the first element of the tokens and sum up all the elements until there is only one number left
        for (int i = 0; i < tokens.Count;) 
        {
            if (tokens.Count == 1) break;
            
            var token = tokens[i];

            if (int.TryParse(token, out int num2))
            {
                if (i + 2 < tokens.Count && tokens[i + 1] == "+" && int.TryParse(tokens[i + 2], out int num3))
                {
                    tokens[i] = (num2 + num3).ToString();

                    tokens.RemoveAt(i + 2);
                    tokens.RemoveAt(i + 1);
                }
            }
        }

        return int.Parse(tokens.First());
    }
}
