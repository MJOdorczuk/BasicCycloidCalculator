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

    public enum ErrorTypes
    {
        OUT_OF_RANGE,
        NO_POSSIBLE_CLIQUE,
        INCOMPLETE_CLIQUE,
        CURVATURE_REQUIREMENT,
        TOOTH_CUTTING_REQUIREMENT,
        NEIGHBOURHOOD_REQUIREMENT
    }

    abstract class GeometryModel
    {
        // The view and the control for geometry computation
        protected GeometryMenu view = null;
        

        private static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 360;
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
        public virtual GeometryMenu GetMenu()
        {
            if (view is null)
            {
                var parameterControls = new List<Control>();
                
                int ParamBoxWidth() => StaticFields.PARAM_BOX_WIDTH - 20;
                Dictionary<CycloParams, Func<object>> getterCalls = new Dictionary<CycloParams, Func<object>>();
                Dictionary<CycloParams, Action<object>> setterCalls = new Dictionary<CycloParams, Action<object>>();
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
                            Name = "ProfileTypeGroupBox",
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
                            Name = "ProfileTypeComboBox",
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
                        getterCalls.Add(CycloParams.EPI, () => ProfileTypeComboBox.SelectedIndex < 1);
                        ProfileTypeComboBox.SelectedIndex = 0;
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
                            getterCalls.Add(param, () => ParameterUpDown.Value);
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
                            var ParameterValueBox = new TextBox
                            {
                                Location = new System.Drawing.Point(7, 22),
                                Size = new System.Drawing.Size(100, 22),
                                TabIndex = 0,
                                Text = "0"
                            };
                            ParameterGroupBox.Controls.Add(ParameterValueBox);
                            ParameterValueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler((sender, e) =>
                            {
                                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                                {
                                    e.Handled = true;
                                }

                                // only allow one decimal point
                                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                                {
                                    e.Handled = true;
                                }
                            });
                            parameterControls.Add(ParameterGroupBox);
                            getterCalls.Add(param, () => double.Parse("0" + ParameterValueBox.Text));
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
                            var ParameterValueBox = new TextBox
                            {
                                Location = new System.Drawing.Point(7, 22),
                                Size = new System.Drawing.Size(100, 22),
                                TabIndex = 0,
                                Enabled = false,
                                Text = "0"
                            };
                            ParameterValueBox.KeyPress += new KeyPressEventHandler((sender, e) =>
                            {
                                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                                {
                                    e.Handled = true;
                                }

                                // only allow one decimal point
                                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                                {
                                    e.Handled = true;
                                }
                            });
                            getterCalls.Add(param, () => double.Parse(ParameterValueBox.Text));
                            setterCalls.Add(param, v => ParameterValueBox.Text = "" + v);
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
                            ParameterGroupBox.Controls.Add(ParameterValueBox);
                            ParameterCheckBox.CheckedChanged += new EventHandler((sender, e) =>
                            {
                                ParameterValueBox.Enabled = ParameterCheckBox.Checked;
                            });
                            parameterControls.Add(ParameterGroupBox);
                            availabilityCalls.Add(param, () => ParameterCheckBox.Checked);
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
                            Name = "OutputTableLayout",
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
            return view;
        }

        // Transferring data to and from the view
        public Dictionary<CycloParams, object> DownLoadData()
        {
            var ret = new Dictionary<CycloParams, object>
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
        public void UpLoadData(Dictionary<CycloParams, object> data)
        {
            foreach(var param in data)
            {
                view.Set(param.Key, param.Value);
            }
        }

        // Requirements checkers with actions
        protected abstract bool IsNonNegativeValue(CycloParams param, double value);
        protected abstract bool IsCliquePossible(List<CycloParams> clique);
        protected abstract bool IsCurvatureRequirementMet(Dictionary<CycloParams, double> vals);
        protected abstract bool IsToothCuttingRequirementMet(Dictionary<CycloParams, double> vals);
        protected abstract bool IsNeighbourhoodRequirementMet(Dictionary<CycloParams, double> vals);

        // Overall requirements checker
        protected bool AreRequirementsMet(Dictionary<CycloParams, double> vals)
        {
            var ret = true;
            foreach(var val in vals)
            {
                ret = true && IsNonNegativeValue(val.Key, val.Value);
            }
            ret = true && IsCliquePossible(vals.Keys.Intersect(OptionalParams()).ToList());
            ret = true && IsCurvatureRequirementMet(vals);
            ret = true && IsToothCuttingRequirementMet(vals);
            ret = true && IsNeighbourhoodRequirementMet(vals);
            return ret;
        }

        // Name calls for parameters
        public Func<string> CallName(CycloParams param)
        {
            switch (param)
            {
                case CycloParams.Z:
                    return () => Vocabulary.ParameterLabels.Geometry.TeethQuantity();
                case CycloParams.G:
                    return () => Vocabulary.ParameterLabels.Geometry.RollDiameter();
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

        protected abstract Dictionary<CycloParams, Func<string>> NameCallGenerators();

        public abstract void Compute();

        protected abstract Func<double, PointF> GetCurve(Dictionary<CycloParams, double> data);
    }
}