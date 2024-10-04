using Calculator.Core;
using FluentAssertions;

namespace Calculator.Tests.Engine;

public class BasicArithmeticsTests
{
    [Fact]
    public void AdditionShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("3+3").Should().Be(6);
        engine.Solve("3+3+3").Should().Be(9);
        engine.Solve("33+33+22").Should().Be(88);
    }
}
