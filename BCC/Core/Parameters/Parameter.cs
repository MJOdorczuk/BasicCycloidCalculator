using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Parameters
{
    public interface IParameter
    {

    }

    public abstract class Parameter<T> : IParameter
    {
        public T LowerBound { get; set; }
        public T UpperBound { get; set; }
        private readonly Func<string> nameCall;

        public Parameter(Func<string> nameCall, T lowerBound, T upperBound)
        {
            this.nameCall = nameCall;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        protected abstract T FitIntoBoundaries(T value);
        public Func<string> NameCall => () => nameCall();
    }

    public abstract class EnableableParameter<T> : Parameter<T>
    {
        public EnableableParameter(Func<string> nameCall, T lowerBound, T upperBound) : base(nameCall, lowerBound, upperBound)
        {
        }

        public bool IsEnabled { get; set; }
    }

    public abstract class ListParameter<T> : Parameter<T>
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

    public abstract class SingleParameter<T> : Parameter<T>
    {
        private T value;
        private Action<T> valueChangedListener = value => { };
        public SingleParameter(Func<string> nameCall, T lowerBound, T upperBound, T value = default(T)) 
            : base(nameCall, lowerBound, upperBound)
        {
            this.value = FitIntoBoundaries(value);
        }

        public Func<T> Get => () => value;
        public Action<T> Set => value =>
        {
            this.value = FitIntoBoundaries(value);
            valueChangedListener(value);
        };

        public void AddValueChangedListener(Action<T> listener)
        {
            valueChangedListener += listener;
        }
    }

    public class IntParameter : SingleParameter<int>
    {
        public IntParameter(Func<string> nameCall, int lowerBound = 1, int upperBound = 100, int value = 1) 
            : base(nameCall, lowerBound, upperBound, value)
        {

        }

        protected override int FitIntoBoundaries(int value)
        {
            if (value < LowerBound) return LowerBound;
            if (value > UpperBound) return UpperBound;
            else return value;
        }
    }

    public class FloatParameter : SingleParameter<double>
    {
        public FloatParameter(Func<string> nameCall, double lowerBound = 0.0, double upperBound = 50000.0, double value = 0.0) 
            : base(nameCall, lowerBound, upperBound, value)
        {
        }

        protected override double FitIntoBoundaries(double value)
        {
            if (value < LowerBound) return LowerBound;
            if (value > UpperBound) return UpperBound;
            else return value;
        }
    }

    public class BoolParameter : SingleParameter<bool>
    {
        public BoolParameter(Func<string> nameCall, bool value = false) : base(nameCall, false, true, value)
        {
        }

        protected override bool FitIntoBoundaries(bool value)
        {
            return value;
        }
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

    public class BoolListParameter : ListParameter<bool>
    {
        public BoolListParameter(Func<string> nameCall, int count = 1, bool value = false) : base(nameCall, false, true, count, value)
        {
        }

        protected override bool FitIntoBoundaries(bool value)
        {
            return value;
        }
    }
}
