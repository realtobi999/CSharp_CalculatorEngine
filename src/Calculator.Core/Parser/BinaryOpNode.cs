using Calculator.Core.Lexer;

namespace Calculator.Core.Parser;

public class BinaryOpNode : Node
{
    public BinaryOpNode(Node left, TokenType op, Node right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }

    public Node Left { get; set; }
    public TokenType Operator { get; set; }
    public Node Right { get; set; }
}
