using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mathos.Parser;

namespace OptLab.Func
{
    public class Function
    {
        private string _func;
        private ReadOnlyCollection<string> _list;
        private MathParser parser = new MathParser();

        public static void TryParse(string func, double xF, double yF)
        {
            var parser = new MathParser();
            parser.LocalVariables["x"] = (decimal) xF;
            parser.LocalVariables["y"] = (decimal) yF;
            try
            {
                parser.Parse(func);
            }
            catch (Exception exception)
            {
                throw new Exception("f(" + xF.ToString("0.0") + "," + yF.ToString("0.0") + ") = " + func + " > " +
                                    exception.Message);
            }
        }
        public Function(string func)
        {
            _func = func;
            parser.LocalFunctions.Add("max", x =>
                {
                    if (x[0] > x[1]) return x[0];
                    return x[1];
                });
            _list = parser.GetTokens(_func);
        }
        public override string ToString()
        {
            return _func;
        }
        internal void SetFunc(string func)
        {
            _func = func;
            _list = parser.GetTokens(_func);
        }
        public void SetConstCoef(string name, double coef)
        {
            parser.LocalVariables[name] = (decimal) coef;
        }
        public double Eval(double x, double y)
        {
            parser.LocalVariables["x"] = (decimal) x;
            parser.LocalVariables["y"] = (decimal) y;
            var result = (double) parser.Parse(_list);
            return result;
        }
        public double Eval(Vector vector)
        {
            parser.LocalVariables["x"] = (decimal) vector.X;
            parser.LocalVariables["y"] = (decimal) vector.Y;
            var result = (double) parser.Parse(_list);
            return result;
        }
    }
}
