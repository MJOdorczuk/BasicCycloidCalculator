using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Core.Geometry
{
    // The simple version of geometry model.
    // No equation solving
    // Only predefined methods
    class SimpleGeometryModel : GeometryModel
    {
        private readonly Cycloid cycloid = new Cycloid();
        private static class StaticFields
        {
            public static readonly Dictionary<CycloParams, Func<string>> nameCallGenerators = new Dictionary<CycloParams, Func<string>>()
            {
                {CycloParams.DA, Vocabulary.ParameterLabels.Geometry.MajorDiameter },
                {CycloParams.DB, Vocabulary.ParameterLabels.Geometry.BaseDiameter },
                {CycloParams.DF, Vocabulary.ParameterLabels.Geometry.RootDiameter },
                {CycloParams.DG, Vocabulary.ParameterLabels.Geometry.RollSpacingDiameter },
                {CycloParams.DW, Vocabulary.ParameterLabels.Geometry.PinSpacingDiameter },
                {CycloParams.E, Vocabulary.ParameterLabels.Geometry.Eccentricity },
                {CycloParams.EPI, Vocabulary.ParameterLabels.Geometry.ProfileType },
                {CycloParams.G, Vocabulary.ParameterLabels.Geometry.RollDiameter },
                {CycloParams.H, Vocabulary.ParameterLabels.Geometry.ToothHeight },
                {CycloParams.Z, Vocabulary.ParameterLabels.Geometry.TeethQuantity },
                {CycloParams.Λ, Vocabulary.ParameterLabels.Geometry.ToothHeightFactor },
                {CycloParams.Ρ, Vocabulary.ParameterLabels.Geometry.RollingCircleDiameter }
            };
            public static readonly List<CycloParams> obligatoryFloatParams = new List<CycloParams>()
            {
                CycloParams.G
            };
            public static readonly List<CycloParams> obligatoryIntParams = new List<CycloParams>()
            {
                CycloParams.Z
            };
            public static readonly List<CycloParams> optionalParams = new List<CycloParams>()
            {
                CycloParams.DA,
                CycloParams.DF,
                CycloParams.DG,
                CycloParams.E,
                CycloParams.H
            };
            public static readonly List<CycloParams> outputParams = new List<CycloParams>()
            {
                CycloParams.DB,
                CycloParams.DW,
                CycloParams.Λ,
                CycloParams.Ρ
            };
            public static readonly List<List<CycloParams>> possibleCliques = new List<List<CycloParams>>()
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
            public static void PopupMessage(string message)
            {
                var popup = new Form()
                {
                    Text = message,
                    Width = 200,
                    Height = 100,
                    Visible = true,
                    Enabled = true
                };
                popup.Show();
            }
        }

        protected override bool IsCliquePossible(List<CycloParams> clique)
        {
            if (cycloid.AllIsSet) return true;
            var possibleCompletions = PossibleCliques(clique);
            if (possibleCompletions.Count > 1) StaticFields.PopupMessage("Many possibilities");
            else if (possibleCompletions.Count == 0) StaticFields.PopupMessage("No possibilities");
            return false;
        }

        protected override bool IsCurvatureRequirementMet(Dictionary<CycloParams, double> vals)
        {
            if (cycloid.CurveReq) return true;
            else
            {
                StaticFields.PopupMessage("Curvature requirement not met");
                return false;
            }
        }

        protected override bool IsNeighbourhoodRequirementMet(Dictionary<CycloParams, double> vals)
        {
            if (cycloid.NeighReq) return true;
            else
            {
                StaticFields.PopupMessage("Neighbourhood requirement not met");
                return false;
            }
        }

        protected override bool IsNonNegativeValue(CycloParams param, double value)
        {
            if (value < 0)
            {
                StaticFields.PopupMessage("Negative value of " + CallName(param)());
                return false;
            }
            else return true;
        }

        protected override bool IsToothCuttingRequirementMet(Dictionary<CycloParams, double> vals)
        {
            if (cycloid.CutReq) return true;
            else
            {
                StaticFields.PopupMessage("Tooth cutting requirement not met");
                return false;
            }
        }

        protected override List<CycloParams> ObligatoryFloatParams()
        {
            return StaticFields.obligatoryFloatParams;
        }

        protected override List<CycloParams> ObligatoryIntParams()
        {
            return StaticFields.obligatoryIntParams;
        }

        protected override List<CycloParams> OptionalParams()
        {
            return StaticFields.optionalParams;
        }

        protected override List<CycloParams> OutputParams()
        {
            return StaticFields.outputParams;
        }

        protected override List<List<CycloParams>> PossibleCliques()
        {
            return StaticFields.possibleCliques;
        }

        public override void Compute()
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
                            cycloid.Set(val.Key, b ? Cycloid.TRUE : Cycloid.FALSE);
                            break;
                        }
                    case double d:
                        {
                            cycloid.Set(val.Key, d);
                            break;
                        }
                    case decimal d:
                        {
                            cycloid.Set(val.Key, (double)d);
                            break;
                        }
                    default:
                        {
                            cycloid.Set(val.Key, (double)val.Value);
                            break;
                        }
                }
            }
            cycloid.Calculate();
            var epi = (bool)data[CycloParams.EPI];
            var doubleData = cycloid.GetAll();
            //doubleData.Remove(CycloParams.EPI);
            if (AreRequirementsMet(doubleData))
            {
                var toUpload = new Dictionary<CycloParams, object>();
                foreach(var param in OptionalParams())
                {
                    toUpload.Add(param, cycloid.Get(param));
                }
                foreach(var param in OutputParams())
                {
                    toUpload.Add(param, cycloid.Get(param));
                }
                UpLoadData(toUpload);
                var curve = GetCurve(doubleData);
                view.GetRenderer()(doubleData[CycloParams.DA], curve);
            }
        }

        protected override Dictionary<CycloParams, Func<string>> NameCallGenerators()
        {
            return StaticFields.nameCallGenerators;
        }

        protected override Func<double, PointF> GetCurve(Dictionary<CycloParams, double> data)
        {
            var z = (int)data[CycloParams.Z];
            var g = data[CycloParams.G];
            var λ = data[CycloParams.Λ];
            var ρ = data[CycloParams.Ρ];
            if (data[CycloParams.EPI] == Cycloid.TRUE)
            {
                float X(double t) => (float)(ρ * ((z + 1) * Math.Cos(t) - λ * Math.Cos((z + 1) * t)) - g * (Math.Cos(t) - λ * Math.Cos((z + 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                float Y(double t) => (float)(ρ * ((z + 1) * Math.Sin(t) - λ * Math.Sin((z + 1) * t)) - g * (Math.Sin(t) - λ * Math.Sin((z + 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                return new Func<double,PointF>(t => new PointF(X(t), Y(t)));
            }
            else
            {
                float X(double t) => (float)(ρ * ((z - 1) * Math.Cos(t) + λ * Math.Cos((z - 1) * t)) + g * (Math.Cos(t) - λ * Math.Cos((z - 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                float Y(double t) => (float)(ρ * ((z - 1) * Math.Sin(t) + λ * Math.Sin((z - 1) * t)) + g * (Math.Sin(t) - λ * Math.Sin((z - 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                return new Func<double,PointF>(t => new PointF(X(t), Y(t)));
            }
        }
    }
}
