using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Archivised.Controls
{
    class Cycloid
    {
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
        private double da, df, e,  dg, g; // input
        private double lambda, dw, ro, db; // output
        private uint z;
        private bool epi;

        public Cycloid()
        {
            da = df = e = lambda = dw = ro = dg = db = g = z = 0;
            epi = true;
        }
        public void Reset()
        {
            da = df = e = dg = g = lambda = dw = ro = db = z = 0;
            epi = true;
        }
        public void Calculate()
        {
            if (!BaseValuesSet) return;
            if (da > 0)
            {
                if (df > 0)
                {
                    e = (da - df) / 4;
                    ComputeLambda();
                    ro = e / lambda;
                    ComputeDg();
                    ComputeDb();
                    Dw = e * z;
                }
                else if (e > 0)
                {
                    df = da - 4 * e;
                    ComputeLambda();
                    ro = e / lambda;
                    ComputeDg();
                    ComputeDb();
                    Dw = e * z;
                }
                else if (dg > 0)
                {
                    if (epi)
                    {
                        ro = dg / (2 * (z + 1));
                    }
                    else
                    {
                        ro = dg / (2 * (z - 1));
                    }
                    ComputeLambdaFromRa();
                    ComputeDb();
                    e = lambda * ro;
                    ComputeDf();
                    Dw = e * z;
                }
            }
            else if (df > 0)
            {
                if (e > 0)
                {
                    da = df / 2 + 2 * e;
                    ComputeLambda();
                    ro = e / lambda;
                    ComputeDg();
                    ComputeDb();
                    Dw = e * z;
                }
                else if (dg > 0)
                {
                    if (epi)
                    {
                        ro = dg / (2 * (z + 1));
                    }
                    else
                    {
                        ro = dg / (2 * (z - 1));
                    }
                    ComputeLambdaFromRf();
                    ComputeDb();
                    e = lambda * ro;
                    ComputeDa();
                    Dw = e * z;
                }
            }
            else if (dg > 0 && e > 0)
            {
                if (epi)
                {
                    ro = Rg/ (z + 1);
                }
                else
                {
                    ro = Rg/ (z - 1);
                }
                lambda = e / ro;
                ComputeDb();
                ComputeDa();
                ComputeDf();
                Dw = e * z / 2;
            }
        }
        private void ComputeLambda()
        {
            if (epi)
            {
                Double rag = 1 / (Ra+ g);
                Double rfg = 1 / (Rf+ g);
                lambda = (z + 1) * ((rfg - rag) / (rag + rfg));
            }
            else
            {
                Double rag = 1 / (Ra- g);
                Double rfg = 1 / (Rf- g);
                lambda = (z - 1) * ((rfg - rag) / (rag + rfg));
            }
        }
        private void ComputeLambdaFromRa()
        {
            if (epi)
            {
                lambda = (Ra+ g) / ro - z - 1;
            }
            else
            {
                lambda = (g - Ra) / ro + z - 1;
            }
        }
        private void ComputeLambdaFromRf()
        {
            if (epi)
            {
                lambda = z + 1 - ((Rf+ g) / ro);
            }
            else
            {
                lambda = ((Rf- g) / ro) - z + 1;
            }
        }
        private void ComputeDg()
        {
            if (epi)
            {
                dg = 2 * ro * (z + 1);
            }
            else
            {
                dg = 2 * ro * (z - 1);
            }
        }
        private void ComputeDb()
        {
            if (epi)
            {
                Db = ro * z;
            }
            else
            {
                Db = ro * (z - 1);
            }
        }
        private void ComputeDa()
        {
            if (epi)
            {
                da = 2 * (ro * (z + 1 + lambda) - g);
            }
            else
            {
                da = 2 * (ro * (z - 1 - lambda) + g);
            }
        }
        private void ComputeDf()
        {
            if (epi)
            {
                df = 2 * (ro * (z + 1 - lambda) - g);
            }
            else
            {
                df = 2 * (ro * (z - 1 + lambda) + g);
            }
        }
        public uint Z { get => z; set => z = value; }
        public double G { get => g; set => g = value; }
        public double Da { get => da; set => da = value; }
        public double Ra { get => da / 2; set => da = value * 2; }
        public double Ro { get => ro; set => ro = value; }
        public double Lambda { get => lambda; set => lambda = value; }
        public double Df { get => df; set => df = value; }
        public double Rf { get => df / 2; set => df = value * 2; }
        public double H { get => e * 2; set => e = value/2; }
        public double E { get => e; set => e = value; }
        public double Dg { get => dg; set => dg = value; }
        public double Rg { get => dg / 2; set => dg = value * 2; }
        public void MakeEpicycloid() => epi = true;
        public void MakeHipocycloid() => epi = false;
        public Boolean IsEpicycloid => epi;
        public Boolean IsHipocycloid => !epi;
        private Boolean AllIsSet => da > 0 && df > 0 && e > 0 && lambda > 0 && Dw > 0 && ro > 0 && dg > 0 && Db > 0 && g > 0 && z > 0;
        private Boolean CurveReq    
        {
            get
            {
                if (!AllIsSet)
                    return false;
                else
                {
                    if (epi)
                    {
                        return lambda <= 1 && lambda >= ((z - 1) / (2 * z + 1));
                    }
                    else
                    {
                        return lambda <= 1 && lambda >= ((z + 1) / (2 * z - 1));
                    }
                }
            }
        }

        private Boolean CutReq
        {
            get
            {
                if (!AllIsSet) return false;
                else
                {
                    if (epi)
                    {
                        Double c = Math.Sqrt((lambda * lambda) / (1 - lambda * lambda));
                        c *= Math.Sqrt(1 + (2 / z));
                        c *= (z + 2) / (Math.Sqrt(27) * (z + 1));
                        c *= g;
                        return e >= c;
                    }
                    else
                    {
                        Double c = Math.Sqrt((lambda * lambda) / (1 - lambda * lambda));
                        c *= Math.Sqrt(1 - (2 / z));
                        c *= (z - 2) / (Math.Sqrt(27) * (z - 1));
                        c *= g;
                        return e >= c;
                    }
                }
            }
        }

        private Boolean NeighReq
        {
            get
            {
                if (!AllIsSet) return false;
                else
                {
                    if (epi)
                    {
                        return e > g * lambda / ((z + 1) * Math.Sin(Math.PI / (z + 1)));
                    }
                    else
                    {
                        return e > g * lambda / (z * Math.Sin(Math.PI / (z - 1)));
                    }
                }
            }
        }
        public void ReqChecker()
        {
            if (!AllIsSet) throw new Exception("Not every parameter was calculated");
            if (!CurveReq) throw new Exception("The curvature condition not met");
            if (!CutReq) throw new Exception("The undercut codition not met");
            if (!NeighReq) throw new Exception("The tooth proximity condition not met");
        }

        public Boolean AllReqsMet => CurveReq && CutReq && NeighReq;
        private Boolean BaseValuesSet => z > 0 && g > 0;

        public double Dw { get => dw; set => dw = value; }
        public double Db { get => db; set => db = value; }
    }
}

