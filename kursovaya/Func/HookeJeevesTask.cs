using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OptLab.Func
{
    class HookeJeevesTask
    {
        public string Name { get; set; }

        private Function _Q;
        private Vector _startPoint;
        private List<Vector> _points = new List<Vector>();
        private string _info = "";
        private double _h = 0.0001d;
        private double _sigma = 0.0001d;

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
        }
        public string StartPoint
        {
            get { return _startPoint.ToString();  }
        }
        public double Sigma
        {
            get { return _sigma; }
            set { _sigma = value; }
        }
        public double H
        {
            get { return _h; }
            set { _h = value; }
        }
        public List<Vector> Points
        {
            get { return _points; }
        }

        public HookeJeevesTask(string func, Vector startPoint)
        {
            Name = "TaskName";
            _Q = new Function(func);
            _startPoint = new Vector(startPoint);
        }
        
        public Function GetFunc()
        {
            return _Q;
        }
        public void SetFunc(Function f)
        {
            _Q = f;
        }
        public bool ChangeSP(double x, double y)
        {
            try
            {
                _Q.Eval(x, y);
            }
            catch (Exception e)
            {
                return false;
            }
            _startPoint.X = x;
            _startPoint.Y = y;
            return true;
        }
        public void SetFunc(string func)
        {
            _Q.SetFunc(func);
        }
        public string OutInfo()
        {
            string text = Name + "\n Q(x,y)  = " + _Q;
            text = text + "\nParams: \n\th = " + _h + " \n\tσ = " + _sigma;
            return text;
        }
        
        private Vector Config(Vector v,double h)
        {
            var x = new Vector(v);
            var e = new Vector[2] { new Vector(1d,0d), new Vector(0d,1d) };
            for (int i = 0; i < 2; i++)
                if (_Q.Eval(x + h * e[i]) < _Q.Eval(x)) x = x + h * e[i];
                else if (_Q.Eval(x + h*e[i]) >= _Q.Eval(x) && _Q.Eval(x) > _Q.Eval(x - h*e[i])) x = x - h*e[i];
            return x;
        }
        public Vector StartSearch()
        {
            _info = "\nSearch started:\n\tstart = " + _startPoint;
            double h = _h;
            var x = new Vector(_startPoint);
            var y = new Vector(x);
            int k = 0;
            while (true)
            {
                _points.Add(x);
                var config = Config(y,h);
                if (_Q.Eval(config) < _Q.Eval(x))
                {
                    y = config + 2 * (config - x);
                    x = new Vector(config);
                    k++;
                }
                else if (k > 0)
                {
                    k = 0;
                    y = new Vector(x);
                }
                else if (h < _sigma) break;
                else h = h/2;
                _info += "\n x[k]=" + x.ToString("0.00000") + "\n\tk= " + k + " z=" + config;
            }
            _info += "\nFinished:\n\tfinish = " + x;
            return x;
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

//public Vector GetConfig(Vector z,double h)
//{
//    var config = new Vector(z);
//    var ex = new Vector(1, 0);
//    var ey = new Vector(0, 1);
//    if (_Q.Eval(config + h*ex) < _Q.Eval(config)) config = config + h*ex;
//    else if (_Q.Eval(config - h*ex) < _Q.Eval(config)) config = config - h*ex;
//    if (_Q.Eval(config + h * ey) < _Q.Eval(config)) config = config + h * ey;
//    else if (_Q.Eval(config - h * ey) < _Q.Eval(config)) config = config - h * ey;
//    return config;
//}
//public string StartSearch()
//{
//    _points.Add(_startPoint);
//    string text = "";
//    _x.Add(_startPoint);
//    double h = _h;
//    int k = 0;
//    Vector z = new Vector(_x[k]);
//    while (true)
//    {
//        text += "\n k= " + k + " z=" + z + " x[k]=" + _x[k];
//        _x.Add(GetConfig(z,h));
//        if (_Q.Eval(_x[k + 1]) < _Q.Eval(_x[k]))
//        {
//            k++;
//            z = _x[k] + 2 * (_x[k] - _x[k - 1]);
//        }
//        else
//            if (h < _sigma) break;
//            else
//                if (k > 0)
//                {
//                    var tmp = _x[k];
//                    k = 0;
//                    _points.Add(tmp);
//                    _x.Clear();
//                    _x.Add(tmp);
//                    z = tmp;
//                }
//                else if (k == 0) h = h/2;
//    }
//    return text;
//}