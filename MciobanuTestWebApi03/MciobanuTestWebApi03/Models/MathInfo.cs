using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MciobanuTestWebApi03.Models
{
    public class MathInfo1
    {
        public string Operation { get; set; }
        public double Result { get; set; }
        public double X { get; set; }

        public MathInfo1(string Operation, double Result, double X)
        {
            this.Operation = Operation;
            this.Result = Result;
            this.X = X;
        }
    }

    public class MathInfo2 : MathInfo1
    {
        public double Y { get; set; }

        public MathInfo2(string Operation, double Result, double X, double Y) : base(Operation, Result, X)
        {
            this.Y = Y;
        }
    }

    public class MathInfo4 : MathInfo2
    {
        public double Z { get; set; }
        public double T { get; set; }

        public MathInfo4(string Operation, double Result, double X, double Y = 0, double Z = 0, double T = 0) : base(Operation, Result, X, Y)
        {
            this.Z = Z;
            this.T = T;
        }
    }
}