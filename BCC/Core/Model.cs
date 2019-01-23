using BCC.Core.Parameters;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core
{
    public class PageSeed
    {
        private static class StaticFields
        {
            public static int PARAMETER_GROUP_BOX_WIDTH = 370;
        }

        public Func<string> NameCall { get; private set; }
        public Panel WorkSpace { get; private set; }
        public Graphics G { get; private set; }
        private FlowLayoutPanel parameterFlowPanel;
        public void AddFlowPanelControl(Control control)
        {
            parameterFlowPanel.Controls.Add(control);
        }
        public UserControl Page { get; private set; }
        public List<IParameter> Parameters
        {
            get => new List<IParameter>(parameters);
            private set => parameters = value;
        }

        private List<IParameter> parameters;

        public PageSeed(Func<string> nameCall, List<IParameter> parameters)
        {
            NameCall = nameCall;
            Parameters = parameters;
            Page = new UserControl
            {
                Size = new Size(1280, 720),
                Visible = true,
                Enabled = true,
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            WorkSpace = new Panel
            {
                BackColor = SystemColors.ActiveCaptionText,
                Dock = DockStyle.Fill,
                Location = new Point(500, 0),
                Size = new Size(2000, 2000),
            };
            G = WorkSpace.CreateGraphics();
            Page.Controls.Add(WorkSpace);
            var ParameterGroupBox = new GroupBox
            {
                Dock = DockStyle.Left,
                Location = new Point(0, 0),
                Size = new Size(StaticFields.PARAMETER_GROUP_BOX_WIDTH, 954),
                TabStop = false,
            };
            Page.Controls.Add(ParameterGroupBox);
            parameterFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Location = new Point(3, 16),
                Size = new Size(208, 935)
            };
            ParameterGroupBox.Controls.Add(parameterFlowPanel);
        }

    }

    public static class ParameterComponentGenerator
    {
        private const int VALUE_PRECISION = 3;
        private const int TOLERATED_VALUE_PRECISION = 5;
        private const double MAX_TOLERANCE_RANGE = 1.0;
        private static readonly Color PARAMETER_FIELD_COLOR = SystemColors.Window;
        private static readonly Color LABEL_COLOR = SystemColors.Control;
        private static readonly Color WARNING_COLOR = Color.Red;
        public const int PARAMETER_BOX_WIDTH = 170;
        public const int PARAMETER_BOX_HEIGHT = 60;
        public const int OUTPUT_ROW_HEIGHT = 40;
        public const int INPUT_BOX_LOCATION_Y = 30;
        public const int SMALL_MARGIN = 3;
        public const int BIG_MARGIN = 7;
        public const int GROUP_BOX_TOP_MARGIN = 23;
        public const int RADIO_HEIGHT = 23;
        public const int TABLE_HEIGHT = 300;

        private static readonly ToolTip bubble = new ToolTip();
        public static GroupBox Generate(IParameter parameter, Action act)
        {
            var GroupBox = new GroupBox
            {
                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                TabStop = false,
            };
            Vocabulary.AddNameCall(() => GroupBox.Text = parameter.NameCall());
            switch(parameter)
            {
                case ISingleParameter isp:
                    {
                        GroupBox.Size = new Size(PARAMETER_BOX_WIDTH, PARAMETER_BOX_HEIGHT);
                        var UpDown = new NumericUpDown
                        {
                            Location = new Point(7, INPUT_BOX_LOCATION_Y),
                            Size = new Size(120, 22),
                        };
                        GroupBox.Controls.Add(UpDown);
                        isp.AddBubbleCall(message =>
                        {
                            if (message is null)
                            {
                                UpDown.BackColor = PARAMETER_FIELD_COLOR;
                                bubble.SetToolTip(GroupBox, "");
                                bubble.SetToolTip(UpDown, "");
                            }
                            else
                            {
                                UpDown.BackColor = WARNING_COLOR;
                                bubble.SetToolTip(GroupBox, message);
                                bubble.SetToolTip(UpDown, message);
                            }
                        });
                        switch (isp)
                        {
                            case IEnableable enableable:
                                {
                                    UpDown.Enabled = false;
                                    var CheckBox = new CheckBox
                                    {
                                        AutoSize = true,
                                        Location = new Point(150, 22),
                                        Size = new Size(15, 14),
                                        TabIndex = 1,
                                        UseVisualStyleBackColor = true
                                    };
                                    GroupBox.Controls.Add(CheckBox);
                                    CheckBox.CheckedChanged += new EventHandler((sender, e) =>
                                    {
                                        UpDown.Enabled = CheckBox.Checked;
                                        enableable.Enabled = CheckBox.Checked;
                                        act();
                                    });
                                    switch (enableable)
                                    {
                                        case EnableableIntParameter eip:
                                            {
                                                UpDown.Maximum = eip.UpperBound;
                                                UpDown.Value = eip.Get();
                                                UpDown.Minimum = eip.LowerBound;
                                                UpDown.Increment = 1;
                                                UpDown.DecimalPlaces = 0;
                                                UpDown.ValueChanged += (sender, e) =>
                                                {
                                                    eip.Set((int)UpDown.Value);
                                                    act();
                                                };
                                                eip.AddValueChangedListener(value => UpDown.Value = value);
                                                break;
                                            }
                                        case EnableableFloatParameter efp:
                                            {
                                                UpDown.Maximum = (decimal)efp.UpperBound;
                                                UpDown.Value = (decimal)efp.Get();
                                                UpDown.Minimum = (decimal)efp.LowerBound;
                                                UpDown.Increment = (decimal)Math.Pow(10, 1 - VALUE_PRECISION);
                                                UpDown.DecimalPlaces = VALUE_PRECISION;
                                                UpDown.ValueChanged += (sender, e) =>
                                                {
                                                    efp.Set((double)UpDown.Value);
                                                    act();
                                                };
                                                efp.AddValueChangedListener(value => UpDown.Value = (decimal)value);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case IntParameter ip:
                                {
                                    UpDown.Maximum = ip.UpperBound;
                                    UpDown.Value = ip.Get();
                                    UpDown.Minimum = ip.LowerBound;
                                    UpDown.Increment = 1;
                                    UpDown.DecimalPlaces = 0;
                                    UpDown.ValueChanged += (sender, e) =>
                                    {
                                        ip.Set((int)UpDown.Value);
                                        act();
                                    };
                                    ip.AddValueChangedListener(value => UpDown.Value = value);
                                    break;
                                }
                            case FloatParameter fp:
                                {
                                    UpDown.Maximum = (decimal)fp.UpperBound;
                                    UpDown.Value = (decimal)fp.Get();
                                    UpDown.Minimum = (decimal)fp.LowerBound;
                                    UpDown.Increment = (decimal)Math.Pow(10, 1 - VALUE_PRECISION);
                                    UpDown.DecimalPlaces = VALUE_PRECISION;
                                    UpDown.ValueChanged += (sender, e) =>
                                    {
                                        fp.Set((double)UpDown.Value);
                                        act();
                                    };
                                    fp.AddValueChangedListener(value => UpDown.Value = (decimal)value);
                                    break;
                                }
                        }
                        break;
                    }
                case UpDownParameterGroup udpg:
                    {
                        // A checkbox on top
                        // flow panel below
                        //      radio buttons inside
                        // Two groupboxes below
                        // UpDownGroupBox on the left
                        //      Two buttons (+-) and NumericUpDowns inside
                        // ElementNumber on the right
                        //      Checkbox and NumericUpDown inside
                        GroupBox.Size = new Size(2 * PARAMETER_BOX_WIDTH, udpg.Parameters.Count * RADIO_HEIGHT + PARAMETER_BOX_HEIGHT * 2);
                        Vocabulary.AddNameCall(() => GroupBox.Text = udpg.NameCall());

                        // Check box for enabling whole group
                        var CheckBox = new CheckBox
                        {
                            AutoSize = true,
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Size = new Size(PARAMETER_BOX_WIDTH, RADIO_HEIGHT - SMALL_MARGIN),
                            UseVisualStyleBackColor = true,
                            Checked = true
                        };
                        udpg.Enabled = true;
                        
                        // Name call for check box: "Include engineering tolerances"
                        Vocabulary.AddNameCall(() =>
                        {
                            CheckBox.Text = Vocabulary.ParameterLabels.Dimensioning.IncludeEngineeringTolerances();
                        });
                        GroupBox.Controls.Add(CheckBox);
                        
                        var Panel = new Panel
                        {
                            Location = new Point(SMALL_MARGIN, GROUP_BOX_TOP_MARGIN),
                            Size = new Size(GroupBox.Width - BIG_MARGIN, udpg.Parameters.Count * RADIO_HEIGHT + BIG_MARGIN)
                        };
                        GroupBox.Controls.Add(Panel);

                        var radioBottom = SMALL_MARGIN;
                        var Upper = new NumericUpDown
                        {
                            Size = new Size(120, 20),
                            DecimalPlaces = TOLERATED_VALUE_PRECISION,
                            Minimum = (decimal)-MAX_TOLERANCE_RANGE,
                            Maximum = (decimal)MAX_TOLERANCE_RANGE,
                            Increment = (decimal)Math.Pow(10, 1 - TOLERATED_VALUE_PRECISION)
                        };
                        var Lower = new NumericUpDown
                        {
                            Size = new Size(120, 20),
                            DecimalPlaces = TOLERATED_VALUE_PRECISION,
                            Minimum = (decimal)-MAX_TOLERANCE_RANGE,
                            Maximum = (decimal)MAX_TOLERANCE_RANGE,
                            Increment = (decimal)Math.Pow(10, 1 - TOLERATED_VALUE_PRECISION)
                        };
                        var radioIndexer = 0;
                        foreach (var param in udpg.Parameters)
                        {
                            var RadioButton = new RadioButton
                            {
                                AutoSize = true,
                                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                                Location = new Point(SMALL_MARGIN, radioBottom),
                                UseVisualStyleBackColor = true
                            };
                            if (radioIndexer++ == 0)
                                RadioButton.Checked = true;
                            radioBottom = RadioButton.Bottom;
                            RadioButton.CheckedChanged += (sender, e) =>
                            {
                                if (RadioButton.Checked)
                                {
                                    udpg.Focus = param;
                                    Upper.Value = (decimal)udpg.Upper;
                                    Lower.Value = (decimal)udpg.Lower;
                                }
                            };
                            Panel.Controls.Add(RadioButton);
                            Vocabulary.AddNameCall(() =>
                            {
                                RadioButton.Text = param.NameCall();
                            });
                        }

                        var UpperLowerGroupBox = new GroupBox
                        {
                            Location = new Point(SMALL_MARGIN, Panel.Bottom + SMALL_MARGIN),
                            Size = new Size((GroupBox.Width - 12)/2, 60)
                        };
                        GroupBox.Controls.Add(UpperLowerGroupBox);

                        var Plus = new Button
                        {
                            Enabled = false,
                            Location = new Point(SMALL_MARGIN, 12),
                            Size = new Size(20, 20),
                            Text = "+"
                        };
                        var Minus = new Button
                        {
                            Enabled = false,
                            Location = new Point(Plus.Left, Plus.Bottom),
                            Size = new Size(20, 20),
                            Text = "-"
                        };
                        UpperLowerGroupBox.Controls.Add(Plus);
                        UpperLowerGroupBox.Controls.Add(Minus);

                        Upper.Location = new Point(Plus.Right, Plus.Top);
                        Upper.ValueChanged += (sender, e) =>
                        {
                            udpg.Upper = (double)Upper.Value;
                            act();
                        };
                        Lower.Location = new Point(Upper.Left, Upper.Bottom);
                        Lower.ValueChanged += (sender, e) =>
                        {
                            udpg.Lower = (double)Lower.Value;
                            act();
                        };
                        UpperLowerGroupBox.Controls.Add(Upper);
                        UpperLowerGroupBox.Controls.Add(Lower);


                        var ElementNumberGroupBox = new GroupBox
                        {
                            Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                            Location = new Point(UpperLowerGroupBox.Right + SMALL_MARGIN, Panel.Bottom + SMALL_MARGIN),
                            Size = new Size(GroupBox.Width - UpperLowerGroupBox.Right - BIG_MARGIN, UpperLowerGroupBox.Height)
                        };
                        GroupBox.Controls.Add(ElementNumberGroupBox);
                        Vocabulary.AddNameCall(() =>
                        {
                            ElementNumberGroupBox.Text = Vocabulary.ToleratedElementNumber();
                        });

                        var NumberCheckBox = new CheckBox
                        {
                            AutoSize = true,
                            Location = new Point(SMALL_MARGIN,INPUT_BOX_LOCATION_Y),
                            Size = new Size(100, 20)
                        };
                        ElementNumberGroupBox.Controls.Add(NumberCheckBox);
                        Vocabulary.AddNameCall(() =>
                        {
                            NumberCheckBox.Text = Vocabulary.Include();
                        });

                        var IndexCounter = new NumericUpDown
                        {
                            Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                            Location = new Point(ElementNumberGroupBox.Width - 50 - SMALL_MARGIN, NumberCheckBox.Top),
                            Size = new Size(50, 20),
                            TextAlign = HorizontalAlignment.Right,
                            Enabled = false,
                            Minimum = 0,
                            Maximum = udpg.Count - 1
                        };
                        udpg.AddCountChangedListener(count => IndexCounter.Maximum = count - 1);
                        NumberCheckBox.CheckedChanged += (sender, e) =>
                        {
                            udpg.IndexingEnabled = IndexCounter.Enabled = NumberCheckBox.Checked;
                            IndexCounter.Value = 0;
                            udpg.Index = 0;
                            act();
                        };
                        IndexCounter.ValueChanged += (sender, e) =>
                        {
                            udpg.Index = (int)IndexCounter.Value;
                            Upper.Value = (decimal)udpg.Upper;
                            Lower.Value = (decimal)udpg.Lower;
                        };
                        ElementNumberGroupBox.Controls.Add(IndexCounter);

                        void Enable(bool state)
                        {
                            udpg.Enabled = state;
                            foreach (Control radio in Panel.Controls)
                            {
                                radio.Enabled = state;
                            }
                            Upper.Enabled = state;
                            Lower.Enabled = state;
                            IndexCounter.Enabled = state && NumberCheckBox.Checked;
                            if (state) GroupBox.Height = UpperLowerGroupBox.Bottom + SMALL_MARGIN;
                            else GroupBox.Height = CheckBox.Bottom + SMALL_MARGIN;
                        }

                        CheckBox.CheckedChanged += (sender, e) =>
                        {
                            foreach (Control control in CheckBox.Controls)
                            {
                                if (control != CheckBox) control.Visible = CheckBox.Checked;
                            }
                            Enable(CheckBox.Checked);
                            act();
                        };
                        break;
                    }
                case OutputParameterList opl:
                    {
                        GroupBox.Size = new Size(2 * PARAMETER_BOX_WIDTH, opl.Parameters.Count * OUTPUT_ROW_HEIGHT);
                        var TableLayoutPanel = new TableLayoutPanel
                        {
                            Dock = DockStyle.Fill,
                            ColumnCount = 2,
                            RowCount = 0,
                            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                            Width = 2 * PARAMETER_BOX_WIDTH
                        };
                        TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                        TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                        TableLayoutPanel.RowStyles.Clear();
                        GroupBox.Controls.Add(TableLayoutPanel);
                        int currentRow = 0;
                        foreach(var param in opl.Parameters)
                        {
                            var NameLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new Point(3, 3),
                                Size = new Size(50, 15),
                                TabIndex = TableLayoutPanel.RowCount * 2
                            };
                            Vocabulary.AddNameCall(() => NameLabel.Text = param.NameCall());
                            var ValueLabel = new Label()
                            {
                                AutoSize = true,
                                Location = new Point(3, 3),
                                Size = new Size(50, 15),
                                TabIndex = TableLayoutPanel.RowCount * 2 + 1
                            };
                            param.AddListener(value => ValueLabel.Text = "" + value);
                            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                            TableLayoutPanel.Controls.Add(NameLabel, 0, currentRow);
                            TableLayoutPanel.Controls.Add(ValueLabel, 1, currentRow);
                            param.AddBubbleCall(message =>
                            {
                                if(message is null)
                                {
                                    ValueLabel.BackColor = LABEL_COLOR;
                                    bubble.SetToolTip(ValueLabel, "");
                                    bubble.SetToolTip(NameLabel, "");
                                }
                                else
                                {
                                    ValueLabel.BackColor = WARNING_COLOR;
                                    bubble.SetToolTip(ValueLabel, message);
                                    bubble.SetToolTip(NameLabel, message);
                                }
                            });
                            currentRow++;
                        }
                        break;
                    }
                case ParameterGroup pg:
                    {
                        GroupBox.Size = new Size(PARAMETER_BOX_WIDTH * 2 + 2 * BIG_MARGIN, (1 + pg.Parameters.Count / 2) * PARAMETER_BOX_HEIGHT);
                        Vocabulary.AddNameCall(() => GroupBox.Text = pg.NameCall());
                        var ComboBox = new ComboBox
                        {
                            FormattingEnabled = true,
                            Location = new Point(BIG_MARGIN, INPUT_BOX_LOCATION_Y),
                            Size = new Size(PARAMETER_BOX_WIDTH, INPUT_BOX_LOCATION_Y)
                        };
                        var FlowPanel = new FlowLayoutPanel
                        {
                            Location = new Point(SMALL_MARGIN, ComboBox.Location.Y + ComboBox.Height + SMALL_MARGIN),
                            Size = new Size(2 * (PARAMETER_BOX_WIDTH + SMALL_MARGIN), pg.Parameters.Count * PARAMETER_BOX_HEIGHT)
                        };
                        Vocabulary.AddNameCall(() =>
                        {
                            var index = ComboBox.SelectedIndex;
                            ComboBox.Items.Clear();
                            if (pg.Customable) ComboBox.Items.Add(Vocabulary.ParameterLabels.Dimensioning.Custom());
                            foreach (var param in pg.Predefined)
                            {
                                ComboBox.Items.Add(param.Item1());
                            }
                            ComboBox.SelectedIndex = index >= 0 ? index : 0;
                        });
                        var SubInputs = new Dictionary<ISingleEnableableParameter, NumericUpDown>();
                        var flowHeight = 0;
                        foreach(var param in pg.Parameters)
                        {
                            var SubGroupBox = new GroupBox()
                            {
                                Size = new Size(PARAMETER_BOX_WIDTH - BIG_MARGIN, PARAMETER_BOX_HEIGHT),
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() =>
                            {
                                SubGroupBox.Text = param.NameCall();
                            });
                            var UpDown = new NumericUpDown()
                            {
                                DecimalPlaces = VALUE_PRECISION,
                                Increment = (decimal)Math.Pow(10, 1 - VALUE_PRECISION),
                                Location = new Point(SMALL_MARGIN, INPUT_BOX_LOCATION_Y),
                                Maximum = Convert.ToDecimal(param.UpperBound),
                                Minimum = Convert.ToDecimal(param.LowerBound),
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Value = Convert.ToDecimal(param.Get),
                            };
                            UpDown.ValueChanged += (sender, e) =>
                            {
                                param.Set(UpDown.Value);
                            };
                            switch(param)
                            {
                                case EnableableIntParameter eip:
                                    {
                                        UpDown.DecimalPlaces = 0;
                                        UpDown.Increment = 1;
                                        break;
                                    }
                                case EnableableFloatParameter efp:
                                    {
                                        UpDown.DecimalPlaces = VALUE_PRECISION;
                                        UpDown.Increment = (decimal)Math.Pow(10, 1 - VALUE_PRECISION);
                                        break;
                                    }
                            }
                            SubGroupBox.Controls.Add(UpDown);
                            SubGroupBox.Height = UpDown.Bottom + SMALL_MARGIN;
                            FlowPanel.Controls.Add(SubGroupBox);
                            SubInputs.Add(param, UpDown);
                            flowHeight = Math.Max(flowHeight, SubGroupBox.Bottom + SMALL_MARGIN);
                        }
                        FlowPanel.Height = flowHeight;
                        ComboBox.SelectedIndexChanged += (sedner, e) =>
                        {
                            pg.Set(ComboBox.SelectedIndex);
                            foreach(var input in SubInputs)
                            {
                                input.Value.Value = Convert.ToDecimal(input.Key.Get);
                                input.Value.Enabled = input.Key.Enabled;
                            }
                            act();
                        };
                        GroupBox.Controls.Add(ComboBox);
                        GroupBox.Controls.Add(FlowPanel);
                        break;
                    }
                case TableParameterGroup tpg:
                    {
                        GroupBox.Width = (PARAMETER_BOX_WIDTH + BIG_MARGIN) * 2;
                        Vocabulary.AddNameCall(() => GroupBox.Text = tpg.NameCall());
                        var DataView = new DataGridView
                        {
                            AllowUserToAddRows = false,
                            AllowUserToDeleteRows = false,
                            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                            Location = new Point(SMALL_MARGIN, INPUT_BOX_LOCATION_Y),
                            ReadOnly = true,
                            Width = GroupBox.Width - BIG_MARGIN,
                            Height = TABLE_HEIGHT
                        };
                        foreach(DataGridViewTextBoxColumn column in DataView.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            column.ReadOnly = true;
                        }
                        DataView.DataSource = tpg.Data;
                        GroupBox.Controls.Add(DataView);
                        break;
                    }
            }
            var bottom = 0;
            foreach(Control control in GroupBox.Controls)
            {
                bottom = Math.Max(bottom, control.Bottom);
            }
            GroupBox.Height = bottom + SMALL_MARGIN;
            return GroupBox;
        }
    }

    abstract class Model
    {
        private Dictionary<UserControl, Func<string>> menusGotten = null;

        protected abstract List<PageSeed> PageSeeds();

        public Dictionary<UserControl, Func<string>> GetMenus()
        {
            if(menusGotten is null)
            {
                menusGotten = new Dictionary<UserControl, Func<string>>();
                foreach (var seed in PageSeeds())
                {
                    var parameters = seed.Parameters;
                    foreach (var param in parameters)
                    {
                        seed.AddFlowPanelControl(ParameterComponentGenerator.Generate(param, Act));
                    }
                    menusGotten.Add(seed.Page, seed.NameCall);
                }
            }
            return menusGotten;
        }

        protected abstract void Act();
    }
}
