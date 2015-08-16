using MciobanuTestWebApi03.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// based on http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api

namespace MciobanuTestWebApi03.Controllers
{
    public class MathController : ApiController
    {
        static List<MathInfo1> Infos = new List<MathInfo1>(); //!!! just a demo; shouldn't use a static in this way

        public IEnumerable<MathInfo1> GetHistory()
        {
            if (Infos.Count() == 0)
            {
                MathInfo1[] FakeInfos = new MathInfo1[1];
                FakeInfos[0] = new MathInfo1("<empty list>", 0, 0);
                return FakeInfos;
            }
            return Infos;
        }

        public IHttpActionResult GetSquare(double X)
        {
            double ResVal = X * X;
            MathInfo1 Res = new MathInfo1("square", ResVal, X);
            Infos.Add(Res);
            return Ok(Res);
        }

        // !!! use different names
        // !!! use names that don't begin with the HTTP method name
        // !!! use nullable optional parameters
        [HttpGet]  // http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
        [ActionName("add")]
        public IHttpActionResult AddImpl(double X, double? Y = null, double Z = 0, double T = 0)
        {
            double YConv = Y == null ? 0 : (double)Y;
            double ResVal = X + YConv + Z + T;
            MathInfo1 Res = new MathInfo4("double", ResVal, X, YConv, Z, T);
            Infos.Add(Res);
            return Ok(Res);
        }

        [HttpDelete]
        [ActionName("delete_history")]
        public HttpResponseMessage DeleteHistory()
        {
            Infos.Clear();
            return Request.CreateResponse(HttpStatusCode.OK, "history cleaned");
        }
    }
}
