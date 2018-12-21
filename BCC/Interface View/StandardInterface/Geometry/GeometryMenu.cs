using BCC.Miscs;
using System;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface.Geometry
{
    class GeometryMenu : UserControl
    {
        private GroupBox ParamsGroupBox;
        private GroupBox ProfileTypeGroupBox;
        private GroupBox RollDiameterGroupBox;
        private GroupBox TeethQuantityGroupBox;
        private NumericUpDown TeethQuantityUpDown;
        private TextBox RollDiameterValueBox;
        private GroupBox BaseDiameterGroupBox;
        private CheckBox BaseDiameterCheckBox;
        private TextBox BaseDiameterValueBox;
        private GroupBox MajorDiameterGroupBox;
        private CheckBox MajorDiameterCheckBox;
        private TextBox MajorDiameterValueBox;
        private ComboBox ProfileTypeComboBox;

        public GeometryMenu()
        {
            InitializeComponent();
            // Initialization of handlers
            new Action(() => {
                this.RollDiameterValueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler((sender, e) =>
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
            })();
            // Initialization of parameter groupBoxes
            new Action(() => {
                // Profile type group
                new Action(() => {
                    // 
                    // ProfileTypeGroupBox
                    // 
                    this.ProfileTypeGroupBox.Controls.Add(this.ProfileTypeComboBox);
                    this.ProfileTypeGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                    this.ProfileTypeGroupBox.Location = new System.Drawing.Point(6, 19);
                    this.ProfileTypeGroupBox.Name = "ProfileTypeGroupBox";
                    this.ProfileTypeGroupBox.Size = new System.Drawing.Size(188, 61);
                    this.ProfileTypeGroupBox.TabIndex = 1;
                    this.ProfileTypeGroupBox.TabStop = false;
                    this.ProfileTypeGroupBox.Text = Vocabulary.ProfileType();
                    // 
                    // ProfileTypeComboBox
                    // 
                    this.ProfileTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    this.ProfileTypeComboBox.FormattingEnabled = true;
                    this.ProfileTypeComboBox.Items.AddRange(new object[] {
                    Vocabulary.Epicycloid(),
                    Vocabulary.Hipocycloid()});
                    this.ProfileTypeComboBox.Location = new System.Drawing.Point(6, 25);
                    this.ProfileTypeComboBox.Name = "ProfileTypeComboBox";
                    this.ProfileTypeComboBox.Size = new System.Drawing.Size(121, 24);
                    this.ProfileTypeComboBox.TabIndex = 1;
                })();
            })();
        }

        private void InitializeComponent()
        {
            this.ParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.RollDiameterGroupBox = new System.Windows.Forms.GroupBox();
            this.RollDiameterValueBox = new System.Windows.Forms.TextBox();
            this.TeethQuantityGroupBox = new System.Windows.Forms.GroupBox();
            this.TeethQuantityUpDown = new System.Windows.Forms.NumericUpDown();
            this.ProfileTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.ProfileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.MajorDiameterGroupBox = new System.Windows.Forms.GroupBox();
            this.MajorDiameterValueBox = new System.Windows.Forms.TextBox();
            this.MajorDiameterCheckBox = new System.Windows.Forms.CheckBox();
            this.BaseDiameterGroupBox = new System.Windows.Forms.GroupBox();
            this.BaseDiameterCheckBox = new System.Windows.Forms.CheckBox();
            this.BaseDiameterValueBox = new System.Windows.Forms.TextBox();
            this.ParamsGroupBox.SuspendLayout();
            this.RollDiameterGroupBox.SuspendLayout();
            this.TeethQuantityGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TeethQuantityUpDown)).BeginInit();
            this.ProfileTypeGroupBox.SuspendLayout();
            this.MajorDiameterGroupBox.SuspendLayout();
            this.BaseDiameterGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParamsGroupBox
            // 
            this.ParamsGroupBox.Controls.Add(this.BaseDiameterGroupBox);
            this.ParamsGroupBox.Controls.Add(this.MajorDiameterGroupBox);
            this.ParamsGroupBox.Controls.Add(this.RollDiameterGroupBox);
            this.ParamsGroupBox.Controls.Add(this.TeethQuantityGroupBox);
            this.ParamsGroupBox.Controls.Add(this.ProfileTypeGroupBox);
            this.ParamsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParamsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ParamsGroupBox.Name = "ParamsGroupBox";
            this.ParamsGroupBox.Size = new System.Drawing.Size(200, 636);
            this.ParamsGroupBox.TabIndex = 0;
            this.ParamsGroupBox.TabStop = false;
            // 
            // RollDiameterGroupBox
            // 
            this.RollDiameterGroupBox.Controls.Add(this.RollDiameterValueBox);
            this.RollDiameterGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RollDiameterGroupBox.Location = new System.Drawing.Point(6, 140);
            this.RollDiameterGroupBox.Name = "RollDiameterGroupBox";
            this.RollDiameterGroupBox.Size = new System.Drawing.Size(188, 49);
            this.RollDiameterGroupBox.TabIndex = 3;
            this.RollDiameterGroupBox.TabStop = false;
            this.RollDiameterGroupBox.Text = "Roll diameter";
            // 
            // RollDiameterValueBox
            // 
            this.RollDiameterValueBox.Location = new System.Drawing.Point(7, 22);
            this.RollDiameterValueBox.Name = "RollDiameterValueBox";
            this.RollDiameterValueBox.Size = new System.Drawing.Size(100, 22);
            this.RollDiameterValueBox.TabIndex = 0;
            // 
            // TeethQuantityGroupBox
            // 
            this.TeethQuantityGroupBox.Controls.Add(this.TeethQuantityUpDown);
            this.TeethQuantityGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TeethQuantityGroupBox.Location = new System.Drawing.Point(6, 86);
            this.TeethQuantityGroupBox.Name = "TeethQuantityGroupBox";
            this.TeethQuantityGroupBox.Size = new System.Drawing.Size(188, 48);
            this.TeethQuantityGroupBox.TabIndex = 2;
            this.TeethQuantityGroupBox.TabStop = false;
            this.TeethQuantityGroupBox.Text = "Teeth quantity";
            // 
            // TeethQuantityUpDown
            // 
            this.TeethQuantityUpDown.Location = new System.Drawing.Point(7, 22);
            this.TeethQuantityUpDown.Name = "TeethQuantityUpDown";
            this.TeethQuantityUpDown.Size = new System.Drawing.Size(120, 22);
            this.TeethQuantityUpDown.TabIndex = 0;
            // 
            // ProfileTypeGroupBox
            // 
            this.ProfileTypeGroupBox.Controls.Add(this.ProfileTypeComboBox);
            this.ProfileTypeGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.ProfileTypeGroupBox.Location = new System.Drawing.Point(6, 19);
            this.ProfileTypeGroupBox.Name = "ProfileTypeGroupBox";
            this.ProfileTypeGroupBox.Size = new System.Drawing.Size(188, 61);
            this.ProfileTypeGroupBox.TabIndex = 1;
            this.ProfileTypeGroupBox.TabStop = false;
            this.ProfileTypeGroupBox.Text = "Profile type";
            // 
            // ProfileTypeComboBox
            // 
            this.ProfileTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ProfileTypeComboBox.FormattingEnabled = true;
            this.ProfileTypeComboBox.Items.AddRange(new object[] {
            "Epicycloid",
            "Hipocycloid"});
            this.ProfileTypeComboBox.Location = new System.Drawing.Point(6, 25);
            this.ProfileTypeComboBox.Name = "ProfileTypeComboBox";
            this.ProfileTypeComboBox.Size = new System.Drawing.Size(121, 24);
            this.ProfileTypeComboBox.TabIndex = 1;
            // 
            // MajorDiameterGroupBox
            // 
            this.MajorDiameterGroupBox.Controls.Add(this.MajorDiameterCheckBox);
            this.MajorDiameterGroupBox.Controls.Add(this.MajorDiameterValueBox);
            this.MajorDiameterGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MajorDiameterGroupBox.Location = new System.Drawing.Point(6, 195);
            this.MajorDiameterGroupBox.Name = "MajorDiameterGroupBox";
            this.MajorDiameterGroupBox.Size = new System.Drawing.Size(188, 49);
            this.MajorDiameterGroupBox.TabIndex = 4;
            this.MajorDiameterGroupBox.TabStop = false;
            this.MajorDiameterGroupBox.Text = "Major diameter";
            // 
            // MajorDiameterValueBox
            // 
            this.MajorDiameterValueBox.Location = new System.Drawing.Point(7, 22);
            this.MajorDiameterValueBox.Name = "MajorDiameterValueBox";
            this.MajorDiameterValueBox.Size = new System.Drawing.Size(100, 22);
            this.MajorDiameterValueBox.TabIndex = 0;
            // 
            // MajorDiameterCheckBox
            // 
            this.MajorDiameterCheckBox.AutoSize = true;
            this.MajorDiameterCheckBox.Location = new System.Drawing.Point(167, 22);
            this.MajorDiameterCheckBox.Name = "MajorDiameterCheckBox";
            this.MajorDiameterCheckBox.Size = new System.Drawing.Size(15, 14);
            this.MajorDiameterCheckBox.TabIndex = 1;
            this.MajorDiameterCheckBox.UseVisualStyleBackColor = true;
            // 
            // BaseDiameterGroupBox
            // 
            this.BaseDiameterGroupBox.Controls.Add(this.BaseDiameterCheckBox);
            this.BaseDiameterGroupBox.Controls.Add(this.BaseDiameterValueBox);
            this.BaseDiameterGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BaseDiameterGroupBox.Location = new System.Drawing.Point(6, 250);
            this.BaseDiameterGroupBox.Name = "BaseDiameterGroupBox";
            this.BaseDiameterGroupBox.Size = new System.Drawing.Size(188, 49);
            this.BaseDiameterGroupBox.TabIndex = 5;
            this.BaseDiameterGroupBox.TabStop = false;
            this.BaseDiameterGroupBox.Text = "Base diameter";
            // 
            // BaseDiameterCheckBox
            // 
            this.BaseDiameterCheckBox.AutoSize = true;
            this.BaseDiameterCheckBox.Location = new System.Drawing.Point(167, 22);
            this.BaseDiameterCheckBox.Name = "BaseDiameterCheckBox";
            this.BaseDiameterCheckBox.Size = new System.Drawing.Size(15, 14);
            this.BaseDiameterCheckBox.TabIndex = 1;
            this.BaseDiameterCheckBox.UseVisualStyleBackColor = true;
            // 
            // BaseDiameterValueBox
            // 
            this.BaseDiameterValueBox.Location = new System.Drawing.Point(7, 22);
            this.BaseDiameterValueBox.Name = "BaseDiameterValueBox";
            this.BaseDiameterValueBox.Size = new System.Drawing.Size(100, 22);
            this.BaseDiameterValueBox.TabIndex = 0;
            // 
            // GeometryMenu
            // 
            this.Controls.Add(this.ParamsGroupBox);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(969, 636);
            this.ParamsGroupBox.ResumeLayout(false);
            this.RollDiameterGroupBox.ResumeLayout(false);
            this.RollDiameterGroupBox.PerformLayout();
            this.TeethQuantityGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TeethQuantityUpDown)).EndInit();
            this.ProfileTypeGroupBox.ResumeLayout(false);
            this.MajorDiameterGroupBox.ResumeLayout(false);
            this.MajorDiameterGroupBox.PerformLayout();
            this.BaseDiameterGroupBox.ResumeLayout(false);
            this.BaseDiameterGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        

    }
}
