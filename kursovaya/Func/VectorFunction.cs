using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Mathos.Parser;

namespace OptLab.Func
{
    public class VectorFunction
    {
        private string _funcX;
        private ReadOnlyCollection<string> _list1;
        public string FuncX
        {
            get { return _funcX; }
        }
        private string _funcY;
        private ReadOnlyCollection<string> _list2;
        public string FuncY
        {
            get { return _funcY; }
        }
        private MathParser parser = new MathParser();

        public VectorFunction(string funcX,string funcY)
        {
            _funcX = funcX;
            _funcY = funcY;
            parser.LocalFunctions.Add("max", x =>
            {
                if (x[0] > x[1]) return x[0];
                return x[1];
            });
            parser.LocalFunctions.Add("maxD", x =>
            {
                if (x[0] > x[1]) return x[2];
                return x[3];
            });
            _list1 = parser.GetTokens(_funcX);
            _list2 = parser.GetTokens(_funcY);
        }
        public override string ToString()
        {
            return "\n\t∂/∂x = " + _funcX + "\n\t∂/∂y = " + _funcY;
        }
        internal void SetFunc(string funcX,string funcY)
        {
            _funcX = funcX;
            _funcY = funcY;
            _list1 = parser.GetTokens(_funcX);
            _list2 = parser.GetTokens(_funcY);
        }
        public Vector Eval(double x, double y)
        {
            parser.LocalVariables["x"] = (decimal)x;
            parser.LocalVariables["y"] = (decimal)y;
            var resultX = (double)parser.Parse(_list1);
            var resultY = (double)parser.Parse(_list2);
            return new Vector(resultX,resultY);
        }
        public Vector Eval(Vector vector)
        {
            parser.LocalVariables["x"] = (decimal)vector.X;
            parser.LocalVariables["y"] = (decimal)vector.Y;
            var resultX = (double)parser.Parse(_list1);
            var resultY = (double)parser.Parse(_list2);
            return new Vector(resultX, resultY);
        }
        public void SetConstCoef(string sh, double coef)
        {
            parser.LocalVariables[sh] = (decimal)coef;
        }
    }
}
