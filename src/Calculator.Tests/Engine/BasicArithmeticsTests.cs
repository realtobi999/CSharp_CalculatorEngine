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

    [Fact]
    public void SubtractionShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("9-3").Should().Be(6);
        engine.Solve("-3-9").Should().Be(-12);
        engine.Solve("3-9").Should().Be(-6);
        engine.Solve("9-3-3-3").Should().Be(0);
        engine.Solve("90-30-30-30").Should().Be(0);
    }

    [Fact]
    public void SubtractionAndAdditionShouldWorkTogether()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("90+30-90+30").Should().Be(60);
        engine.Solve("22-0-50+30").Should().Be(2);
    }
}
