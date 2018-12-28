using BCC.Core.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface.Geometry
{
    

    internal class GeometryMenu : UserControl
    {
        public struct InitialParameters
        {
            internal GeometryModel model;
            internal int PARAMBOX_WIDTH;
            internal List<Control> parameterControls;
            internal Dictionary<CycloParams, Func<object>> getterCalls;
            internal Dictionary<CycloParams, Action<object>> setterCalls;
            internal Dictionary<CycloParams, Func<bool>> availabilityCalls;
        }

        private static class StaticFields
        {
            public static readonly Pen widePen = new Pen(Brushes.Black)
            {
                Width = 4.0F,
                LineJoin = LineJoin.Bevel
            };
            public const int INITIAL_POINT_DENSITY = 10000;
        }

        private GroupBox ParamsGroupBox;
        private FlowLayoutPanel ParameterFlowLayoutPanel;
        private readonly Dictionary<CycloParams, Func<object>> getterCalls;
        private readonly Dictionary<CycloParams, Action<object>> setterCalls;
        private readonly Dictionary<CycloParams, Func<bool>> availabilityCalls;
        private readonly Graphics displayGraphics;
        private Button button1;
        private Panel DisplayPanel;
        private readonly GeometryModel model;

        public GeometryMenu(InitialParameters initialParameters)
        {
            this.model = initialParameters.model;
            InitializeComponent();

            ParameterFlowLayoutPanel.Controls.AddRange(initialParameters.parameterControls.ToArray());

            this.ParamsGroupBox.Width = initialParameters.PARAMBOX_WIDTH;
            this.getterCalls = initialParameters.getterCalls;
            this.setterCalls = initialParameters.setterCalls;
            this.availabilityCalls = initialParameters.availabilityCalls;
            displayGraphics = DisplayPanel.CreateGraphics();
        }

        private void InitializeComponent()
        {
            this.ParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ParameterFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.ParamsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParamsGroupBox
            // 
            this.ParamsGroupBox.Controls.Add(this.button1);
            this.ParamsGroupBox.Controls.Add(this.ParameterFlowLayoutPanel);
            this.ParamsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParamsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ParamsGroupBox.Name = "ParamsGroupBox";
            this.ParamsGroupBox.Size = new System.Drawing.Size(214, 636);
            this.ParamsGroupBox.TabIndex = 0;
            this.ParamsGroupBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ParameterFlowLayoutPanel
            // 
            this.ParameterFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParameterFlowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.ParameterFlowLayoutPanel.Name = "ParameterFlowLayoutPanel";
            this.ParameterFlowLayoutPanel.Size = new System.Drawing.Size(208, 617);
            this.ParameterFlowLayoutPanel.TabIndex = 2;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(214, 0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(755, 636);
            this.DisplayPanel.TabIndex = 1;
            // 
            // GeometryMenu
            // 
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.ParamsGroupBox);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(969, 636);
            this.ParamsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public Panel GetDisplayPanel => DisplayPanel;

        public Action<double, Func<double,PointF>> GetRenderer(int pointDensity = StaticFields.INITIAL_POINT_DENSITY)
        {
            return (da, curve) =>
            {
                var box = DisplayPanel.Width > DisplayPanel.Height ? DisplayPanel.Height : DisplayPanel.Width;
                var factor = 0.5 * box / da;
                var x0 = DisplayPanel.Width / 2;
                var y0 = DisplayPanel.Height / 2;
                var curvePoints = new List<PointF>();
                for(int i = 0; i < pointDensity; i++)
                {
                    var t = 2.0 * Math.PI * i / pointDensity;
                    var x = (float)(x0 + curve(t).X * factor);
                    var y = (float)(y0 + curve(t).Y * factor);
                    curvePoints.Add(new PointF(x, y));
                }
                //Bitmap bitmap = new Bitmap(2 * x0, 2 * y0, PixelFormat.Format32bppArgb);
                //Graphics graphics = Graphics.FromImage(bitMap);
                DisplayPanel.Refresh();
                displayGraphics.Clear(Color.White);
                displayGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, x0 * 2, y0 * 2);
                //graphics.Clear(Color.White);

                displayGraphics.DrawClosedCurve(StaticFields.widePen, curvePoints.ToArray());
                //graphics.DrawClosedCurve(widePen, cycloid);
                //var name = "" + rnd.Next();
                //bitMap.Save(@"D:\Desktop\studia\Automatyka i robotyka\Praca dyplomowa\" + name + ".bmp", ImageFormat.Bmp);
                

            };
        }

        public object Get(CycloParams param) => 
            IsAvailable(param) ? getterCalls[param]() : null;

        public void Set(CycloParams param, object val) => setterCalls[param](val);

        public bool IsAvailable(CycloParams param) => 
            availabilityCalls.ContainsKey(param) ? availabilityCalls[param]() : true;

        private void button1_Click(object sender, EventArgs e)
        {
            model.Compute();
        }
    }
}
