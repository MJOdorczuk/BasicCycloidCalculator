using BCC.Interface;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Core
{
    static class Model
    {

        private static StandardForm main;
        private static Panel workSpace;
        private static MenuBar bar;

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
                ShowIcon = false
            };
            Model.workSpace = Model.main.WorkSpace;
            Model.bar = new MenuBar()
            {
                Font = new Font(FontFamily.GenericMonospace, 18),
                Dock = DockStyle.Top,
                AutoSize = true,
                Visible = true
            };
            workSpace.Controls.Add(bar);
            bar.Show();
            foreach (var m in Generator.GetGenerateMenus())
            {
                workSpace.Controls.Add(m.Value);
                bar.PushMenu(m.Value, m.Key);
            }
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
            bar.Show(menu);
        }

        internal static void Disable(UserControl menu)
        {
            bar.Show(menu, false);
        }
    }
}
