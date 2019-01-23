using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BCC.Core.Parameters
{
    public class UpDownParameterGroup : IParameter
    {
        /*
         Default display:
            check box for enability of whole group
                if not enabled then every parameter
                shall be equal to its default value
            Panel with list for choosing focus
                the focused element shall be displayed
                and enabled to get or set value
            Two group boxes
               -one for inputting upper and lower values
                those should be entangled with focused 
                parameter and set index (0, if indexing not enabled)
               -another with check box for enabling indexing and
                input for setting the index
             */
        private List<IUpDownParameter> parameters;
        private bool enabled;
        // Name call of whole group
        private readonly Func<string> nameCall;
        private Action<int> countChangedListener = count => { };
        // Bubble call of whole group
        private Action<string> bubbleCall = message => { };
        /// <summary>
        /// Determines, which parameter should be displayed on UI
        /// </summary>
        public IUpDownParameter Focus { get; set; }
        private int index;
        private int count;
        /// <summary>
        /// Determines if list parameters should be treated as such
        /// or as single parameters
        /// </summary>
        private bool indexingEnabled = false;
        public bool IndexingEnabled { get => indexingEnabled; set
            {
                indexingEnabled = value;
                foreach(var param in parameters)
                {
                    if(param is UpDownListParameter udlp)
                    {
                        udlp.IndexingEnabled = value;
                    }
                }
            } }
        /// <summary>
        /// Determines if values of the parameters included in this group 
        /// should be considered or only their default values
        /// </summary>
        public bool Enabled { get => enabled; set
            {
                enabled = value;
                foreach(var parameter in parameters)
                {
                    parameter.Enabled = value;
                }
            } }


        // ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameCall"> Name call for whole group</param>
        public UpDownParameterGroup(Func<string> nameCall)
        {
            this.nameCall = nameCall;
            parameters = new List<IUpDownParameter>();
            SetCount(1);
            Focus = null;
        }


        // miscs
        /// <summary>
        /// Name call for whole group
        /// </summary>
        public Func<string> NameCall => () => nameCall();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"> Bubble call for whole group</param>
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        /// <summary>
        /// Calls bubble call for whole group
        /// </summary>
        /// <param name="message"> message to display in the bubble</param>
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
        /// <summary>
        /// Adds action to be fired when the group parameter count is changed
        /// </summary>
        /// <param name="listener"></param>
        public void AddCountChangedListener(Action<int> listener)
        {
            countChangedListener += listener;
        }


        // parameter
        public void AddParameter(IUpDownParameter parameter)
        {
            parameters.Add(parameter);
            if (Focus is null) Focus = parameter;
            if (parameter is UpDownListParameter udlp)
            {
                udlp.SetCount(count);
            }
        }
        public List<IUpDownParameter> Parameters => new List<IUpDownParameter>(parameters);
        /// <summary>
        /// Focused index
        /// </summary>
        public int Index { get { return index; } set { if (value < count) index = value; } }
        /// <summary>
        /// Sets count of considered elements in all listed list parameters
        /// </summary>
        /// <param name="count"> If it is larger than elements count new elements will
        /// be initialised with default values</param>
        public void SetCount(int count)
        {
            if(count > 0)
            {
                foreach (var param in Parameters)
                {
                    if (param is UpDownListParameter list)
                    {
                        list.SetCount(count);
                    }
                }
                this.count = count;
                countChangedListener(count);
            }
        }
        public int Count => count;


        // I/O
        public double Upper
        {
            get
            {
                if (Enabled)
                {
                    if (IndexingEnabled)
                    {
                        return Focus.Upper(index);
                    }
                    else return Focus.Upper(0);
                }
                else return Focus.DefaultUpper;
            }
            set
            {
                if (Enabled)
                {
                    if (IndexingEnabled)
                    {
                        Focus.Upper(index, value);
                    }
                    else Focus.Upper(0, value);
                }
            }
        }
        public double Lower
        {
            get
            {
                if (Enabled)
                {
                    if (IndexingEnabled)
                    {
                        return Focus.Lower(index);
                    }
                    else return Focus.Lower(0);
                }
                else return Focus.DefaultLower;
            }
            set
            {
                if (Enabled)
                {
                    if (IndexingEnabled)
                    {
                        Focus.Lower(index, value);
                    }
                    else Focus.Lower(0, value);
                }
            }
        }
    }

    /// <summary>
    /// Interface for elements listed in UpDownParameterGroup object
    /// Preferably with pair (or list of pairs) of floating-point values
    /// Default use for engineering tolerances
    /// </summary>
    public interface IUpDownParameter : IParameter
    {
        /// <summary>
        /// Gets the upper value under given index
        /// If parameter is single parameter then index is not considered
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        double Upper(int index);
        /// <summary>
        /// Sets the upper value to the value parameter under given index
        /// If parameter is single parameter the index is not considered
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void Upper(int index, double value);
        /// <summary>
        /// Gets the lower value under given index
        /// If parameter is single parameter then index is not considered
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        double Lower(int index);
        /// <summary>
        /// Sets the lower value to the value parameter under given index
        /// If parameter is single parameter the index is not considered
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void Lower(int index, double value);
        /// <summary>
        /// Gives the default upper value of the parameter
        /// </summary>
        double DefaultUpper { get; }
        /// <summary>
        /// Gives the default lower value of the parameter
        /// </summary>
        double DefaultLower { get; }
        bool Enabled { get; set; }
    }

    public class UpDownListParameter : IUpDownParameter, IEnumerator<Tuple<double, double>>
    {
        private List<double> uppers, lowers;
        private readonly Func<string> nameCall;
        private Action<string> bubbleCall = message => { };
        private int count;
        private int index;
        public bool IndexingEnabled { get; set; }


        // ctor
        public UpDownListParameter(Func<string> nameCall, double defaultLower = 0.0, double defaultUpper = 0.0)
        {
            uppers = new List<double>() { 0.0 };
            lowers = new List<double>() { 0.0 };
            this.nameCall = nameCall;
            DefaultLower = defaultLower;
            DefaultUpper = defaultUpper;
        }


        // miscs
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
        public Func<string> NameCall => () => nameCall();


        // values controling part
        public double Upper(int index) { return Enabled ? uppers[index] : DefaultUpper; }
        public double Lower(int index) { return Enabled ? lowers[index] : DefaultLower; }
        public List<double> Uppers
        {
            get
            {
                if (IndexingEnabled && Enabled) return new List<double>(uppers.Take(count));
                else
                {
                    var ret = new List<double>();
                    for(int i = 0; i < count; i++)
                    {
                        ret.Add(Enabled ? uppers[0] : DefaultUpper);
                    }
                    return ret;
                }
            }
        }
        public List<double> Lowers
        {
            get
            {
                if (IndexingEnabled && Enabled) return new List<double>(lowers.Take(count));
                else
                {
                    var ret = new List<double>();
                    for (int i = 0; i < count; i++)
                    {
                        ret.Add(Enabled ? lowers[0] : DefaultUpper);
                    }
                    return ret;
                }
            }
        }
        public double DefaultUpper { get; private set; }
        public double DefaultLower { get; private set; }
        public void Upper(int index, double value)
        {
            if (index < count) uppers[index] = value;
        }
        public void Lower(int index, double value)
        {
            if (index < count) lowers[index] = value;
        }
        public void SetCount(int count)
        {
            this.count = count;
            while (uppers.Count < count)
            {
                uppers.Add(DefaultUpper);
                lowers.Add(DefaultLower);
            }
        }


        // IEnumerator part
        public void Dispose(){}
        public bool MoveNext()
        {
            index++;
            if(index >= count)
            {
                index = count - 1;
                return false;
            }
            return true;
        }
        public void Reset()
        {
            index = 0;
        }
        public Tuple<double, double> Current => new Tuple<double, double>(uppers[index], lowers[index]);
        object IEnumerator.Current => Current;

        public bool Enabled { get; set; }
    }

    public class UpDownSingleParameter : IUpDownParameter
    {
        private readonly Func<string> nameCall;
        private Action<string> bubbleCall = message => { };
        private double lower, upper;
        

        // ctor
        public UpDownSingleParameter(Func<string> nameCall, double lower = 0.0, double upper = 0.0)
        {
            this.nameCall = nameCall;
            DefaultLower = lower;
            DefaultUpper = upper;
            Lower = lower;
            Upper = upper;
        }


        // miscs
        public Func<string> NameCall => () => nameCall();
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }


        // value controling part
        double IUpDownParameter.Upper(int index) => Upper;
        void IUpDownParameter.Upper(int index, double value) => Upper = value;
        double IUpDownParameter.Lower(int index) => Lower;
        void IUpDownParameter.Lower(int index, double value) => Lower = value;
        public double Lower { get => Enabled ? lower : DefaultLower; set => lower = value; }
        public double Upper { get => Enabled ? upper : DefaultUpper; set => upper = value; }
        public double DefaultUpper { get; private set; }
        public double DefaultLower { get; private set; }
        public bool Enabled { get; set; }
    }
}
