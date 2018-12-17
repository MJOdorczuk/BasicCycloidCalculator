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
    public partial class ResultControl : UserControl
    {
        public readonly string parameterName;
        public ResultControl(string parameterName)
        {
            this.parameterName = parameterName;
            Name = parameterName + "ResultControl";
            InitializeComponent();
            ParameterNameLabel.Text = parameterName;
        }

        private void ResultControl_Load(object sender, EventArgs e)
        {

        }

        public void Value(double value)
        {
            if (value == Math.Floor(value))
            {
                ParameterValueTextBox.Text = ((int)value).ToString();
            }
            else ParameterValueTextBox.Text = value.ToString();
        }
    }
}
