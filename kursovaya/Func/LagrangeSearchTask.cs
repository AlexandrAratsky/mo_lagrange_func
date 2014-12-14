using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptLab.Func
{
    public class LagrangeSearchTask
    {
        public string Name { get; set; }
        private string _origin;
        private Function _Q;
        private VectorFunction _dQ;
        private Vector _startPoint;
        private List<Vector> _points = new List<Vector>();
        private bool _isGradient = false;
        private Dictionary<int, Function> _h = new Dictionary<int, Function>();
        private Dictionary<int, VectorFunction> _dh = new Dictionary<int, VectorFunction>();
        private Dictionary<int, double> _coefH = new Dictionary<int, double>();
        private Dictionary<int, Function> _l = new Dictionary<int, Function>();
        private Dictionary<int, VectorFunction> _dl = new Dictionary<int, VectorFunction>();
        private Dictionary<int, double> _coefL = new Dictionary<int, double>();
        private double _sigma = 0.001d;
        private Penalty _penalty;
        private List<string> _stepsStr = new List<string>();
        private List<List<Vector>> _steps = new List<List<Vector>>();
        private List<string> _stepsName = new List<string>();
        private string _info;
        public string GetSearchInfo()
        {
            return _info;
        }
        public string GetoriginFunc()
        {
            return _origin;
        }
        public Function GetFunc()
        {
            return _Q;
        }
        public List<string> StepsStr
        {
            get { return _stepsStr; }
            set { _stepsStr = value; }
        }
        public List<List<Vector>> Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }
        public List<string> StepsName
        {
            get { return _stepsName; }
            set { _stepsName = value; }
        }
        public List<Vector> Points
        {
            get { return _points; }
        }
        public double Sigma
        {
            get { return _sigma; }
            set { _sigma = value; }
        }

        public bool IsGradient
        {
            get { return _isGradient; }
            set { _isGradient = value; }
        }

        public Penalty Penalty
        {
            get { return _penalty; }
            set { _penalty = value; }
        }

        public Vector StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public Dictionary<int, Function> H
        {
            get { return _h; }
        }

        public Dictionary<int, VectorFunction> Dh
        {
            get { return _dh; }
        }

        public Dictionary<int, double> CoefH
        {
            get { return _coefH; }
        }

        public Dictionary<int, Function> L
        {
            get { return _l; }
        }

        public Dictionary<int, VectorFunction> Dl
        {
            get { return _dl; }
        }

        public Dictionary<int, double> CoefL
        {
            get { return _coefL; }
        }

        public LagrangeSearchTask(string q, string dxQ, string dyQ, Vector startPoint)
        {
            StartPoint = new Vector(startPoint);
            _dQ = new VectorFunction(dxQ,dyQ);
            _Q = new Function(q);
            Name = "LagrangeOptTask";
            _origin = q;
            Penalty = new Penalty(Penalty.Type.Linear, 1d, 0.1d) { Current = _sigma };
        }

        public void AddHFunc(int key, Function f, VectorFunction df, double cF)
        {
            H.Add(key, f);
            Dh.Add(key, df);
            CoefH.Add(key, cF);
        }
        public void AddLFunc(int key, Function f, VectorFunction df, double cF)
        {
            L.Add(key, f);
            Dl.Add(key, df);
            CoefL.Add(key, cF);
        }

        public void CreateLagrange()
        {
            if (H.Count > 0)
            {
                string func = _Q.ToString();
                string funcdX = _dQ.FuncX;
                string funcdY = _dQ.FuncY;
                foreach (var key in H.Keys)
                {
                    func += "+m" + key + "*(" + H[key] + ")";
                    func += "+(γ/2)*(" + H[key] + ")^2";
                    _Q.SetConstCoef("γ", Penalty.Start);
                    _Q.SetFunc(func);
                    _Q.SetConstCoef("m" + key, CoefH[key]);
                    funcdX += "+m" + key + "*(" + Dh[key].FuncX + ")";
                    funcdX += "+γ*(" + H[key] + ")*(" + Dh[key].FuncX + ")";
                    funcdY += "+m" + key + "*(" + Dh[key].FuncY + ")";
                    funcdY += "+γ*(" + H[key] + ")*(" + Dh[key].FuncY + ")";
                    _dQ.SetConstCoef("γ", Penalty.Start);
                    _dQ.SetFunc(funcdX, funcdY);
                    _dQ.SetConstCoef("m" + key, CoefH[key]);
                }
            }
            if (L.Count > 0)
            {
                string func = _Q.ToString();
                string funcdX = _dQ.FuncX;
                string funcdY = _dQ.FuncY;
                foreach (var k in L.Keys)
                {
                    func += "+(1/(2*γ))*((max(0,γ*(" + L[k] + ")+λ" + k + "))^2-λ" + k + "^2)";
                    _Q.SetConstCoef("γ", Penalty.Start);
                    _Q.SetFunc(func);
                    _Q.SetConstCoef("λ" + k, CoefL[k]);
                    funcdX += "+(1/γ)*(max(0,γ*(" + L[k] + ")+λ" + k + "))"
                        + "*(maxD(0,γ*(" + L[k] + ")+λ" + k + ",0,γ*(" + Dl[k].FuncX + ")))";
                    funcdY += "+(1/γ)*(max(0,γ*(" + L[k] + ")+λ" + k + "))"
                        + "*(maxD(0,γ*(" + L[k] + ")+λ" + k + ",0,γ*(" + Dl[k].FuncY + ")))";
                    _dQ.SetConstCoef("γ", Penalty.Start);
                    _dQ.SetFunc(funcdX, funcdY);
                    _dQ.SetConstCoef("λ" + k, CoefL[k]);
                }
            }
        }

        private string NeravenstvaOut()
        {
            string text = "\n s(k) = " + Penalty + " γ = " + Penalty.Start;

            if (H.Count > 0)
                foreach (int key in H.Keys)
                    text += "\n " + H[key] + " = 0; m" + key + " = " + CoefH[key] + "; dh = " + Dh[key];
            if (L.Count > 0)
                foreach (int key in L.Keys)
                    text += "\n " + L[key] + " <= 0; λ" + key + " = " + CoefL[key] + "; dl = " + Dl[key];
            return text;
        }
        public string OutInfo()
        {
            string text = Name + "\nQ(x,y)  = " + _Q + _dQ;
            text += NeravenstvaOut();
            return text;
        }
        private string OurRes(double r1, double r2, double? r3)
        {
            string str = "\n\t||gradLᵞ(xk,λk,μk)|| = " + r1.ToString("0.000000") + "\n\tG(xk) = " + r2.ToString("0.000000");
            str += (r3 == null) ? "" : "\n\tH(xk) = " + ((double) r3).ToString("0.000000");
            return str;
        }
        public bool IsEnd(Vector v, out double r1, out double r2, out double? r3)
        {
            double res = _dQ.Eval(v).Norm;

            if (L.Count == 0)
            {
                double res2 = Math.Abs(H[H.Keys.First()].Eval(v));
                res2 = H.Keys.Select(k => Math.Abs(H[k].Eval(v))).Concat(new[] {res2}).Max();
                r1 = res;
                r2 = res2;
                r3 = null;
                if (res <= Sigma && res2 <= Sigma) return true;
            }
            else
            {
                double res2 = L.Keys.Select(k => L[k].Eval(v)).Concat(new double[] {0}).Max();
                res2 = H.Keys.Select(k => Math.Abs(H[k].Eval(v))).Concat(new[] {res2}).Max();
                double res3 = Math.Abs(CoefL[L.Keys.First()] * L[L.Keys.First()].Eval(v));
                res3 = L.Keys.Select(k => Math.Abs(CoefL[k]*L[k].Eval(v))).Concat(new[] {res3}).Max();
                r1 = res;
                r2 = res2;
                r3 = res3;
                if (res <= Sigma && res2 <= Sigma && res3 <= Sigma) return true;
            }
            return false;
        }

        public Vector StartSearchLagrange()
        {
            int count = 0;
            Vector s = StartPoint;
            _info = "\nSearch started: X = " + s.ToString("0.00000");
            while (true)
            {
                _info += "\n\n[" + count + "] " + "X" + count + " = " + s.ToString("0.000000"); ;
                var task = new HookeJeevesTask(_Q.ToString(), s);
                task.SetFunc(_Q);
                Vector f = task.StartSearch();
                _stepsStr.Add(task.GetSearchInfo());
                _info += "\nLocalSearch " + s + " -> " + f;
                _steps.Add(task.Points.Select(point => new Vector(point.X, point.Y)).ToList());
                foreach (Vector v in task.Points)  _points.Add(v);
                _stepsName.Add("[" + count + "]"+" X" + count + " " + s);
                s = f;
                double r1, r2;
                double? r3;
                bool end = IsEnd(f, out r1, out r2, out r3);
                _info += "\n Next step-> " + (end ? "stop" : "next") + " (σ = " + _sigma.ToString("0.00000") +")";
                double penalty = Penalty.Eval(count, r1);
                count++;
                _Q.SetConstCoef("γ", penalty);
                _dQ.SetConstCoef("γ", penalty);
                _info += Penalty + "\n\tγ = " + penalty.ToString("0.00000"); ;
                _info += OurRes(r1, r2, r3);

                var keysH = new List<int>(CoefH.Keys);
                _info += "\n Equality:";
                foreach (int key in keysH)
                {
                    CoefH[key] = CoefH[key] + penalty * H[key].Eval(f.X, f.Y);
                    _info += "\n\tm" + key + " = " + CoefH[key];
                    _Q.SetConstCoef("m" + key, CoefH[key]);
                    _dQ.SetConstCoef("m" + key, CoefH[key]);
                }
                var keysL = new List<int>(CoefL.Keys);
                _info += "\n Inequality:";
                foreach (int key in keysL)
                {
                    CoefL[key] = Math.Max(0, CoefL[key] + penalty * L[key].Eval(f.X, f.Y));
                    _info += "\n\tλ" + key + " = " + CoefL[key];
                    _Q.SetConstCoef("λ" + key, CoefL[key]);
                    _dQ.SetConstCoef("λ" + key, CoefL[key]);
                }
                
                if (end) break;
            }
            _info += "\n\nResult: X = " + s.ToString("0.00000");
            return s;
        }

    }

}
