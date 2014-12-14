using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OptLab.Func
{
    public class GradientTaskTmp
    {
        protected Function _Q = new Function("x^2+(1/3)*y^2-x*y");
        protected VectorFunction _dQ = new VectorFunction("2*x-y", "(2*y)/3-x");
        protected Vector _startPoint = new Vector(6f, 8f);
        internal Function GetFunc()
        {
            return _Q;
        }
        protected List<Vector> listOfPoints = new List<Vector>();
        public List<Vector> ListOfPoints
        {
            get { return listOfPoints; }
        }
        protected double _mu = 0.001F;
        public double Mu
        {
            get { return _mu; }
            set { _mu = value>0?value:_mu; }
        }
        protected double _eta = 0.1F;
        public double Eta
        {
            get { return _eta; }
            set { _eta = value>=_mu?value:_eta; }
        }
        protected double _delta = 0.001F;
        public double Delta
        {
            get { return _delta; }
            set { _delta = value>0?value:_delta; }
        }
        protected double _sigma = 0.1F;
        public double Sigma
        {
            get { return _sigma; }
            set { _sigma = value>0?value:_sigma; }
        }
        protected int _maxLoops = 500;

        public GradientTaskTmp()
        {
            Name = "TaskName";
        }
        public GradientTaskTmp(GradientTaskTmp taskTmp)
        {
            _Q.SetFunc(taskTmp.Q);
            _dQ.SetFunc(taskTmp.dxQ,taskTmp.dyQ);
            _startPoint.X = taskTmp.X;
            _startPoint.Y = taskTmp.Y;

            _delta = taskTmp.Delta;
            _eta = taskTmp.Eta;
            _mu = taskTmp.Mu;
            _sigma = taskTmp.Sigma;
        }

        public int MaxLoops
        {
            get { return _maxLoops; }
            set { _maxLoops = value>200?value:_maxLoops; }
        }
        public double X
        {
            get { return _startPoint.X; }
        }
        public double Y
        {
            get { return _startPoint.Y; }
        }
        public string Q
        {
            get { return _Q.ToString(); }
            set { _Q.SetFunc(value); }
        }
        public string dxQ
        {
            get { return _dQ.FuncX; }
            set { _dQ.SetFunc(value,_dQ.FuncY); }
        }
        public string dyQ
        {
            get { return _dQ.FuncY; }
            set { _dQ.SetFunc(_dQ.FuncX,value); }
        }
        public string StartPoint
        {
            get { return _startPoint.ToString();  }
        }

        public string Name { get; set; }

        public bool ChangeSP(double x, double y)
        {
            try
            {
                _Q.Eval(x, y);
                _dQ.Eval(x, y);
            }
            catch (Exception e)
            {
                return false;
            }
            _startPoint.X = x;
            _startPoint.Y = y;
            return true;
        }

        private double GetT(Vector xk, Vector dk)
        {
            double d = _delta;
            double ts = 0;
            double fs = _Q.Eval(xk + ts * dk);
            int count = 0;
            double T;
            while (true)
            {
                double tn = ts + d;
                double fn = _Q.Eval(xk + tn * dk);
                d = 2*d;
                if (fn > fs) { T = tn;  break; }
                else { ts = tn; fs = fn;}

                if (count++ == _maxLoops)
                {
                    EndlessLoop("GetT");
                    count = 0;
                }
            }
            return T;
        }

        private bool InP1(double t, Vector xk, Vector dk)
        {
            if (_Q.Eval(xk + t*dk) - _Q.Eval(xk) - _mu*t*(_dQ.Eval(xk)*dk) < 0) return true;
            return false;
        }
        private bool InP2(double t, Vector xk, Vector dk)
        {
            if (Math.Abs(_dQ.Eval(xk + t*dk)*dk) - _eta*Math.Abs(_dQ.Eval(xk)*dk) < 0) return true;
            return false;
        }

        private double GoldenSearch(double ta, double tb, Vector xk, Vector dk)
        {
            double fi = (Math.Sqrt(5f) - 1) / 2;
            double a = ta;
            double b = tb;
            double tx = b - (b - a) * fi;
            double ty = a + (b - a) * fi;
            int count = 0;
            while (true)
            {
                if (_Q.Eval(xk + tx*dk) < _Q.Eval(xk + ty*dk))
                {
                    b = ty;
                    ty = tx;
                    tx = b - (b - a)*fi;
                }
                else
                {
                    a = tx;
                    tx = ty;
                    ty = a + (b - a)*fi;
                }
                double tt = (a + b) / 2;
                if (InP1(tt, xk, dk) && InP2(tt, xk, dk)) return tt;
                if (count++ == _maxLoops)
                {
                    EndlessLoop("GoldenSearch");
                    count = 0;
                }
            }

        }

        private void EndlessLoop(string info)
        {
            DialogResult dialogResult = MessageBox.Show(@"Число итераций в одном из внутренних циклов достигло " + _maxLoops + @"! Продолжить?", 
                                            @"Бесконечный цикл", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                throw new Exception("Endless Loop in" + info);
            }
        }

        public virtual string OutInfo()
        {
            listOfPoints.Add(_startPoint);
            string text = "\n\nQ(x,y)  = " + _Q.ToString() + "\n ∂Q(x,y) = " + _dQ.ToString();
            text = text + "\n∆ = " + _delta.ToString("0.00000") + " η = " + _eta.ToString("0.00000") + " μ = " + _mu.ToString("0.00000");
            return text;
        }
        public string StartSearch()
        {
            Vector x = _startPoint;
            int k = 1;
            string text = "";
            while (_dQ.Eval(x).Norm > _sigma)
            {

                text += "\n\n" + k + ") " + x.ToString("0.00000") + " ||∂Q(x)|| = " + _dQ.Eval(x).Norm.ToString("0.00000");
                Vector d = -_dQ.Eval(x);
                double T = GetT(x, d);
                double t = GoldenSearch(0, T, x, d);
                text += "\n t = [0;" + T.ToString("0.00000") + "] -> " + t.ToString("0.00000");
                x = x + t*d;
                listOfPoints.Add(x);
                k++;
            }
            text += "\n\n" + k + ") " + x.ToString("0.00000") + " ||∂Q(x)|| = " + _dQ.Eval(x).Norm.ToString("0.00000");
            return text;
        }

        public void ResetPoints()
        {
            listOfPoints.Clear();
        }
    }

}
