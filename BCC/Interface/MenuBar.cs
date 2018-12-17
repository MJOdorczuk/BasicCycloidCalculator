using BCC.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Interface
{
    class MenuBar : MenuStrip
    {
        private readonly Dictionary<UserControl, ToolStripMenuItem> calls =
            new Dictionary<UserControl, ToolStripMenuItem>();
        private int visibleItems = 1;

        public void PushMenu(UserControl control, string label)
        {
            var first = calls.Count == 0;
            ToolStripMenuItem item = new ToolStripMenuItem()
            {
                Font = new Font(FontFamily.GenericMonospace, 14),
                AutoSize = true,
                Visible = first,
                Enabled = first,
                Text = label
            };
            item.Click += new EventHandler((sender, e) =>
            {
                Model.Call(control);
            });
            calls.Add(control, item);
            Items.Add(item);
        }

        public void Show(UserControl control, bool visible = true)
        {
            visibleItems += visible ? 1 : -1;
            if (calls.ContainsKey(control))
            {
                calls[control].Visible = visible;
                calls[control].Enabled = visible;
            }
        }

    }
}
