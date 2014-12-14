using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptLab.Func;

namespace OptLab.Graph
{
    public partial class ContourPlot : UserControl
    {
        private struct Point3F
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; private set; }
            public Point3F(float x, float y, float z) : this()
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }
        private string fName = "f(x,y) = 10*x*sin(y) + 10*y*cos(x)"; 
        private Rectangle _area = new Rectangle(-4, -4, 8, 8);
        private Func<double, double, double> _func = new Func<double, double, double>((x, y) => 10 * x * Math.Sin(y) + 10 * y * Math.Cos(x));
        private int _padding = 20;
        private int _step = 5;
        private int _numLevels = 25;
        private bool _drawGrid = false;
        private bool _drawLines = true;
        private bool _drawColor = false;
        private bool _drawSteps = false;
        private bool _drawArea = false;
        private bool _draeAreaR = false;
        private Dictionary<int, Function> _h = null;
        private Dictionary<int, Function> _l = null;
        public Rectangle Area
        {
            get { return _area; }
            set { _area = value; Invalidate(); }
        }
        public void SetFunction(Func<double, double, double> value, string name)
        {
            _func = value; Invalidate();
            fName = "f(x,y) = " + name;
        }
        public bool GridOn
        {
            get { return _drawGrid; }
            set { _drawGrid = value; Invalidate(); }
        }
        public bool LineOn
        {
            get { return _drawLines; }
            set { _drawLines = value; Invalidate(); }
        }
        public bool ColorOn
        {
            get { return _drawColor; }
            set { _drawColor = value; Invalidate(); }
        }
        public int Levels
        {
            get { return _numLevels; }
            set { _numLevels = value; Invalidate(); }
        }
        public int StepSize
        {
            get { return _step; }
            set { _step = value; Invalidate(); }
        }
        public bool DrawSteps
        {
            get { return _drawSteps; }
            set { _drawSteps = value; Invalidate(); }
        }
        public int Offset
        {
            get { return _padding; }
            set { _padding = value; Invalidate(); }
        }

        private List<Vector> _listOfPoints = null;
        public List<Vector> ListOfPoints
        {
            get { return _listOfPoints; }
            set { _listOfPoints = value; }
        }

        public Dictionary<int, Function> H
        {
            get { return _h; }
            set { _h = value; }
        }
        public Dictionary<int, Function> L
        {
            get { return _l; }
            set { _l = value; }
        }

        public bool DrawAreaL
        {
            get { return _drawArea; }
            set { _drawArea = value; Invalidate(); }
        }
        public bool DraeAreaH
        {
            get { return _draeAreaR; }
            set { _draeAreaR = value; Invalidate(); }
        }
    
        private void DrawColorPallete(Graphics g, float zmin, float zmax)
        {
            var pta = new PointF[4];
            int size = (int)((this.ClientRectangle.Height - _padding * 2) * 1f);
            ControlPaint.DrawBorder(g, new Rectangle((int)(this.ClientRectangle.Width - _padding * 0.8f), _padding, (int)(_padding * 0.6f), size + 1),
                SystemColors.ControlDarkDark, ButtonBorderStyle.Solid);
            for (int i = 0; i < size; i++)
            {
                pta[0] = new PointF(this.ClientRectangle.Width - _padding * 0.8f, this.ClientRectangle.Height - i - _padding + 1);
                pta[1] = new PointF(this.ClientRectangle.Width - _padding * 0.3f, this.ClientRectangle.Height - i - _padding + 1);
                pta[2] = new PointF(this.ClientRectangle.Width - _padding * 0.8f, this.ClientRectangle.Height - i - _padding);
                pta[3] = new PointF(this.ClientRectangle.Width - _padding * 0.3f, this.ClientRectangle.Height - i - _padding);
                using (var aBrush = new SolidBrush(Color.FromArgb(200, GetColor((float)(i * (zmax - zmin) / size + zmin), zmin, zmax))))
                { g.FillPolygon(aBrush, pta); }
            }
        }
        private void DrawContour(Graphics g, Point3F[,] pts, float zmin, float zmax, int ncount)
        {
            using (var aPen = new Pen(Color.DimGray) { Width = 1f })
            {
                var pta = new PointF[2];
                var zlevels = new float[ncount];
                for (int i = 0; i < ncount; i++) zlevels[i] = zmin + i * (zmax - zmin) / (ncount - 1);
                int i0, i1, i2, j0, j1, j2;
                float zratio = 1;
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        for (int k = 0; k < ncount; k++)
                        {
                            // Left triangle:
                            i0 = i;
                            j0 = j;
                            i1 = i;
                            j1 = j + 1;
                            i2 = i + 1;
                            j2 = j + 1;
                            if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                 zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                 zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z) &&
                                     (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF((1 - zratio) * pts[i0, j0].X + zratio * pts[i2, j2].X,
                                               (1 - zratio) * pts[i0, j0].Y + zratio * pts[i2, j2].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                     (zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y)
                                    ;
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[1] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                               pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            } // right triangle:
                            i0 = i;
                            j0 = j;
                            i1 = i + 1;
                            j1 = j;
                            i2 = i + 1;
                            j2 = j + 1;
                            if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                 zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                 zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z) &&
                                     (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                               pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                     (zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y);
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[1] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                               pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                        }
                    }
                }
            }

        }
        private static Color GetColor(float value, float maxValue, float minValue)
        {
            int int_value = 1023 - (int)(1023 * (value - maxValue) / (minValue - maxValue));

            if (int_value < 256)
            {
                // Red to yellow. (255, 0, 0) to (255, 255, 0).
                return Color.FromArgb(255, int_value, 0);
            }
            else if (int_value < 512)
            {
                // Yellow to green. (255, 255, 0) to (0, 255, 0).
                int_value -= 256;
                return Color.FromArgb(255 - int_value, 255, 0);
            }
            else if (int_value < 768)
            {
                // Green to aqua. (0, 255, 0) to (0, 255, 255).
                int_value -= 512;
                return Color.FromArgb(0, 255, int_value);
            }
            else
            {
                // Aqua to blue. (0, 255, 255) to (0, 0, 255).
                int_value -= 768;
                return Color.FromArgb(0, 255 - int_value, 255);
            }
        }
        private static void DrawColor(Graphics g, Point3F[,] pts, float zmin, float zmax)
        {
            var pta = new PointF[4];
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    pta[0] = new PointF(pts[i, j].X, pts[i, j].Y);
                    pta[1] = new PointF(pts[i, j + 1].X, pts[i, j + 1].Y);
                    pta[2] = new PointF(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y);
                    pta[3] = new PointF(pts[i + 1, j].X, pts[i + 1, j].Y);
                    using (var aBrush = new SolidBrush(Color.FromArgb(200, GetColor(pts[i, j].Z, zmin, zmax))))
                    { g.FillPolygon(aBrush, pta); }
                }
        }
        private void DrawDrid(Graphics g,int n,int m, Rectangle rect)
        {
            float stepx = (float)n / _area.Width;
            float stepy = (float)m / _area.Height;

            float[] dashValues = { 3, 3 };
            var pen = new Pen(SystemColors.ControlDark, 0.5f) { DashPattern = dashValues };
            var penXY = new Pen(SystemColors.ControlDarkDark, 1f) { DashPattern = dashValues };
            var t = Math.Min(_area.Width, _area.Height) < 5 ? 0.5f : 1f;
            for (float i = _area.X; i < (_area.Width + _area.X); i+=t)
                g.DrawLine(i == 0 ? penXY : pen, (i - _area.X) * stepx * _step, 
                    rect.Height, (i - _area.X) * stepx * _step, 0);
            for (float j = _area.Y; j < (_area.Height + _area.Y); j+=t)
                g.DrawLine(j == 0 ? penXY : pen, rect.Width, (j - _area.Y) * stepy * _step, 
                    0, (j - _area.Y) * stepy * _step);
 
        }
        private void DrawPoints(Graphics g, int n, int m, Rectangle rect)
        {
            const float size = 5; var font = new Font(this.Font.FontFamily, 7f);
            float stepx = (float)n / _area.Width;
            float stepy = (float)m / _area.Height;
            Vector v = _listOfPoints.First();
            g.Clip = new Region(new Rectangle(0, 0, rect.Width, rect.Height));
            for (int i = 1; i < _listOfPoints.Count; i++)
            {
                float x = (float)((v.X - _area.X) * stepx * _step);
                float y = (float)((v.Y - _area.Y) * stepy * _step);
                v = _listOfPoints[i];
                g.DrawLine(new Pen(Color.Black, 1f), (float)((v.X - _area.X) * stepx * _step), (float)((v.Y - _area.Y) * stepy * _step), x, y);
            }
            g.FillEllipse(new SolidBrush(Color.Green), (float)((v.X - _area.X) * stepx * _step - size / 2), (float)((v.Y - _area.Y) * stepy * _step - size / 2), size, size);
            v = _listOfPoints.First();
            g.FillEllipse(new SolidBrush(Color.Red), (float)((v.X - _area.X) * stepx * _step - size / 2), (float)((v.Y - _area.Y) * stepy * _step - size / 2), size, size);
            g.ResetClip();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var r = ClientRectangle;
            var graphics = e.Graphics;
            r.Inflate(-_padding, -_padding);
            Draw(graphics,r);
            graphics.DrawRectangle(new Pen(SystemColors.ControlDarkDark), r); 
            if (_drawColor) DrawColorPallete(graphics,1,10);
            float stepx = (float)(ClientRectangle.Width - _padding*2) / _area.Width;
            float stepy = (float)(ClientRectangle.Height - _padding*2) / _area.Height;
            var font = new Font(Font.FontFamily, 6f);
            for (int i = 0; i <= _area.Width; i++)
                graphics.DrawString((i+_area.X).ToString("+#;-#;0"), font, Brushes.Black,
                                    _padding + i * stepx, ClientRectangle.Height - _padding * 0.75f);
            for (int i = 0; i <= _area.Height; i++)
                graphics.DrawString((i+_area.Y).ToString("+#;-#;0"), font, Brushes.Black,
                                    _padding * 0.25f - Font.Size * 0.5f , ClientRectangle.Height - _padding - i * stepy);
            graphics.DrawString(fName, font, Brushes.Black, _padding, _padding*0.2f);
            Cursor.Current = Cursors.Default;
        }
        private void Draw(Graphics graphics, Rectangle area)
        {
            
            graphics.ScaleTransform(1.0F, -1.0F);
            graphics.TranslateTransform(area.X, -area.Height-area.Y);

            if (_func != null && Enabled)
            {
                float min = Single.MaxValue;
                float max = Single.MinValue;

                int n = area.Width / _step + 1;
                int m = area.Height / _step + 1;

                var pts = new Point3F[n,m];
                

                float xoff = _area.X;
                float yoff = _area.Y;
                float xreal = _area.Width;
                float yreal = _area.Height;

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        var pt = new Point3F(i*_step, j*_step,
                                            (float) _func(xoff + i*xreal/n, yoff + j*yreal/m));
                        
                        if (i == n - 1) pt.X = area.Width;
                        if (j == m - 1) pt.Y = area.Height;

                        pts[i, j] = pt;
                        
                        if (pts[i, j].Z > max) max = pts[i, j].Z;
                        if (pts[i, j].Z < min) min = pts[i, j].Z;
                    }

                if (DrawAreaL && _l != null)
                {
                    var pas = new Point3F[n, m];
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                        {
                            var pa = new Point3F(i * _step, j * _step, (float)InArea(xoff + i * xreal / n, yoff + j * yreal / m));
                            pas[i, j] = pa;
                        }
                    DrawArea(graphics, pas);
                }
                if (DraeAreaH && _h!=null) DrawFunc(graphics, area);
                if (_drawLines) DrawContour(graphics, pts, min, max, _numLevels);
                if (_drawColor) DrawColor(graphics, pts, min, max);
                if (_drawSteps && _listOfPoints != null) DrawPoints(graphics,n,m,area);
                if (_drawGrid) DrawDrid(graphics, n, m, area);
            
                graphics.ResetTransform();
            }
        }

        private void DrawFunc(Graphics g, Rectangle r)
        {
            g.Clip = new Region(new Rectangle(0, 0, r.Width, r.Height));
            for (int i = r.X; i < (r.X + r.Width); i++)
                for (int j = r.Y; j < (r.Y + r.Height); j++)
                {
                    float ty = _area.Y + (float)((j - 0) * _area.Height) / r.Height;
                    float tx = _area.X + (float)((i - 0) * _area.Width) / r.Width;
                    if (InAreaR(tx, ty)) g.FillEllipse(new SolidBrush(Color.Black), i - 1, j - 1, 3, 3);
                }
            g.ResetClip();
        }

        private bool InAreaR(float x, float y)
        {
            return H.Any(h => Math.Abs(h.Value.Eval(x, y)) < 0.001d);
        }

        private double InArea(float x, float y)
        {
            if (L.All(l => l.Value.Eval(x, y) < 0)) return -1;
            else return 1;
        }

        private void DrawArea(Graphics g, Point3F[,] pts)
        {
            var pta = new PointF[4];
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    g.FillEllipse(new SolidBrush((pts[i,j].Z<0)? Color.Black : Color.Transparent),pts[i,j].X,pts[i,j].Y,3,3);
        }

        public ContourPlot()
        {
            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);

            InitializeComponent();
        }
        private void ContourPlot_Load(object sender, EventArgs e)
        {
            
        }
    }
}
