using BCC.Interface_View.StandardInterface.Tension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Load
{
    public enum LoadParams
    {
        EHOLE,
        ESHAFT,
        ΝHOLE,
        ΝSHAFT,
        M,
        RW
    }

    

    class LoadModel
    {
        // The view and the control for geometry computation
        protected LoadMenu view = null;

        protected readonly Dictionary<LoadParams, Action<string>> BubbleCalls = new Dictionary<LoadParams, Action<string>>();

        public static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 360;
            public static readonly int VALUE_PRECISION = 3;
            public static readonly double TRUE = 1.0, FALSE = 2.0;
            public static readonly double NULL = double.NegativeInfinity;
            
        }

        private static class NameCalls
        {
            // Name calls for parameters
            public static Func<string> CallName(LoadParams param)
            {
                throw new NotImplementedException();
            }

            
            
        }

        //public LoadMenu GetLoadMenu
    }
}
