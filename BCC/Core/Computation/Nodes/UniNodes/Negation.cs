using BCC.Core.Computation.Nodes.Unions;
using System;

namespace BCC.Core.Computation.Nodes.UniNodes
{
    class Negation : UniNode
    {
        public Negation(Node node) : 
            base(new Func<Node, Node>(n =>
            {
                switch(n)
                {
                    case Negation neg:
                        return neg.NodesUnder[0];
                    case Value val:
                        return new Value(0.0 - val.GetValue);
                }
                return new Negation(n);
            }), "-", node)
        {
        }
    }
}
