using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BCC.Menus.Geometry;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BCC.Menus.Main
{
    public partial class GeometryMenu : UserControl
    {

        private const int GIC_VOFFSET = 60;
        private const int GIC_HOFFSET = 310;
        Model model;
        private List<string> intParameters = new List<string>();
        private List<string> floatParameters = new List<string>();
        private List<string> resultParameters = new List<string>();
        private bool isEpicycloid = true;
        private readonly UserControl parameterPanel;
        private readonly UserControl resultPanel;
        private readonly UserControl cycloDisplay;
        Graphics formGraphics;
        private readonly Random rnd = new Random();
        private readonly Pen widePen = new Pen(Brushes.Black)
        {
            Width = 4.0F,
            LineJoin = LineJoin.Bevel
        };

        public List<string> IntParameters { get => intParameters; set => intParameters = value; }
        public List<string> FloatParameters { get => floatParameters; set => floatParameters = value; }
        public List<string> ResultParameters { get => resultParameters; set => resultParameters = value; }

        public GeometryMenu(Model model)
        {
            
            this.model = model;
            model.GeometryMenu = this;
            Point GICanchor = new Point(0, 0);
            IntParameters = model.IntegerGeometryParameters;
            FloatParameters = model.FloatGeometryParameters;
            resultParameters = model.ResultGeometryParameters;
            InitializeComponent();
            parameterPanel = new UserControl()
            {
                Name = "ParameterPanel",
                Width = 630,
                Height = 260,
                Location = new Point(0, 60),
                AutoSize = true,
                Visible = true
            };
            resultPanel = new UserControl()
            {
                Name = "ResultPanel",
                Width = 630,
                Height = 260,
                Location = new Point(0, parameterPanel.Height + 60),
                AutoSize = true,
                Visible = true
            };
            cycloDisplay = new UserControl()
            {
                Name = "CycloDisplay",
                Width = 300,
                Height = Width,
                Location = new Point(parameterPanel.Width, DebugTextBox.Height + 50),
                AutoSize = true,
                Visible = true
            };
            Controls.Add(parameterPanel);
            parameterPanel.Show();
            Controls.Add(resultPanel);
            resultPanel.Show();
            Controls.Add(cycloDisplay);
            cycloDisplay.Show();
            formGraphics = cycloDisplay.CreateGraphics();
            foreach (string parameter in IntParameters)
            {
                IntegerGeometryInputControl control = new IntegerGeometryInputControl(parameter,this)
                {
                    Name = parameter + "GeometryInputControl",
                    AutoSize = true,
                    Visible = true
                };
                parameterPanel.Controls.Add(control);
                control.Location = GICanchor;
                GICanchor.Y += GIC_VOFFSET;
                if (GICanchor.Y > Size.Height)
                {
                    GICanchor.Y = 0;
                    GICanchor.X += GIC_HOFFSET;
                }
            }
            foreach (string parameter in FloatParameters)
            {
                FloatGeometryInputControl control = new FloatGeometryInputControl(parameter,this)
                {
                    Name = parameter + "GeometryInputControl",
                    AutoSize = true,
                    Visible = true
                };
                parameterPanel.Controls.Add(control);
                control.Location = GICanchor;
                GICanchor.Y += GIC_VOFFSET;
                if (GICanchor.Y > parameterPanel.Size.Height - GIC_VOFFSET)
                {
                    GICanchor.Y = 0;
                    GICanchor.X += GIC_HOFFSET;
                }
            }
            if (GICanchor.X > parameterPanel.Width - GIC_HOFFSET + 1)
            {
                ScrollBar hScrollBar = new HScrollBar
                {
                    Dock = DockStyle.Bottom
                };
                hScrollBar.Scroll += (sender, e) => { this.VerticalScroll.Value = hScrollBar.Value; };
                parameterPanel.Controls.Add(hScrollBar);
            }
            GICanchor = new Point(0, 0);
            foreach (string parameter in ResultParameters)
            {
                ResultControl control = new ResultControl(parameter)
                {
                    Name = parameter + "ResultControl",
                    AutoSize = true,
                    Visible = true
                };
                resultPanel.Controls.Add(control);
                control.Location = GICanchor;
                GICanchor.Y += GIC_VOFFSET;
                if(GICanchor.Y > resultPanel.Size.Height - GIC_VOFFSET)
                {
                    GICanchor.Y = 0;
                    GICanchor.X += GIC_HOFFSET;
                }
            }
            if (GICanchor.X > resultPanel.Size.Width - GIC_HOFFSET)
            {
                ScrollBar hScrollBar = new HScrollBar
                {
                    Dock = DockStyle.Bottom
                };
                hScrollBar.Scroll += (sender, e) => { this.VerticalScroll.Value = hScrollBar.Value; };
                resultPanel.Controls.Add(hScrollBar);
            }
        }

        internal void ShowResults(Dictionary<string, double> results)
        {
            foreach(ControlCollection cc in new List<ControlCollection>() { parameterPanel.Controls, resultPanel.Controls})
            {
                foreach (UserControl control in cc)
                {
                    switch (control)
                    {
                        case FloatGeometryInputControl fgic:
                            fgic.Value(results[fgic.parameterName]);
                            break;
                        case IntegerGeometryInputControl igic:
                            igic.Value(results[igic.parameterName]);
                            break;
                        case ResultControl rc:
                            rc.Value(results[rc.parameterName]);
                            break;
                    }
                }
            }
            
        }

        internal void Compute()
        {
            Dictionary<string, double> parameters = new Dictionary<string, double>();
            foreach(KeyValuePair<string,double> parameter in FloatValues())
            {
                parameters.Add(parameter.Key, parameter.Value);
            }
            foreach(KeyValuePair<string,int> parameter in IntValues())
            {
                parameters.Add(parameter.Key, parameter.Value);
            }
            model.ComputeGeometry(parameters, isEpicycloid);
        }

        public void SetModel(Model model) => this.model = model;

        private void GeometryMenu_Load(object sender, EventArgs e)
        {

        }

        private Dictionary<string,int> IntValues()
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();
            foreach(Control control in parameterPanel.Controls)
            {
                if(control is IntegerGeometryInputControl GIC)
                {
                    if(GIC.Available())
                    {
                        ret.Add(GIC.parameterName, GIC.GetValue());
                    }
                }
            }
            return ret;
        }

        private Dictionary<string,double> FloatValues()
        {
            Dictionary<string, double> ret = new Dictionary<string, double>();
            foreach(Control control in parameterPanel.Controls)
            {
                if(control is FloatGeometryInputControl GIC)
                {
                    if(GIC.Available())
                    {
                        ret.Add(GIC.parameterName, GIC.Value());
                    }
                }
            }
            return ret;
        }

        private void EpicycloidButton_Click(object sender, EventArgs e)
        {
            isEpicycloid = !isEpicycloid;
            if (!isEpicycloid) EpicycloidButton.Text = "hipocycloid";
            else EpicycloidButton.Text = "epicycloid";
            ComputationButtonAction();
        }

        private void ComputionButton_Click(object sender, EventArgs e)
        {
            ComputationButtonAction();
        }

        public void ComputationButtonAction()
        {
            try
            {
                Compute();
            }
            catch (Exception exception)
            {
                DebugTextBox.Text = exception.Message;
                return;
            }
            DebugTextBox.Text = "Computation succeded";
            Func<double, Tuple<double, double>> outline = model.Outline;
            //formGraphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 2000, 3000));
            int quality = 1000;
            PointF[] cycloid = new PointF[quality];
            for (int i = 0; i < quality; i++)
            {
                Tuple<double, double> point = outline(2 * Math.PI * i / quality);
                float x = (float)((point.Item1 * 0.7 + 0.5) * cycloDisplay.Width);
                float y = (float)((point.Item2 * 0.7 + 0.5) * cycloDisplay.Width);
                cycloid[i] = new PointF(x, y);
            }
            cycloDisplay.Refresh();
            Bitmap bitMap = new Bitmap(cycloDisplay.Width, cycloDisplay.Width, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitMap);
            formGraphics.Clear(Color.White);
            graphics.Clear(Color.White);
            formGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, cycloDisplay.Width, cycloDisplay.Width));
            
            formGraphics.DrawClosedCurve(widePen, cycloid);
            graphics.DrawClosedCurve(widePen, cycloid);
            var name = "" + rnd.Next();
            bitMap.Save(@"D:\Desktop\studia\Automatyka i robotyka\Praca dyplomowa\" + name + ".bmp", ImageFormat.Bmp);
        }
    }
}
