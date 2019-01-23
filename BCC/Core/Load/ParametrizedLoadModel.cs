using BCC.Core.Parameters;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BCC.Core.Load
{
    class ParametrizedLoadModel : Model
    {
        private List<PageSeed> seeds = null;
        private readonly EnableableFloatParameter Egear, Esleeve, Ngear, Nsleeve;
        private readonly ParameterGroup gear, sleeve;
        private readonly IntParameter n;
        private readonly FloatParameter e, B, Rsleeve, Rspacing, M;
        private readonly UpDownListParameter DRhole, DRsleeve, DRhole_spacing,
            DRsleeve_spacing, DFhole, DFsleeve;
        private readonly UpDownParameterGroup ToleranceBox;
        private readonly UpDownSingleParameter De;
        private readonly (OutputListParameter<int> upper, OutputListParameter<int> lower) I;
        private readonly (OutputListParameter<double> upper, OutputListParameter<double> lower) Q, sigma;
        private readonly (TableParameterGroup upper, TableParameterGroup lower) DataTable;

        private Renderer renderer = null;

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
            n = new IntParameter(Vocabulary.ParameterLabels.Dimensioning.RollQuantity, value: 8);
            e = new FloatParameter(Vocabulary.ParameterLabels.Geometry.Eccentricity, value: 5.0);
            B = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.FaceWidth, value: 40.0);
            Rsleeve = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.SleeveRadius, value: 20.0);

            ToleranceBox = new UpDownParameterGroup(Vocabulary.ParameterLabels.Dimensioning.EngineeringTolerances);
            DRhole = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.HoleRadius);
            DRsleeve = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.SleeveRadius);
            DRhole_spacing = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.HoleSpacingRadius);
            DRsleeve_spacing = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.SleeveSpacingRadius);
            DFhole = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.HoleSpacingAngle);
            DFsleeve = new UpDownListParameter(Vocabulary.ParameterLabels.Dimensioning.SleeveSpacingAngle);
            De = new UpDownSingleParameter(Vocabulary.ParameterLabels.Geometry.Eccentricity);
            foreach (var param in new IUpDownParameter[] { DRhole, DRsleeve, DRhole_spacing, DRsleeve_spacing, DFhole, DFsleeve, De })
            {
                ToleranceBox.AddParameter(param);
            }

            Rspacing = new FloatParameter(Vocabulary.ParameterLabels.Dimensioning.HoleSpacingRadius, value: 100);
            M = new FloatParameter(Vocabulary.ParameterLabels.Result.Torque);
            I = (upper: new OutputListParameter<int>(Vocabulary.ParameterLabels.Result.RollNumber),
                lower: new OutputListParameter<int>(Vocabulary.ParameterLabels.Result.RollNumber));
            Q = (upper: new OutputListParameter<double>(Vocabulary.ParameterLabels.Result.Force),
                lower: new OutputListParameter<double>(Vocabulary.ParameterLabels.Result.Force));
            sigma = (upper: new OutputListParameter<double>(Vocabulary.ParameterLabels.Result.ContactStress),
                lower: new OutputListParameter<double>(Vocabulary.ParameterLabels.Result.ContactStress));
            DataTable = (upper: new TableParameterGroup(Vocabulary.ForUpperDeviation, new IOutputListParameter[] { I.upper, Q.upper, sigma.upper }),
                lower: new TableParameterGroup(Vocabulary.ForLowerDeviation, new IOutputListParameter[] { I.lower, Q.lower, sigma.lower }));
        }

        protected override List<PageSeed> PageSeeds()
        {
            if (seeds is null)
            {
                seeds = new List<PageSeed>();
                var dimensioningPageSeed = new PageSeed(Vocabulary.TabPagesNames.Dimensioning, new List<IParameter>()
                {
                    gear, sleeve, n, e, B, Rsleeve, ToleranceBox
                });
                seeds.Add(dimensioningPageSeed);
                var dimensioninigWorkSpace = dimensioningPageSeed.WorkSpace;
                var resultPageSeed = new PageSeed(Vocabulary.TabPagesNames.Results, new List<IParameter>()
                {
                    Rspacing, M, DataTable.upper, DataTable.lower
                });
                seeds.Add(resultPageSeed);
                var resultWorkSpace = resultPageSeed.WorkSpace;
                Chart ForceChart = new Chart
                {
                    Dock = DockStyle.Top,
                    Height = resultWorkSpace.Height / 2
                };
                Title forceTitle = new Title();
                ForceChart.Titles.Add(forceTitle);
                Vocabulary.AddNameCall(() => forceTitle.Text = Vocabulary.ParameterLabels.Result.CarriedForces());
                Chart StressChart = new Chart
                {
                    Dock = DockStyle.Fill
                };
                Title stressTitle = new Title();
                StressChart.Titles.Add(stressTitle);
                Vocabulary.AddNameCall(() => stressTitle.Text = Vocabulary.ParameterLabels.Result.ContactStresses());
                resultWorkSpace.Controls.Add(StressChart);
                resultWorkSpace.Controls.Add(ForceChart);
                resultWorkSpace.SizeChanged += (sender, e) => ForceChart.Height = resultWorkSpace.Height / 2;
                Chart LoosenessChart = new Chart
                {
                    Dock = DockStyle.Fill
                };
                Title loosenessTitle = new Title();
                LoosenessChart.Titles.Add(loosenessTitle);
                Vocabulary.AddNameCall(() => loosenessTitle.Text = Vocabulary.ParameterLabels.Result.LoosenessDistributionInMechanism());
                dimensioninigWorkSpace.Controls.Add(LoosenessChart);

                // Set font and colours
                foreach(var title in new Title[] { forceTitle, stressTitle, loosenessTitle})
                {
                    title.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
                    title.ForeColor = Color.White;
                }
                foreach(var chart in new Chart[] { ForceChart, StressChart, LoosenessChart})
                {
                    chart.BackColor = Color.FromArgb(0, 0, 64);
                }

                renderer = new Renderer(ForceChart, StressChart, LoosenessChart);
            }
            return seeds;
        }

        protected override void Act()
        {
            const int GEAR_COUNT = 2;
            var E = (gear: Egear.Get(), sleeve: Esleeve.Get());
            var N = (gear: Ngear.Get(), sleeve: Nsleeve.Get());
            var n = this.n.Get();
            ToleranceBox.SetCount(n);
            DataTable.upper.Count = DataTable.lower.Count = n;
            var (e, B, Rtz, Rw, M) = (this.e.Get(), this.B.Get(), this.Rsleeve.Get(), this.Rspacing.Get(), this.M.Get());
            var deltaRotw = (upper: DRhole.Uppers.ToArray(), lower: DRhole.Lowers.ToArray());
            var deltaRtz = (upper: DRsleeve.Uppers.ToArray(), lower: DRsleeve.Lowers.ToArray());
            var deltaRwk = (upper: DRhole_spacing.Uppers.ToArray(), lower: DRhole_spacing.Lowers.ToArray());
            var deltaRwt = (upper: DRsleeve_spacing.Uppers.ToArray(), lower: DRsleeve_spacing.Lowers.ToArray());
            var deltaPhik = (upper: DFhole.Uppers.ToArray(), lower: DFhole.Lowers.ToArray());
            var deltaPhit = (upper: DFsleeve.Uppers.ToArray(), lower: DFsleeve.Lowers.ToArray());
            var deltaE = (upper: De.Upper, lower: De.Lower);
            var l = B * GEAR_COUNT;
            I.upper.Set(Enumerable.Range(0, n).ToArray());
            I.lower.Set(Enumerable.Range(0, n).ToArray());

            var Rotw = Rtz + e;
            var deltaPhi = 2 * Math.PI / n;
            var Qmaxden = 0.0;
            for(int i = 0; i < n; i++)
            {
                Qmaxden += Math.Pow(Math.Sin(deltaPhi * i), 2);
            }
            var Qmax0 = M / (Rw * Qmaxden);
            var c = Math.Sqrt((2 * Qmax0 / (Math.PI * l))
                * (((1 - N.gear * N.gear) / E.gear) + (1 - N.sleeve * N.sleeve) / E.sleeve) * Rotw * Rtz / e);
            var deltamax = Qmax0 == 0 ? 0 : (2 * Qmax0 / (Math.PI * l)) * (((1 - N.gear * N.gear) / E.gear)
                * (1.0 / 3.0 + Math.Log(4 * Rotw / c)) + ((1 - N.sleeve * N.sleeve / E.sleeve)
                * (1.0 / 3.0 + Math.Log(4 * Rtz / c))));
            var Q = (upper: new double[n], lower: new double[n]);
            var sigma = (upper: new double[n], lower: new double[n]);
            var Delta = (upper: new double[n], lower: new double[n]);
            var Qden = (upper: 0.0, lower: 0.0);

            // Paste here the algorithm

            renderer.PushForces(Q.upper, Q.lower);
            renderer.PushLoosenesses(Delta.upper, Delta.lower);
            renderer.PushStresses(sigma.upper, sigma.lower);
        }

        private class Renderer
        {
            private readonly object dataLock = new object();
            private readonly ((Series upper, Series lower) point, (Series upper, Series lower) line) 
                ForceSeries, StressSeries, LoosenessSeries;
            private (List<double> upper, List<double> lower) forces, stresses, loosenesses;
            private bool dataAwaiting = false;
            private bool running = true;
            private readonly Dictionary<Chart, double> boundary;
            private const double LOG_BASE = 4.0;
            private const double BASE_VALUE = 0.001;
            private const int INTERVAL = 100;
            private int count;
            protected delegate void SetSeriesCallBack(Chart parent, Series series, double[] points);
            protected delegate void SetBoudariesCallBack(Chart parent, ChartArea area, double bound, int n);

            public Renderer(Chart ForceChart, Chart StressChart, Chart LoosenessChart)
            {
                // Probably unnecesary operation
                forces = stresses = loosenesses = (new List<double>(), new List<double>());

                ForceChart.Disposed += (sender, e) => this.Dispose();

                // Assigning areas and legends to charts
                foreach(var chart in new Chart[]
                {
                    ForceChart, StressChart, LoosenessChart
                })
                {
                    var area = new ChartArea { Name = "Area", BackColor = Color.LightGray };
                    area.AxisX.TitleForeColor = area.AxisY.TitleForeColor = Color.Wheat;
                    area.AxisX.LabelStyle.ForeColor = area.AxisY.LabelStyle.ForeColor = Color.Wheat;
                    area.AxisX.LineColor = area.AxisY.LineColor = area.AxisX.MajorGrid.LineColor = area.AxisY.MajorGrid.LineColor = Color.Gray;
                    chart.ChartAreas.Add(area);
                    chart.Legends.Add(new Legend { Name = "Legend", BackColor = Color.Navy, ForeColor = Color.LightGray });
                }

                ForceSeries = ((new Series(), new Series()), (new Series(), new Series()));
                StressSeries = ((new Series(), new Series()), (new Series(), new Series()));
                LoosenessSeries = ((new Series(), new Series()), (new Series(), new Series()));
                
                boundary = new Dictionary<Chart, double>()
                {
                    {ForceChart, BASE_VALUE },
                    {StressChart, BASE_VALUE },
                    {LoosenessChart, BASE_VALUE }
                };

                // Setting all series
                foreach(var series in new Series[]
                {
                    ForceSeries.line.upper, ForceSeries.line.lower,
                    ForceSeries.point.upper, ForceSeries.point.lower,
                    StressSeries.line.upper, StressSeries.line.lower,
                    StressSeries.point.upper, StressSeries.point.lower,
                    LoosenessSeries.line.upper, LoosenessSeries.line.lower,
                    LoosenessSeries.point.upper, LoosenessSeries.point.lower
                })
                {
                    series.ChartArea = "Area";
                    series.Legend = "Legend";
                    series.ChartType = SeriesChartType.Point;
                    series.LabelForeColor = Color.Wheat;
                }

                // Distinguishing all line series
                foreach(var series in new Series[]
                {
                    ForceSeries.line.upper, ForceSeries.line.lower,
                    StressSeries.line.upper, StressSeries.line.lower,
                    LoosenessSeries.line.upper, LoosenessSeries.line.lower
                })
                {
                    series.IsVisibleInLegend = false;
                    series.ChartType = SeriesChartType.Line;
                }

                // Assigning all series to their charts
                foreach(var series in new Series[]
                {
                    ForceSeries.line.upper, ForceSeries.line.lower,
                    ForceSeries.point.upper, ForceSeries.point.lower
                })
                {
                    ForceChart.Series.Add(series);
                }
                foreach (var series in new Series[]
                {
                    StressSeries.line.upper, StressSeries.line.lower,
                    StressSeries.point.upper, StressSeries.point.lower
                })
                {
                    StressChart.Series.Add(series);
                }
                foreach (var series in new Series[]
                {
                    LoosenessSeries.line.upper, LoosenessSeries.line.lower,
                    LoosenessSeries.point.upper, LoosenessSeries.point.lower
                })
                {
                    LoosenessChart.Series.Add(series);
                }

                Vocabulary.AddNameCall(() =>
                {
                    foreach(var series in new Series[]
                    {
                        ForceSeries.point.upper, StressSeries.point.upper, LoosenessSeries.point.upper
                    })
                    {
                        series.Name = Vocabulary.ForUpperDeviation();
                    }

                    foreach (var series in new Series[]
                    {
                        ForceSeries.point.lower, StressSeries.point.lower, LoosenessSeries.point.lower
                    })
                    {
                        series.Name = Vocabulary.ForLowerDeviation();
                    }
                });

                void SetSeries(Chart parent, Series series, double[] points)
                {
                    if(parent.InvokeRequired)
                    {
                        var d = new SetSeriesCallBack(SetSeries);
                        parent.Invoke(d, new object[] { parent, series, points });
                    }
                    else
                    {
                        var bound = GetBoundary(parent);
                        series.Points.Clear();
                        for(int i = 0; i < points.Length; i++)
                        {
                            series.Points.AddXY(i, points[i]);
                            bound = Math.Max(bound, Math.Abs(points[i]));
                        }
                        SetBoundary(parent, bound);
                    }
                }

                void SetBoundaries(Chart parent, ChartArea area, double bound, int n)
                {
                    if(parent.InvokeRequired)
                    {
                        var d = new SetBoudariesCallBack(SetBoundaries);
                        parent.Invoke(d, new object[] { parent, area, bound, n });
                    }
                    else
                    {
                        area.AxisY.Maximum = bound;
                        area.AxisY.Minimum = -bound;
                        area.AxisX.Maximum = n < 1 ? 1 : n - 1;
                        area.AxisX.Minimum = 0;
                    }
                }

                new Task(() =>
                {
                    while(Running)
                    {
                        if(DataAwaiting)
                        {
                            SetBoundary(ForceChart, BASE_VALUE);
                            SetSeries(ForceChart, ForceSeries.line.upper, forces.upper.ToArray());
                            SetSeries(ForceChart, ForceSeries.point.upper, forces.upper.ToArray());
                            SetSeries(ForceChart, ForceSeries.line.lower, forces.lower.ToArray());
                            SetSeries(ForceChart, ForceSeries.point.lower, forces.lower.ToArray());
                            var forceBound = BASE_VALUE * Math.Pow(LOG_BASE, Math.Ceiling(Math.Log(GetBoundary(ForceChart) / BASE_VALUE, LOG_BASE)));
                            SetBoundaries(ForceChart, ForceChart.ChartAreas[0], forceBound, Count);

                            SetBoundary(StressChart, BASE_VALUE);
                            SetSeries(StressChart, StressSeries.line.upper, stresses.upper.ToArray());
                            SetSeries(StressChart, StressSeries.point.upper, stresses.upper.ToArray());
                            SetSeries(StressChart, StressSeries.line.lower, stresses.lower.ToArray());
                            SetSeries(StressChart, StressSeries.point.lower, stresses.lower.ToArray());
                            var stressBound = BASE_VALUE * Math.Pow(LOG_BASE, Math.Ceiling(Math.Log(GetBoundary(StressChart) / BASE_VALUE, LOG_BASE)));
                            SetBoundaries(StressChart, StressChart.ChartAreas[0], stressBound, Count);

                            SetBoundary(LoosenessChart, BASE_VALUE);
                            SetSeries(LoosenessChart, LoosenessSeries.line.upper, loosenesses.upper.ToArray());
                            SetSeries(LoosenessChart, LoosenessSeries.point.upper, loosenesses.upper.ToArray());
                            SetSeries(LoosenessChart, LoosenessSeries.line.lower, loosenesses.lower.ToArray());
                            SetSeries(LoosenessChart, LoosenessSeries.point.lower, loosenesses.lower.ToArray());
                            var loosenessBound = BASE_VALUE * Math.Pow(LOG_BASE, Math.Ceiling(Math.Log(GetBoundary(LoosenessChart) / BASE_VALUE, LOG_BASE)));
                            SetBoundaries(LoosenessChart, LoosenessChart.ChartAreas[0], loosenessBound, Count);
                        }
                        Thread.Sleep(INTERVAL);
                    }
                }).Start();
            }

            public void PushForces(double[] upper, double[] lower)
            {
                lock(dataLock)
                {
                    forces = (new List<double>(upper), new List<double>(lower));
                    dataAwaiting = true;
                    count = upper.Length;
                }
            }

            public void PushStresses(double[] upper, double[] lower)
            {
                lock(dataLock)
                {
                    stresses = (new List<double>(upper), new List<double>(lower));
                    dataAwaiting = true;
                    count = upper.Length;
                }
            }

            public void PushLoosenesses(double[] upper, double[] lower)
            {
                lock(dataLock)
                {
                    loosenesses = (new List<double>(upper), new List<double>(lower));
                    dataAwaiting = true;
                    count = upper.Length;
                }
            }

            public void Dispose()
            {
                lock(dataLock)
                {
                    running = false;
                }
            }

            private bool Running
            {
                get {
                    var ret = true;
                    lock (dataLock) { ret = running; }
                    return ret;
                }
            }

            private bool DataAwaiting { get
                {
                    bool ret;
                    lock(dataLock)
                    {
                        ret = dataAwaiting;
                    }
                    return ret;
                }
                set
                {
                    lock(dataLock)
                    {
                        dataAwaiting = value;
                    }
                }
            }

            private void SetBoundary(Chart chart, double value)
            {
                lock(dataLock)
                {
                    if (value < BASE_VALUE) boundary[chart] = BASE_VALUE;
                    else boundary[chart] = value;
                }
            }

            private double GetBoundary(Chart chart)
            {
                double ret;
                lock(dataLock)
                {
                    ret = boundary[chart];
                }
                return ret;
            }

            private int Count { get
                {
                    int ret;
                    lock(dataLock)
                    {
                        ret = count;
                    }
                    return ret;
                }
                set
                {
                    lock(dataLock)
                    {
                        count = value;
                    }
                }
            }
        }
    }
}