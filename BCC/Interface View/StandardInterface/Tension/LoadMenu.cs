using BCC.Core.Load;
using System;
using System.Collections.Generic;
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
            internal Dictionary<DimensioningParams, Func<double, double>> getterCalls;
            internal Dictionary<DimensioningParams, Action<double, double>> setterCalls;
        }
        private GroupBox InputDataGroupBox;
        private readonly Dictionary<DimensioningParams, Func<double, double>> getterCalls;
        private readonly Dictionary<DimensioningParams, Action<double, double>> setterCalls;
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
            this.InputDataGroupBox = new System.Windows.Forms.GroupBox();
            this.InputDataFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.InputDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputDataGroupBox
            // 
            this.InputDataGroupBox.Controls.Add(this.InputDataFlowPanel);
            this.InputDataGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.InputDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.InputDataGroupBox.Name = "InputDataGroupBox";
            this.InputDataGroupBox.Size = new System.Drawing.Size(384, 2000);
            this.InputDataGroupBox.TabIndex = 0;
            this.InputDataGroupBox.TabStop = false;
            // 
            // InputDataFlowPanel
            // 
            this.InputDataFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDataFlowPanel.Location = new System.Drawing.Point(3, 16);
            this.InputDataFlowPanel.Name = "InputDataFlowPanel";
            this.InputDataFlowPanel.Size = new System.Drawing.Size(378, 1981);
            this.InputDataFlowPanel.TabIndex = 0;
            // 
            // LoadMenu
            // 
            this.Controls.Add(this.InputDataGroupBox);
            this.Name = "LoadMenu";
            this.Size = new System.Drawing.Size(2000, 2000);
            this.InputDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public Func<double, double> Get(DimensioningParams param) => getterCalls[param];
        public void Set(DimensioningParams param, double val, double index)
        {
            if (setterCalls.ContainsKey(param)) setterCalls[param](index, val);
        }
    }
}
