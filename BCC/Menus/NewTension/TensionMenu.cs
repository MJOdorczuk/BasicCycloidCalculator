using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Menus.NewTension
{
    class TensionMenu : UserControl
    {
        public TensionMenu()
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TensionMenu
            // 
            this.Name = "TensionMenu";
            this.Load += new System.EventHandler(this.TensionMenu_Load);
            this.ResumeLayout(false);

        }

        private void TensionMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
