using BCC.Core.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
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

        public class RenderComponents
        {
            public readonly Mutex mutex = new Mutex();
            public readonly Renderer renderer;
            public bool graphicsRendered = false;
            public Func<double, PointF> curve;

            public RenderComponents(Renderer renderer)
            {
                this.renderer = renderer;
            }
        }

        private static class StaticFields
        {
            public static readonly Pen widePen = new Pen(Brushes.Black)
            {
                Width = 4.0F,
                LineJoin = LineJoin.Bevel
            };
            public const int INITIAL_POINT_DENSITY = 10000;

            public static int RefreshDelay = 100;
        }

        private GroupBox ParamsGroupBox;
        private FlowLayoutPanel ParameterFlowLayoutPanel;
        private readonly Dictionary<CycloParams, Func<object>> getterCalls;
        private readonly Dictionary<CycloParams, Action<object>> setterCalls;
        private readonly Dictionary<CycloParams, Func<bool>> availabilityCalls;
        private Button button1;
        private Panel DisplayPanel;
        private readonly GeometryModel model;
        private readonly RenderComponents renderComponents;
        

        public GeometryMenu(InitialParameters initialParameters)
        {
            this.model = initialParameters.model;
            InitializeComponent();

            ParameterFlowLayoutPanel.Controls.AddRange
                (initialParameters.parameterControls.ToArray());

            this.ParamsGroupBox.Width = initialParameters.PARAMBOX_WIDTH;
            this.getterCalls = initialParameters.getterCalls;
            this.setterCalls = initialParameters.setterCalls;
            this.availabilityCalls = initialParameters.availabilityCalls;
            renderComponents = new RenderComponents
                (new GeometryRenderer(DisplayPanel.CreateGraphics()));
            var menu = this;
            Thread t = new Thread(() =>
            {
                while(true)
                {
                    Thread.Sleep(StaticFields.RefreshDelay);
                    var renderer = Renderer;
                    if (renderer is null) { }
                    else
                    {
                        renderer.Display();
                    }
                }
            });
            Disposed += new EventHandler((sender, e) =>
            {
                t.Abort();
            });
            SizeChanged += new EventHandler((sender, e) =>
            {
                SetCurve();
            });
            t.Start();
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
            this.ParamsGroupBox.Size = new System.Drawing.Size(214, 2500);
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
            this.ParameterFlowLayoutPanel.Size = new System.Drawing.Size(208, 2481);
            this.ParameterFlowLayoutPanel.TabIndex = 2;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(214, 0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(2286, 2500);
            this.DisplayPanel.TabIndex = 1;
            // 
            // GeometryMenu
            // 
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.ParamsGroupBox);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(2500, 2500);
            this.ParamsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Renderer Renderer
        {
            get
            {
                renderComponents.mutex.WaitOne();
                Renderer ret = null;
                if (!renderComponents.graphicsRendered)
                {
                    ret = renderComponents.renderer;
                    renderComponents.graphicsRendered = true;
                }
                renderComponents.mutex.ReleaseMutex();
                return ret;
            }
        }

        public void SetCurve(Func<double, PointF> curve = null)
        {
            renderComponents.mutex.WaitOne();
            renderComponents.renderer.Reset();
            if(curve is null)
            {
                if(!(renderComponents.curve is null))
                    renderComponents.renderer.AddCentralCurve
                        (renderComponents.curve, DisplayPanel.Width, DisplayPanel.Height);
            }
            else
            {
                renderComponents.renderer.AddCentralCurve
                    (curve, DisplayPanel.Width, DisplayPanel.Height);
                renderComponents.curve = curve;
            }
            renderComponents.graphicsRendered = false;
            renderComponents.mutex.ReleaseMutex();
        }

        public object Get(CycloParams param) => 
            IsAvailable(param) ? getterCalls[param]() : null;

        public void Set(CycloParams param, object val) => setterCalls[param](val);

        public bool IsAvailable(CycloParams param) => 
            availabilityCalls.ContainsKey(param) ? availabilityCalls[param]() : true;

        private void button1_Click(object sender, EventArgs e)
        {
            model.Act();
        }
    }
}
