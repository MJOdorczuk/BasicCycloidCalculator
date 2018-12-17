namespace BCC
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenuButton = new System.Windows.Forms.Button();
            this.GeometryMenuButon = new System.Windows.Forms.Button();
            this.TensionMenuButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mololToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wololToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wafToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wdawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuButton
            // 
            this.MainMenuButton.Location = new System.Drawing.Point(0, 27);
            this.MainMenuButton.Name = "MainMenuButton";
            this.MainMenuButton.Size = new System.Drawing.Size(200, 80);
            this.MainMenuButton.TabIndex = 0;
            this.MainMenuButton.Text = "Main Menu";
            this.MainMenuButton.UseVisualStyleBackColor = true;
            this.MainMenuButton.Click += new System.EventHandler(this.MainMenuButton_Click);
            // 
            // GeometryMenuButon
            // 
            this.GeometryMenuButon.Location = new System.Drawing.Point(206, 27);
            this.GeometryMenuButon.Name = "GeometryMenuButon";
            this.GeometryMenuButon.Size = new System.Drawing.Size(200, 80);
            this.GeometryMenuButon.TabIndex = 1;
            this.GeometryMenuButon.Text = "Cycloid Geometry";
            this.GeometryMenuButon.UseVisualStyleBackColor = true;
            this.GeometryMenuButon.Click += new System.EventHandler(this.GeometryMenuButon_Click);
            // 
            // TensionMenuButton
            // 
            this.TensionMenuButton.Location = new System.Drawing.Point(412, 27);
            this.TensionMenuButton.Name = "TensionMenuButton";
            this.TensionMenuButton.Size = new System.Drawing.Size(200, 80);
            this.TensionMenuButton.TabIndex = 2;
            this.TensionMenuButton.Text = "Tension Calculator";
            this.TensionMenuButton.UseVisualStyleBackColor = true;
            this.TensionMenuButton.Click += new System.EventHandler(this.TensionMenuButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lolToolStripMenuItem,
            this.mololToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lolToolStripMenuItem
            // 
            this.lolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wololToolStripMenuItem,
            this.wafToolStripMenuItem,
            this.wdawToolStripMenuItem});
            this.lolToolStripMenuItem.Name = "lolToolStripMenuItem";
            this.lolToolStripMenuItem.Size = new System.Drawing.Size(32, 20);
            this.lolToolStripMenuItem.Text = "lol";
            // 
            // mololToolStripMenuItem
            // 
            this.mololToolStripMenuItem.Name = "mololToolStripMenuItem";
            this.mololToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.mololToolStripMenuItem.Text = "molol";
            // 
            // wololToolStripMenuItem
            // 
            this.wololToolStripMenuItem.Name = "wololToolStripMenuItem";
            this.wololToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wololToolStripMenuItem.Text = "wolol";
            // 
            // wafToolStripMenuItem
            // 
            this.wafToolStripMenuItem.Name = "wafToolStripMenuItem";
            this.wafToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wafToolStripMenuItem.Text = "waf";
            // 
            // wdawToolStripMenuItem
            // 
            this.wdawToolStripMenuItem.Name = "wdawToolStripMenuItem";
            this.wdawToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wdawToolStripMenuItem.Text = "wdaw";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.TensionMenuButton);
            this.Controls.Add(this.GeometryMenuButon);
            this.Controls.Add(this.MainMenuButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MainMenuButton;
        private System.Windows.Forms.Button GeometryMenuButon;
        private System.Windows.Forms.Button TensionMenuButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wololToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wafToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wdawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mololToolStripMenuItem;
    }
}

