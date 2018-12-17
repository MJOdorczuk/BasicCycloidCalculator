using System;
using System.Collections.Generic;

namespace BCC.Archivised.Controls
{
    class SimpleCycloidGeometryControl : CycloidGeometryControl
    {
        private static readonly Cycloid cyclo = new Cycloid();
        private readonly Dictionary<string, Action<double>> cycloidSetterCalls = new Dictionary<string, Action<double>>()
        {
            ["da"] = (x) => cyclo.Da = x,
            ["z"] = (x) => cyclo.Z = (uint)x,
            ["df"] = (x) => cyclo.Df = x,
            ["g"] = (x) => cyclo.G = x,
            ["e"] = (x) => cyclo.E = x,
            ["h"] = (x) => cyclo.H = x,
            ["dg"] = (x) => cyclo.Dg = x,
            ["dw"] = (x) => cyclo.Dw = x,
            ["ro"] = (x) => cyclo.Ro = x,
            ["lambda"] = (x) => cyclo.Lambda = x,
            ["db"] = (x) => cyclo.Db = x
        };
        private readonly Dictionary<string, Func<double>> cycloidGetterCalls = new Dictionary<string, Func<double>>()
        {
            ["da"] = () => cyclo.Da,
            ["z"] = () => cyclo.Z,
            ["df"] = () => cyclo.Df,
            ["g"] = () => cyclo.G,
            ["e"] = () => cyclo.E,
            ["h"] = () => cyclo.H,
            ["dg"] = () => cyclo.Dg,
            ["dw"] = () => cyclo.Dw,
            ["ro"] = () => cyclo.Ro,
            ["lambda"] = () => cyclo.Lambda,
            ["db"] = () => cyclo.Db
        };
        public SimpleCycloidGeometryControl()
        {
            integerParameters = new List<string>() { "z" };
            floatParameters = new List<string>()
            {
                "g", "da", "df", "h", "e", "dg"
            };
            necessaryParameters = new List<string>()
            {
                "z",
                "g"
            };
            possibleCliques = new List<List<string>>()
            {
                new List<string>(){ "da", "df" },
                new List<string>(){ "da", "h" },
                new List<string>(){ "da", "e" },
                new List<string>(){ "da", "dg" },
                new List<string>(){ "df", "h" },
                new List<string>(){ "df", "e" },
                new List<string>(){ "df", "dg" },
                new List<string>(){ "dg", "h" },
                new List<string>(){ "dg", "de" }
            };
            resultParameters = new List<string>()
            {
                "lambda",
                "dw",
                "ro",
                "db"
            };
        }

        public override List<string> ShortenClique(List<string> clique)
        {
            List<List<string>> leftPossibilities = new List<List<string>>(possibleCliques);
            clique.RemoveAll((x) => necessaryParameters.Contains(x));
            clique.Reverse();
            List<string> ret = new List<string>();
            foreach (string parameter in clique)
            {
                List<List<string>> temp = leftPossibilities.FindAll(
                    (List<string> x) =>
                    {
                        foreach (string y in ret)
                        {
                            if (!x.Contains(y)) return false;
                        }
                        return true;
                    }
                    );
                if (temp != null)
                {
                    leftPossibilities = temp;
                    ret.Add(parameter);
                }
            }
            ret.Reverse();
            ret.AddRange(necessaryParameters);
            return ret;
        }

        public override Dictionary<string,double> Compute(Dictionary<string, double> parameters, bool isEpicycloid)
        {
            // The returning value predefinition
            Dictionary<string, double> ret = new Dictionary<string, double>();
            // List of names of parameters given
            List<string> givenParameters = new List<string>(parameters.Keys);
            // If the parameters list lack at least one necessary parameter then throw exception
            foreach(string parameter in necessaryParameters)
            {
                if (!givenParameters.Contains(parameter))
                    throw new Exception(parameter + " is necessary");
            }
            // List necessary to find, which clique is to be considered
            List<string> cliqueParameters = new List<string>(givenParameters);
            // There is no need to check the clique for necessary parameters
            // These are in every clique
            cliqueParameters.RemoveAll((p) => necessaryParameters.Contains(p));
            // List of all possible cliques for clique finding
            // If a parameter in the clique isn't in one of the cliques from the list
            // then the one is removed to be not considered
            List<List<string>> SPC = new List<List<string>>();
            foreach(List<string> clique in possibleCliques)
            {
                SPC.Add(new List<string>(clique));
            }
            // Removing all the cliques that are not to be considered
            // Also shortening left cliques to inform, what can be added to the clique
            foreach(string parameter in cliqueParameters)
            {
                List<List<string>> temp = new List<List<string>>();
                temp = SPC.FindAll((c) => c.Contains(parameter));
                foreach(List<string> c in temp)
                {
                    c.RemoveAll((p) => p == parameter);
                }
                SPC = temp;
            }
            // If there is no possible clique that covers all the parameters
            // of the clique then throw exception
            if (SPC.Count == 0)
            {
                string retErr = "";
                foreach(string member in cliqueParameters)
                {
                    retErr += member + " ";
                }
                throw new Exception(retErr + "not possible as clique");
            }
            // If there are more than one possible clique covering all given parameters
            // or the one left has more members than given clique then throw exception
            if(SPC.Count > 1 || SPC[0].Count > 0)
            {
                string err = "Need more parameters, it can be :\n";
                foreach(List<string> c in SPC)
                {
                    err += "{ ";
                    foreach(string s in c)
                    {
                        err += s + " ";
                    }
                    err += "}\n";
                }
                throw new Exception(err);
            }
            // Reset all parameters of cycloid for safety
            cyclo.Reset();
            // Set all given values in cycloid
            if (isEpicycloid) cyclo.MakeEpicycloid();
            else cyclo.MakeHipocycloid();
            foreach(string parameter in givenParameters)
            {
                cycloidSetterCalls[parameter](parameters[parameter]);
            }
            // No fucking idea, why is it working, but it works and it was made by me,
            // so it have to be thought through. // Ha! Id wasn't. Id doesn't work!
            //if(cyclo.AllReqsMet)
            cyclo.Calculate();
            cyclo.ReqChecker();
            if (isEpicycloid)
                cycloidOutline = EpicycloidOutlineGenerator(cyclo.Ro, cyclo.Lambda, cyclo.Z, cyclo.G);
            else
                cycloidOutline = HipocycloidOutlineGenerator(cyclo.Ro, cyclo.Lambda, cyclo.Z, cyclo.G);
            List<string> allParameters = new List<string>(ResultParameters);
            allParameters.AddRange(IntegerParameters);
            allParameters.AddRange(FloatParameters);
            foreach(string parameter in allParameters)
            {
                ret.Add(parameter, cycloidGetterCalls[parameter]());
            }
            return ret;
        }

        internal override Func<double, Tuple<double, double>> EpicycloidOutlineGenerator(double ro, double lambda, double z, double g)
        {
            return (n =>
             {
                 double c = Math.Cos(n);
                 double c1 = Math.Cos((z + 1) * n);
                 double s = Math.Sin(n);
                 double s1 = Math.Sin((z + 1) * n);
                 double sq = Math.Sqrt(1 - 2 * lambda * Math.Cos(z * n) + lambda * lambda);
                 double x = ro * ((z + 1) * c - lambda * c1) - g * (c - lambda * c1) / sq;
                 double y = ro * ((z + 1) * s - lambda * s1) - g * (s - lambda * s1) / sq;
                 scale = ro * (z + 1) + lambda * ro - g;
                 return new Tuple<double,double>(x/(2*scale),y/(2*scale));
             });
        }

        internal override Func<double, Tuple<double, double>> HipocycloidOutlineGenerator(double ro, double lambda, double z, double g)
        {
            return (n =>
            {
                double c = Math.Cos(n);
                double c1 = Math.Cos((z - 1) * n);
                double s = Math.Sin(n);
                double s1 = Math.Sin((z - 1) * n);
                double sq = Math.Sqrt(1 - 2 * lambda * Math.Cos(z * n) + lambda * lambda);
                double x = ro * ((z - 1) * c + lambda * c1) + g * (c - lambda * c1) / sq;
                double y = ro * ((z - 1) * s + lambda * s1) + g * (s - lambda * s1) / sq;
                scale = ro * (z - 1) + lambda * ro + g;
                return new Tuple<double, double>(x / (2 * scale), y / (2 * scale));
            });
        }
    }
}
