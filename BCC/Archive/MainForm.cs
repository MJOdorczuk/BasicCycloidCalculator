using BCC.Archivised;
using BCC.Menus.Main;
using BCC.Menus.Tension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC
{
    public partial class MainForm : Form
    {
        private List<Button> menuButtons;
        private ArchivisedModel model;
        public MainForm()
        {

            Menus.Main.MainMenu mainMenu = new Menus.Main.MainMenu()
            {
                Width = this.Width,
                Height = this.Height - 100,
                AutoSize = true,
                Visible = true,
                Location = new Point(0, 100),
                Enabled = true
            };
            Controls.Add(mainMenu);
            model = new ArchivisedModel();
            mainMenu.SetModel(model);
            InitializeComponent();
            GeometryMenu geometryMenu = new GeometryMenu(model)
            {
                Width = this.Width,
                Height = this.Height - 100,
                AutoSize = true,
                Visible = false,
                Location = new Point(0, 100),
                Enabled = false
            };
            Controls.Add(geometryMenu);
            foreach (var control in Controls)
            {
                if (control is Menus.Main.MainMenu mm)
                {
                    mm.SetModel(model);
                }
            }
            TensionMenu tensionMenu = new TensionMenu(model)
            {
                Width = this.Width,
                Height = this.Height - 100,
                AutoSize = true,
                Visible = false,
                Location = new Point(0, 100),
                Enabled = false
            };
            Controls.Add(tensionMenu);
            menuButtons = new List<Button>()
            {
                MainMenuButton,
                GeometryMenuButon,
                TensionMenuButton
            };
            
        }

        /*public List<GeometryInputControl> GetGICs()
        {
            List<GeometryInputControl> ret = new List<GeometryInputControl>();
            foreach(Control control in GICPanel)
        }*/

        public ArchivisedModel GetModel => model;

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            foreach(Button button in menuButtons)
            {
                button.Enabled = true;
            }
            MainMenuButton.Enabled = false;
            foreach(var control in Controls)
            {
                if (control is Menus.Main.MainMenu menu)
                {
                    menu.Enabled = true;
                    menu.Visible = true;
                }
                else if (control is UserControl uc)
                {
                    uc.Enabled = false;
                    uc.Visible = false;
                }
            }
        }

        private void GeometryMenuButon_Click(object sender, EventArgs e)
        {
            foreach (Button button in menuButtons)
            {
                button.Enabled = true;
            }
            GeometryMenuButon.Enabled = false;
            foreach (var control in Controls)
            {
                if (control is GeometryMenu menu)
                {
                    menu.Enabled = true;
                    menu.Visible = true;
                }
                else if (control is UserControl uc)
                {
                    uc.Enabled = false;
                    uc.Visible = false;
                }
            }
        }

        private void TensionMenuButton_Click(object sender, EventArgs e)
        {
            foreach (Button button in menuButtons)
            {
                button.Enabled = true;
            }
            TensionMenuButton.Enabled = false;
            foreach (var control in Controls)
            {
                if (control is TensionMenu menu)
                {
                    menu.Enabled = true;
                    menu.Visible = true;
                }
                else if (control is UserControl uc)
                {
                    uc.Enabled = false;
                    uc.Visible = false;
                }
            }
        }
    }
}
