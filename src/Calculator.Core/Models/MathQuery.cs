namespace Calculator.Core.Models;

/// <summary>
/// Represents a mathematical expression consisting of numbers and operators.
/// </summary>
public struct MathQuery
{
    /// <summary>
    /// Gets or sets the numbers in the expression.
    /// </summary>
    /// <remarks>
    /// The key represents the index of the number in the expression.
    /// </remarks>
    public Dictionary<int, double> Numbers { get; set; }

    /// <summary>
    /// Gets or sets the operators in the expression.
    /// </summary>
    /// <remarks>
    /// The key represents the index of the number that the operator is assigned to.
    /// </remarks>
    public Dictionary<int, MathOperators> Operators { get; set; }
}

