namespace BCC.Menus.Geometry
{
    partial class IntegerGeometryInputControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ParameterNameLabel = new System.Windows.Forms.Label();
            this.ParameterAvailabilityCheckBox = new System.Windows.Forms.CheckBox();
            this.ParameterValueCounter = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ParameterValueCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // ParameterNameLabel
            // 
            this.ParameterNameLabel.AutoSize = true;
            this.ParameterNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ParameterNameLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ParameterNameLabel.Location = new System.Drawing.Point(3, 0);
            this.ParameterNameLabel.Name = "ParameterNameLabel";
            this.ParameterNameLabel.Size = new System.Drawing.Size(60, 24);
            this.ParameterNameLabel.TabIndex = 1;
            this.ParameterNameLabel.Text = "label1";
            // 
            // ParameterAvailabilityCheckBox
            // 
            this.ParameterAvailabilityCheckBox.AutoSize = true;
            this.ParameterAvailabilityCheckBox.Location = new System.Drawing.Point(282, 3);
            this.ParameterAvailabilityCheckBox.Name = "ParameterAvailabilityCheckBox";
            this.ParameterAvailabilityCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ParameterAvailabilityCheckBox.TabIndex = 4;
            this.ParameterAvailabilityCheckBox.UseVisualStyleBackColor = true;
            this.ParameterAvailabilityCheckBox.CheckedChanged += new System.EventHandler(this.ParameterAvailabilityCheckBox_CheckedChanged);
            // 
            // ParameterValueCounter
            // 
            this.ParameterValueCounter.Enabled = false;
            this.ParameterValueCounter.Location = new System.Drawing.Point(177, 23);
            this.ParameterValueCounter.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ParameterValueCounter.Name = "ParameterValueCounter";
            this.ParameterValueCounter.Size = new System.Drawing.Size(120, 20);
            this.ParameterValueCounter.TabIndex = 5;
            this.ParameterValueCounter.ValueChanged += new System.EventHandler(this.ParameterValueCounter_ValueChanged);
            // 
            // IntegerGeometryInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParameterValueCounter);
            this.Controls.Add(this.ParameterAvailabilityCheckBox);
            this.Controls.Add(this.ParameterNameLabel);
            this.Name = "IntegerGeometryInputControl";
            this.Size = new System.Drawing.Size(300, 50);
            this.Load += new System.EventHandler(this.IntegerGeometryInputControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ParameterValueCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ParameterNameLabel;
        private System.Windows.Forms.CheckBox ParameterAvailabilityCheckBox;
        private System.Windows.Forms.NumericUpDown ParameterValueCounter;
    }
}
