using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC
{
    public partial class GeometryInputControl : UserControl
    {
        public GeometryInputControl()
        {
            InitializeComponent();
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
                    float.Parse(ParameterValueTextBox.Text.ToString());
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
    }
}
