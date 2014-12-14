using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OptLab.Func
{
    class GradientTask
    {
        public string Name { get; set; }
        
        private Function _Q;
        private VectorFunction _dQ;
        private Vector _startPoint;
        
        private List<Vector> _points = new List<Vector>();
        private double _mu = 0.0001d;
        private double _eta = 0.01d;
        private double _delta = 0.0001d;
        private double _sigma = 0.0001d;
        private int _maxLoops = 500;
        private string _info = "";
        
        public double Mu
        {
            get { return _mu; }
            set { _mu = value>0?value:_mu; }
        }
        public double Eta
        {
            get { return _eta; }
            set { _eta = value>=_mu?value:_eta; }
        }
        public double Delta
        {
            get { return _delta; }
            set { _delta = value>0?value:_delta; }
        }
        public double Sigma
        {
            get { return _sigma; }
            set { _sigma = value>0?value:_sigma; }
        }
        public List<Vector> Points
        {
            get { return _points; }
        }
        public int MaxLoops
        {
            get { return _maxLoops; }
            set { _maxLoops = value > 200 ? value : _maxLoops; }
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
            set { _dQ.SetFunc(value, _dQ.FuncY); }
        }
        public string dyQ
        {
            get { return _dQ.FuncY; }
            set { _dQ.SetFunc(_dQ.FuncX, value); }
        }
        public string StartPoint
        {
            get { return _startPoint.ToString(); }
        }


        public GradientTask(string func, string dxFunc, string dyFunc, Vector startPoint)
        {
            Name = "TaskName";
            _Q = new Function(func);
            _dQ = new VectorFunction(dxFunc,dyFunc);
            _startPoint = new Vector(startPoint);
        }

        public Function GetFunc()
        {
            return _Q;
        }
        public VectorFunction GetDFunc()
        {
            return _dQ;
        }
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
        public void SetFunc(Function f, VectorFunction df)
        {
            _Q = f;
            _dQ = df;
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
                    //EndlessLoop("GoldenSearch");
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

        public Vector StartSearch()
        {
            _info = "\nSearch started:\n\tstart = " + _startPoint;
            Vector x = _startPoint;
            _points.Add(x);
            int k = 1;
            while (_dQ.Eval(x).Norm > _sigma)
            {

                _info += "\n\n" + k + ") " + x + " ||∂Q(x)|| = " + _dQ.Eval(x).Norm;
                Vector d = -_dQ.Eval(x);
                double T = GetT(x, d);
                double t = GoldenSearch(0, T, x, d);
                _info += "\n t = [0;" + T + "] -> " + t;
                x = x + t * d;
                _points.Add(x);
                k++;
                if (k == _maxLoops) break;
            }
            _info += "\n\n" + k + ") " + x.ToString("0.00000") + " ||∂Q(x)|| = " + _dQ.Eval(x).Norm;
            _info += "\nFinished:\n\tfinish = " + x;
            return x;
        }

        public string OutInfo()
        {
            string text = Name + "\nQ(x,y)  = " + _Q  + _dQ;
            text = text + "\nParams:\n\t∆ = " + _delta + "\n\tη = " + _eta + "\n\tμ = " + _mu;
            return text;
        }
        public string GetSearchInfo()
        {
            return _info;
        }
        public void ResetPoints()
        {
            _points.Clear();
        }
    }
}
