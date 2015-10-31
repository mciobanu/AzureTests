using MciobanuTestWebApi03.Models;
using MciobanuTestWebApi03.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;
using log4net;  // based on http://ruchirac.blogspot.ro/2012/10/configure-log4net-with-aspnet-logging.html

// based on http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api

namespace MciobanuTestWebApi03.Controllers
{

    public class FileResult
    {
        /// <summary>
        /// Gets or sets the local path of the file saved on the server.
        /// </summary>
        /// <value>
        /// The local path.
        /// </value>
        public IEnumerable<string> FileNames { get; set; }

        /// <summary>
        /// Gets or sets the submitter as indicated in the HTML form used to upload the data.
        /// </summary>
        /// <value>
        /// The submitter.
        /// </value>
        public string Submitter { get; set; }
    }

    public class MathController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger("MathLog");

        static List<MathInfo1> Infos = new List<MathInfo1>(); //!!! just a demo; shouldn't use a static in this way
        static DocumentDbLink dbLink = new DocumentDbLink();

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

        public async Task<double> GetSquareDbl(double X)
        {
            double ResVal = X * X;
            MathInfo1 Res = new MathInfo1("square", ResVal, X);
            Infos.Add(Res);
            await dbLink.saveResp(Res);
            return ResVal;
        }

        public IHttpActionResult GetTestParams(String p1, string p2)
        {
            List<string> Res = new List<string>();
            Res.Add(p1);
            Res.Add(p2);
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

        //public async Task<IHttpActionResult> PostFile(int k = 9)
        public async Task<FileResult> PostFileOnDisk(int k = 9)
        {
            log.Info("k=" + k);

            // from http://aspnet.codeplex.com/sourcecontrol/latest#Samples/WebApi/FileUploadSample/Controllers/FileUploadController.cs
            // see also: http://blogs.msdn.com/b/henrikn/archive/2012/03/01/file-upload-and-asp-net-web-api.aspx

            // stores data locally in a file
            string ServerUploadFolder = "C:/Users/mciob/Downloads/1";

            // Verify that this is an HTML Form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }

            // Create a stream provider for setting up output streams
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder);

            // Read the MIME multipart asynchronously content using the stream provider we just created.
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            // Create response
            FileResult fr = new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Submitter = streamProvider.FormData["submitter"]
            };//*/
            return fr;
            //return Ok();
        }

        public async Task<IHttpActionResult> PostFile(int k = 9)
        {
            log.Info("k=" + k);

            // from http://aspnet.codeplex.com/sourcecontrol/latest#Samples/WebApi/FileUploadSample/Controllers/FileUploadController.cs

            // Verify that this is an HTML Form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }

            // Create a stream provider for setting up output streams
            MultipartMemoryStreamProvider streamProvider = new MultipartMemoryStreamProvider();

            // Read the MIME multipart asynchronously content using the stream provider we just created.
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            string msg = "uploaded sizes:";
            foreach (var content in streamProvider.Contents)
            {
                log.Info("content=" + content);
                byte[] b = await content.ReadAsByteArrayAsync();
                log.Info("size=" + b.Length);
                msg += " " + b.Length;
            }

            return Ok(msg);
        }

        public DateTime GetTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}
