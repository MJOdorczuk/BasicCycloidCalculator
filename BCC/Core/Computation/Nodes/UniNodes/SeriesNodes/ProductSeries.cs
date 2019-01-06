using BCC.Core.Computation.Nodes.MultiNodes;
using BCC.Core.Computation.Nodes.Unions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Computation.Nodes.UniNodes.SeriesNodes
{
    class ProductSeries : SeriesNode
    {
        private static readonly Value sBase = new Value(1.0);

        public ProductSeries(Node node, string iterator, int from, int to) :
            base((acc, add) =>
            {
                switch (add)
                {
                    case Value val:
                        {
                            if (acc is Value val2) return new Value(val.GetValue * val2.GetValue);
                            else if (acc is Product product)
                                if (product.NodesUnder[0] is Value v)
                                {
                                    var retNodes = new List<Node>(product.NodesUnder)
                                    {
                                        [0] = new Value(val.GetValue + v.GetValue)
                                    };
                                    return new Product(retNodes.ToArray());
                                }
                                else
                                {
                                    var retNodes = new List<Node>() { val };
                                    retNodes.AddRange(product.NodesUnder);
                                    return new Product(retNodes.ToArray());
                                }
                            else return new Product(add, acc);
                        }
                    case Product product:
                        {
                            var retNodes = new List<Node>(product.NodesUnder);
                            if (acc is Value val && product.NodesUnder[0] is Value val2)
                                retNodes[0] = new Value(val.GetValue * val2.GetValue);
                            else if (acc is Product product2) retNodes.AddRange(product2.NodesUnder);
                            else retNodes.Add(acc);
                            return new Product(retNodes.ToArray());
                        }
                    default:
                        {
                            if (acc is Product product)
                                return new Sum(new List<Node>(product.NodesUnder)
                                {
                                    add
                                }.ToArray());
                            return new Product(acc, add);
                        }
                }
            }, "Π", from, to, iterator, node, sBase)
        {
        }
    }
}
