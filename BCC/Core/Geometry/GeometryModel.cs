using BCC.Interface_View.StandardInterface;
using BCC.Interface_View.StandardInterface.Geometry;
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
    public enum CycloParams
    {
        Z,
        G,
        DA,
        DF,
        E,
        H,
        DG,
        Λ,
        DW,
        Ρ,
        DB,
        EPI
    }

    abstract class GeometryModel
    {
        // The view and the control for geometry computation
        protected GeometryMenu view = null;

        protected readonly Dictionary<CycloParams, Action<string>> BubbleCalls = new Dictionary<CycloParams, Action<string>>();

        public static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 360;
            public static readonly int VALUE_PRECISION = 3;
            public static readonly double TRUE = 1.0, FALSE = 2.0;
            public static readonly double NULL = double.NegativeInfinity;
        }

        private static class NameCalls
        {
            // Name calls for parameters
            public static Func<string> CallName(CycloParams param)
            {
                switch (param)
                {
                    case CycloParams.Z:
                        return () => Vocabulary.ParameterLabels.Geometry.TeethQuantity();
                    case CycloParams.G:
                        return () => Vocabulary.ParameterLabels.Geometry.RollRadius();
                    case CycloParams.DA:
                        return () => Vocabulary.ParameterLabels.Geometry.MajorDiameter();
                    case CycloParams.DF:
                        return () => Vocabulary.ParameterLabels.Geometry.RootDiameter();
                    case CycloParams.E:
                        return () => Vocabulary.ParameterLabels.Geometry.Eccentricity();
                    case CycloParams.H:
                        return () => Vocabulary.ParameterLabels.Geometry.ToothHeight();
                    case CycloParams.DG:
                        return () => Vocabulary.ParameterLabels.Geometry.RollSpacingDiameter();
                    case CycloParams.Λ:
                        return () => Vocabulary.ParameterLabels.Geometry.ToothHeightFactor();
                    case CycloParams.DW:
                        return () => Vocabulary.ParameterLabels.Geometry.PinSpacingDiameter();
                    case CycloParams.Ρ:
                        return () => Vocabulary.ParameterLabels.Geometry.RollingCircleDiameter();
                    case CycloParams.DB:
                        return () => Vocabulary.ParameterLabels.Geometry.BaseDiameter();
                    case CycloParams.EPI:
                        return () => Vocabulary.ParameterLabels.Geometry.ProfileType();
                    default:
                        return () => Vocabulary.NotImplementedYet();
                }
            }


        }

        // Parameters lists
        protected abstract List<CycloParams> OptionalParams();
        protected abstract List<CycloParams> ObligatoryIntParams();
        protected abstract List<CycloParams> ObligatoryFloatParams();
        protected abstract List<CycloParams> OutputParams();
        protected abstract List<List<CycloParams>> PossibleCliques();

        protected List<List<CycloParams>> PossibleCliques(List<CycloParams> clique)
        {
            if (clique.Count() == 0) return PossibleCliques();
            var possibleCompletions = new List<List<CycloParams>>();
            foreach (var v in PossibleCliques())
            {
                possibleCompletions.Add(new List<CycloParams>(v));
            }
            foreach (var param in clique)
            {
                possibleCompletions = possibleCompletions.FindAll(l => l.Contains(param));
                possibleCompletions.ForEach(l => l.Remove(param));
            }
            return possibleCompletions.Distinct().ToList();
        }

        // Generating and binding a view with the model
        public Dictionary<UserControl, Func<string>> GetMenus()
        {
            GeometryModel parentModel = this;
            if (view is null)
            {
                var parameterControls = new List<Control>();
                var toolTip = new ToolTip();

                int ParamBoxWidth() => StaticFields.PARAM_BOX_WIDTH - 20;
                Dictionary<CycloParams, Func<double>> getterCalls = new Dictionary<CycloParams, Func<double>>();
                Dictionary<CycloParams, Action<double>> setterCalls = new Dictionary<CycloParams, Action<double>>();
                Dictionary<CycloParams, Func<bool>> availabilityCalls = new Dictionary<CycloParams, Func<bool>>();

                var nameCallGenerators = NameCallGenerators();

                // Initialization of groupBoxes
                new Action(() => 
                {
                    int i = 1;

                    // Profile type group
                    new Action(() => 
                    {
                        // 
                        // ProfileTypeGroupBox
                        // 
                        var ProfileTypeGroupBox = new GroupBox
                        {
                            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold),
                            Size = new System.Drawing.Size(ParamBoxWidth(), 61),
                            TabIndex = 1,
                            TabStop = false,
                            Dock = DockStyle.Top
                        };
                        Vocabulary.AddNameCall(new Action(() => {
                            ProfileTypeGroupBox.Text = Vocabulary.ParameterLabels.Geometry.ProfileType();
                        }));
                        // 
                        // ProfileTypeComboBox
                        // Profile type set only by user
                        // 
                        var ProfileTypeComboBox = new ComboBox
                        {
                            Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                            FormattingEnabled = true,
                            Location = new System.Drawing.Point(6, 25),
                            Size = new System.Drawing.Size(121, 24),
                            TabIndex = 1
                        };
                        ProfileTypeComboBox.Items.AddRange(new object[] {
                            Vocabulary.ParameterLabels.Geometry.Epicycloid(),
                            Vocabulary.ParameterLabels.Geometry.Hipocycloid()});///////
                        ProfileTypeGroupBox.Controls.Add(ProfileTypeComboBox);
                        parameterControls.Add(ProfileTypeGroupBox);
                        Vocabulary.AddNameCall(new Action(() => {
                            ProfileTypeComboBox.Items.Clear();
                            ProfileTypeComboBox.Items.AddRange(new object[] {
                        Vocabulary.ParameterLabels.Geometry.Epicycloid(),
                        Vocabulary.ParameterLabels.Geometry.Hipocycloid()});
                        }));
                        getterCalls.Add(CycloParams.EPI, () => ProfileTypeComboBox.SelectedIndex < 1 ? StaticFields.TRUE : StaticFields.FALSE);
                        BubbleCalls.Add(CycloParams.EPI, message => 
                        {
                            if(message is null)
                            {
                                ProfileTypeComboBox.BackColor = SystemColors.Window;
                                toolTip.SetToolTip(ProfileTypeGroupBox, "");
                            }
                            else
                            {
                                ProfileTypeComboBox.BackColor = Color.Red;
                                toolTip.SetToolTip(ProfileTypeGroupBox, message);
                            }
                        });
                        ProfileTypeComboBox.SelectedIndex = 0;
                        // 
                        ProfileTypeComboBox.SelectedIndexChanged += (sender, e) => Act();
                    })();

                    // Obligatory integer parameters
                    new Action(() => {
                        
                        foreach(var param in ObligatoryIntParams())
                        {
                            // 
                            // ParameterGroupBox
                            // 
                            var ParameterGroupBox = new GroupBox()
                            {
                                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                                Size = new System.Drawing.Size(ParamBoxWidth(), 48),
                                TabIndex = i++,
                                TabStop = false,
                                Dock = DockStyle.Top
                            };
                            Vocabulary.AddNameCall(() => ParameterGroupBox.Text = nameCallGenerators[param]());
                            // 
                            // ParameterUpDown
                            // The parameter is set only by user
                            //
                            var ParameterUpDown = new NumericUpDown()
                            {
                                Location = new System.Drawing.Point(7, 22),
                                Size = new System.Drawing.Size(120, 22),
                                TabIndex = 0,
                                Maximum = 100,
                                Minimum = 1,
                                Value = 1
                            };
                            ParameterGroupBox.Controls.Add(ParameterUpDown);
                            parameterControls.Add(ParameterGroupBox);
                            ParameterUpDown.ValueChanged += (sender, e) => Act();
                            getterCalls.Add(param, () => (double)ParameterUpDown.Value);
                            BubbleCalls.Add(param, message =>
                            {
                                if (message is null)
                                {
                                    ParameterUpDown.BackColor = SystemColors.Window;
                                    toolTip.SetToolTip(ParameterGroupBox, "");
                                }
                                else
                                {
                                    ParameterUpDown.BackColor = Color.Red;
                                    toolTip.SetToolTip(ParameterGroupBox, message);
                                }
                            });
                        }
                    })();

                    // Obligatory floating-point parameters
                    new Action(() => {
                        foreach(var param in ObligatoryFloatParams())
                        {
                            // 
                            // ParameterGroupBox
                            // 
                            var ParameterGroupBox = new GroupBox()
                            {
                                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                                Size = new System.Drawing.Size(ParamBoxWidth(), 48),
                                TabIndex = i++,
                                TabStop = false,
                                Dock = DockStyle.Top
                            };
                            Vocabulary.AddNameCall(() => ParameterGroupBox.Text = nameCallGenerators[param]());
                            // 
                            // ParameterValueBox
                            // The parameter is set only by user
                            // 
                            var ParameterUpDown = new NumericUpDown()
                            {
                                Location = new System.Drawing.Point(7, 22),
                                Size = new System.Drawing.Size(120, 22),
                                TabIndex = 0,
                                Maximum = 10000,
                                Value = (decimal)5.0,
                                Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                DecimalPlaces = StaticFields.VALUE_PRECISION
                            };
                            ParameterGroupBox.Controls.Add(ParameterUpDown);
                            parameterControls.Add(ParameterGroupBox);
                            ParameterUpDown.ValueChanged += (sender, e) => Act();
                            getterCalls.Add(param, () => (double)ParameterUpDown.Value);
                            BubbleCalls.Add(param, message =>
                            {
                                if (message is null)
                                {
                                    ParameterUpDown.BackColor = SystemColors.Window;
                                    toolTip.SetToolTip(ParameterGroupBox, "");
                                }
                                else
                                {
                                    ParameterUpDown.BackColor = Color.Red;
                                    toolTip.SetToolTip(ParameterGroupBox, message);
                                }
                            });
                        }
                    })();

                    // Optional input parameters groups loop
                    new Action(() => {
                        foreach (var param in OptionalParams())
                        {
                            // 
                            // ParameterGroupBox
                            // 
                            var ParameterGroupBox = new GroupBox
                            {
                                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                                Size = new System.Drawing.Size(ParamBoxWidth(), 49),
                                TabIndex = i++,
                                TabStop = false,
                                Dock = DockStyle.Top
                            };
                            Vocabulary.AddNameCall(() => ParameterGroupBox.Text = nameCallGenerators[param]());
                            // 
                            // ParameterValueBox
                            // Value set either by user or by model
                            // 
                            var ParameterUpDown = new NumericUpDown()
                            {
                                Location = new System.Drawing.Point(7, 22),
                                Size = new System.Drawing.Size(120, 22),
                                TabIndex = 0,
                                Maximum = 10000,
                                Value = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                DecimalPlaces = StaticFields.VALUE_PRECISION,
                                Enabled = false
                            };
                            ParameterGroupBox.Controls.Add(ParameterUpDown);
                            parameterControls.Add(ParameterGroupBox);
                            ParameterUpDown.ValueChanged += (sender, e) => Act();
                            getterCalls.Add(param, () => (double)ParameterUpDown.Value);
                            BubbleCalls.Add(param, message =>
                            {
                                if (message is null)
                                {
                                    ParameterUpDown.BackColor = SystemColors.Window;
                                    toolTip.SetToolTip(ParameterGroupBox, "");
                                }
                                else
                                {
                                    ParameterUpDown.BackColor = Color.Red;
                                    toolTip.SetToolTip(ParameterGroupBox, message);
                                }
                            });
                            setterCalls.Add(param, v => ParameterUpDown.Text = "" + v);
                            // 
                            // ParameterCheckBox
                            // 
                            var ParameterCheckBox = new CheckBox
                            {
                                AutoSize = true,
                                Location = new System.Drawing.Point(167, 22),
                                Size = new System.Drawing.Size(15, 14),
                                TabIndex = 1,
                                UseVisualStyleBackColor = true
                            };
                            ParameterGroupBox.Controls.Add(ParameterCheckBox);
                            ParameterGroupBox.Controls.Add(ParameterUpDown);
                            ParameterCheckBox.CheckedChanged += new EventHandler((sender, e) =>
                            {
                                ParameterUpDown.Enabled = ParameterCheckBox.Checked;
                            });
                            parameterControls.Add(ParameterGroupBox);
                            availabilityCalls.Add(param, () => ParameterUpDown.Enabled);
                        }
                    })();

                    // Output parameters groups loop
                    new Action(() => {

                        // 
                        // OutputTableLayout
                        // 
                        var OutputTableLayout = new TableLayoutPanel
                        {
                            AutoSize = true,
                            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink,
                            ColumnCount = 2,
                            RowCount = 0,
                            TabIndex = i++,
                            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                        };
                        OutputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        OutputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        OutputTableLayout.RowStyles.Clear();
                        int currentRow = 0;
                        foreach (var param in OutputParams())
                        {
                            var ParameterNameLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new System.Drawing.Point(3, 3),
                                Size = new System.Drawing.Size(50, 15),
                                TabIndex = OutputTableLayout.RowCount * 2
                            };
                            Vocabulary.AddNameCall(() => ParameterNameLabel.Text = nameCallGenerators[param]());
                            // Value set by model
                            var ParameterValueLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new System.Drawing.Point(3, 3),
                                Size = new System.Drawing.Size(50, 15),
                                TabIndex = OutputTableLayout.RowCount * 2 + 1
                            };
                            setterCalls.Add(param, v => ParameterValueLabel.Text = "" + v);
                            OutputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                            OutputTableLayout.Controls.Add(ParameterNameLabel, 0, currentRow);
                            OutputTableLayout.Controls.Add(ParameterValueLabel, 1, currentRow++);
                            BubbleCalls.Add(param, message =>
                            {
                                if (message is null)
                                {
                                    ParameterValueLabel.BackColor = SystemColors.Control;
                                    toolTip.SetToolTip(ParameterValueLabel, "");
                                }
                                else
                                {
                                    ParameterValueLabel.BackColor = Color.Red;
                                    toolTip.SetToolTip(ParameterValueLabel, message);
                                }
                            });
                        }
                        OutputTableLayout.Size = new System.Drawing.Size(ParamBoxWidth(), OutputTableLayout.RowCount * 30);
                        parameterControls.Add(OutputTableLayout);
                    })();
                })();

                view = new GeometryMenu(new GeometryMenu.InitialParameters
                {
                    model = this,
                    PARAMBOX_WIDTH = StaticFields.PARAM_BOX_WIDTH,
                    parameterControls = parameterControls,
                    getterCalls = getterCalls,
                    setterCalls = setterCalls,
                    availabilityCalls = availabilityCalls
                })
                {
                    Visible = true,
                    Enabled = true,
                    AutoSize = true,
                    Dock = DockStyle.Fill
                };
            }
            return new Dictionary<UserControl, Func<string>>()
            {
                {view, Vocabulary.TabPagesNames.Geometry}
            };
        }

        // Transferring data to and from the view
        public Dictionary<CycloParams, double> DownLoadData()
        {
            var ret = new Dictionary<CycloParams, double>
            {
                { CycloParams.EPI, view.Get(CycloParams.EPI) }
            };
            foreach (var param in ObligatoryIntParams())
            {
                ret.Add(param, view.Get(param));
            }
            foreach(var param in ObligatoryFloatParams())
            {
                ret.Add(param, view.Get(param));
            }
            foreach(var param in OptionalParams())
            {
                if (view.IsAvailable(param)) ret.Add(param, view.Get(param));
            }
            return ret;
        }
        public void UpLoadData(Dictionary<CycloParams, double> data)
        {
            foreach(var param in data)
            {
                view.Set(param.Key, param.Value);
            }
        }

        protected abstract bool IsCurvatureRequirementMet(Dictionary<CycloParams, double> vals);
        protected abstract bool IsToothCuttingRequirementMet(Dictionary<CycloParams, double> vals);
        protected abstract bool IsNeighbourhoodRequirementMet(Dictionary<CycloParams, double> vals);

        // Overall requirements checker
        protected bool AreRequirementsMet(Dictionary<CycloParams, double> vals)
        {
            foreach(var call in BubbleCalls)
            {
                call.Value(null);
            }
            var ret = true;
            ret = ret && IsCurvatureRequirementMet(vals);
            ret = ret && IsToothCuttingRequirementMet(vals);
            ret = ret && IsNeighbourhoodRequirementMet(vals);
            return ret;
        }
        
        protected abstract Dictionary<CycloParams, Func<string>> NameCallGenerators();

        public void Act()
        {
            List<CycloParams> available = new List<CycloParams>();
            foreach(var param in OptionalParams())
            {
                if (view.IsAvailable(param)) available.Add(param);
            }
            if (new Func<bool>(() =>
             {
                 var found = false;
                 foreach (var p_clique in PossibleCliques())
                 {
                     var fits = true;
                     var temp = new List<CycloParams>(available);
                     foreach (var p in p_clique)
                     {
                         if (temp.Contains(p))
                         {
                             temp.Remove(p);
                         }
                         else fits = false;
                     }
                     if (fits) found = true && temp.Count == 0;
                 }
                 if (found) return true;
                 else
                 {
                     foreach (var param in OptionalParams())
                     {
                         BubbleCalls[param](new Func<string>(() =>
                         {
                             string message = Vocabulary.BubbleMessages.Geometry.TheCliqueIsImproper() +
                             '\n' + Vocabulary.BubbleMessages.Geometry.PossibleCliquesAre() + ":\n";
                             foreach (var p_clique in PossibleCliques())
                             {
                                 message += '{';
                                 foreach (var p in p_clique)
                                 {
                                     message += ' ' + NameCalls.CallName(p)() + ',';
                                 }
                                 message.Remove(message.LastIndexOf(','));
                                 message += "},\n";
                             }
                             message.Remove(message.LastIndexOf(','));
                             return message;
                         })());
                     }
                     return false;
                 }
             })())
            {
                var doubleData = ExtractData(DownLoadData());
                if (AreRequirementsMet(doubleData))
                {
                    UpLoadData(doubleData);
                    view.SetCurve(GetCurve(doubleData));
                }
            }
        }

        protected abstract Func<double, PointF> GetCurve(Dictionary<CycloParams, double> data);

        protected abstract Dictionary<CycloParams, double> ExtractData(Dictionary<CycloParams, double> data);
    }
}