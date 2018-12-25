using BCC.Core.Computation.Nodes.Unions;
using System;

namespace BCC.Core.Computation.Nodes.BiNodes
{
    class Power : BiNode
    {
        public Power(Node expBase, Node exponent) : base(new Func<Node, Node, Node>((b,e) =>
        {
            if (b is Value valB && e is Value valE)
            {
                return new Value(Math.Pow(valB.GetValue, valE.GetValue));
            }
            else return new Power(b, e);
        }), "", "^", expBase, exponent)
        {
        }
    }
}
