using BCC.Core;
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
            internal Dictionary<Enum, Func<double>> getterCalls;
            internal Dictionary<Enum, Action<double>> setterCalls;
            internal Dictionary<Enum, Func<bool>> availabilityCalls;
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

        private GroupBox ParametersGroupBox;
        private FlowLayoutPanel ParameterFlowLayoutPanel;
        private readonly Dictionary<Enum, Func<double>> getterCalls;
        private readonly Dictionary<Enum, Action<double>> setterCalls;
        private readonly Dictionary<Enum, Func<bool>> availabilityCalls;
        private Panel DisplayPanel;
        private readonly GeometryModel model;
        private readonly RenderComponents renderComponents;


        public GeometryMenu(InitialParameters initialParameters)
        {
            this.model = initialParameters.model;
            InitializeComponent();

            ParameterFlowLayoutPanel.Controls.AddRange
                (initialParameters.parameterControls.ToArray());

            this.ParametersGroupBox.Width = initialParameters.PARAMBOX_WIDTH;
            this.getterCalls = initialParameters.getterCalls;
            this.setterCalls = initialParameters.setterCalls;
            this.availabilityCalls = initialParameters.availabilityCalls;
            renderComponents = new RenderComponents
                (new CycloidGeometryRenderer(DisplayPanel.CreateGraphics()));
            var menu = this;
            Thread t = new Thread(() =>
            {
                while (true)
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
            this.ParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.ParameterFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.ParametersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParametersGroupBox
            // 
            this.ParametersGroupBox.Controls.Add(this.ParameterFlowLayoutPanel);
            this.ParametersGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParametersGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ParametersGroupBox.Name = "ParametersGroupBox";
            this.ParametersGroupBox.Size = new System.Drawing.Size(214, 954);
            this.ParametersGroupBox.TabIndex = 0;
            this.ParametersGroupBox.TabStop = false;
            // 
            // ParameterFlowLayoutPanel
            // 
            this.ParameterFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParameterFlowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.ParameterFlowLayoutPanel.Name = "ParameterFlowLayoutPanel";
            this.ParameterFlowLayoutPanel.Size = new System.Drawing.Size(208, 935);
            this.ParameterFlowLayoutPanel.TabIndex = 2;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(214, 0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(2286, 954);
            this.DisplayPanel.TabIndex = 1;
            // 
            // GeometryMenu
            // 
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.ParametersGroupBox);
            this.Name = "GeometryMenu";
            this.Size = new System.Drawing.Size(2500, 954);
            this.ParametersGroupBox.ResumeLayout(false);
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
            if (curve is null)
            {
                if (!(renderComponents.curve is null))
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

        public double Get(Enum param) =>
            IsAvailable(param) ? getterCalls[param]() : Model.NULL;
        public void Set(Enum param, double val)
        {
            if (setterCalls.ContainsKey(param)) setterCalls[param](val);
        }
            
        public bool IsAvailable(Enum param) => 
            availabilityCalls.ContainsKey(param) ? availabilityCalls[param]() : true;
    }
}
