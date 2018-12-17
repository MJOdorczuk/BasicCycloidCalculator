using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Controls
{
    abstract class CycloidGeometryControl : CycloidControl
    {
        protected Func<double, Tuple<double, double>> cycloidOutline;
        protected double scale;

        public Func<double, Tuple<double, double>> CycloidOutline => cycloidOutline;
        public double Scale => scale;

        internal abstract Func<double, Tuple<double, double>> EpicycloidOutlineGenerator(double ro, double lambda, double z, double g);
        internal abstract Func<double, Tuple<double, double>> HipocycloidOutlineGenerator(double ro, double lambda, double z, double g);
    }
}
