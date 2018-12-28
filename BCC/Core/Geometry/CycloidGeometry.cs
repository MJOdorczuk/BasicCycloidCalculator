using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Geometry
{
    static class CycloidGeometry
    {
        public static double TRUE = 1.0, FALSE = 2.0;

        // z and g are necessary
        // two other variables necessary can be pairs of
        // da and df
        // da and h (2*e)
        // da and e
        // da and dg
        // df and h
        // df and e
        // df and dg
        // dg and e
        // dg and h
        private static double da, df, e, dg, g; // input
        private static double lambda, dw, ro, db; // output
        private static int z;
        private static bool epi;
        public static readonly List<List<CycloParams>> PossibleCliques = new List<List<CycloParams>>()
        {
            new List<CycloParams>(){CycloParams.DA, CycloParams.DF },
            new List<CycloParams>(){CycloParams.DA, CycloParams.DG },
            new List<CycloParams>(){CycloParams.DA, CycloParams.E },
            new List<CycloParams>(){CycloParams.DA, CycloParams.H },
            new List<CycloParams>(){CycloParams.DF, CycloParams.DG },
            new List<CycloParams>(){CycloParams.DF, CycloParams.E },
            new List<CycloParams>(){CycloParams.DF, CycloParams.H },
            new List<CycloParams>(){CycloParams.DG, CycloParams.E },
            new List<CycloParams>(){CycloParams.DG, CycloParams.H }
        };

        static CycloidGeometry()
        {
            Reset();
        }

        public static void Reset()
        {
            da = df = e = dg = g = lambda = dw = ro = db = z = 0;
            epi = true;
        }

        public static void Calculate()
        {
            if(da > 0)
            {
                if(df > 0)
                {
                    e = (da - df) * (epi ? 4 : -4);
                    ro = ((da / 2) + g + (epi ? -e : e)) / (z + (epi ? 1 : -1));
                    lambda = e / ro;
                    dg = ro * (z + (epi ? 1 : -1));
                }
                else if(dg > 0)
                {
                    ro = dg / (2 * (z + (epi ? 1 : -1)));
                    lambda = (epi ? 1 : -1) * (0.5 * da - ro * (z + (epi ? 1 : -1) + g) / (2 * ro));
                    e = lambda * ro;
                    df = 2 * (ro * (z + (epi ? 1 : -1) - lambda) - g);
                }
                else if(e > 0)
                {
                    df = da + (epi ? -4 : 4) * e;
                    ro = ((da / 2) + g + (epi ? -e : e)) / (z + (epi ? 1 : -1));
                    lambda = e / ro;
                    dg = ro * (z + (epi ? 1 : -1));
                }
            }
            else if(df > 0)
            {
                if(dg > 0)
                {
                    ro = dg / (2 * (z + (epi ? 1 : -1)));
                    lambda = (epi ? -1 : 1) * (0.5 * df - ro * (z + (epi ? 1 : -1)) + g) / (2 * ro);
                    e = lambda * ro;
                    da = 2 * (ro * (z + (epi ? 1 : -1) + lambda) - g);
                }
                else if(e > 0)
                {
                    da = df + (epi ? -4 : 4) * e;
                    ro = ((da / 2) + g + (epi ? -e : e)) / (z + (epi ? 1 : -1));
                    lambda = e / ro;
                    dg = ro * (z + (epi ? 1 : -1));
                }
            }
            else if(dg > 0 && e > 0)
            {
                ro = dg / (2 * (z + (epi ? 1 : -1)));
                lambda = e / ro;
                da = 2 * (ro * (z + (epi ? 1 : -1) + lambda) - g);
                df = da + (epi ? -4 : 4) * e;
            }
            db = z * ro;
            dw = e * z;
        }
    }
}
