using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Menus.Tension
{
    public partial class TensionMenu : UserControl
    {
        Model model;
        private bool isEpicycloid = true;

        public TensionMenu(Model model)
        {
            this.model = model;
            InitializeComponent();
        }

        public TensionMenu()
        {
            InitializeComponent();
        }

        internal void SetModel(Model model) => this.model = model;

        private void TensionMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
