using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Parameters
{
    /// <summary>
    /// Interface for all parameters, parameters lists, groups and so on
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Functor for calling parameter's name or label
        /// </summary>
        Func<string> NameCall { get; }
        /// <summary>
        /// Action for calling tooltip bubble
        /// </summary>
        /// <param name="message"> message displayed on tooltip</param>
        void CallBubble(string message);
        /// <summary>
        /// Add action to execute when the tooltip bubble is called
        /// </summary>
        /// <param name="call"></param>
        void AddBubbleCall(Action<string> call);
    }
    /// <summary>
    /// Interface for choosable parameters with possibility of checking enability property
    /// </summary>
    public interface IEnableable
    {
        bool Enabled { get; set; }
    }

    public abstract class Parameter<T> : IParameter
    {
        private Action<string> bubbleCall;
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
        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }
        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
    }

    /*public abstract class GroupParameter
    {
        private class GroupLabel
        {
            private Func<string> nameCall;
            private List<IParameter> subParameters;

            public Func<string> NameCall => () => nameCall();
            public void Add(IParameter parameter)
            {
                subParameters.Add(parameter);
            }
        }

        private class GroupOptionLabel
        {
            private Func<string> nameCall;
            public Func<string> NameCall => () => nameCall();
        }

        private List<GroupLabel> predefines;
        private List<Func<string>> nameCalls;
        private int index;
        public bool IsCustomizable { get; private set; }

        public Func<string> NameCall => nameCalls[index];
        public void AddSubParameter(Func<IParameter> parameterConstructor)
        {
            foreach(var predefine in predefines)
            {
                predefine.Add(parameterConstructor());
            }
        }
    }*/

    public class OutputSingleParameter : IParameter
    {
        private object value;
        private Func<string> nameCall;
        private Action<object> changeListener;
        private Action<string> bubbleCall;

        public OutputSingleParameter(Func<string> nameCall, object value)
        {
            this.nameCall = nameCall;
            this.value = value;
        }

        public Action<object> Set => value =>
        {
            this.value = value;
            changeListener(value);
        };
        public object Get => value;

        public Func<string> NameCall => () => nameCall();

        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }

        public void AddListener(Action<object> listener)
        {
            changeListener += listener;
        }

        public void CallBubble(string message)
        {
            bubbleCall(message);
        }
    }

    public class OutputParameterList : IParameter
    {
        private Func<string> nameCall;
        private Action<string> bubbleCall;
        private List<OutputSingleParameter> parameters;
        public OutputParameterList(Func<string> nameCall, params OutputSingleParameter[] parameters)
        {
            this.parameters = new List<OutputSingleParameter>(parameters);
            this.nameCall = nameCall;
        }
        public void AddParameter(OutputSingleParameter parameter)
        {
            parameters.Add(parameter);
        }

        public void CallBubble(string message)
        {
            bubbleCall(message);
        }

        public void AddBubbleCall(Action<string> call)
        {
            bubbleCall += call;
        }

        public List<OutputSingleParameter> Parameters => new List<OutputSingleParameter>(parameters);
        /// <summary>
        /// Functor returning list's name
        /// </summary>
        public Func<string> NameCall => () => nameCall();
    }
}
