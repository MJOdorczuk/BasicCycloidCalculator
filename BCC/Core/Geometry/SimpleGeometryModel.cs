using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Core.Geometry
{
    public enum CycloParams
    {
        Z = 0,
        G = 1,
        DA = 2,
        DF = 3,
        E = 4,
        H = 5,
        DG = 6,
        Λ = 7,
        DW = 8,
        Ρ = 9,
        DB = 10,
        EPI = 11
    }

    // The simple version of geometry model.
    // No equation solving
    // Only predefined methods
    class SimpleGeometryModel : GeometryModel
    {

        private static new class StaticFields
        {
            public static readonly Dictionary<Enum, Func<string>> nameCallGenerators = new Dictionary<Enum, Func<string>>()
            {
                {CycloParams.DA, Vocabulary.ParameterLabels.Geometry.MajorDiameter },
                {CycloParams.DB, Vocabulary.ParameterLabels.Geometry.BaseDiameter },
                {CycloParams.DF, Vocabulary.ParameterLabels.Geometry.RootDiameter },
                {CycloParams.DG, Vocabulary.ParameterLabels.Geometry.RollSpacingDiameter },
                {CycloParams.DW, Vocabulary.ParameterLabels.Geometry.PinSpacingDiameter },
                {CycloParams.E, Vocabulary.ParameterLabels.Geometry.Eccentricity },
                {CycloParams.EPI, Vocabulary.ParameterLabels.Geometry.ProfileType },
                {CycloParams.G, Vocabulary.ParameterLabels.Geometry.RollRadius },
                {CycloParams.H, Vocabulary.ParameterLabels.Geometry.ToothHeight },
                {CycloParams.Z, Vocabulary.ParameterLabels.Geometry.TeethQuantity },
                {CycloParams.Λ, Vocabulary.ParameterLabels.Geometry.ToothHeightFactor },
                {CycloParams.Ρ, Vocabulary.ParameterLabels.Geometry.RollingCircleDiameter }
            };
            public static readonly List<Enum> obligatoryFloatParams = new List<Enum>()
            {
                CycloParams.G
            };
            public static readonly List<Enum> obligatoryIntParams = new List<Enum>()
            {
                CycloParams.Z
            };
            public static readonly List<Enum> optionalParams = new List<Enum>()
            {
                CycloParams.DA,
                CycloParams.DF,
                CycloParams.DG,
                CycloParams.E,
                CycloParams.H
            };
            public static readonly List<Enum> outputParams = new List<Enum>()
            {
                CycloParams.DB,
                CycloParams.DW,
                CycloParams.Λ,
                CycloParams.Ρ
            };
            public static readonly List<List<Enum>> possibleCliques = new List<List<Enum>>()
            {
                new List<Enum>()
                {
                    CycloParams.DA,
                    CycloParams.DF
                },
                new List<Enum>()
                {
                    CycloParams.DA,
                    CycloParams.DG
                },
                new List<Enum>()
                {
                    CycloParams.DA,
                    CycloParams.E
                },
                new List<Enum>()
                {
                    CycloParams.DA,
                    CycloParams.H
                },
                new List<Enum>()
                {
                    CycloParams.DF,
                    CycloParams.DG
                },
                new List<Enum>()
                {
                    CycloParams.DF,
                    CycloParams.E
                },
                new List<Enum>()
                {
                    CycloParams.DF,
                    CycloParams.H
                },
                new List<Enum>()
                {
                    CycloParams.DF,
                    CycloParams.E
                },
                new List<Enum>()
                {
                    CycloParams.DG,
                    CycloParams.E
                },
                new List<Enum>()
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

        protected override bool IsCurvatureRequirementMet(Dictionary<Enum, double> vals)
        {
            if (CycloidGeometry.CurveReq)
            {
                BubbleCalls[CycloParams.Λ](null);
                return true;
            }
            else
            {
                BubbleCalls[CycloParams.Λ]("Curvature requirement not met");
                return false;
            }
        }
        protected override bool IsNeighbourhoodRequirementMet(Dictionary<Enum, double> vals)
        {
            if (CycloidGeometry.NeighReq)
            {
                BubbleCalls[CycloParams.G](null);
                return true;
            }
            else
            {
                BubbleCalls[CycloParams.G]("Neighbourhood requirement not met");    
                return false;
            }
        }
        protected override bool IsToothCuttingRequirementMet(Dictionary<Enum, double> vals)
        {
            if (CycloidGeometry.CutReq)
            {
                BubbleCalls[CycloParams.E](null);
                return true;
            }
            else
            {
                BubbleCalls[CycloParams.E]("Teeth cutting requirement not met");
                return false;
            }
        }

        protected override List<Enum> ObligatoryFloatParams()
        {
            return StaticFields.obligatoryFloatParams;
        }
        protected override List<Enum> ObligatoryIntParams()
        {
            return StaticFields.obligatoryIntParams;
        }
        protected override List<Enum> OptionalParams()
        {
            return StaticFields.optionalParams;
        }
        protected override List<Enum> OutputParams()
        {
            return StaticFields.outputParams;
        }
        protected override List<List<Enum>> PossibleCliques()
        {
            return StaticFields.possibleCliques;
        }

        protected override Dictionary<Enum, Func<string>> NameCallGenerators()
        {
            return StaticFields.nameCallGenerators;
        }
        protected override Func<double, PointF> GetCurve(Dictionary<Enum, double> data)
        {
            var z = (int)data[CycloParams.Z];
            var g = data[CycloParams.G];
            var λ = data[CycloParams.Λ];
            var ρ = data[CycloParams.Ρ];
            if (data[CycloParams.EPI] == CycloidGeometry.TRUE)
            {
                float X(double t) => (float)(ρ * ((z + 1) * Math.Cos(t) - λ * Math.Cos((z + 1) * t)) - g * (Math.Cos(t) - λ * Math.Cos((z + 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                float Y(double t) => (float)(ρ * ((z + 1) * Math.Sin(t) - λ * Math.Sin((z + 1) * t)) - g * (Math.Sin(t) - λ * Math.Sin((z + 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                return new Func<double,PointF>(t => new PointF(X(t), Y(t)));
            }
            else
            {
                float X(double t) => (float)(ρ * ((z - 1) * Math.Cos(t) + λ * Math.Cos((z - 1) * t)) + g * (Math.Cos(t) - λ * Math.Cos((z - 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                float Y(double t) => (float)(ρ * ((z - 1) * Math.Sin(t) - λ * Math.Sin((z - 1) * t)) + g * (Math.Sin(t) + λ * Math.Sin((z - 1) * t)) / Math.Sqrt(1 - 2 * λ * Math.Cos(z * t) + λ * λ));
                return new Func<double,PointF>(t => new PointF(X(t), Y(t)));
            }
        }
        protected override Dictionary<Enum, double> ExtractData(Dictionary<Enum, double> data)
        {
            CycloidGeometry.Reset();
            foreach (var val in data)
            {
                CycloidGeometry.Set(val.Key, val.Value);
            }
            CycloidGeometry.Calculate();
            return CycloidGeometry.GetAll();
        }
    }
}
