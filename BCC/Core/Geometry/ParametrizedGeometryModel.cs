using BCC.Core.Parameters;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Geometry
{
    class ParametrizedGeometryModel : Model
    {
        // Static fields
        private const int PRIMES_BOUND = 50;
        private static readonly int[] PRIMES =
                (from i in Enumerable.Range(2, PRIMES_BOUND).AsParallel()
                 where Enumerable.Range(2, (int)Math.Sqrt(i)).All(j => i % j != 0)
                 select i).ToArray();
        private const double BOUND_MARGIN = 1.2;
        private static readonly Pen widePen = new Pen(Brushes.White)
        {
            Width = 2.0F,
            LineJoin = LineJoin.Bevel
        };
        private const int DEFAULT_CURVE_RESOLUTION = 10000;

        // Seeds to return
        private List<PageSeed> seeds = null;

        // Parameters
        private readonly OutputParameterList output;
        private readonly IntParameter z;
        private readonly FloatParameter g;
        private readonly EnableableFloatParameter da, df, e, h, dg;
        private readonly OutputSingleParameter lambda, dw, rho, db;
        private readonly ParameterGroup epi;
        private readonly IParameter[] parameters;
        private readonly ISingleEnableableParameter[][] possibleCliques;

        // Auxiliary variables
        private readonly int EPICYCLOID, HIPOCYCLOID;
        private Renderer renderer = null;
        private Graphics graphics;

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
                renderer = new Renderer(geometrySeed.WorkSpace);
                graphics = geometrySeed.G;
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
                bool isEpicycloid = IsEpicycloid;
                if (this.da.Enabled)
                {
                    da = this.da.Get();
                    if (this.df.Enabled)
                    {
                        df = this.df.Get();
                        e = (da - df) / (isEpicycloid? 4 : -4);
                        rho = ((da / 2) + (isEpicycloid? g - e : e - g)) / (z + (isEpicycloid? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (isEpicycloid? 1 : -1));
                    }
                    else if (this.dg.Enabled)
                    {
                        dg = this.dg.Get();
                        rho = dg / (2 * (z + (isEpicycloid? 1 : -1)));
                        lambda = (isEpicycloid? 1 : -1) * (0.5 * da - rho * (z + (isEpicycloid? 1 : -1))
                            + (isEpicycloid? g : -g)) / rho;
                        e = lambda * rho;
                        df = 2 * (rho * (z + (isEpicycloid? 1 : -1) - lambda) + (isEpicycloid? -g : g));
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
                        df = da + (isEpicycloid? -4 : 4) * e;
                        rho = ((da / 2) + (isEpicycloid? g - e : e - g)) / (z + (isEpicycloid? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (isEpicycloid? 1 : -1));
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
                        rho = dg / (2 * (z + (isEpicycloid? 1 : -1)));
                        lambda = (isEpicycloid? -1 : 1) * (0.5 * df - rho * (z + (isEpicycloid? 1 : -1)) + (isEpicycloid? g : -g)) / rho;
                        e = lambda * rho;
                        da = 2 * (rho * (z + (isEpicycloid? 1 + lambda : -1 - lambda)) + (isEpicycloid? -g : g));
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
                        da = df + (isEpicycloid? 4 : -4) * e;
                        rho = ((da / 2) + (isEpicycloid? g - e : e - g)) / (z + (isEpicycloid? 1 : -1));
                        lambda = e / rho;
                        dg = 2 * rho * (z + (isEpicycloid? 1 : -1));
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
                        rho = dg / (2 * (z + (isEpicycloid? 1 : -1)));
                        lambda = e / rho;
                        da = 2 * (rho * (z + (isEpicycloid? 1 : -1) + lambda) - g);
                        df = da + (isEpicycloid? -4 : 4) * e;
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
                // Setters
                new Action(() => 
                {
                    this.da.Set(da);
                    this.df.Set(df);
                    this.dg.Set(dg);
                    this.e.Set(e);
                    h.Set(2 * e);
                    this.lambda.Set(lambda);
                    this.dw.Set(dw);
                    this.rho.Set(rho);
                    this.db.Set(db);
                })();
                
                if(lambda > 1 || lambda < ((z + (isEpicycloid? -1 : 1)) / (2 * z + (isEpicycloid? 1 : -1))))
                {
                    this.lambda.CallBubble(Vocabulary.BubbleMessages.Geometry.CurvatureRequirementNotMet());
                }
                if(e < Math.Sqrt(lambda * lambda / (1 - lambda * lambda))
                    * Math.Sqrt(1 + (isEpicycloid? 2 / z : -2 / z))
                    * (z + (isEpicycloid? 2 : -2)) / (Math.Sqrt(27) * (z + (isEpicycloid? 1 : -1))) * g)
                {
                    this.e.CallBubble(Vocabulary.BubbleMessages.Geometry.ToothCutRequirementNotMet());
                }
                if(e <= g * lambda / ((z + (isEpicycloid? 1 : 0)) * Math.Sin(Math.PI / (z + (isEpicycloid? 1 : -1)))))
                {
                    this.g.CallBubble(Vocabulary.BubbleMessages.Geometry.RollNeighbourhoodRequirementNotMet());
                }
                new Task(() =>
                {
                    if (new List<double>() { g, da, df, dg, e, lambda, dw, rho, db }.All(x => x > 0) && !(renderer is null))
                    {
                        Func<double, PointF> curve;
                        if(isEpicycloid)
                        {
                            float X(double t) => (float)(rho * ((z + 1) * Math.Cos(t) 
                            - lambda * Math.Cos((z + 1) * t)) - g * (Math.Cos(t) 
                            - lambda * Math.Cos((z + 1) * t)) / Math.Sqrt(1 - 2 * lambda * Math.Cos(z * t) + lambda * lambda));
                            float Y(double t) => (float)(rho * ((z + 1) * Math.Sin(t) 
                            - lambda * Math.Sin((z + 1) * t)) - g * (Math.Sin(t) 
                            - lambda * Math.Sin((z + 1) * t)) / Math.Sqrt(1 - 2 * lambda * Math.Cos(z * t) + lambda * lambda));
                            curve = new Func<double, PointF>(t => new PointF(X(t), Y(t)));
                        }
                        else
                        {
                            float X(double t) => (float)(rho * ((z - 1) * Math.Cos(t) 
                            + lambda * Math.Cos((z - 1) * t)) + g * (Math.Cos(t) 
                            - lambda * Math.Cos((z - 1) * t)) / Math.Sqrt(1 - 2 * lambda * Math.Cos(z * t) + lambda * lambda));
                            float Y(double t) => (float)(rho * ((z - 1) * Math.Sin(t) 
                            - lambda * Math.Sin((z - 1) * t)) + g * (Math.Sin(t) 
                            + lambda * Math.Sin((z - 1) * t)) / Math.Sqrt(1 - 2 * lambda * Math.Cos(z * t) + lambda * lambda));
                            curve =  new Func<double, PointF>(t => new PointF(X(t), Y(t)));
                        }
                        double distance(PointF p) => Math.Sqrt(p.X * p.X + p.Y * p.Y);
                        var bound = (curve(0).X + 2 * e) * BOUND_MARGIN;
                        foreach (var prime in PRIMES)
                        {
                            var dt = 2.0 * Math.PI / prime;
                            for (int i = 1; i < prime; i++)
                            {
                                var temp = distance(curve(dt * i));
                                if (temp > bound) bound = temp;
                            }
                        }
                        renderer.SetAction(workSpace =>
                        {
                            var width = workSpace.Width;
                            var height = workSpace.Height;
                            var box = width > height ? height : width;
                            var factor = 0.5 * box / bound;
                            var x0 = width / 2;
                            var y0 = height / 2;
                            var curvePoints = new List<PointF>();
                            for (int i = 0; i < DEFAULT_CURVE_RESOLUTION; i++)
                            {
                                var t = 2.0 * Math.PI * i / DEFAULT_CURVE_RESOLUTION;
                                var x = (float)(x0 + curve(t).X * factor);
                                var y = (float)(y0 + curve(t).Y * factor);
                                curvePoints.Add(new PointF(x, y));
                            }
                            graphics.Clear(Color.Black);
                            graphics.DrawClosedCurve(widePen, curvePoints.ToArray());
                        });
                    }
                }).Start();
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

        private class Renderer
        {
            private Action action = () => { };
            private bool actionAwaiting = false;
            private bool disposing = false;
            private readonly object dataLock = new object();
            private readonly Panel workSpace;
            private const int INTERVAL = 100;

            public Renderer(Panel workSpace)
            {
                this.workSpace = workSpace;
                workSpace.SizeChanged += (sender, e) =>
                {
                    if (workSpace.Visible) this.RequestUpdate();
                };
                workSpace.VisibleChanged += (sender, e) => this.RequestUpdate();
                workSpace.Paint += (sender, e) => this.RequestUpdate();
                workSpace.Disposed += (sender, e) =>
                {
                    lock(dataLock)
                    {
                        disposing = true;
                    }
                };
                new Task(() =>
                {  while(new Func<bool>(() => 
                    {
                        bool disposing;
                        lock(dataLock)
                        {
                            disposing = this.disposing;
                        }
                        return !disposing;
                    })())
                    {
                        if (ActionRequested())
                        {
                            lock(dataLock)
                            {
                                action();
                                actionAwaiting = false;
                            }
                        }
                        Thread.Sleep(INTERVAL);
                    }
                }).Start();
            }

            public void SetAction(Action<Panel> toSet)
            {
                lock(dataLock)
                {
                    this.action = () => toSet(workSpace);
                    this.actionAwaiting = true;
                }
            }

            public void RequestUpdate()
            {
                lock(dataLock)
                {
                    actionAwaiting = true;
                }
            }

            private bool ActionRequested()
            {
                bool ret;
                lock(dataLock)
                {
                    ret = actionAwaiting;
                }
                return ret;
            }
        }

    }
}
