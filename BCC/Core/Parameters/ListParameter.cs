using System;
using System.Collections.Generic;

namespace BCC.Core.Parameters
{
    public abstract class ListParameter<T> : Parameter<T>, IListParameter
    {
        private List<T> values;

        public ListParameter(Func<string> nameCall, T lowerBound, T upperBound, int count = 1, T value = default(T)) 
            : base(nameCall, lowerBound, upperBound)
        {
            values = new List<T>();
            for(int i = 0; i < count; i++)
            {
                values.Add(FitIntoBoundaries(value));
            }
        }

        public Func<int, T> Get => i => values[i];
        public Action<int, T> Set => (i, value) =>
        {
            values[i] = FitIntoBoundaries(value);
        };
        public int Count => values.Count;
        public void SetCount(int count)
        {
            if (count < 1) values.Clear();
            else if(count > values.Count)
            {
                for(int i = 0; i < count - values.Count; i++)
                {
                    values.Add(default(T));
                }
            }
            else if(count < values.Count)
            {
                values.RemoveRange(count, values.Count - count);
            }
        }
    }

    interface IListParameter : IParameter
    {
        int Count { get; }
        void SetCount(int count);
    }

    public class IntListParameter : ListParameter<int>
    {
        public IntListParameter(Func<string> nameCall, int lowerBound = 1, int upperBound = 100, int count = 1, int value = 1)
            : base(nameCall, lowerBound, upperBound, count, value)
        {
        }

        protected override int FitIntoBoundaries(int value)
        {
            if (value < LowerBound) return LowerBound;
            if (value > UpperBound) return UpperBound;
            else return value;
        }
    }

    public class FloatListParameter : ListParameter<double>
    {
        public FloatListParameter(Func<string> nameCall, double lowerBound = 0.0, double upperBound = 50000.0, int count = 1, double value = 0.0)
            : base(nameCall, lowerBound, upperBound, count, value)
        {
        }

        protected override double FitIntoBoundaries(double value)
        {
            if (value < LowerBound) return LowerBound;
            if (value > UpperBound) return UpperBound;
            else return value;
        }
    }
}
