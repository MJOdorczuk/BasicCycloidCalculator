using BCC.Interface_View.StandardInterface;
using BCC.Interface_View.StandardInterface.Geometry;
using BCC.Miscs;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Core
{
    static class Model
    {

        private static StandardForm main;
        private static TabControl workSpace;
        private static GeometryMenu geometryMenu;
        private static TabPage geometryPage = new TabPage() {
            Text = Vocabulary.Geometry()
        };
        private static TabPage loadPage = new TabPage()
        {
            Text = Vocabulary.Load()
        };

        public static Form Initialize()
        {
            Model.main = new StandardForm()
            {
                Width = 1280,
                Height = 720,
                Visible = true,
                AutoSize = true,
                Enabled = true,
                MaximumSize = new Size(1280, 720),
                MinimumSize = new Size(1280, 720),
                Text = "CycloCalc",
                IsMdiContainer = true,
                ShowIcon = false,
            };
            Model.workSpace = Model.main.WorkSpace;
            Model.geometryMenu = new GeometryMenu()
            {
                Visible = true,
                Enabled = true,
                AutoSize = true,
                Dock = DockStyle.Fill
            };
            workSpace.TabPages.Add(geometryPage);
            workSpace.TabPages.Add(loadPage);
            geometryPage.Controls.Add(geometryMenu);
            return main;
        }

        internal static void Call(UserControl control)
        {
            if (workSpace.Controls.Contains(control))
            {
                foreach (var c in workSpace.Controls)
                {
                    if (c is UserControl menu)
                    {
                        menu.Visible = menu.Enabled = false;
                    }
                }
                control.Visible = control.Enabled = true;
            }
        }

        internal static void Enable(UserControl menu)
        {
            //bar.Show(menu);
        }

        internal static void Disable(UserControl menu)
        {
            //bar.Show(menu, false);
        }
    }
}
