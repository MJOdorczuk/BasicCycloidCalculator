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
        private readonly GeometryModel model;
        private readonly RenderComponents renderComponents;
        private readonly Panel displayPanel;


        public GeometryMenu(InitialParameters initialParameters)
        {
            this.model = initialParameters.model;
            this.displayPanel = new Panel
            {
                BackColor = SystemColors.ActiveCaptionText,
                Dock = DockStyle.Fill,
                Location = new Point(500, 0),
                Size = new Size(2000, 2000),
                TabIndex = 3
            };
            Controls.Add(displayPanel);
            InitializeComponent();

            ParameterFlowLayoutPanel.Controls.AddRange
                (initialParameters.parameterControls.ToArray());

            this.ParametersGroupBox.Width = initialParameters.PARAMBOX_WIDTH;
            this.getterCalls = initialParameters.getterCalls;
            this.setterCalls = initialParameters.setterCalls;
            this.availabilityCalls = initialParameters.availabilityCalls;
            
            renderComponents = new RenderComponents
                (new CycloidGeometryRenderer(displayPanel.CreateGraphics()));
            
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
            ParametersGroupBox = new GroupBox();
            ParameterFlowLayoutPanel = new FlowLayoutPanel();
            ParametersGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ParametersGroupBox
            // 
            ParametersGroupBox.Controls.Add(ParameterFlowLayoutPanel);
            ParametersGroupBox.Dock = DockStyle.Left;
            ParametersGroupBox.Location = new Point(0, 0);
            ParametersGroupBox.Name = "ParametersGroupBox";
            ParametersGroupBox.Size = new Size(214, 954);
            ParametersGroupBox.TabIndex = 0;
            ParametersGroupBox.TabStop = false;
            // 
            // ParameterFlowLayoutPanel
            // 
            ParameterFlowLayoutPanel.Dock = DockStyle.Fill;
            ParameterFlowLayoutPanel.Location = new Point(3, 16);
            ParameterFlowLayoutPanel.Name = "ParameterFlowLayoutPanel";
            ParameterFlowLayoutPanel.Size = new Size(208, 935);
            ParameterFlowLayoutPanel.TabIndex = 2;
            // 
            // GeometryMenu
            // 
            Controls.Add(this.ParametersGroupBox);
            Name = "GeometryMenu";
            Size = new Size(2500, 954);
            ParametersGroupBox.ResumeLayout(false);
            ResumeLayout(false);

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
                        (renderComponents.curve, displayPanel.Width, displayPanel.Height);
            }
            else
            {
                renderComponents.renderer.AddCentralCurve
                    (curve, displayPanel.Width, displayPanel.Height);
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
