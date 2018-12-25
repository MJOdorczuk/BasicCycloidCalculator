namespace BCC.Core.Computation.Nodes.Unions
{
    class Value : Union
    {
        public double GetValue { get; }
        public Value(double value) : base(() => new Value(value), value.ToString())
        {
            this.GetValue = value;
        }
    }
}
