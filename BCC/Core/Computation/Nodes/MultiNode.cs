using BCC.Core.Computation.Nodes.Unions;
using System;
using System.Collections.Generic;

namespace BCC.Core.Computation.Nodes
{
    abstract class MultiNode : Node
    {

        public MultiNode(Func<MultiNode,List<Node>> folder, Func<double,double,double> operand,
            Func<List<Node>, MultiNode> clone, double mBase, string separator, params Node[] nodes) :
            base(new Func<List<Node>, Node>(list => 
            {
                var carry = mBase;
                var retNodes = new List<Node>();
                retNodes.AddRange(list); // Don't ask me, why. Sth was wrong and I didn't have any clue what.
                var nodeCarry = new List<Node>();
                retNodes.ForEach(node =>
                {
                    if (node is MultiNode multiNode)
                    {
                        nodeCarry.AddRange(folder(multiNode));
                    }
                });
                retNodes.RemoveAll(node => node is MultiNode);
                retNodes.ForEach(node => carry = node is Value val ? operand(carry, val.GetValue) : carry);
                retNodes.RemoveAll(node => node is Value);
                return clone(retNodes);
            }),"",separator,nodes)
        {

        }
    }
}
