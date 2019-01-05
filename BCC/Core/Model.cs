using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core
{
    abstract class Model
    {
        protected readonly Dictionary<Enum, Action<string>> BubbleCalls = new Dictionary<Enum, Action<string>>();
        
        protected abstract Dictionary<Enum, Func<string>> NameCallGenerators();
    }
}
