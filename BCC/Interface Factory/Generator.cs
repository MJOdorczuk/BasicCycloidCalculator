using BCC.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.InterfaceFactory
{
    public static class Generator
    {
        private static class MenuGenerator
        {
            private static readonly Dictionary<string, UserControl> menus = new Dictionary<string, UserControl>();

            static MenuGenerator()
            {
                // Dummy custom menus
                var menuA = new UserControl();
                var menuB = new UserControl();
                var menuC = new UserControl();
                var menuD = new UserControl();

                UserControl GeometryMenu()
                {
                    var geometryMenu = new UserControl();
                    

                    return geometryMenu;
                }
                


                var enableBButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Enable menu B",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 200)
                };
                enableBButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Enable(menuB);
                });
                var enableCButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Enable menu C",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 200)
                };
                enableCButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Enable(menuC);
                });
                var enableDButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Enable menu D",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 200)
                };
                enableDButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Enable(menuD);
                });
                var disableBButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Disable menu B",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 400)
                };
                disableBButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Disable(menuB);
                });
                var disableCButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Disable menu C",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 400)
                };
                disableCButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Disable(menuC);
                });
                var disableDButton = new Button()
                {
                    Font = new Font(FontFamily.GenericMonospace, 14),
                    Text = "Disable menu D",
                    Height = 100,
                    Width = 200,
                    Visible = true,
                    Enabled = true,
                    Location = new Point(100, 400)
                };
                disableDButton.Click += new EventHandler((sender, e) =>
                {
                    Model.Disable(menuD);
                });

                menuA.Controls.AddRange(new Control[] { enableBButton, disableBButton });
                menuB.Controls.AddRange(new Control[] { enableCButton, disableCButton });
                menuC.Controls.AddRange(new Control[] { enableDButton, disableDButton });

                menus.Add("menuA", menuA);
                menus.Add("menuB", menuB);
                menus.Add("menuC", menuC);
                menus.Add("menuD", menuD);
                menus.Add("Cycloid Geometry", GeometryMenu());

                foreach (var menu in menus.Values)
                {
                    menu.Visible = menu.Enabled = false;
                    menu.Dock = DockStyle.Fill;
                }
                menuA.Enabled = true;
                menus["Cycloid Geometry"].Enabled = true;

            }

            internal static Dictionary<string, UserControl> Menus => new Dictionary<string, UserControl>(menus);
        }

        public static Dictionary<string, UserControl> GetGenerateMenus()
        {
            var ret = MenuGenerator.Menus;
            if (ret.Count > 0)
            {
                var main = ret.ToList()[0].Value;
                main.Visible = main.Enabled = true;
            }
            return ret;
        }
    }
}
