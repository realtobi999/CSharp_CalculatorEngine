using Calculator.Core.Exceptions;
using Calculator.Core.Lexer;
using Calculator.Core.Parser;

namespace Calculator.Core.Evaluator;

public class Evaluator : IEvaluator
{
    public double Calculate(Node node)
    {
        return Evaluate(node).Value;
    }

    private NumberNode Evaluate(Node node)
    {
        // handle binary operations (e.g. +, -, *, /)
        if (node is BinaryOpNode binaryOpNode)
        {
            var leftNode = Evaluate(binaryOpNode.Left);
            var rightNode = Evaluate(binaryOpNode.Right);

            double result;

            // perform operation based on the operator type and return the result
            switch (binaryOpNode.Operator)
            {
                case TokenType.Plus:
                    result = leftNode.Value + rightNode.Value;
                    break;
                case TokenType.Minus:
                    result = leftNode.Value - rightNode.Value;
                    break;
                case TokenType.Multiply:
                    result = leftNode.Value * rightNode.Value;
                    break;
                case TokenType.Divide:
                    if (rightNode.Value == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }

                    result = leftNode.Value / rightNode.Value;
                    break;
                case TokenType.Exponentiation:
                    result = Math.Pow(leftNode.Value, rightNode.Value);
                    break;
                default:
                    throw new NotSupportedException($"Operator '{binaryOpNode.Operator}' is not supported.");
            }

            // return the result wrapped in a NumberNode
            return new NumberNode(result);
        }

        // if the node is a number node return it
        if (node is NumberNode numberNode)
        {
            return numberNode;
        }

        throw new EvaluatingException("Expected a NumberNode, but found a different node type.");
    }
}

