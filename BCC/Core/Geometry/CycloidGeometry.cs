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
                    e = (da - df) / (epi ? 4 : -4); // CHECKED
                    ro = ((da / 2) + (epi ? g - e : e - g)) / (z + (epi ? 1 : -1)); // CHECKED
                    lambda = e / ro; // CHECKED
                    dg = 2 * ro * (z + (epi ? 1 : -1)); // CHECKED
                }
                else if(dg > 0)
                {
                    ro = dg / (2 * (z + (epi ? 1 : -1))); // CHECKED
                    lambda = (epi ? 1 : -1) * (0.5 * da - ro * (z + (epi ? 1 : -1)) + (epi ? g : - g)) / ro; // CHECKED
                    e = lambda * ro; // CHECKED
                    df = 2 * (ro * (z + (epi ? 1 : -1) - lambda) + (epi ? -g : g)); // CHECKED
                }
                else if(e > 0)
                {
                    df = da + (epi ? -4 : 4) * e; // CHECKED
                    ro = ((da / 2) + (epi ? g - e : e - g)) / (z + (epi ? 1 : -1)); // CHECKED
                    lambda = e / ro; // CHECKED
                    dg = 2 * ro * (z + (epi ? 1 : -1)); // CHECKED
                }
            }
            else if(df > 0)
            {
                if(dg > 0)
                {
                    ro = dg / (2 * (z + (epi ? 1 : -1))); // CHECKED
                    lambda = (epi ? -1 : 1) * (0.5 * df - ro * (z + (epi ? 1 : -1)) + (epi ? g : -g)) / ro;
                    e = lambda * ro;
                    da = 2 * (ro * (z + (epi ? 1 + lambda : - 1 - lambda)) + (epi ? -g : g));
                }
                else if(e > 0)
                {
                    da = df + (epi ? 4 : -4) * e; // CHECKED
                    ro = ((da / 2) + (epi ? g - e : e - g)) / (z + (epi ? 1 : -1)); // CHECKED
                    lambda = e / ro; // CHECKED
                    dg = 2 * ro * (z + (epi ? 1 : -1)); // CHECKED
                }
            }
            else if(dg > 0 && e > 0)
            {
                ro = dg / (2 * (z + (epi ? 1 : -1)));
                lambda = e / ro;
                da = 2 * (ro * (z + (epi ? 1 : -1) + lambda) - g);
                df = da + (epi ? -4 : 4) * e;
            }
            db = 2 * z * ro; // Checked
            dw = 2 * e * z; // Checked
        }

        private static double H { get => e * 2; set => e = value / 2; }

        public static bool AllIsSet =>
            da > 0 && df > 0 && e > 0 && lambda > 0 && dw > 0 &&
            ro > 0 && dg > 0 && db > 0 && g > 0 && z > 0;

        public static bool CurveReq =>
            lambda <= 1 && lambda >= ((z + (epi ? -1 : 1)) / (2 * z + (epi ? 1 : -1)));

        public static bool CutReq =>
            e >= Math.Sqrt(lambda * lambda / (1 - lambda * lambda))
            * Math.Sqrt(1 + (epi ? 2 / z : -2 / z))
            * (z + (epi ? 2 : -2)) / (Math.Sqrt(27) * (z + (epi ? 1 : -1))) * g;

        public static bool NeighReq =>
            e > g * lambda / ((z + (epi ? 1 : 0)) * Math.Sin(Math.PI / (z + (epi ? 1 : -1))));

        public static void Set(CycloParams param, double val)
        {
            switch (param)
            {
                case CycloParams.Z:
                    z = (int)val;
                    break;
                case CycloParams.G:
                    g = val;
                    break;
                case CycloParams.DA:
                    da = val;
                    break;
                case CycloParams.DF:
                    df = val;
                    break;
                case CycloParams.E:
                    e = val;
                    break;
                case CycloParams.H:
                    H = val;
                    break;
                case CycloParams.DG:
                    dg = val;
                    break;
                case CycloParams.Λ:
                    lambda = val;
                    break;
                case CycloParams.DW:
                    dw = val;
                    break;
                case CycloParams.Ρ:
                    ro = val;
                    break;
                case CycloParams.DB:
                    db = val;
                    break;
                case CycloParams.EPI:
                    epi = val == TRUE;
                    break;
            }
        }

        public static double Get(CycloParams param)
        {
            switch (param)
            {
                case CycloParams.Z:
                    return z;
                case CycloParams.G:
                    return g;
                case CycloParams.DA:
                    return da;
                case CycloParams.DF:
                    return df;
                case CycloParams.E:
                    return e;
                case CycloParams.H:
                    return H;
                case CycloParams.DG:
                    return dg;
                case CycloParams.Λ:
                    return lambda;
                case CycloParams.DW:
                    return dw;
                case CycloParams.Ρ:
                    return ro;
                case CycloParams.DB:
                    return db;
                case CycloParams.EPI:
                    return epi ? TRUE : FALSE;
            }
            return 0;
        }

        public static Dictionary<CycloParams, double> GetAll()
        {
            var ret = new Dictionary<CycloParams, double>();
            var enumValues = Enum.GetValues(typeof(CycloParams));
            foreach (CycloParams param in Enum.GetValues(typeof(CycloParams)))
            {
                var val = Get(param);
                if (val > 0) ret.Add(param, val);
            }
            return ret;
        }
    }
}
