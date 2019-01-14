using BCC.Core.Geometry;
using BCC.Core.Load;
using BCC.Interface_View.StandardInterface;
using BCC.Interface_View.StandardInterface.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Core
{
    static class Initializator
    {

        private static StandardForm main;
        private static TabControl workSpace;
        private static GeometryModel geometryModel = new SimpleGeometryModel();
        private static LoadModel loadModel = new SimpleLoadModel();

        public static Form Initialize()
        {
            Initializator.main = new StandardForm()
            {
                Width = 1280,
                Height = 720,
                Visible = true,
                AutoSize = true,
                Enabled = true,
                MinimizeBox = true,
                MaximizeBox = true,
                MinimumSize = new Size(1280, 720),
                Text = "CycloCalc",
                IsMdiContainer = true,
                ShowIcon = false,
            };
            Initializator.workSpace = Initializator.main.WorkSpace;
            void DeployMenus(Dictionary<UserControl,Func<string>> menusPairs)
            {
                foreach(var pair in menusPairs)
                {
                    var page = new TabPage();
                    workSpace.TabPages.Add(page);
                    Vocabulary.AddNameCall(() => page.Text = pair.Value());
                    page.Controls.Add(pair.Key);
                }
            }
            DeployMenus(geometryModel.GetMenus());
            DeployMenus(loadModel.GetMenus());
            DeployMenus(new ParametrizedGeometryModel().GetMenus());
            DeployMenus(new ParametrizedLoadModel().GetMenus());
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
