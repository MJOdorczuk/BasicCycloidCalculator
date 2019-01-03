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
            internal Dictionary<LoadParams, Func<double>> getterCalls;
            internal Dictionary<LoadParams, Action<double>> setterCalls;
        }
        private GroupBox InputDataGroupBox;
        private readonly Dictionary<LoadParams, Func<double>> getterCalls;
        private readonly Dictionary<LoadParams, Action<double>> setterCalls;
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
            this.InputDataGroupBox.Size = new System.Drawing.Size(236, 645);
            this.InputDataGroupBox.TabIndex = 0;
            this.InputDataGroupBox.TabStop = false;
            // 
            // InputDataFlowPanel
            // 
            this.InputDataFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDataFlowPanel.Location = new System.Drawing.Point(3, 16);
            this.InputDataFlowPanel.Name = "InputDataFlowPanel";
            this.InputDataFlowPanel.Size = new System.Drawing.Size(230, 626);
            this.InputDataFlowPanel.TabIndex = 0;
            // 
            // LoadMenu
            // 
            this.Controls.Add(this.InputDataGroupBox);
            this.Name = "LoadMenu";
            this.Size = new System.Drawing.Size(991, 645);
            this.InputDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public double Get(LoadParams param) => getterCalls[param]();
        public void Set(LoadParams param, double val)
        {
            if (setterCalls.ContainsKey(param)) setterCalls[param](val);
        }
    }
}
