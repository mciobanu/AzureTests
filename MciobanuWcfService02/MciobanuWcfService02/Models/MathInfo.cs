using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MciobanuWcfService02.Models
{
    //DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute
    [DataContract]
    public class MathInfo1
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public double Result { get; set; }

        [DataMember]
        public double X { get; set; }

        public MathInfo1(string Operation, double Result, double X)
        {
            this.Operation = Operation;
            this.Result = Result;
            this.X = X;
        }

        public override string ToString()
        {
            return "Operation: " + Operation + ", Result: " + Result + ", X: " + X;
        }
    }

    [DataContract]
    public class MathInfo2 : MathInfo1
    {
        [DataMember]
        public double Y { get; set; }

        public MathInfo2(string Operation, double Result, double X, double Y)
            : base(Operation, Result, X)
        {
            this.Y = Y;
        }
    }

    [DataContract]
    public class MathInfo4 : MathInfo2
    {
        [DataMember]
        public double Z { get; set; }

        [DataMember]
        public double T { get; set; }

        public MathInfo4(string Operation, double Result, double X, double Y = 0, double Z = 0, double T = 0)
            : base(Operation, Result, X, Y)
        {
            this.Z = Z;
            this.T = T;
        }
        
        public override string ToString()
        {
            return "Operation: " + Operation + ", Result: " + Result + ", X: " + X + ", Y: " + Y + ", Z: " + Z + ", T: " + T;
        }
    }//*/
}
