using System.Runtime.InteropServices;
using Calculator.Core.Extensions;
using Calculator.Core.Interfaces;
using Calculator.Core.Models;

namespace Calculator.Core;

public class QueryEngine : IQueryEngine
{
    public double Calculate(MathQuery query)
    {
        foreach (var key in query.Operators.Where(oprt => oprt.Value == MathOperators.Addition).Select(oprt => oprt.Key).ToList())
        {
            query.Operators.Remove(key);
        }

        foreach (var oprt in query.Operators.Where(oprt => oprt.Value == MathOperators.Exponentiation))
        {
            var num2Key = query.Numbers.NextKey(oprt.Key);

            var num1 = query.Numbers[oprt.Key];
            var num2 = query.Numbers[num2Key];

            num2 = Math.Pow(num1, num2);

            query.Numbers[num2Key] = num2;

            query.Numbers.Remove(oprt.Key);
            query.Operators.Remove(oprt.Key);
        }

        for (int i = 0; query.Operators.Count != 0;)
        {
            var oprt = query.Operators.ElementAt(i);

            if (oprt.Value == MathOperators.Multiplication)
            {
                var num2Key = query.Numbers.NextKey(oprt.Key);

                var num1 = query.Numbers[oprt.Key];
                var num2 = query.Numbers[num2Key];

                num2 = num1 * num2;

                query.Numbers[num2Key] = num2;
            }
            else if (oprt.Value == MathOperators.Division)
            {
                var num2Key = query.Numbers.NextKey(oprt.Key);

                var num1 = query.Numbers[oprt.Key];
                var num2 = query.Numbers[num2Key];

                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }

                num2 = num1 / num2;

                query.Numbers[num2Key] = num2;
            }

            query.Numbers.Remove(oprt.Key);
            query.Operators.Remove(oprt.Key);
        }

        return query.Numbers.Sum(n => n.Value);
    }
}
