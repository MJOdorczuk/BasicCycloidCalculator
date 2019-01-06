using BCC.Core.Load;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface.Tension
{
    class LoadMenu : UserControl
    {
        public struct InitialParameters
        {
            internal LoadModel model;
            internal int PARAMBOX_WIDTH;
            internal List<Control> parameterControls;
            internal Dictionary<Enum, Func<int, double>> getterCalls;
            internal Dictionary<Enum, Action<int, double>> setterCalls;
        }
        private GroupBox InputDataGroupBox;
        private readonly Dictionary<Enum, Func<int, double>> getterCalls;
        private readonly Dictionary<Enum, Action<int, double>> setterCalls;
        private FlowLayoutPanel InputDataFlowPanel;
        private readonly LoadModel model;

        public LoadMenu(InitialParameters initialParameters)
        {
            this.model = initialParameters.model;
            InitializeComponent();

            InputDataFlowPanel.Controls.AddRange
                (initialParameters.parameterControls.ToArray());

            this.InputDataFlowPanel.Width = initialParameters.PARAMBOX_WIDTH;
            this.getterCalls = initialParameters.getterCalls;
            this.setterCalls = initialParameters.setterCalls;
        }

        private void InitializeComponent()
        {
            InputDataGroupBox = new GroupBox();
            InputDataFlowPanel = new FlowLayoutPanel();
            InputDataGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // InputDataGroupBox
            // 
            InputDataGroupBox.Controls.Add(InputDataFlowPanel);
            InputDataGroupBox.Dock = DockStyle.Left;
            InputDataGroupBox.Location = new Point(0, 0);
            InputDataGroupBox.Name = "InputDataGroupBox";
            InputDataGroupBox.Size = new Size(384, 2000);
            InputDataGroupBox.TabIndex = 0;
            InputDataGroupBox.TabStop = false;
            // 
            // InputDataFlowPanel
            // 
            InputDataFlowPanel.Dock = DockStyle.Fill;
            InputDataFlowPanel.Location = new Point(3, 16);
            InputDataFlowPanel.Name = "InputDataFlowPanel";
            InputDataFlowPanel.Size = new Size(378, 1981);
            InputDataFlowPanel.TabIndex = 0;
            // 
            // LoadMenu
            // 
            Controls.Add(InputDataGroupBox);
            Name = "LoadMenu";
            Size = new Size(2000, 2000);
            InputDataGroupBox.ResumeLayout(false);
            ResumeLayout(false);

        }

        public Func<int, double> Get(Enum param) => getterCalls[param];
        public void Set(Enum param, double val, int index)
        {
            if (setterCalls.ContainsKey(param)) setterCalls[param](index, val);
        }
    }
}
