namespace BCC.Menus.Tension
{
    partial class TensionMenu
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
            this.ParameterValueTextBox = new System.Windows.Forms.TextBox();
            this.TorqueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ParameterValueTextBox
            // 
            this.ParameterValueTextBox.Enabled = false;
            this.ParameterValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ParameterValueTextBox.ForeColor = System.Drawing.SystemColors.MenuText;
            this.ParameterValueTextBox.Location = new System.Drawing.Point(81, 3);
            this.ParameterValueTextBox.Name = "ParameterValueTextBox";
            this.ParameterValueTextBox.Size = new System.Drawing.Size(150, 29);
            this.ParameterValueTextBox.TabIndex = 4;
            // 
            // TorqueLabel
            // 
            this.TorqueLabel.AutoSize = true;
            this.TorqueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TorqueLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.TorqueLabel.Location = new System.Drawing.Point(3, 0);
            this.TorqueLabel.Name = "TorqueLabel";
            this.TorqueLabel.Size = new System.Drawing.Size(72, 24);
            this.TorqueLabel.TabIndex = 3;
            this.TorqueLabel.Text = "Torque";
            // 
            // TensionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParameterValueTextBox);
            this.Controls.Add(this.TorqueLabel);
            this.Name = "TensionMenu";
            this.Size = new System.Drawing.Size(1200, 500);
            this.Load += new System.EventHandler(this.TensionMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ParameterValueTextBox;
        private System.Windows.Forms.Label TorqueLabel;
    }
}
