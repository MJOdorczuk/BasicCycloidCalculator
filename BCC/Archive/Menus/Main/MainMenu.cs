using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCC.Archivised;

namespace BCC.Menus.Main
{
    public partial class MainMenu : UserControl
    {
        ArchivisedModel model;
        public MainMenu()
        {
            InitializeComponent();
        }

        internal void SetModel(ArchivisedModel model) => this.model = model;

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
