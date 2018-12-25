using System;

namespace BCC.Core.Computation.Nodes.UniNodes
{
    class SeriesNode : UniNode
    {
        private readonly string iterator;
        public SeriesNode(Func<Node, Node, Node> functor, string preSignature, int from, int to, string iterator, Node node, Node sBase) :
            base(new Func<Node, Node>(n => 
            {
                if (to > from) return sBase;
                Node ret = sBase;
                for(int i = from; i <= to; i++)
                {
                    ret = functor(sBase, node.Apply(iterator, i));
                }
                return ret;
            }), preSignature + "[" + from.ToString() + ":" + to.ToString() +"]", node)
        {
            this.iterator = iterator;
        }

        public override Node Apply(string varName, double value)
        {
            if (varName.Equals(iterator)) return functor(NodesUnder);
            else return base.Apply(varName, value);
        }
    }
}
