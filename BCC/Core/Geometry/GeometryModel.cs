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
    

    abstract class GeometryModel : Model
    {
        // The view and the control for geometry computation
        protected GeometryMenu view = null;

        public static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 360;
        }

        // Parameters lists
        protected abstract List<Enum> OptionalParams();
        protected abstract List<Enum> OutputParams();
        protected abstract List<List<Enum>> PossibleCliques();

        protected List<List<Enum>> PossibleCliques(List<Enum> clique)
        {
            if (clique.Count() == 0) return PossibleCliques();
            var possibleCompletions = new List<List<Enum>>();
            foreach (var v in PossibleCliques())
            {
                possibleCompletions.Add(new List<Enum>(v));
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
            if (view is null)
            {
                var parameterControls = new List<Control>();
                var toolTip = new ToolTip();

                int ParamBoxWidth() => StaticFields.PARAM_BOX_WIDTH - 20;
                var getterCalls = new Dictionary<Enum, Func<double>>();
                var setterCalls = new Dictionary<Enum, Action<double>>();
                var availabilityCalls = new Dictionary<Enum, Func<bool>>();

                var nameCallGenerators = NameCallGenerators();

                // Initialization of groupBoxes
                new Action(() => 
                {
                    var i = 1;

                    // Profile type group
                    new Action(() => 
                    {
                        // 
                        // ProfileTypeGroupBox
                        // 
                        var ProfileTypeGroupBox = new GroupBox
                        {
                            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                            Size = new Size(ParamBoxWidth(), 61),
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
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            FormattingEnabled = true,
                            Location = new Point(6, 25),
                            Size = new Size(121, 24),
                            TabIndex = 1
                        };
                        ProfileTypeComboBox.Items.AddRange(new object[] {
                            Vocabulary.ParameterLabels.Geometry.Epicycloid(),
                            Vocabulary.ParameterLabels.Geometry.Hipocycloid()});///////
                        ProfileTypeGroupBox.Controls.Add(ProfileTypeComboBox);
                        parameterControls.Add(ProfileTypeGroupBox);
                        Vocabulary.AddNameCall(new Action(() => {
                            var index = ProfileTypeComboBox.SelectedIndex;
                            ProfileTypeComboBox.Items.Clear();
                            ProfileTypeComboBox.Items.AddRange(new object[] {
                        Vocabulary.ParameterLabels.Geometry.Epicycloid(),
                        Vocabulary.ParameterLabels.Geometry.Hipocycloid()});
                            ProfileTypeComboBox.SelectedIndex = index < 0 ? 0 : index;
                        }));
                        getterCalls.Add(CycloParams.EPI, () => ProfileTypeComboBox.SelectedIndex < 1 ? TRUE : FALSE);
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

                    // Obligatory parameters
                    new Action(() =>
                    {
                        var paramList = new List<Enum>();
                        paramList.AddRange(ObligatoryIntParams());
                        paramList.AddRange(ObligatoryFloatParams());
                        foreach(var param in paramList)
                        {
                            bool intParam = ObligatoryIntParams().Contains(param);
                            var ParameterGroupBox = new GroupBox()
                            {
                                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                                Size = new Size(ParamBoxWidth(), 48),
                                TabIndex = i++,
                                TabStop = false,
                                Dock = DockStyle.Top
                            };
                            Vocabulary.AddNameCall(() => ParameterGroupBox.Text = nameCallGenerators[param]());
                            var ParameterUpDown = new NumericUpDown()
                            {
                                Location = new Point(7, 22),
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Maximum = 10000,
                                Minimum = intParam ? 1 : (decimal)Math.Pow(10, - VALUE_PRECISION),
                                Value = intParam ? 1 : (decimal)5.0,
                                Increment = intParam ? 1 : (decimal)Math.Pow(10, 1 - VALUE_PRECISION),
                                DecimalPlaces = intParam ? 0 : VALUE_PRECISION
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
                                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                                Size = new Size(ParamBoxWidth(), 49),
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
                                Location = new Point(7, 22),
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Maximum = 10000,
                                Value = (decimal)Math.Pow(10, 1 - VALUE_PRECISION),
                                Minimum = (decimal)Math.Pow(10, - VALUE_PRECISION),
                                Increment = (decimal)Math.Pow(10, 1 - VALUE_PRECISION),
                                DecimalPlaces = VALUE_PRECISION,
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
                                Location = new Point(167, 22),
                                Size = new Size(15, 14),
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
                        OutputTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                        OutputTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                        OutputTableLayout.RowStyles.Clear();
                        int currentRow = 0;
                        foreach (var param in OutputParams())
                        {
                            var ParameterNameLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new Point(3, 3),
                                Size = new Size(50, 15),
                                TabIndex = OutputTableLayout.RowCount * 2
                            };
                            Vocabulary.AddNameCall(() => ParameterNameLabel.Text = nameCallGenerators[param]());
                            // Value set by model
                            var ParameterValueLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new Point(3, 3),
                                Size = new Size(50, 15),
                                TabIndex = OutputTableLayout.RowCount * 2 + 1
                            };
                            setterCalls.Add(param, v => ParameterValueLabel.Text = "" + v);
                            OutputTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
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
                        OutputTableLayout.Size = new Size(ParamBoxWidth(), OutputTableLayout.RowCount * 30);
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
                view.VisibleChanged += (sender, e) =>
                {
                    if (view.Visible) Act();
                };
            }
            return new Dictionary<UserControl, Func<string>>()
            {
                {view, Vocabulary.TabPagesNames.Geometry}
            };
        }

        // Transferring data to and from the view
        public Dictionary<Enum, double> DownLoadData()
        {
            var ret = new Dictionary<Enum, double>
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
        public void UpLoadData(Dictionary<Enum, double> data)
        {
            foreach(var param in data)
            {
                view.Set(param.Key, param.Value);
            }
        }

        protected abstract bool IsCurvatureRequirementMet(Dictionary<Enum, double> vals);
        protected abstract bool IsToothCuttingRequirementMet(Dictionary<Enum, double> vals);
        protected abstract bool IsNeighbourhoodRequirementMet(Dictionary<Enum, double> vals);

        // Overall requirements checker
        protected bool AreRequirementsMet(Dictionary<Enum, double> vals)
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
        
        public void Act()
        {
            var available = new List<Enum>();
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
                     var temp = new List<Enum>(available);
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
                                     message += ' ' + NameCallGenerators()[p]() + ',';
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

        protected abstract Func<double, PointF> GetCurve(Dictionary<Enum, double> data);

        protected abstract Dictionary<Enum, double> ExtractData(Dictionary<Enum, double> data);
    }
}