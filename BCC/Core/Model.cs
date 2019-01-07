using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core
{
    abstract class Model
    {
        public static readonly double NULL = double.NegativeInfinity, TRUE = 1.0, FALSE = 2.0;
        public static readonly int VALUE_PRECISION = 3;
        public static readonly int TOLERANCE_PRECISION = 5;
        protected delegate void SetValueCallBack(object value);

        protected readonly Dictionary<Enum, Action<string>> BubbleCalls = new Dictionary<Enum, Action<string>>();
        protected abstract List<Enum> ObligatoryIntParams();
        protected abstract List<Enum> ObligatoryFloatParams();
        protected abstract Dictionary<Enum, Func<string>> NameCallGenerators();
    }
}
