using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Computation.Nodes
{
    class UniNode : Node
    {
        public UniNode(Func<Node,Node> functor, string preSignature, Node node) : 
            base(new Func<List<Node>, Node>(l => functor(l[0])), preSignature, "", node)
        {
        }
    }
}
