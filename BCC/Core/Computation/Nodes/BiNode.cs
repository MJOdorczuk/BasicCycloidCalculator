using System;
using System.Collections.Generic;

namespace BCC.Core.Computation.Nodes
{
    class BiNode : Node
    {
        public BiNode(Func<Node, Node, Node> functor, string preSignature, string separator, Node left, Node right) : 
            base(new Func<List<Node>, Node>(l => functor(l[0],l[1])), preSignature, separator, left, right)
        {
        }
    }
}
