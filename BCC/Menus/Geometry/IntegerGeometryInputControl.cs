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

namespace BCC.Menus.Geometry
{
    public partial class IntegerGeometryInputControl : UserControl
    {
        public readonly string parameterName;
        private int value;
        private readonly GeometryMenu parent;

        public int GetValue()
        {
            return value;
        }

        public IntegerGeometryInputControl(string parameterName, GeometryMenu parent)
        {
            this.parent = parent;
            this.parameterName = parameterName;
            InitializeComponent();
            ParameterNameLabel.Text = parameterName;
        }

        private void ParameterValueTextBox_TextChanged(object sender, EventArgs e)
        {
            /*if (ParameterValueTextBox.Text.ToString() != string.Empty)
            {
                try
                {
                    value = int.Parse(ParameterValueTextBox.Text.ToString());
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
            */
        }

        private void ParameterAvailabilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParameterValueCounter.Enabled = ParameterAvailabilityCheckBox.Checked;
            if (ParameterValueCounter.Enabled)
            {
                ParameterNameLabel.ForeColor = Color.Black;
            }
            else ParameterNameLabel.ForeColor = Color.DarkGray;
        }

        private void IntegerGeometryInputControl_Load(object sender, EventArgs e)
        {

        }

        internal void Value(double v)
        {
            if (!ParameterAvailabilityCheckBox.Checked)
                ParameterValueCounter.Value = value;
        }

        internal bool Available()
        {
            return ParameterAvailabilityCheckBox.Checked;
        }

        private void ParameterValueCounter_ValueChanged(object sender, EventArgs e)
        {
            value = (int)ParameterValueCounter.Value;
            parent.ComputationButtonAction();
        }
    }
}
