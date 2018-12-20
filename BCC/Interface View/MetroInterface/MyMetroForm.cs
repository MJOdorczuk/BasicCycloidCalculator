using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Interface_View.MetroInterface
{
    class MyMetroForm : MetroForm
    {
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private System.ComponentModel.IContainer components;
        private MetroFramework.Controls.MetroTabControl MenuTabControl;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroComboBox metroComboBox2;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;

        public MyMetroForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.MenuTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroComboBox2 = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.MenuTabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // MenuTabControl
            // 
            this.MenuTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuTabControl.Controls.Add(this.metroTabPage1);
            this.MenuTabControl.Controls.Add(this.metroTabPage2);
            this.MenuTabControl.Controls.Add(this.metroTabPage3);
            this.MenuTabControl.Controls.Add(this.metroTabPage4);
            this.MenuTabControl.Location = new System.Drawing.Point(24, 64);
            this.MenuTabControl.Name = "MenuTabControl";
            this.MenuTabControl.SelectedIndex = 0;
            this.MenuTabControl.Size = new System.Drawing.Size(916, 475);
            this.MenuTabControl.TabIndex = 0;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.metroComboBox2);
            this.metroTabPage1.Controls.Add(this.metroLabel2);
            this.metroTabPage1.Controls.Add(this.metroComboBox1);
            this.metroTabPage1.Controls.Add(this.metroLabel1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(908, 436);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "metroTabPage1";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // metroComboBox2
            // 
            this.metroComboBox2.FormattingEnabled = true;
            this.metroComboBox2.ItemHeight = 23;
            this.metroComboBox2.Items.AddRange(new object[] {
            "Default",
            "Black",
            "White",
            "Silver",
            "Blue",
            "Green",
            "Lime",
            "Teal",
            "Orange",
            "Brown",
            "Pink",
            "Magenta",
            "Purple",
            "Red",
            "Yellow"});
            this.metroComboBox2.Location = new System.Drawing.Point(324, 69);
            this.metroComboBox2.Name = "metroComboBox2";
            this.metroComboBox2.Size = new System.Drawing.Size(121, 29);
            this.metroComboBox2.TabIndex = 5;
            this.metroComboBox2.SelectedIndexChanged += new System.EventHandler(this.metroComboBox2_SelectedIndexChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(282, 72);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(36, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Style";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Items.AddRange(new object[] {
            "Default",
            "Light",
            "Dark"});
            this.metroComboBox1.Location = new System.Drawing.Point(112, 69);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(121, 29);
            this.metroComboBox1.TabIndex = 3;
            this.metroComboBox1.SelectedIndexChanged += new System.EventHandler(this.metroComboBox1_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(57, 72);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(49, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Theme";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(192, 61);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "metroTabPage2";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(192, 61);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "metroTabPage3";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(192, 61);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "metroTabPage4";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            // 
            // MyMetroForm
            // 
            this.ClientSize = new System.Drawing.Size(943, 540);
            this.Controls.Add(this.MenuTabControl);
            this.Name = "MyMetroForm";
            this.Text = "CycloCalc";
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.MenuTabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(metroComboBox1.SelectedIndex)
            {
                case 0:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Default;
                    break;
                case 1:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                    break;
                case 2:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                    break;
            }
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(metroComboBox2.SelectedIndex)
            {
                case 0:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Default;
                    break;
                case 1:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Black;
                    break;
                case 2:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.White;
                    break;
                case 3:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Silver;
                    break;
                case 4:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
                    break;
                case 5:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Green;
                    break;
                case 6:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Lime;
                    break;
                case 7:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Teal;
                    break;
                case 8:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange;
                    break;
                case 9:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Brown;
                    break;
                case 10:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Pink;
                    break;
                case 11:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Magenta;
                    break;
                case 12:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple;
                    break;
                case 13:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Red;
                    break;
                case 14:
                    metroStyleManager1.Style = MetroFramework.MetroColorStyle.Yellow;
                    break;
            }
        }
    }
}
