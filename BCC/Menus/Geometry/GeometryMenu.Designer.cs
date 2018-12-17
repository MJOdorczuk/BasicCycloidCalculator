namespace BCC.Menus.Main
{
    partial class GeometryMenu
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
            this.EpicycloidButton = new System.Windows.Forms.Button();
            this.ComputionButton = new System.Windows.Forms.Button();
            this.DebugTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // EpicycloidButton
            // 
            this.EpicycloidButton.Location = new System.Drawing.Point(4, 4);
            this.EpicycloidButton.Name = "EpicycloidButton";
            this.EpicycloidButton.Size = new System.Drawing.Size(100, 50);
            this.EpicycloidButton.TabIndex = 0;
            this.EpicycloidButton.Text = "epicycloid";
            this.EpicycloidButton.UseVisualStyleBackColor = true;
            this.EpicycloidButton.Click += new System.EventHandler(this.EpicycloidButton_Click);
            // 
            // ComputionButton
            // 
            this.ComputionButton.Location = new System.Drawing.Point(1122, 474);
            this.ComputionButton.Name = "ComputionButton";
            this.ComputionButton.Size = new System.Drawing.Size(75, 23);
            this.ComputionButton.TabIndex = 1;
            this.ComputionButton.Text = "COMPUTE";
            this.ComputionButton.UseVisualStyleBackColor = true;
            this.ComputionButton.Click += new System.EventHandler(this.ComputionButton_Click);
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(1023, 4);
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.Size = new System.Drawing.Size(174, 187);
            this.DebugTextBox.TabIndex = 2;
            this.DebugTextBox.Text = "";
            // 
            // GeometryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DebugTextBox);
            this.Controls.Add(this.ComputionButton);
            this.Controls.Add(this.EpicycloidButton);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(1200, 500);
            this.Load += new System.EventHandler(this.GeometryMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EpicycloidButton;
        private System.Windows.Forms.Button ComputionButton;
        private System.Windows.Forms.RichTextBox DebugTextBox;
    }
}
