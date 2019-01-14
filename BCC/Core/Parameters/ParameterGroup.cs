using BCC.Miscs;
using System;
using System.Collections.Generic;

namespace BCC.Core.Parameters
{
    public class ParameterGroup : IParameter
    {
        private Func<string> nameCall;
        private int index;
        private Action<string> bubbleCall = message => { };
        public bool Customable { get; private set; }
        private List<ISingleEnableableParameter> parameters;
        private List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>> predefined;

        private ParameterGroup(Func<string> nameCall, bool customable, List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>> predefined, List<ISingleEnableableParameter> parameters)
        {
            this.nameCall = nameCall;
            this.Customable = customable;
            this.predefined = predefined;
            this.parameters = parameters;
            index = 0;
        }

        /// <summary>
        /// Functor returning group's name
        /// </summary>
        public Func<string> NameCall => () => nameCall();
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
        public List<ISingleEnableableParameter> Parameters => new List<ISingleEnableableParameter>(parameters);
        public List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>> Predefined =>
            new List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>>(predefined);
        /// <summary>
        /// Gets the current selected index (0 is custom if customable)
        /// </summary>
        public int Get => index;
        /// <summary>
        /// Sets the current selected index (0 is custom if customable)
        /// </summary>
        /// <param name="index"></param>
        public void Set(int index)
        {
            if(Customable)
            {
                if(index == 0)
                {
                    foreach(var param in parameters)
                    {
                        param.Enabled = true;
                    }
                }
                index--;
            }
            if(index >= 0)
                foreach (var param in predefined[index].Item2)
                {
                    param.Key.Set(param.Value);
                    param.Key.Enabled = false;
                }
        }

        public class ParameterGroupGenerator
        {
            private Func<string> nameCall;
            private List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>> predefined;
            private List<ISingleEnableableParameter> parameters;
            private readonly bool customable;

            public ParameterGroupGenerator(Func<string> nameCall, List<ISingleEnableableParameter> parameters, bool customable = false)
            {
                this.parameters = new List<ISingleEnableableParameter>(parameters);
                this.customable = customable;
                predefined = new List<Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>>();
                this.nameCall = nameCall;
            }

            public int AddPredefine(Func<string> call, Dictionary<ISingleEnableableParameter, object> predefinedValues)
            {
                var values = new Dictionary<ISingleEnableableParameter, object>();
                foreach (var param in parameters)
                {
                    if (predefinedValues.ContainsKey(param))
                    {
                        values.Add(param, predefinedValues[param]);
                    }
                    else
                    {
                        values.Add(param, param.Get);
                    }
                }
                predefined.Add(new Tuple<Func<string>, Dictionary<ISingleEnableableParameter, object>>(call, values));
                return predefined.Count;
            }

            public ParameterGroup Generate()
            {
                if(!customable && predefined.Count < 1)
                {
                    throw new Exception("Not enough predefined states");
                }
                return new ParameterGroup(nameCall, customable, predefined, parameters);
            }
        }
    }
}
