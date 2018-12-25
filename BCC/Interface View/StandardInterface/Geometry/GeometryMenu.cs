using BCC.Core.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface.Geometry
{
    class GeometryMenu : UserControl
    {
        private GroupBox ParamsGroupBox;
        private FlowLayoutPanel ParameterFlowLayoutPanel;
        private static readonly int PARAMBOX_WIDTH = 360;
        private readonly List<Action> nameCalls = new List<Action>();
        private readonly Dictionary<CycloParams, Func<object>> getterCalls = new Dictionary<CycloParams, Func<object>>();
        private readonly Dictionary<CycloParams, Action<object>> setterCalls = new Dictionary<CycloParams, Action<object>>();
        private readonly Dictionary<CycloParams, Func<bool>> availabilityCalls = new Dictionary<CycloParams, Func<bool>>();
        private Panel DisplayPanel;
        private Func<int> ParamBoxWidth;

        public GeometryMenu()
        {
            InitializeComponent();

            this.ParamsGroupBox.Width = PARAMBOX_WIDTH;

            // Initialization of lambdas
            new Action(() => {
                this.ParamBoxWidth = () => ParameterFlowLayoutPanel.Width - 20;
            })();

            // Initialization of groupBoxes
            new Action(() => {
                var parameterControls = new List<Control>();

                // Profile type group
                new Action(() => {
                    // 
                    // ProfileTypeGroupBox
                    // 
                    var ProfileTypeGroupBox = new GroupBox
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold),
                        Location = new System.Drawing.Point(6, 19),
                        Name = "ProfileTypeGroupBox",
                        Size = new System.Drawing.Size(ParamBoxWidth(), 61),
                        TabIndex = 1,
                        TabStop = false,
                        Text = Vocabulary.ProfileType(),
                        Dock = DockStyle.Top
                    };
                    nameCalls.Add(new Action(() => {
                        ProfileTypeGroupBox.Text = Vocabulary.ProfileType();
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
                    Vocabulary.Epicycloid(),
                    Vocabulary.Hipocycloid()});
                    ProfileTypeGroupBox.Controls.Add(ProfileTypeComboBox);
                    ProfileTypeComboBox.SelectedIndex = 0;
                    parameterControls.Add(ProfileTypeGroupBox);
                    nameCalls.Add(new Action(() => {
                        ProfileTypeComboBox.Items.Clear();
                        ProfileTypeComboBox.Items.AddRange(new object[] {
                        Vocabulary.Epicycloid(),
                        Vocabulary.Hipocycloid()});
                    }));
                    getterCalls.Add(CycloParams.EPI, () => ProfileTypeComboBox.SelectedIndex == 0);
                })();

                // Teeth quantity group
                new Action(() => {
                    // 
                    // TeethQuantityGroupBox
                    // 
                    var TeethQuantityGroupBox = new GroupBox
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                        Location = new System.Drawing.Point(6, 86),
                        Name = "TeethQuantityGroupBox",
                        Size = new System.Drawing.Size(ParamBoxWidth(), 48),
                        TabIndex = 2,
                        TabStop = false,
                        Text = Vocabulary.TeethQuantity(),
                        Dock = DockStyle.Top
                    };
                    nameCalls.Add(new Action(() => {
                        TeethQuantityGroupBox.Text = Vocabulary.TeethQuantity();
                    }));
                    // 
                    // TeethQuantityUpDown
                    // Teeth quantity set only by user
                    // 
                    var TeethQuantityUpDown = new NumericUpDown
                    {
                        Location = new System.Drawing.Point(7, 22),
                        Name = "TeethQuantityUpDown",
                        Size = new System.Drawing.Size(120, 22),
                        TabIndex = 0
                    };
                    TeethQuantityGroupBox.Controls.Add(TeethQuantityUpDown);
                    parameterControls.Add(TeethQuantityGroupBox);
                    getterCalls.Add(CycloParams.Z, () => TeethQuantityUpDown.Value);
                })();

                // Roll diameter group
                new Action(() => {
                    // 
                    // RollDiameterGroupBox
                    // 
                    var RollDiameterGroupBox = new GroupBox
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                        Location = new System.Drawing.Point(6, 140),
                        Name = "RollDiameterGroupBox",
                        Size = new System.Drawing.Size(ParamBoxWidth(), 49),
                        TabIndex = 3,
                        TabStop = false,
                        Text = Vocabulary.RollDiameter(),
                        Dock = DockStyle.Top
                    };
                    nameCalls.Add(new Action(() => {
                        RollDiameterGroupBox.Text = Vocabulary.RollDiameter();
                    }));
                    // 
                    // RollDiameterValueBox
                    // Roll diameter set only by user
                    // 
                    var RollDiameterValueBox = new TextBox
                    {
                        Location = new System.Drawing.Point(7, 22),
                        Name = "RollDiameterValueBox",
                        Size = new System.Drawing.Size(100, 22),
                        TabIndex = 0
                    };
                    RollDiameterGroupBox.Controls.Add(RollDiameterValueBox);
                    RollDiameterValueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler((sender, e) =>
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
                    parameterControls.Add(RollDiameterGroupBox);
                    getterCalls.Add(CycloParams.G, () => double.Parse(RollDiameterValueBox.Text));
                })();

                // Optional input parameters groups loop
                new Action(() => {
                    foreach(var param in new Dictionary<CycloParams, Func<string>>()
                    {
                        {CycloParams.DA, () => Vocabulary.MajorDiameter() + "(Dₐ)" },
                        {CycloParams.DF, () => Vocabulary.RootDiameter() + "(Df)"},
                        {CycloParams.DG, () => Vocabulary.RollSpacingDiameter() + "(Dg)" },
                        {CycloParams.E, () => Vocabulary.Eccentricity() + "(e)"},
                        {CycloParams.H, () => Vocabulary.ToothHeight() + "(h)"}
                    })
                    {
                        // 
                        // ParameterGroupBox
                        // 
                        var ParameterGroupBox = new GroupBox
                        {
                            Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                            Name = param.Key + "GroupBox",
                            Size = new System.Drawing.Size(ParamBoxWidth(), 49),
                            TabIndex = 4,
                            TabStop = false,
                            Text = param.Value(),
                            Dock = DockStyle.Top
                        };
                        nameCalls.Add(() => { ParameterGroupBox.Text = param.Value(); });
                        // 
                        // ParameterValueBox
                        // Value set either by user or by model
                        // 
                        var ParameterValueBox = new TextBox
                        {
                            Location = new System.Drawing.Point(7, 22),
                            Name = param.Key + "ValueBox",
                            Size = new System.Drawing.Size(100, 22),
                            TabIndex = 0,
                            Enabled = false
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
                        getterCalls.Add(param.Key, () => double.Parse(ParameterValueBox.Text));
                        setterCalls.Add(param.Key, v => ParameterValueBox.Text = "" + v);
                        // 
                        // ParameterCheckBox
                        // 
                        var ParameterCheckBox = new CheckBox
                        {
                            AutoSize = true,
                            Location = new System.Drawing.Point(167, 22),
                            Name = param.Key + "CheckBox",
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
                        availabilityCalls.Add(param.Key, () => ParameterCheckBox.Checked);
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
                        TabIndex = 0,
                        CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                };
                    OutputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    OutputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    OutputTableLayout.RowStyles.Clear();
                    int currentRow = 0;
                    foreach (var param in new Dictionary<CycloParams, Func<string>>()
                    {
                        {CycloParams.Λ, () => Vocabulary.ToothHeightFactor() + "(λ)"},
                        {CycloParams.DW, () => Vocabulary.PinSpacingDiameter() + "(Dw)"},
                        {CycloParams.Ρ, () => Vocabulary.RollingCircleDiameter() + "(ρ)" },
                        {CycloParams.DB, () => Vocabulary.BaseDiameter() + "(Db)" }
                    })
                    {
                        var ParameterNameLabel = new Label()
                        {
                            AutoSize = true,
                            Location = new System.Drawing.Point(3, 3),
                            Size = new System.Drawing.Size(50, 15),
                            Name = param.Key + "NameLabel",
                            Text = param.Value(),
                            TabIndex = OutputTableLayout.RowCount * 2
                        };
                        nameCalls.Add(() => { ParameterNameLabel.Text = param.Value(); });
                        // Value set by model
                        var ParameterValueLabel = new Label()
                        {
                            AutoSize = true,
                            Location = new System.Drawing.Point(3, 3),
                            Size = new System.Drawing.Size(50, 15),
                            Name = param.Key + "ValueLabel",
                            TabIndex = OutputTableLayout.RowCount * 2 + 1
                        };
                        setterCalls.Add(param.Key, v => ParameterValueLabel.Text = "" + v);
                        OutputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        OutputTableLayout.Controls.Add(ParameterNameLabel, 0, currentRow);
                        OutputTableLayout.Controls.Add(ParameterValueLabel, 1, currentRow++);
                    }
                    OutputTableLayout.Size = new System.Drawing.Size(ParamBoxWidth(),OutputTableLayout.RowCount * 30);
                    parameterControls.Add(OutputTableLayout);
                })();


                this.ParameterFlowLayoutPanel.Controls.AddRange(parameterControls.ToArray());
            })();
        }

        private void InitializeComponent()
        {
            this.ParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.ParameterFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.ParamsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParamsGroupBox
            // 
            this.ParamsGroupBox.Controls.Add(this.ParameterFlowLayoutPanel);
            this.ParamsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParamsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ParamsGroupBox.Name = "ParamsGroupBox";
            this.ParamsGroupBox.Size = new System.Drawing.Size(214, 636);
            this.ParamsGroupBox.TabIndex = 0;
            this.ParamsGroupBox.TabStop = false;
            // 
            // ParameterFlowLayoutPanel
            // 
            this.ParameterFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParameterFlowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.ParameterFlowLayoutPanel.Name = "ParameterFlowLayoutPanel";
            this.ParameterFlowLayoutPanel.Size = new System.Drawing.Size(208, 617);
            this.ParameterFlowLayoutPanel.TabIndex = 2;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(214, 0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(755, 636);
            this.DisplayPanel.TabIndex = 1;
            // 
            // GeometryMenu
            // 
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.ParamsGroupBox);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(969, 636);
            this.ParamsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public Panel GetDisplayPanel => DisplayPanel;

        public object Get(CycloParams param) => getterCalls[param]();

        public void Set(CycloParams param, object val) => setterCalls[param](val);


        public bool IsAvailable(CycloParams param) => availabilityCalls.ContainsKey(param) ? availabilityCalls[param]() : false;
    }
}
