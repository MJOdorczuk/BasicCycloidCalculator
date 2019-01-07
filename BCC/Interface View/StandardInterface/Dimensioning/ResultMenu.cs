using BCC.Core.Load;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BCC.Interface_View.StandardInterface.Dimensioning
{
    class ResultMenu : UserControl
    {
        public struct InitialParameters
        {
            internal LoadModel model;
            internal int PARAMBOX_WIDTH;
            internal List<Control> parameterControls;
            internal Dictionary<Enum, Func<double>> getterCalls;
            internal Dictionary<Enum, Action<double>> setterCalls;
        }
        private readonly Dictionary<Enum, Func<double>> getterCalls;
        private readonly Dictionary<Enum, Action<double>> setterCalls;
        private readonly LoadModel model;

        private FlowLayoutPanel InputFlowPanel;
        private DataGridView ResultDataGrid;
        private DataGridViewTextBoxColumn RollNumber;
        private DataGridViewTextBoxColumn Force;
        private DataGridViewTextBoxColumn Stress;
        private System.Windows.Forms.DataVisualization.Charting.Chart ResultForceChart;
        private GroupBox ResultDataGroupBox;
        private FlowLayoutPanel ResultFlowPanel;
        private GroupBox InputGroupBox;

        public ResultMenu(InitialParameters initialParameters)
        {
            model = initialParameters.model;
            getterCalls = initialParameters.getterCalls;
            setterCalls = initialParameters.setterCalls;
            InitializeComponent();
            ResultFlowPanel.Controls.AddRange(initialParameters.parameterControls.ToArray());
        }

        private void InitializeComponent()
        {
            this.ResultDataGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ResultDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultDataGroupBox
            // 
            this.ResultDataGroupBox.Controls.Add(this.ResultFlowPanel);
            this.ResultDataGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ResultDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ResultDataGroupBox.Name = "ResultDataGroupBox";
            this.ResultDataGroupBox.Size = new System.Drawing.Size(370, 720);
            this.ResultDataGroupBox.TabIndex = 0;
            this.ResultDataGroupBox.TabStop = false;
            // 
            // ResultFlowPanel
            // 
            this.ResultFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultFlowPanel.Location = new System.Drawing.Point(3, 16);
            this.ResultFlowPanel.Name = "ResultFlowPanel";
            this.ResultFlowPanel.Size = new System.Drawing.Size(364, 701);
            this.ResultFlowPanel.TabIndex = 0;
            // 
            // ResultMenu
            // 
            this.Controls.Add(this.ResultDataGroupBox);
            this.Name = "ResultMenu";
            this.Size = new System.Drawing.Size(1280, 720);
            this.ResultDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
