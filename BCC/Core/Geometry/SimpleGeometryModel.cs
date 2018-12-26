using BCC.Interface_View.StandardInterface.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCC.Core.Geometry
{
    class SimpleGeometryModel : GeometryModel
    {
        private readonly Cycloid cycloid = new Cycloid();

        protected override bool IsCliquePossible(List<CycloParams> clique)
        {
            throw new NotImplementedException();
        }

        protected override bool IsCurvatureRequirementMet(Dictionary<CycloParams, double> vals, bool epi)
        {
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourhoodRequirementMet(Dictionary<CycloParams, double> vals, bool epi)
        {
            throw new NotImplementedException();
        }

        protected override bool IsPositiveValue(CycloParams param, double value)
        {
            throw new NotImplementedException();
        }

        protected override bool IsToothCuttingRequirementMet(Dictionary<CycloParams, double> vals, bool epi)
        {
            throw new NotImplementedException();
        }

        public override List<CycloParams> ObligatoryFloatParams()
        {
            return new List<CycloParams>()
            {
                CycloParams.G
            };
        }

        public override List<CycloParams> ObligatoryIntParams()
        {
            return new List<CycloParams>()
            {
                CycloParams.Z
            };
        }

        public override List<CycloParams> OptionalParams()
        {
            return new List<CycloParams>()
            {
                CycloParams.DA,
                CycloParams.DF,
                CycloParams.DG,
                CycloParams.E,
                CycloParams.H
            };
        }

        public override List<CycloParams> OutputParams()
        {
            return new List<CycloParams>()
            {
                CycloParams.DB,
                CycloParams.DW,
                CycloParams.Λ,
                CycloParams.Ρ
            };
        }

        public override List<List<CycloParams>> PossibleCliques(params List<CycloParams>[] cliques)
        {
            return new List<List<CycloParams>>()
            {
                new List<CycloParams>()
                {
                    CycloParams.DA,
                    CycloParams.DF
                },
                new List<CycloParams>()
                {
                    CycloParams.DA,
                    CycloParams.DG
                },
                new List<CycloParams>()
                {
                    CycloParams.DA,
                    CycloParams.E
                },
                new List<CycloParams>()
                {
                    CycloParams.DA,
                    CycloParams.H
                },
                new List<CycloParams>()
                {
                    CycloParams.DF,
                    CycloParams.DG
                },
                new List<CycloParams>()
                {
                    CycloParams.DF,
                    CycloParams.E
                },
                new List<CycloParams>()
                {
                    CycloParams.DF,
                    CycloParams.H
                },
                new List<CycloParams>()
                {
                    CycloParams.DF,
                    CycloParams.E
                },
                new List<CycloParams>()
                {
                    CycloParams.DG,
                    CycloParams.E
                },
                new List<CycloParams>()
                {
                    CycloParams.DG,
                    CycloParams.H
                }
            };
        }

        private void Compute()
        {
            // Reseting cycloid geometry calculator for new computations
            cycloid.Reset();
            // Downloading data from geometry control
            var data = DownLoadData();
            // 
            foreach(var val in data)
            {
                switch(val.Value)
                {
                    case int i:
                        {
                            cycloid.Set(val.Key, (double)i);
                            break;
                        }
                    case bool b:
                        {
                            cycloid.Set(val.Key, b ? 1.0 : 0.0);
                            break;
                        }
                    default:
                        {
                            cycloid.Set(val.Key, (double)val.Value);
                            break;
                        }
                }
            }
        }
    }
}
