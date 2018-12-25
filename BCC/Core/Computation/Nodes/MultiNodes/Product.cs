using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Computation.Nodes.MultiNodes
{
    class Product : MultiNode
    {
        public Product(params Node[] nodes) : 
            base(multiNode => multiNode is Product product ? product.NodesUnder : new List<Node>() { multiNode },
                (a, b) => a * b, list => new Product(list.ToArray()), 1.0, "*", nodes)
        {
        }
    }
}
