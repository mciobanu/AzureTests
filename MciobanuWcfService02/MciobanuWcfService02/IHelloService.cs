using MciobanuWcfService02.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net.Http;

namespace MciobanuWcfService02
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHelloService" in both code and config file together.
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Hello/{name}", ResponseFormat = WebMessageFormat.Json)]
        string Hello(string name);

        [OperationContract]
        [WebGet(UriTemplate = "GetHistory", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<string> GetHistory();

        [OperationContract]
        [WebInvoke(UriTemplate = "delete_history", ResponseFormat = WebMessageFormat.Json)]
        void DeleteHistory();

        [OperationContract]
        //[WebGet(UriTemplate = "GetSquare/{X}", ResponseFormat = WebMessageFormat.Json)] //!!! this doesn't work - see 
        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/969ce363-4c0f-4642-b751-35e517336b0d/wcf-rest-variables-for-uritemplate-path-segments-must-have-type-string?forum=wcf
        [WebGet(UriTemplate = "GetSquare?X={X}", ResponseFormat = WebMessageFormat.Json)]
        //MathInfo1 GetSquare(double X);
        double GetSquare(double X);

        /*[OperationContract]
        [WebGet(UriTemplate = "Add?X={X}&Y={Y}&Z={Z}&T={T}", ResponseFormat = WebMessageFormat.Json)]
        double GetAdd(double X, double Y = 0, double Z = 0, double T = 0);//*/

        [OperationContract]
        [WebGet(UriTemplate = "Add?X={X}&Y={Y}&Z={Z}&T={T}", ResponseFormat = WebMessageFormat.Json)]
        //MathInfo1 GetAdd(double X, double Y = 0, double Z = 0, double T = 0); // ttt0 doesn't seem possible to return a class that was derived from the return
        //MathInfo4 GetAdd(double X, double? Y = null, double Z = 0, double T = 0); //ttt0 nullable optional params don't seem possible
        MathInfo4 GetAdd(double X, double Y = 0, double Z = 0, double T = 0);
        //*/

        [OperationContract]
        [WebGet(UriTemplate = "test_guid?guid={guid}", ResponseFormat = WebMessageFormat.Json)]
        string TestGuid(Guid guid);

        [OperationContract]
        [WebGet(UriTemplate = "test_int?x={x}", ResponseFormat = WebMessageFormat.Json)]
        int TestInt(int x);

        /*[OperationContract] //!!! IHttpActionResult is an WebAPI2 thing, so doesn't make sense here
        [WebGet(UriTemplate = "Add?X={X}&Y={Y}&Z={Z}&T={T}", ResponseFormat = WebMessageFormat.Json)]
        IHttpActionResult GetAdd(double X, double Y = 0, double Z = 0, double T = 0);//*/

        /*[OperationContract]
        [WebGet(UriTemplate = "Add?X={X}&Y={Y}&Z={Z}&T={T}", ResponseFormat = WebMessageFormat.Json)]
        HttpResponseMessage GetAdd(double X, double Y = 0, double Z = 0, double T = 0);//*/
        
    }
}
