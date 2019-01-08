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

        private GroupBox ResultDataGroupBox;
        private FlowLayoutPanel ResultFlowPanel;
        private Series SplineSeries, PointSeries;

        public ResultMenu(InitialParameters initialParameters)
        {
            model = initialParameters.model;
            getterCalls = initialParameters.getterCalls;
            setterCalls = initialParameters.setterCalls;
            var ResultDataChart = new Chart
            {
                Dock = DockStyle.Fill,
                Location = new Point(370, 0),
                Size = new Size(514, 562),
                TabIndex = 1,
                Text = "chart1"
            };
            Controls.Add(ResultDataChart);
            InitializeComponent();
            ResultFlowPanel.Controls.AddRange(initialParameters.parameterControls.ToArray());
            var ChartArea = new ChartArea
            {
                Name = "ChartArea"
            };
            SplineSeries = new Series
            {
                ChartArea = ChartArea.Name,
                Name = "SplineSeries"
            };
            PointSeries = new Series
            {
                ChartArea = ChartArea.Name,
                Name = "PointSeries"
            };
            SplineSeries.ChartType = SeriesChartType.Spline;
            PointSeries.ChartType = SeriesChartType.Point;
            
            ResultDataChart.ChartAreas.Add(ChartArea);
            ResultDataChart.Series.Add(SplineSeries);
            ResultDataChart.Series.Add(PointSeries);
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
            this.ResultDataGroupBox.Size = new System.Drawing.Size(370, 562);
            this.ResultDataGroupBox.TabIndex = 0;
            this.ResultDataGroupBox.TabStop = false;
            // 
            // ResultFlowPanel
            // 
            this.ResultFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultFlowPanel.Location = new System.Drawing.Point(3, 16);
            this.ResultFlowPanel.Name = "ResultFlowPanel";
            this.ResultFlowPanel.Size = new System.Drawing.Size(364, 543);
            this.ResultFlowPanel.TabIndex = 0;
            // 
            // ResultMenu
            // 
            this.Controls.Add(this.ResultDataGroupBox);
            this.Name = "ResultMenu";
            this.Size = new System.Drawing.Size(884, 562);
            this.ResultDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public void SetSeries(double[] points)
        {
            SplineSeries.Points.Clear();
            SplineSeries.Points.Add(points);
            PointSeries.Points.Clear();
            PointSeries.Points.Add(points);
        }

        public double Get(Enum param) => getterCalls[param]();
        public void Set(Enum param, double val)
        {
            if (setterCalls.ContainsKey(param)) setterCalls[param](val);
        }
    }
}
