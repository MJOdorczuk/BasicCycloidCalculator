using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Load
{
    public class MaterialGroupContainer : PredefinedGroupContainer
    {
        public MaterialGroupContainer() : base(new List<Enum>()
        {
            DimensioningParams.E_MATERIAL,
            DimensioningParams.Ν_MATERIAL
        }, true)
        {
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Steel,
                new Dictionary<Enum, double>()
                {
                    {DimensioningParams.E_MATERIAL, 200 },
                    {DimensioningParams.Ν_MATERIAL, 0.3 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.CastIron,
                new Dictionary<Enum, double>()
                {
                    {DimensioningParams.E_MATERIAL, 92.4 },
                    {DimensioningParams.Ν_MATERIAL, 0.25 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Bronze,
                new Dictionary<Enum, double>()
                {
                    {DimensioningParams.E_MATERIAL, 100 },
                    {DimensioningParams.Ν_MATERIAL, 0.34 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Brass,
                new Dictionary<Enum, double>()
                {
                    {DimensioningParams.E_MATERIAL, 117 },
                    {DimensioningParams.Ν_MATERIAL, 0.36 }
                });
        }
    }

    public enum DimensioningParams
    {
        E_MATERIAL = 0,
        Ν_MATERIAL = 1,
        R_SPACING = 2,
        ΔR_HOLE_SPACING = 3,
        ΔΦ_HOLE = 4,
        ΔR_SLEEVE_SPACING = 5,
        ΔΦ_SLEEVE = 6,
        ΔE = 7,
        ΔR_SLEEVE = 8,
        ΔR_HOLE = 9,
        B = 10,
        R_SLEEVE = 11,
        R_HOLE = 12,
        E = 13,
        N = 14,
        GEAR_MATERIAL = 15,
        SLEEVE_MATERIAL = 16,
        Δ = 17
    }

    class SimpleLoadModel : LoadModel
    {
        protected override List<Enum> PluralFitParams()
        {
            return new List<Enum>()
            {
                DimensioningParams.ΔR_HOLE,
                DimensioningParams.ΔR_SLEEVE,
                DimensioningParams.ΔR_HOLE_SPACING,
                DimensioningParams.ΔR_SLEEVE_SPACING,
                DimensioningParams.ΔΦ_HOLE,
                DimensioningParams.ΔΦ_SLEEVE
            };
        }

        protected override Dictionary<Enum, Func<string>> NameCallGenerators()
        {
            return new Dictionary<Enum, Func<string>>()
            {
                {DimensioningParams.B, Vocabulary.ParameterLabels.Dimensioning.FaceWidth },
                {DimensioningParams.E, Vocabulary.ParameterLabels.Geometry.Eccentricity },
                {DimensioningParams.E_MATERIAL, Vocabulary.ParameterLabels.Dimensioning.YoungsModulus },
                {DimensioningParams.N, Vocabulary.ParameterLabels.Dimensioning.RollQuantity },
                {DimensioningParams.R_HOLE, Vocabulary.ParameterLabels.Dimensioning.HoleRadius},
                {DimensioningParams.R_SLEEVE, Vocabulary.ParameterLabels.Dimensioning.SleeveRadius },
                {DimensioningParams.R_SPACING, Vocabulary.ParameterLabels.Dimensioning.RollSpacingRadius },
                {DimensioningParams.ΔE, Vocabulary.ParameterLabels.Geometry.Eccentricity },
                {DimensioningParams.ΔR_HOLE, Vocabulary.ParameterLabels.Dimensioning.HoleRadius },
                {DimensioningParams.ΔR_SLEEVE, Vocabulary.ParameterLabels.Dimensioning.SleeveRadius },
                {DimensioningParams.ΔR_HOLE_SPACING, Vocabulary.ParameterLabels.Dimensioning.HoleSpacingRadius },
                {DimensioningParams.ΔR_SLEEVE_SPACING, Vocabulary.ParameterLabels.Dimensioning.SleeveSpacingRadius },
                {DimensioningParams.ΔΦ_HOLE, Vocabulary.ParameterLabels.Dimensioning.HoleSpacingAngle },
                {DimensioningParams.ΔΦ_SLEEVE, Vocabulary.ParameterLabels.Dimensioning.SleeveSpacingAngle },
                {DimensioningParams.Ν_MATERIAL, Vocabulary.ParameterLabels.Dimensioning.PoissonsRatio },
                {DimensioningParams.GEAR_MATERIAL, Vocabulary.ParameterLabels.Dimensioning.GearMaterial },
                {DimensioningParams.SLEEVE_MATERIAL, Vocabulary.ParameterLabels.Dimensioning.SleeveMaterial },
                {DimensioningParams.Δ, Vocabulary.ParameterLabels.Dimensioning.EngineeringTolerances },
            };
        }

        protected override List<Enum> ObligatoryFloatParams()
        {
            return new List<Enum>()
            {
                DimensioningParams.E,
                DimensioningParams.B,
                DimensioningParams.R_SLEEVE
            };
        }

        protected override List<Enum> ObligatoryIntParams()
        {
            return new List<Enum>()
            {
                DimensioningParams.N
            };
        }

        protected override Dictionary<Enum, PredefinedGroupContainer> PredefinedGroups()
        {
            var groupContainer = new MaterialGroupContainer();
            return new Dictionary<Enum, PredefinedGroupContainer>()
            {
                {DimensioningParams.GEAR_MATERIAL, groupContainer },
                {DimensioningParams.SLEEVE_MATERIAL, groupContainer },
            };
        }

        protected override int ToleratedElementsQuantity()
        {
            return dimensioningPart is null ? 0 : (int)dimensioningPart.Get(DimensioningParams.N)(0) - 1;
        }

        protected override List<Enum> SingularFitParams()
        {
            return new List<Enum>()
            {
                DimensioningParams.ΔE
            };
        }
    }
}
