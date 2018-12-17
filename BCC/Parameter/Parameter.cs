using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC
{
    class Parameter
    {
        private readonly string parameterName;
        private readonly Type parameterType;
        private bool necessary;

        public Parameter(string parameterName, Type parameterType, bool necessary)
        {
            this.parameterName = parameterName;
            this.parameterType = parameterType;
            this.Necessary = necessary;
        }

        public bool Necessary { get => necessary; set => necessary = value; }
        public Type ParameterType => parameterType;
        public string ParameterName => parameterName;
    }
}
