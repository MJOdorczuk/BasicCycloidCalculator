using BCC.Core.Computation.Nodes.MultiNodes;
using BCC.Core.Computation.Nodes.Unions;
using System.Collections.Generic;

namespace BCC.Core.Computation.Nodes.UniNodes.SeriesNodes
{
    class SumSeries : SeriesNode
    {
        private static readonly Value sBase = new Value(0.0);

        public SumSeries(Node node, string iterator, int from, int to) : 
            base((acc,add) => 
            {
                switch(add)
                {
                    case Value val:
                        {
                            if (acc is Value val2) return new Value(val.GetValue + val2.GetValue);
                            else if (acc is Sum sum)
                                if (sum.NodesUnder[0] is Value v)
                                {
                                    var retNodes = new List<Node>(sum.NodesUnder)
                                    {
                                        [0] = new Value(val.GetValue + v.GetValue)
                                    };
                                    return new Sum(retNodes.ToArray());
                                }
                                else
                                {
                                    var retNodes = new List<Node>() { val };
                                    retNodes.AddRange(sum.NodesUnder);
                                    return new Sum(retNodes.ToArray());
                                }
                            else return new Sum(add, acc);
                        }
                    case Sum sum:
                        {
                            var retNodes = new List<Node>(sum.NodesUnder);
                            if (acc is Value val && sum.NodesUnder[0] is Value val2)
                                retNodes[0] = new Value(val.GetValue + val2.GetValue);
                            else if(acc is Sum sum2) retNodes.AddRange(sum2.NodesUnder);
                            else retNodes.Add(acc);
                            return new Sum(retNodes.ToArray());
                        }
                    default:
                        {
                            if(acc is Sum sum)
                                return new Sum(new List<Node>(sum.NodesUnder)
                                {
                                    add
                                }.ToArray());
                            return new Sum(acc, add);
                        }
                }
            }, "Σ", from, to, iterator, node, sBase)
        {
        }
    }
}
