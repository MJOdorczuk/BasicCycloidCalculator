using System;
using System.Collections.Generic;
using BCC.Core.Computation.Nodes.Unions;

namespace BCC.Core.Computation.Nodes.MultiNodes
{
    class Sum : MultiNode
    {
        public Sum(params Node[] nodes) : base(new Func<MultiNode, List<Node>>(multiNode =>
        {
            if (multiNode is Sum sum) return sum.NodesUnder;
            else return new List<Node>() { multiNode };
        }), (a, b) => a + b, list => new Sum(list.ToArray()), 0.0, "+", nodes)
        {
        }
    }
}
