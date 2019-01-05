using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Geometry
{
    abstract class Renderer
    {
        protected const int DEFAULT_RESOLUTION = 5000;
        private Action render;
        protected readonly Graphics graphics;

        public Renderer(Graphics graphics)
        {
            this.graphics = graphics;
            this.Reset();
        }

        protected Action Render { get => render; set => render = value; }
        public void Display() => render();
        public void Reset() => render = () => 
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 
                graphics.ClipBounds.Width, graphics.ClipBounds.Height);
        };

        public abstract void AddCentralCurve(Func<double, PointF> curve, int width, int height, int resolution = DEFAULT_RESOLUTION);
    }

    class CycloidGeometryRenderer : Renderer
    {
        public static class StaticFields
        {
            public static readonly Pen widePen = new Pen(Brushes.White)
            {
                Width = 2.0F,
                LineJoin = LineJoin.Bevel
            };
            private const int PRIMES_BOUND = 30;
            public static readonly int[] PRIMES =
                (from i in Enumerable.Range(2, PRIMES_BOUND).AsParallel()
                 where Enumerable.Range(2, (int)Math.Sqrt(i)).All(j => i % j != 0)
                 select i).ToArray();
            public const double BOUND_MARGIN = 1.3;
        }
        

        public CycloidGeometryRenderer(Graphics graphics) : base(graphics)
        {
        }
        public override void AddCentralCurve(Func<double,PointF> curve, int width, int height, int resolution = DEFAULT_RESOLUTION)
        {
            var former = Render;
            Render = () =>
            {
                double distance(PointF p) => Math.Sqrt(p.X * p.X + p.Y * p.Y);
                var bound = distance(curve(0)) * StaticFields.BOUND_MARGIN;
                foreach (var prime in StaticFields.PRIMES)
                {
                    var dt = 2.0 * Math.PI / prime;
                    for (int i = 1; i < prime; i++)
                    {
                        var temp = distance(curve(dt * i));
                        if (temp > bound) bound = temp;
                    }
                }
                var box = width > height ? height : width;
                var factor = 0.5 * box / bound;
                var x0 = width / 2;
                var y0 = height / 2;
                var curvePoints = new List<PointF>();
                for (int i = 0; i < resolution; i++)
                {
                    var t = 2.0 * Math.PI * i / resolution;
                    var x = (float)(x0 + curve(t).X * factor);
                    var y = (float)(y0 + curve(t).Y * factor);
                    curvePoints.Add(new PointF(x, y));
                }
                former();
                graphics.DrawClosedCurve(StaticFields.widePen, curvePoints.ToArray());
            };
            
        }
    }
}
