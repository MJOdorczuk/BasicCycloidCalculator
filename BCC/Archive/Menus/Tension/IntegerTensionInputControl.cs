using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Menus.Tension
{
    public partial class IntegerTensionInputControl : UserControl
    {
        private readonly TensionMenu parent;
        private int value;
        private readonly string parameterName;

        public int GetValue() => value;

        public IntegerTensionInputControl(string parameterName, TensionMenu parent)
        {
            this.parameterName = parameterName;
            this.parent = parent;
            InitializeComponent();
            ParameterNameLabel.Text = parameterName;
        }

        private void IntegerTensionInputControl_Load(object sender, EventArgs e)
        {

        }

        private void ParameterValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ParameterValueTextBox.Text.ToString() != string.Empty)
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
    }
}
