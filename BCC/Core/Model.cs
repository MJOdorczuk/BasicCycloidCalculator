using BCC.Core.Geometry;
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
        private static GeometryModel geometryModel = new SimpleGeometryModel();
        private static TabPage geometryPage = new TabPage();
        private static TabPage loadPage = new TabPage();

        public static Form Initialize()
        {
            Vocabulary.AddNameCall(() => geometryPage.Text = Vocabulary.TabPagesNames.Geometry());
            Vocabulary.AddNameCall(() => loadPage.Text = Vocabulary.TabPagesNames.Load());
            Model.main = new StandardForm()
            {
                Width = 1280,
                Height = 720,
                Visible = true,
                AutoSize = true,
                Enabled = true,
                MaximumSize = new Size(1920, 1080),
                MinimumSize = new Size(1280, 720),
                Text = "CycloCalc",
                IsMdiContainer = true,
                ShowIcon = false,
            };
            Model.workSpace = Model.main.WorkSpace;
            var geometryMenu = geometryModel.GetMenu();
            workSpace.TabPages.Add(geometryPage);
            workSpace.TabPages.Add(loadPage);
            geometryPage.Controls.Add(geometryMenu);
            Vocabulary.UpdateAllNames();
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
