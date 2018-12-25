using BCC.Interface_View.StandardInterface.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BCC.Core.Geometry
{
    class GeometryModel
    {
        private static class CliqueManager
        {
            private static readonly List<List<CycloParams>> possibleCliques = new List<List<CycloParams>>()
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

            public static bool IsPossibleClique(List<CycloParams> clique)
            {
                return possibleCliques.Exists(c =>
                {
                    foreach(var p in clique) if (!c.Contains(p)) return false;
                    foreach(var p in c) if (!clique.Contains(p)) return false;
                    return true;
                });
            }

            public static List<List<CycloParams>> ComplementCLique(List<CycloParams> clique)
            {
                var ret = new List<List<CycloParams>>();
                foreach(var pClique in possibleCliques)
                {
                    var over = clique.Except(pClique);
                    var c = pClique.Except(clique);
                    if(over.Count() == 0)
                    {
                        ret.Add(c.ToList());
                    }
                }
                return ret;
            }
        }

        private readonly GeometryMenu menu;

        public GeometryModel(GeometryMenu menu)
        {
            this.menu = menu;
        }

        public void Update()
        {
            int z = (int)menu.Get(CycloParams.Z);
            double g = (double)menu.Get(CycloParams.G);
            var clique = new List<CycloParams>();
            foreach
        }
    }
}
