using BCC.Core.Parameters;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Geometry
{
    class ParametrizedGeometryModel : Model
    {
        private List<PageSeed> seeds = null;
        private readonly OutputParameterList output;
        private readonly IntParameter z;
        private readonly FloatParameter g;
        private readonly EnableableFloatParameter da, df, e, h, dg;
        private readonly OutputSingleParameter lambda, dw, rho, db;
        private readonly ParameterGroup epi;
        private readonly IParameter[] parameters;
        private readonly ISingleEnableableParameter[][] possibleCliques;
        private readonly ISingleEnableableParameter[] enableables;
        private readonly int EPICYCLOID, HIPOCYCLOID;

        public ParametrizedGeometryModel()
        {
            var epiGenerator = new ParameterGroup.ParameterGroupGenerator(Vocabulary.ParameterLabels.Geometry.ProfileType, new List<ISingleEnableableParameter>());
            EPICYCLOID = epiGenerator.AddPredefine(Vocabulary.ParameterLabels.Geometry.Epicycloid, new Dictionary<ISingleEnableableParameter, object>());
            HIPOCYCLOID = epiGenerator.AddPredefine(Vocabulary.ParameterLabels.Geometry.Hipocycloid, new Dictionary<ISingleEnableableParameter, object>());
            epi = epiGenerator.Generate();
            z = new IntParameter(Vocabulary.ParameterLabels.Geometry.TeethQuantity);
            g = new FloatParameter(Vocabulary.ParameterLabels.Geometry.RollRadius);
            da = new EnableableFloatParameter(Vocabulary.ParameterLabels.Geometry.MajorDiameter, 0.001);
            df = new EnableableFloatParameter(Vocabulary.ParameterLabels.Geometry.RootDiameter, 0.001);
            e = new EnableableFloatParameter(Vocabulary.ParameterLabels.Geometry.Eccentricity, 0.001);
            h = new EnableableFloatParameter(Vocabulary.ParameterLabels.Geometry.ToothHeight, 0.001);
            dg = new EnableableFloatParameter(Vocabulary.ParameterLabels.Geometry.RollSpacingDiameter, 0.001);
            lambda = new OutputSingleParameter(Vocabulary.ParameterLabels.Geometry.ToothHeightFactor, 0.0);
            dw = new OutputSingleParameter(Vocabulary.ParameterLabels.Geometry.PinSpacingDiameter, 0.0);
            rho = new OutputSingleParameter(Vocabulary.ParameterLabels.Geometry.RollingCircleDiameter, 0.0);
            db = new OutputSingleParameter(Vocabulary.ParameterLabels.Geometry.BaseDiameter, 0.0);
            output = new OutputParameterList(() => "", new OutputSingleParameter[] 
            {
                lambda,
                dw,
                rho,
                db
            });
            parameters = new IParameter[]
            {
                epi, z, g, da, df, e, h, dg, lambda, dw, rho, db
            };
            possibleCliques = new ISingleEnableableParameter[][]
            {
                new ISingleEnableableParameter[]
                {
                    da, df
                },
                new ISingleEnableableParameter[]
                {
                    da, dg
                },
                new ISingleEnableableParameter[]
                {
                    da, e
                },
                new ISingleEnableableParameter[]
                {
                    da, h
                },
                new ISingleEnableableParameter[]
                {
                    df, dg
                },
                new ISingleEnableableParameter[]
                {
                    df, e
                },
                new ISingleEnableableParameter[]
                {
                    df, h
                },
                new ISingleEnableableParameter[]
                {
                    dg, e
                },
                new ISingleEnableableParameter[]
                {
                    dg, h
                }
            };
        }

        private bool IsEpicycloid => epi.Get == EPICYCLOID;

        public Dictionary<UserControl, Func<string>> GetMenus()
        {
            return NewGetMenus();
        }

        protected override List<PageSeed> PageSeeds()
        {
            if(seeds is null)
            {
                seeds = new List<PageSeed>();
                var parameters = new List<IParameter>()
                {
                    epi, z, g, da, df, e, h, dg, output
                };
                var geometrySeed = new PageSeed(Vocabulary.TabPagesNames.Geometry, parameters);
                seeds.Add(geometrySeed);
            }
            return seeds;
        }

        protected override void Act()
        {
            parameters.ToList().ForEach(param => param.CallBubble(null));
            var clique = parameters.ToList().FindAll(x => x is ISingleEnableableParameter enableable && enableable.Enabled);
            if(possibleCliques.Any(p_clique => p_clique.Intersect(clique).Count() == p_clique.Count() && p_clique.Count() == clique.Count()))
            {
                int z = this.z.Get();
                double g = this.g.Get();
                double da, df, dg, e, lambda, dw, rho, db;
                if (this.da.Enabled)
                {
                    da = this.da.Get();
                    if (this.df.Enabled)
                    {
                        df = this.df.Get();
                        e = (da - df) / (IsEpicycloid ? 4 : -4);
                        rho = ((da / 2) + (IsEpicycloid ? g - e : e - g)) / (z + (IsEpicycloid ? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (IsEpicycloid ? 1 : -1));
                    }
                    else if (this.dg.Enabled)
                    {
                        dg = this.dg.Get();
                        rho = dg / (2 * (z + (IsEpicycloid ? 1 : -1)));
                        lambda = (IsEpicycloid ? 1 : -1) * (0.5 * da - rho * (z + (IsEpicycloid ? 1 : -1))
                            + (IsEpicycloid ? g : -g)) / rho;
                        e = lambda * rho;
                        df = 2 * (rho * (z + (IsEpicycloid ? 1 : -1) - lambda) + (IsEpicycloid ? -g : g));
                    }
                    else if (this.e.Enabled || this.h.Enabled)
                    {
                        if(this.h.Enabled)
                        {
                            e = this.h.Get();
                        }
                        else
                        {
                            e = this.e.Get();
                        }
                        df = da + (IsEpicycloid ? -4 : 4) * e;
                        rho = ((da / 2) + (IsEpicycloid ? g - e : e - g)) / (z + (IsEpicycloid ? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (IsEpicycloid ? 1 : -1));
                    }
                    else
                    {
                        throw new Exception("Unknown case alert!");
                    }
                }
                else if (this.df.Enabled)
                {
                    df = this.df.Get();
                    if (this.dg.Enabled)
                    {
                        dg = this.dg.Get();
                        rho = dg / (2 * (z + (IsEpicycloid ? 1 : -1)));
                        lambda = (IsEpicycloid ? -1 : 1) * (0.5 * df - rho * (z + (IsEpicycloid ? 1 : -1)) + (IsEpicycloid ? g : -g)) / rho;
                        e = lambda * rho;
                        da = 2 * (rho * (z + (IsEpicycloid ? 1 + lambda : -1 - lambda)) + (IsEpicycloid ? -g : g));
                    }
                    else if(this.e.Enabled || this.h.Enabled)
                    {
                        if(this.h.Enabled)
                        {
                            e = this.h.Get();
                        }
                        else
                        {
                            e = this.e.Get();
                        }
                        da = df + (IsEpicycloid ? 4 : -4) * e;
                        rho = ((da / 2) + (IsEpicycloid ? g - e : e - g)) / (z + (IsEpicycloid ? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (IsEpicycloid ? 1 : -1));
                    }
                    else
                    {
                        throw new Exception("Unknown case alert!");
                    }
                }
                else if (this.dg.Enabled)
                {
                    dg = this.dg.Get();
                    if(this.e.Enabled || this.h.Enabled)
                    {
                        if(this.h.Enabled)
                        {
                            e = this.h.Get() / 2;
                        }
                        else
                        {
                            e = this.e.Get();
                        }
                        rho = dg / (2 * (z + (IsEpicycloid ? 1 : -1)));
                        lambda = e / rho;
                        da = 2 * (rho * (z + (IsEpicycloid ? 1 : -1) + lambda) - g);
                        df = da + (IsEpicycloid ? -4 : 4) * e;
                    }
                    else
                    {
                        throw new Exception("Unknown case alert!");
                    }
                }
                else
                {
                    throw new Exception("Unknown case alert!");
                }
                db = 2 * z * rho;
                dw = 2 * e * z;
            }
            else
            {
                string message = Vocabulary.BubbleMessages.Geometry.TheCliqueIsImproper() +
                             '\n' + Vocabulary.BubbleMessages.Geometry.PossibleCliquesAre() + ":\n";
                foreach (var p_clique in possibleCliques)
                {
                    message += '{';
                    foreach (var param in p_clique)
                    {
                        message += ' ' + param.NameCall() + ',';
                    }
                    message.Remove(message.LastIndexOf(','));
                    message += "},\n";
                }
                message.Remove(message.LastIndexOf(','));
                parameters.ToList().FindAll(x => x is ISingleEnableableParameter).ForEach(x => x.CallBubble(message));
            }
            
        }





        // Part to remove soon
        protected override Dictionary<Enum, Func<string>> NameCallGenerators()
        {
            throw new NotImplementedException();
        }
        protected override List<Enum> ObligatoryFloatParams()
        {
            throw new NotImplementedException();
        }
        protected override List<Enum> ObligatoryIntParams()
        {
            throw new NotImplementedException();
        }
    }
}
