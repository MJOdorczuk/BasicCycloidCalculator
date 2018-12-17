namespace BCC.Menus.Tension
{
    partial class FloatTensionInputControl
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
            this.ParameterAvailabilityCheckBox = new System.Windows.Forms.CheckBox();
            this.ParameterValueTextBox = new System.Windows.Forms.TextBox();
            this.ParameterNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ParameterAvailabilityCheckBox
            // 
            this.ParameterAvailabilityCheckBox.AutoSize = true;
            this.ParameterAvailabilityCheckBox.Location = new System.Drawing.Point(282, 3);
            this.ParameterAvailabilityCheckBox.Name = "ParameterAvailabilityCheckBox";
            this.ParameterAvailabilityCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ParameterAvailabilityCheckBox.TabIndex = 7;
            this.ParameterAvailabilityCheckBox.UseVisualStyleBackColor = true;
            this.ParameterAvailabilityCheckBox.CheckedChanged += new System.EventHandler(this.ParameterAvailabilityCheckBox_CheckedChanged);
            // 
            // ParameterValueTextBox
            // 
            this.ParameterValueTextBox.Enabled = false;
            this.ParameterValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ParameterValueTextBox.ForeColor = System.Drawing.SystemColors.MenuText;
            this.ParameterValueTextBox.Location = new System.Drawing.Point(147, 18);
            this.ParameterValueTextBox.Name = "ParameterValueTextBox";
            this.ParameterValueTextBox.Size = new System.Drawing.Size(150, 29);
            this.ParameterValueTextBox.TabIndex = 6;
            this.ParameterValueTextBox.TextChanged += new System.EventHandler(this.ParameterValueTextBox_TextChanged);
            // 
            // ParameterNameLabel
            // 
            this.ParameterNameLabel.AutoSize = true;
            this.ParameterNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ParameterNameLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ParameterNameLabel.Location = new System.Drawing.Point(3, 0);
            this.ParameterNameLabel.Name = "ParameterNameLabel";
            this.ParameterNameLabel.Size = new System.Drawing.Size(60, 24);
            this.ParameterNameLabel.TabIndex = 5;
            this.ParameterNameLabel.Text = "label1";
            // 
            // FloatTensionInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParameterAvailabilityCheckBox);
            this.Controls.Add(this.ParameterValueTextBox);
            this.Controls.Add(this.ParameterNameLabel);
            this.Name = "FloatTensionInputControl";
            this.Size = new System.Drawing.Size(300, 50);
            this.Load += new System.EventHandler(this.FloatTensionInputControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ParameterAvailabilityCheckBox;
        private System.Windows.Forms.TextBox ParameterValueTextBox;
        private System.Windows.Forms.Label ParameterNameLabel;
    }
}
