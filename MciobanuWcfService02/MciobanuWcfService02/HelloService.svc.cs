using MciobanuWcfService02.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Http;

/*
using System.Net;
using System.Net.Http;
using System.Web.Http;
*/

// based on http://www.topwcftutorials.net/2013/09/simple-steps-for-restful-service.html
namespace MciobanuWcfService02
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HelloService.svc or HelloService.svc.cs at the Solution Explorer and start debugging.
    public class HelloService : IHelloService
    {
        static List<string> Infos = new List<string>(); //!!! just a demo; shouldn't use a static in this way

        public IEnumerable<string> GetHistory()
        {
            if (Infos.Count() == 0)
            {
                string[] FakeInfos = new string[1];
                FakeInfos[0] = "<empty list>";
                return FakeInfos;
            }
            return Infos;
        }

        public void DeleteHistory()
        {
            Infos.Clear();
        }

        public string Hello(string name)
        {
            return "hi " + name;
        }

        //public IHttpActionResult GetSquare(double X)
        public double GetSquare(double X)
        {
            double ResVal = X * X;
            MathInfo1 Res = new MathInfo1("square", ResVal, X);
            //Infos.Add(Res);
            //return Ok(Res);
            Infos.Add(Res.ToString());
            return ResVal;
        }

        /*public double GetAdd(double X, double Y = 0, double Z = 0, double T = 0)
        {
            double ResVal = X + Y + Z + T;
            MathInfo1 Res = new MathInfo4("double", ResVal, X, Y, Z, T);
            Infos.Add(Res);
            return ResVal;
        }//*/


        /* 
         * Trying to have a nullable param leads to this:
         * 'GetAdd' in contract 'IHelloService' has a query variable named 'Y' of type 'System.Nullable`1[System.Double]', 
         * but type 'System.Nullable`1[System.Double]' is not convertible by 'QueryStringConverter'. Variables for 
         * UriTemplate query values must have types that can be converted by 'QueryStringConverter'
         */
        /*
        public MathInfo4 GetAdd(double X, double? Y = 0, double Z = 0, double T = 0)
        {
            double YConv = (Y == null ? 0 : (double)Y);
            double ResVal = X + YConv + Z + T;
            MathInfo4 Res = new MathInfo4("double", ResVal, X, YConv, Z, T);
            Infos.Add(Res.ToString());
            return Res;
            //return Ok(Res);
        }//*/


        // ttt0 doesn't seem possible to return a class that was derived from the return
        //public MathInfo1 GetAdd(double X, double Y = 0, double Z = 0, double T = 0)
        public MathInfo4 GetAdd(double X, double Y = 0, double Z = 0, double T = 0)
        {
            double ResVal = X + Y + Z + T;
            MathInfo4 Res = new MathInfo4("double", ResVal, X, Y, Z, T);
            Infos.Add(Res.ToString());
            return Res;
            //return Ok(Res);
        }//*/


        // ttt0 couldn't get this to work, but didn't try hard either
        /*public HttpResponseMessage GetAdd(double X, double Y = 0, double Z = 0, double T = 0)
        {  //ttt0 this doesn't work, but it doesn't matter much
            double ResVal = X + Y + Z + T;
            MathInfo4 Res = new MathInfo4("double", ResVal, X, Y, Z, T);
            Infos.Add(Res);
            //return Res;
            //return Ok(Res);
            HttpResponseMessage httpRes = new HttpResponseMessage();
            HttpContent content = new StringContent("conteNT");
            httpRes.Content = content;
            return httpRes;
        }//*/
    }
}
