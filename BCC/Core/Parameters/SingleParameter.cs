using System;

namespace BCC.Core.Parameters
{
    public interface ISingleParameter : IParameter
    {
        object Get { get; }
        void Set(object value);
        object UpperBound { get; }
        object LowerBound { get; }
    }

    public interface ISingleEnableableParameter : IEnableable, ISingleParameter
    {

    }

    public abstract class SingleParameter<T> : Parameter<T>, ISingleParameter
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

        object ISingleParameter.Get => Get();

        object ISingleParameter.UpperBound => UpperBound;

        object ISingleParameter.LowerBound => LowerBound;

        public void AddValueChangedListener(Action<T> listener)
        {
            valueChangedListener += listener;
        }

        void ISingleParameter.Set(object value)
        {
            object converted = null;
            try
            {
                converted = Convert.ChangeType(value, typeof(T));
            }
            catch
            {

            }
            if(!(converted is null) && converted is T t)
            {
                Set(t);
            }
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

    public class EnableableIntParameter : IntParameter, ISingleEnableableParameter
    {
        public EnableableIntParameter(Func<string> nameCall, int lowerBound = 1, int upperBound = 100, int value = 1) : base(nameCall, lowerBound, upperBound, value)
        {
        }

        public bool Enabled { get; set; }
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

    public class EnableableFloatParameter : FloatParameter, ISingleEnableableParameter
    {
        public EnableableFloatParameter(Func<string> nameCall, double lowerBound = 0, double upperBound = 50000, double value = 0) : base(nameCall, lowerBound, upperBound, value)
        {
        }

        public bool Enabled { get; set; }
    }
}
