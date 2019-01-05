using BCC.Interface_View.StandardInterface.Tension;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Load
{
    public abstract class PredefinedGroupContainer
    {
        private List<DimensioningParams> parameters;
        private List<Tuple<Func<string>, Dictionary<DimensioningParams, double>>> predefines;

        public PredefinedGroupContainer(List<DimensioningParams> parameters)
        {
            this.parameters = new List<DimensioningParams>(parameters);
            this.predefines = new List<Tuple<Func<string>, Dictionary<DimensioningParams, double>>>();
        }

        protected void PushPredefine(Func<string> nameCall, Dictionary<DimensioningParams, double> values)
        {
            foreach(var param in parameters)
            {
                if (!values.Keys.Contains(param)) throw new Exception("Not enough parameters");
            }
            foreach(var param in values.Keys)
            {
                if (!parameters.Contains(param)) throw new Exception("Unknonw parameter");
            }
            predefines.Add(new Tuple<Func<string>, Dictionary<DimensioningParams, double>>(nameCall, values));
        }

        public List<Tuple<Func<string>, Dictionary<DimensioningParams, double>>> Predefines => 
            new List<Tuple<Func<string>, Dictionary<DimensioningParams, double>>>(predefines);
        public List<DimensioningParams> Parameters => new List<DimensioningParams>(parameters);
    }

    public enum DimensioningParams
    {
        E_MATERIAL,
        Ν_MATERIAL,
        R_SPACING,
        ΔR_HOLE_SPACING,
        ΔΦ_HOLE,
        ΔR_SLEEVE_SPACING,
        ΔΦ_SLEEVE,
        ΔE,
        ΔR_SLEEVE,
        ΔR_HOLE,
        B,
        R_SLEEVE,
        R_HOLE,
        E,
        N,
        GEAR_MATERIAL,
        SLEEVE_MATERIAL,
        Δ
    }

    abstract class LoadModel : Model
    {
        // The view and the control for geometry computation
        protected LoadMenu dimensioningPart = null;
        
        public static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 175;
            public static readonly int PARAM_BOX_HEIGHT = 50;
            public static readonly int MARGIN = 3;
            public static readonly int VALUE_PRECISION = 3;
            public static readonly int TOLERANCE_PRECISION = 5;
            public static readonly double TRUE = 1.0, FALSE = 2.0;
            public static readonly double NULL = double.NegativeInfinity;
        }

        //protected abstract List<LoadParams>

        public Dictionary<UserControl, Func<string>> GetMenus()
        {
            if(dimensioningPart is null)
            {
                var parameterControls = new List<Control>();
                var getterCalls = new Dictionary<DimensioningParams, Func<double, double>>();
                var setterCalls = new Dictionary<DimensioningParams, Action<double, double>>();
                var paramBoxWidth = StaticFields.PARAM_BOX_WIDTH;
                var paramBoxHeight = StaticFields.PARAM_BOX_HEIGHT;
                var margin = StaticFields.MARGIN;
                var toolTip = new ToolTip();
                var nameCallGenerators = NameCallGenerators();

                new Action(() =>
                {
                    var i = 0;

                    // MaterialGroupBoxes
                    new Action(() =>
                    {
                        foreach (var pair in PredefinedGroups())
                        {
                            var GroupBox = new GroupBox
                            {
                                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                                Location = new Point(3, 3),
                                Size = new Size(paramBoxWidth * 2 + 20, paramBoxHeight * 2 + 20),
                                TabIndex = i++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() =>
                            {
                                GroupBox.Text = nameCallGenerators[pair.Key]();
                            });
                            var FlowPanel = new FlowLayoutPanel()
                            {
                                Location = new Point(margin, paramBoxHeight + margin),
                                Size = new Size(paramBoxWidth * 2 + 20 - 2 * margin, 58),
                            };

                            var ComboBox = new ComboBox()
                            {
                                FormattingEnabled = true,
                                Location = new Point(7, 22),
                                Size = new Size(121, 24),
                                TabIndex = 0
                            };
                            Vocabulary.AddNameCall(() =>
                            {
                                var index = ComboBox.SelectedIndex;
                                ComboBox.Items.Clear();
                                ComboBox.Items.Add(Vocabulary.ParameterLabels.Dimensioning.Custom());
                                ComboBox.SelectedIndex = index >= 0 ? index : 0;
                                foreach (var param in pair.Value.Predefines)
                                {
                                    ComboBox.Items.Add(param.Item1());
                                }
                            });
                            GroupBox.Controls.Add(ComboBox);
                            GroupBox.Controls.Add(FlowPanel);

                            foreach (var param in pair.Value.Parameters)
                            {
                                var j = 0;
                                var SubGroupBox = new GroupBox()
                                {
                                    Location = new Point(margin + (margin + paramBoxWidth) * (j++), margin),
                                    Size = new Size(paramBoxWidth, paramBoxHeight),
                                    TabIndex = j,
                                    TabStop = false
                                };
                                Vocabulary.AddNameCall(() =>
                                {
                                    SubGroupBox.Text = nameCallGenerators[param]();
                                });
                                var UpDown = new NumericUpDown()
                                {
                                    DecimalPlaces = StaticFields.VALUE_PRECISION,
                                    Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                    Location = new Point(margin, 21),
                                    Maximum = 50000,
                                    Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                    Size = new Size(120, 22),
                                    TabIndex = 0,
                                    Value = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                };
                                SubGroupBox.Controls.Add(UpDown);
                                FlowPanel.Controls.Add(SubGroupBox);
                            }
                            
                            parameterControls.Add(GroupBox);
                        }
                    })();

                    // GearParametersGroupBoxes
                    new Action(() =>
                    {
                        int j = 0;
                        // 
                        // GearParametersGroupBox
                        // 
                        var GearParametersGroupBox = new GroupBox
                        {
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(3, 3),
                            Size = new Size(370, 134),
                            TabIndex = i++,
                            TabStop = false
                        };
                        var GearParametersFlowPanel = new FlowLayoutPanel
                        {
                            Location = new Point(3, 18),
                            Size = new Size(364, 113),
                            Dock = DockStyle.Fill,
                            TabIndex = 0
                        };
                        GearParametersGroupBox.Controls.Add(GearParametersFlowPanel);
                        // Obligatory integer parameters
                        foreach(var param in ObligatoryIntParams())
                        {
                            var GroupBox = new GroupBox()
                            {
                                Location = new Point(3, 3),
                                Size = new Size(175, 49),
                                TabIndex = j++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() => GroupBox.Text = nameCallGenerators[param]());
                            var UpDown = new NumericUpDown()
                            {
                                DecimalPlaces = 0,
                                Increment = 1,
                                Location = new Point(7, 22),
                                Maximum = 1000,
                                Minimum = 1,
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Value = 1
                            };
                            GroupBox.Controls.Add(UpDown);
                            GearParametersFlowPanel.Controls.Add(GroupBox);
                        }
                        // Obligatory floating-point parameters
                        foreach(var param in ObligatoryFloatParams())
                        {
                            var GroupBox = new GroupBox()
                            {
                                Location = new Point(3, 3),
                                Size = new Size(175, 49),
                                TabIndex = j++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() => GroupBox.Text = nameCallGenerators[param]());
                            var UpDown = new NumericUpDown()
                            {
                                DecimalPlaces = StaticFields.VALUE_PRECISION,
                                Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                Location = new Point(7, 22),
                                Maximum = 1000,
                                Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Value = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                            };
                            GroupBox.Controls.Add(UpDown);
                            GearParametersFlowPanel.Controls.Add(GroupBox);
                        }
                        parameterControls.Add(GearParametersGroupBox);
                    })();

                    // TolerancesGroupBox
                    new Action(() =>
                    {
                        var GroupBox = new GroupBox()
                        {
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(3, 3),
                            Size = new Size(367, 213),
                            TabIndex = i++,
                            TabStop = false,
                        };
                        Vocabulary.AddNameCall(() =>
                        {
                            GroupBox.Text = Vocabulary.ParameterLabels.Dimensioning.EngineeringTolerances();
                        });

                        var ListBox = new ListBox
                        {
                            FormattingEnabled = true,
                            ItemHeight = 16,
                            Location = new Point(6, 21),
                            Size = new Size(303, 116),
                            TabIndex = 8
                        };
                        Vocabulary.AddNameCall(() =>
                        {
                            var index = ListBox.SelectedIndex;
                            ListBox.Items.Clear();
                            foreach (var param in FitParams())
                            {
                                ListBox.Items.Add(nameCallGenerators[param]());
                            }
                            ListBox.SelectedIndex = index < 0 ? 0 : index;
                        });

                        var IndexUpDown = new NumericUpDown
                        {
                            Location = new Point(255, 143),
                            Maximum = 1,
                            Minimum = 1,
                            RightToLeft = RightToLeft.Yes,
                            Size = new Size(54, 22),
                            TabIndex = 10,
                            UpDownAlign = LeftRightAlignment.Left,
                            Value = 1
                        };
                        var LowerUpDown = new NumericUpDown
                        {
                            DecimalPlaces = StaticFields.TOLERANCE_PRECISION,
                            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(41, 177),
                            Size = new Size(120, 26),
                            TabIndex = 9,
                            Minimum = -10,
                            Maximum = 10,
                            Increment = (decimal)Math.Pow(10, 1 - StaticFields.TOLERANCE_PRECISION),
                        };
                        var UpperUpDown = new NumericUpDown
                        {
                            DecimalPlaces = StaticFields.TOLERANCE_PRECISION,
                            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(41, 145),
                            Size = new Size(120, 26),
                            TabIndex = 8,
                            Minimum = -10,
                            Maximum = 10,
                            Increment = (decimal)Math.Pow(10, 1 - StaticFields.TOLERANCE_PRECISION),
                        };

                        var UpperDeviationLabel = new Button
                        {
                            Enabled = false,
                            Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(6, 140),
                            Margin = new Padding(0),
                            Size = new Size(32, 32),
                            TabIndex = 11,
                            Text = "+",
                            TextAlign = ContentAlignment.BottomCenter,
                            UseVisualStyleBackColor = true
                        };
                        var LowerDeviationLabel = new Button
                        {
                            Enabled = false,
                            Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(6, 172),
                            Margin = new Padding(0),
                            Size = new Size(32, 32),
                            TabIndex = 12,
                            Text = "-",
                            TextAlign = ContentAlignment.BottomCenter,
                            UseVisualStyleBackColor = true
                        };

                        GroupBox.Controls.AddRange(new Control[]{
                            ListBox,
                            IndexUpDown,
                            LowerUpDown,
                            UpperUpDown,
                            LowerDeviationLabel,
                            UpperDeviationLabel,
                        });
                        parameterControls.Add(GroupBox);
                    })();
                })();

                dimensioningPart = new LoadMenu(new LoadMenu.InitialParameters
                {
                    model = this,
                    PARAMBOX_WIDTH = StaticFields.PARAM_BOX_WIDTH,
                    parameterControls = parameterControls,
                    getterCalls = getterCalls,
                    setterCalls = setterCalls
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
                {dimensioningPart, Vocabulary.TabPagesNames.Load }
            };
            
        }
        protected abstract List<DimensioningParams> ObligatoryIntParams();
        protected abstract List<DimensioningParams> ObligatoryFloatParams();
        protected abstract Dictionary<DimensioningParams, PredefinedGroupContainer> PredefinedGroups();
        protected abstract List<DimensioningParams> FitParams();
    }
}