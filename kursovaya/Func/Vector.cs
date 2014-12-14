using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptLab.Func
{
    public struct Vector
    {
        private double _x;
        private double _y;

        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }
        public Vector(Vector v)
        {
            _x = v.X;
            _y = v.Y;
        }
        public double Norm
        {
            get { return Math.Sqrt(X*X + Y*Y); }
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X+b.X, a.Y+b.Y);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator *(double cF, Vector vector)
        {
            return new Vector(cF*vector.X, cF*vector.Y);
        }
        public static double operator *(Vector a, Vector b)
        {
            return a.X*b.X + a.Y*b.Y;
        }
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X,-v.Y);
        }
        public override string ToString()
        {
            return "[" + X.ToString("0.000") + ";" + Y.ToString("0.000") + "]";
        }
        public string ToString(string format)
        {
            return "[" + X.ToString(format) + ";" + Y.ToString(format) + "]";
        }
    }

}
