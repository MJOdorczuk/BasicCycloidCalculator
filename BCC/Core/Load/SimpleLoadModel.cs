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
        public MaterialGroupContainer() : base(new List<DimensioningParams>()
        {
            DimensioningParams.E_MATERIAL,
            DimensioningParams.Ν_MATERIAL
        })
        {
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Steel,
                new Dictionary<DimensioningParams, double>()
                {
                    {DimensioningParams.E_MATERIAL, 200 },
                    {DimensioningParams.Ν_MATERIAL, 0.3 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.CastIron,
                new Dictionary<DimensioningParams, double>()
                {
                    {DimensioningParams.E_MATERIAL, 92.4 },
                    {DimensioningParams.Ν_MATERIAL, 0.25 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Bronze,
                new Dictionary<DimensioningParams, double>()
                {
                    {DimensioningParams.E_MATERIAL, 100 },
                    {DimensioningParams.Ν_MATERIAL, 0.34 }
                });
            this.PushPredefine(Vocabulary.ParameterLabels.Dimensioning.Brass,
                new Dictionary<DimensioningParams, double>()
                {
                    {DimensioningParams.E_MATERIAL, 117 },
                    {DimensioningParams.Ν_MATERIAL, 0.36 }
                });
        }
    }

    class SimpleLoadModel : LoadModel
    {
        protected override List<DimensioningParams> FitParams()
        {
            return new List<DimensioningParams>()
            {
                DimensioningParams.ΔR_HOLE,
                DimensioningParams.ΔR_SLEEVE,
                DimensioningParams.ΔR_HOLE_SPACING,
                DimensioningParams.ΔR_SLEEVE_SPACING,
                DimensioningParams.ΔΦ_HOLE,
                DimensioningParams.ΔΦ_SLEEVE,
                DimensioningParams.ΔE
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

        protected override List<DimensioningParams> ObligatoryFloatParams()
        {
            return new List<DimensioningParams>()
            {
                DimensioningParams.E,
                DimensioningParams.B,
                DimensioningParams.R_SLEEVE
            };
        }

        protected override List<DimensioningParams> ObligatoryIntParams()
        {
            return new List<DimensioningParams>()
            {
                DimensioningParams.N
            };
        }

        protected override Dictionary<DimensioningParams, PredefinedGroupContainer> PredefinedGroups()
        {
            var groupContainer = new MaterialGroupContainer();
            return new Dictionary<DimensioningParams, PredefinedGroupContainer>()
            {
                {DimensioningParams.GEAR_MATERIAL, groupContainer },
                {DimensioningParams.SLEEVE_MATERIAL, groupContainer },
            };
        }
    }
}
