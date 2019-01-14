using BCC.Core.Parameters;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Load
{
    class ParametrizedLoadModel : Model
    {
        private List<PageSeed> seeds = null;
        private readonly IParameter Egear, Esleeve, Ngear, Nsleeve, n, e, b, Rsleeve, DRhole, DRsleeve, DRhole_spacing,
            DRsleeve_spacing, DFhole, DFsleeve, De, Rspacing, M, gear, sleeve;

        public ParametrizedLoadModel()
        {
            Egear = new EnableableFloatParameter(Vocabulary.ParameterLabels.Dimensioning.YoungsModulus, 1);
            Ngear = new EnableableFloatParameter(Vocabulary.ParameterLabels.Dimensioning.PoissonsRatio, 0.001, 0.5);
            gear = new Func<ParameterGroup>(() =>
            {
                var gearGenerator = new ParameterGroup.ParameterGroupGenerator(Vocabulary.ParameterLabels.Dimensioning.GearMaterial,
                new List<ISingleEnableableParameter>() {
                    (ISingleEnableableParameter)Egear,
                    (ISingleEnableableParameter)Ngear }, true);
                gearGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Steel, new Dictionary<ISingleEnableableParameter, object>()
            {
                {(ISingleEnableableParameter)Egear, 200 },
                {(ISingleEnableableParameter)Ngear, 0.3 }
            });
                gearGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.CastIron, new Dictionary<ISingleEnableableParameter, object>()
            {
                {(ISingleEnableableParameter)Egear, 92.4 },
                {(ISingleEnableableParameter)Ngear, 0.25 }
            });
                gearGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Bronze, new Dictionary<ISingleEnableableParameter, object>()
            {
                {(ISingleEnableableParameter)Egear, 100 },
                {(ISingleEnableableParameter)Ngear, 0.34 }
            });
                gearGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Brass, new Dictionary<ISingleEnableableParameter, object>()
            {
                {(ISingleEnableableParameter)Egear, 117 },
                {(ISingleEnableableParameter)Ngear, 0.36 }
            });
                return gearGenerator.Generate();
            })();
            Esleeve = new EnableableFloatParameter(Vocabulary.ParameterLabels.Dimensioning.YoungsModulus, 1);
            Nsleeve = new EnableableFloatParameter(Vocabulary.ParameterLabels.Dimensioning.PoissonsRatio, 0.001, 0.5);
            sleeve = new Func<ParameterGroup>(() =>
            {
                var sleeveGenerator = new ParameterGroup.ParameterGroupGenerator(Vocabulary.ParameterLabels.Dimensioning.SleeveMaterial,
                new List<ISingleEnableableParameter>() {
                    (ISingleEnableableParameter)Esleeve,
                    (ISingleEnableableParameter)Nsleeve }, true);
                sleeveGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Steel, new Dictionary<ISingleEnableableParameter, object>()
                {
                    {(ISingleEnableableParameter)Esleeve, 200 },
                    {(ISingleEnableableParameter)Nsleeve, 0.3 }
                });
                sleeveGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.CastIron, new Dictionary<ISingleEnableableParameter, object>()
                {
                    {(ISingleEnableableParameter)Esleeve, 92.4 },
                    {(ISingleEnableableParameter)Nsleeve, 0.25 }
                });
                sleeveGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Bronze, new Dictionary<ISingleEnableableParameter, object>()
                {
                    {(ISingleEnableableParameter)Esleeve, 100 },
                    {(ISingleEnableableParameter)Nsleeve, 0.34 }
                });
                sleeveGenerator.AddPredefine(Vocabulary.ParameterLabels.Dimensioning.Brass, new Dictionary<ISingleEnableableParameter, object>()
                {
                    {(ISingleEnableableParameter)Esleeve, 117 },
                    {(ISingleEnableableParameter)Nsleeve, 0.36 }
                });
                return sleeveGenerator.Generate();
            })();
            n = new IntParameter(Vocabulary.ParameterLabels.Dimensioning.RollQuantity);
            e = new FloatParameter(Vocabulary.ParameterLabels.Geometry.Eccentricity);
            b = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.FaceWidth);
            Rsleeve = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.SleeveRadius);


            Rspacing = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.HoleRadius);
            M = new FloatParameter(Vocabulary.ParameterLabels.Result.Momentum);
        }

        internal Dictionary<UserControl, Func<string>> GetMenus()
        {
            return NewGetMenus();
        }

        protected override List<PageSeed> PageSeeds()
        {
            if(seeds is null)
            {
                seeds = new List<PageSeed>();
                seeds.Add(new PageSeed(Vocabulary.TabPagesNames.Dimensioning, new List<IParameter>()
                {
                    gear, sleeve, n, e, b, Rsleeve
                }));
                seeds.Add(new PageSeed(Vocabulary.TabPagesNames.Results, new List<IParameter>()
                {
                    Rspacing, M
                }));
            }
            return seeds;
        }

        protected override void Act()
        {
            throw new NotImplementedException();
        }



        // To remove soon
        protected override List<Enum> ObligatoryIntParams()
        {
            throw new NotImplementedException();
        }
        protected override List<Enum> ObligatoryFloatParams()
        {
            throw new NotImplementedException();
        }
        protected override Dictionary<Enum, Func<string>> NameCallGenerators()
        {
            throw new NotImplementedException();
        }
    }
}
