namespace BCC.Core.Computation.Nodes.Unions
{
    class Variable : Union
    {

        public Variable(string name) : base(() => new Variable(name), name)
        {
        }

        public override Node Apply(string varName, double value)
        {
            if (varName.Equals(this.Display())) return new Value(value);
            else return new Variable(this.Display());
        }
    }
}
