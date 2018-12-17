using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Controls
{
    abstract class CycloidTensionControl : CycloidControl
    {
        protected Func<double, int, double> tensionFunction;

        public Func<double, int, double> TensionFunction => tensionFunction;

        internal abstract Func<double, int, double> TensionFunctionGenerator(int zw, double M, double dw, double[] delta);
    }
}
