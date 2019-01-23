using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Miscs
{
    static class Mathematics
    {
        public static double Sum(Func<int, double> term, int count)
        {
            var ret = 0.0;
            for(int i = 0; i < count; i++)
            {
                ret += term(i);
            }
            return ret;
        }
        public static double Product(Func<int, double> term, int count)
        {
            var ret = 1.0;
            for(int i = 0; i < count; i++)
            {
                ret *= term(i);
            }
            return ret;
        }
    }
}
