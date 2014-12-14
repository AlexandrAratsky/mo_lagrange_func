using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Mathos.Parser;

namespace OptLab.Func
{
    public class Penalty
    {
        private delegate double PenaltyFunc(int k, double h);
        public enum Type
        {
            Constant,
            Adaptive,
            Linear,
            Square
        }

        private Type _type;
        private Double _current = 0.001d;
        private readonly Double _start = 1d;
        private readonly Double _multiplier = 0.2d;
        private PenaltyFunc _func;

        public double Current
        {
            get { return _current; }
            set { _current = value; }
        }
        public double Start
        {
            get { return _start; }
        }
        public double Multiplier
        {
            get { return _multiplier; }
        }
        public Type PenaltyType
        {
            get { return _type; }
        }

        private double ConstantFunc(int k, double h)
        {
            return Start;
        }
        private double AdaptiveFunc(int k, double h)
        {
            if (h > 2*_current)
                return _start + _multiplier*k;
            return _start;
        }
        private double LinearFunc(int k, double h)
        {
            return Start + k*Multiplier;
        }
        private double SquareFunc(int k, double h)
        {
            return Start + k*k*Multiplier;
        }

        public Penalty(Type type, Double startCoef, Double multiplier)
        {
            _start = startCoef;
            _multiplier = multiplier;
            _type = type;
            switch (_type)
            {
                case Type.Constant:
                    _func = ConstantFunc;
                    break;
                case Type.Adaptive:
                    _func = AdaptiveFunc;
                    break;
                case Type.Linear:
                    _func = LinearFunc;
                    break;
                case Type.Square:
                    _func = SquareFunc;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public override string ToString()
        {
            switch (_type)
            {
                case Type.Constant:
                    return "Constant: " + _start;
                case Type.Adaptive:
                    return "Adaptive: if (h>2*" + _current + ") return " + _start + "+k*" + _multiplier + " else return " + _start;
                case Type.Linear:
                    return "Linear: " + _start + "k*" + _multiplier;
                case Type.Square:
                    return "Square: " + _start + "k*k*" + _multiplier;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public double Eval(int k, double h)
        {
            return _func(k, h);
        }
    }
}
