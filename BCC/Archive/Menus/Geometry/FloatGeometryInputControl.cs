using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCC.Menus.Main;

namespace BCC
{
    public partial class FloatGeometryInputControl : UserControl
    {
        private readonly GeometryMenu parent;
        public readonly string parameterName;
        private double value;

        public double Value()
        {
            return value;
        }

        public FloatGeometryInputControl(string parameterName, GeometryMenu parent)
        {
            this.parent = parent;
            this.parameterName = parameterName;
            InitializeComponent();
            ParameterNameLabel.Text = parameterName;
        }

        private void ParameterAvailabilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParameterValueTextBox.Enabled = ParameterAvailabilityCheckBox.Checked;
            if (ParameterValueTextBox.Enabled)
            {
                ParameterNameLabel.ForeColor = Color.Black;
            }
            else ParameterNameLabel.ForeColor = Color.DarkGray;
        }

        private void ParameterValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ParameterValueTextBox.Text.ToString() != string.Empty)
            {
                try
                {
                    value = float.Parse(ParameterValueTextBox.Text.ToString());
                }
                catch (Exception)
                {
                    ParameterValueTextBox.BackColor = Color.Red;
                    return;
                }
            }
            else
            {
                ParameterValueTextBox.BackColor = Color.White;
            }
        }

        private void GeometryInputControl_Load(object sender, EventArgs e)
        {

        }

        internal void Value(double v)
        {
            if (!ParameterAvailabilityCheckBox.Checked)
                ParameterValueTextBox.Text = v.ToString();
        }

        internal bool Available()
        {
            return ParameterAvailabilityCheckBox.Checked;
        }
    }
}
