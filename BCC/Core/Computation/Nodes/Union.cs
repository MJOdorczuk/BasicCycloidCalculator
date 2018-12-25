using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Computation.Nodes
{
    class Union : Node
    {
        public Union(Func<Node> functor, string signature) : 
            base(new Func<List<Node>, Node>(l => functor()), signature, "")
        {
        }
    }
}
