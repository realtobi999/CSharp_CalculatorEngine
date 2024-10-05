using Calculator.Core;
using FluentAssertions;

namespace Calculator.Tests.Engine;

public class BasicArithmeticsTests
{
    [Fact]
    public void Solve_AdditionShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("3+3").Should().Be(6);
        engine.Solve("3+3+3").Should().Be(9);
        engine.Solve("33+33+22").Should().Be(88);
    }

    [Fact]
    public void Solve_SubtractionShouldWork()
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
    public void Solve_SubtractionAndAdditionShouldWorkTogether()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("90+30-90+30").Should().Be(60);
        engine.Solve("22-0-50+30").Should().Be(2);
    }

    [Fact]
    public void Solve_MultiplicationShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("2*2").Should().Be(4);
        engine.Solve("3*2*2").Should().Be(12);
        engine.Solve("-3*2").Should().Be(-6);
        engine.Solve("-3*-2").Should().Be(6);
    }


    [Fact]
    public void Solve_DivisionShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("2/2").Should().Be(1);
        engine.Solve("4/2").Should().Be(2);
        engine.Solve("-4/2").Should().Be(-2);
        engine.Solve("-4/-2").Should().Be(2);
        engine.Solve("-4/1").Should().Be(-4);
        engine.Solve("0/1").Should().Be(0);
        Assert.Throws<DivideByZeroException>(() => engine.Solve("0/0"));
    }

    [Fact]
    public void Solve_OrderOfOperationsShouldWork()
    {
        // prepare
        var engine = new CalculatorEngine();

        // act & assert
        engine.Solve("18/3-7+2*5").Should().Be(9);
        engine.Solve("7-24/8*4+6").Should().Be(1);
    }
}
