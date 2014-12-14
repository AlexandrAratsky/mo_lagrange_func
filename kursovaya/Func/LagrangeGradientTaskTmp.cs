using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OptLab.Func
{
    class LagrangeGradientTaskTmp : GradientTaskTmp
    {
        private Dictionary<int,Function> _h = new Dictionary<int,Function>();
        private Dictionary<int,VectorFunction> _dh = new Dictionary<int,VectorFunction>();
        private Dictionary<int,double> _coefH = new Dictionary<int, double>();
        private Dictionary<int, Function> _l = new Dictionary<int, Function>();
        private Dictionary<int, VectorFunction> _dl = new Dictionary<int, VectorFunction>();
        private Dictionary<int, double> _coefL = new Dictionary<int, double>();
        private Penalty _gammaF = new Penalty(Penalty.Type.Square, 1d,0.01d);
        private double _sh = 0.01;

        private List<string> steps_str = new List<string>();
        private List<List<Vector>> steps = new List<List<Vector>>();
        private List<string> steps_name = new List<string>(); 

        public double Sh
        {
            get { return _sh; }
            set { _sh = value; }
        }

        public List<string> StepsStr
        {
            get { return steps_str; }
            set { steps_str = value; }
        }
        public List<List<Vector>> Steps
        {
            get { return steps; }
            set { steps = value; }
        }
        public List<string> StepsName
        {
            get { return steps_name; }
            set { steps_name = value; }
        }

        public LagrangeGradientTaskTmp()
        {
            Name = "LagrangeGradientTaskTmp";
           // _gammaF.SetConstCoef("γ", _sh);
        }

        public void AddHFunc(int key,Function f, VectorFunction df, float cF)
        {
            _h.Add(key,f);
            _dh.Add(key,df);
            _coefH.Add(key,cF);
        }
        public void AddLFunc(int key, Function f, VectorFunction df, float cF)
        {
            _l.Add(key, f);
            _dl.Add(key, df);
            _coefL.Add(key, cF);
        }

        public void CreateLagrange()
        {
            if (_h.Count > 0)
            {
                string func = _Q.ToString();
                string funcdX = _dQ.FuncX;
                string funcdY = _dQ.FuncY;
                foreach (int key in _h.Keys)
                {
                    func += "+m" + key + "*(" + _h[key]+")";
                    func += "+(γ/2)*(" + _h[key] + ")^2";
                    _Q.SetConstCoef("γ", _sh);
                    _Q.SetFunc(func);
                    _Q.SetConstCoef("m" + key, _coefH[key]);
                    funcdX += "+m" + key + "*(" + _dh[key].FuncX + ")";
                    funcdX += "+γ*(" + _h[key] + ")*(" + _dh[key].FuncX + ")";
                    funcdY += "+m" + key + "*(" + _dh[key].FuncY + ")";
                    funcdY += "+γ*(" + _h[key] + ")*(" + _dh[key].FuncY + ")";
                    _dQ.SetConstCoef("γ", _sh);
                    _dQ.SetFunc(funcdX,funcdY);
                    _dQ.SetConstCoef("m" + key, _coefH[key]);
                }
            }
            if (_l.Count > 0)
            {
                string func = _Q.ToString();
                string funcdX = _dQ.FuncX;
                string funcdY = _dQ.FuncY;
                foreach (int k in _l.Keys)
                {
                    func += "+(1/(2*γ))*((max(0,γ*(" + _l[k] + ")+λ" + k + "))^2-λ" + k + "^2)";
                    _Q.SetConstCoef("γ", _sh);
                    _Q.SetFunc(func);
                    _Q.SetConstCoef("λ" + k, _coefL[k]);
                    funcdX += "+(1/γ)*(max(0,γ*(" + _l[k] + ")+λ" + k + "))"
                        + "*(maxD(0,γ*(" + _l[k] + ")+λ" + k + ",0,γ*(" + _dl[k].FuncX + ")))";
                    funcdY += "+(1/γ)*(max(0,γ*(" + _l[k] + ")+λ" + k + "))"
                        + "*(maxD(0,γ*(" + _l[k] + ")+λ" + k + ",0,γ*(" + _dl[k].FuncY + ")))";
                    _dQ.SetConstCoef("γ", _sh);
                    _dQ.SetFunc(funcdX, funcdY);
                    _dQ.SetConstCoef("λ" + k, _coefL[k]);
                }
            }

        }
        private string NeravenstvaOut()
        {
            string text = "\n s(k) = " + _gammaF + " γ = " + _sh.ToString("0.0000");
            
            if (_h.Count > 0)
                foreach (int key in _h.Keys)
                    text += "\n " + _h[key] + " = 0; m" + key + " = " + _coefH[key].ToString("0.0000") + "; dh = " +
                            _dh[key];
            if (_l.Count > 0)
                foreach (int key in _l.Keys)
                    text += "\n " + _l[key] + " <= 0; λ" + key + " = " + _coefL[key].ToString("0.0000") + "; dl = " +
                            _dl[key];
            return text;
        }
        public override string OutInfo()
        {
            string tmp = base.OutInfo();
            tmp += NeravenstvaOut();
            return tmp;
        }

        public bool IsEnd(Vector v,out string str)
        {
            double res = _dQ.Eval(v).Norm;
            
            if (_l.Count == 0)
            {
                double res2 =Math.Abs(_h[_h.Keys.First()].Eval(v));
                foreach (int k in _h.Keys)
                    if (Math.Abs(_h[k].Eval(v)) > res2) res2 = Math.Abs(_h[k].Eval(v));
                str = res.ToString("0.0000") + " && " + res2.ToString("0.000");
                if (res <= _sigma && res2 <= _sigma) return true;
            }
            else
            {
                double res2 = 0;
                foreach (int k in _l.Keys)
                    if (_l[k].Eval(v) > res2) res2 = _l[k].Eval(v);
                foreach (int k in _h.Keys)
                    if (Math.Abs(_h[k].Eval(v)) > res2) res2 = Math.Abs(_h[k].Eval(v));
                double res3 = Math.Abs(_coefL[_l.Keys.First()] * _l[_l.Keys.First()].Eval(v));
                foreach (int k in _l.Keys)
                    if (Math.Abs(_coefL[k] * _l[k].Eval(v)) > res3) res3 = Math.Abs(_coefL[k] * _l[k].Eval(v));
                str = res.ToString("0.0000") + " && " + res2.ToString("0.000") + " && " + res3.ToString("0.000");
                if (res <= _sigma && res2 <= _sigma && res3<=_sigma) return true;
            }
            return false;
        }
        
        public string StartSearchLagrange()
        {
            int k = 0;
            Vector s = _startPoint;
            string text = "";
            while (true)
            {
                text += "\n x" + k + " = " + s.ToString();
                k++;
                string tmp_g = base.OutInfo(); tmp_g += StartSearch();  steps_str.Add(tmp_g);
                Vector f = listOfPoints.Last();
                text += "\n\tGradientSearch" + s.ToString("0.0000") + "-> [" + listOfPoints.Count + "] steps -> " + f.ToString("0.0000");
                steps.Add(listOfPoints.Select(point => new Vector(point.X,point.Y)).ToList());
                steps_name.Add("x" + (k-1)  + " " + s.ToString());
                ResetPoints();
                ChangeSP(f.X,f.Y);
                s = f;
                
                List<int> keysH = new List<int>(_coefH.Keys);
                text += "\n";
                foreach (int key in keysH)
                {
                    _coefH[key] = _coefH[key] + _sh * _h[key].Eval(f.X, f.Y);
                    text += " m" + key + " = " + _coefH[key].ToString("0.000");
                    _Q.SetConstCoef("m" + key, _coefH[key]);
                    _dQ.SetConstCoef("m" + key, _coefH[key]);
                }
                List<int> keysL = new List<int>(_coefL.Keys);
                text += "\n";
                foreach (int key in keysL)
                {
                    _coefL[key] = Math.Max(0,_coefL[key] + _sh * _l[key].Eval(f.X, f.Y));
                    text += " λ" + key + " = " + _coefL[key].ToString("0.000");
                    _Q.SetConstCoef("λ" + key, _coefL[key]);
                    _dQ.SetConstCoef("λ" + key, _coefL[key]);
                }

                string tmp = "";
                bool end = IsEnd(f, out tmp);
                text += "\n res> " + tmp + " <= σ = " + _sigma.ToString("0.000");
                
                _sh = _gammaF.Eval(k,0);
                _Q.SetConstCoef("γ", _sh);
                _dQ.SetConstCoef("γ", _sh);
                text += " γ = " + _sh.ToString("0.000");
                
                if (end) break;
            }
            text += "\n\n x = " + s.ToString();
            return text;
        }

    }
}
