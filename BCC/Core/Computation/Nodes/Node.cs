using System;
using System.Collections.Generic;
using System.Linq;

namespace BCC.Core.Computation.Nodes
{
    class Node
    {
        private readonly List<Node> nodesUnder = new List<Node>();

        protected readonly Func<List<Node>, Node> functor;

        public Node(Func<List<Node>, Node> functor, string preSignature, string separator, params Node[] nodes)
        {
            this.functor = functor;
            this.PreSignature = preSignature;
            this.Separator = separator;
            this.nodesUnder.AddRange(nodes);
        }

        internal List<Node> NodesUnder => nodesUnder;

        public override bool Equals(object obj)
        {
            // Nodes are equal than both are nodes
            if(obj is Node node)
            {
                // Nodes are equal than their presignatures are equal
                if (!this.PreSignature.Equals(node.PreSignature)) return false;
                // Nodes are equal than their separators are equal
                if (!this.Separator.Equals(node.Separator)) return false;
                List<Node> objNodes = new List<Node>(node.nodesUnder);
                // If node a equals node b than there exists a bijection of identicity
                // from as undernodes to b undernodes
                foreach(var uNode in this.nodesUnder)
                {
                    if (!objNodes.Exists(n => n.Equals(uNode))) return false;
                    var found = objNodes.Find(n => n.Equals(uNode));
                    objNodes.Remove(found);
                }
                if (objNodes.Count > 0) return false;
                return true;
            }
            return false;
        }

        public virtual Node Apply(string varName, double value)
        {
            var applied = nodesUnder.ConvertAll<Node>(node => node.Apply(varName, value));
            return functor(applied);
        }

        private string PreSignature { get; }

        private string Separator { get; }

        public string Display()
        {
            var ret = "" + PreSignature;
            if (NodesUnder.Count < 1) return ret;
            ret += "(" + nodesUnder[0];
            foreach(var node in nodesUnder.Skip(1))
            {
                ret += "," + node;
            }
            return ret + ")";
        }

        public override int GetHashCode()
        {
            var hashCode = 118668325;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Node>>.Default.GetHashCode(nodesUnder);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Node>>.Default.GetHashCode(NodesUnder);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Signature);
            return hashCode;
        }
    }
}
