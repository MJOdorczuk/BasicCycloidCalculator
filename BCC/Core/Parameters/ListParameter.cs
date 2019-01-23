using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

    public interface IListParameter : IParameter
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

    public interface IOutputListParameter : IParameter
    {
        object Get(int index);
        void Set(int index, object value);
        void Set(object[] values);
        List<object> Values { get; }
        int Count { get; set; }
        void AddSubscriber(TableParameterGroup parent);
    }

    public class OutputListParameter<T> : IOutputListParameter
    {
        private List<T> values = new List<T>();
        private readonly Func<string> nameCall;
        private Action<string> bubbleCall = message => { };
        private int count = 0;
        private readonly T defaultValue;
        private readonly List<TableParameterGroup> subscribers = new List<TableParameterGroup>();

        public OutputListParameter(Func<string> nameCall, T defaultValue = default)
        {
            this.nameCall = nameCall;
            this.defaultValue = defaultValue;
        }


        public Func<string> NameCall => () => nameCall();
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
        public T Get(int index)
        {
            return values[index];
        }
        public void Set(int index, T value)
        {
            if (index < count)
            {
                values[index] = value;
                subscribers.ForEach(x => x.Update(index));
            }
        }
        object IOutputListParameter.Get(int index)
        {
            return Get(index);
        }
        public void Set(int index, object value)
        {
            if (value is T t) Set(index, t);
        }
        public void AddSubscriber(TableParameterGroup parent)
        {
            subscribers.Add(parent);
        }

        public int Count { get => count; set
            {
                if(value >= 0)
                {
                    if (value < count) values.RemoveRange(value, count - value);
                    else
                    {
                        for (int i = count; i < value; i++)
                        {
                            values.Add(defaultValue);
                        }
                    }
                    count = value;
                }
            }
        }
        public List<T> Values => new List<T>(values);
        List<object> IOutputListParameter.Values => Values.ConvertAll<object>(x => x);
        public void Set(T[] values)
        {
            for(int i = 0; i < Math.Min(count, values.Length); i++)
            {
                Set(i, values[i]);
            }
        }

        public void Set(object[] values)
        {
            Set(values.ToList().ConvertAll<T>(x =>
            {
                if (x is T t) return t;
                else return defaultValue;
            }).ToArray());
        }
    }

    public class TableParameterGroup : IParameter
    {
        private readonly Func<string> nameCall;
        private Action<string> bubbleCall = message => { };
        private List<IOutputListParameter> parameters;
        private int count;
        private readonly DataTable data = new DataTable();
        private readonly List<DataRow> dataRows = new List<DataRow>();

        public TableParameterGroup(Func<string> nameCall, params IOutputListParameter[] parameters)
        {
            this.nameCall = nameCall;
            this.parameters = new List<IOutputListParameter>(parameters);
            Count = 0;
            foreach(var parameter in parameters)
            {
                var Column = new DataColumn
                {
                    ReadOnly = false
                };
                Vocabulary.AddNameCall(() => Column.ColumnName = parameter.NameCall());
                data.Columns.Add(Column);
                parameter.AddSubscriber(this);
            }
        }

        public Func<string> NameCall => () => nameCall();
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
        public List<IOutputListParameter> Parameters => new List<IOutputListParameter>(parameters);
        private object[] GetDataRow(int index)
        {
            return parameters.ConvertAll<object>(x =>
            {
                var value = x.Get(index);
                if (value is double d)
                {
                    if (d == 0) return 0;
                    if (Math.Log10(Math.Abs(d)) < -3) return d.ToString("E04");
                    if (Math.Log10(Math.Abs(d)) < -2) return d.ToString("0.00000");
                    if (Math.Log10(Math.Abs(d)) < -1) return d.ToString("0.0000");
                    if (Math.Log10(Math.Abs(d)) < 0) return d.ToString("0.000");
                    else return d.ToString("0.00");
                }
                else return value;
            }).ToArray();
        }
        public int Count { get => count; set
            {
                if (value >= 0)
                {
                    foreach (var parameter in parameters) parameter.Count = value;
                    while(data.Rows.Count > value)
                    {
                        dataRows.RemoveAt(value);
                        data.Rows.RemoveAt(value);
                    }
                    for (int i = count; i < value; i++)
                        {
                            var Row = data.NewRow();
                            Row.ItemArray = GetDataRow(i);
                            dataRows.Add(Row);
                            data.Rows.Add(Row);
                        }
                    count = value;
                }
            }
        }
        public void Update(int index)
        {
            dataRows[index].ItemArray = GetDataRow(index);
        }
        public DataTable Data => data;
    }


}
