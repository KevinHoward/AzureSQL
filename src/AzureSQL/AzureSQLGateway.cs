using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

using ServiceStack;

using ServiceStack.AzureSQL.Types;

namespace ServiceStack.AzureSQL
{
    public class AzureSQLGateway : IRestGateway
    {

        private const string BaseUrl = "https://management.core.windows.net:8443";
        private const string MSVersion = "2012-03-01";

        private static readonly Dictionary<DataCenter, string> DacUrls 
            = new Dictionary<DataCenter, string> { 
                { DataCenter.EastAsia, "https://hkgprod-dacsvc.azure.com/DACWebService.svc" }, 
                { DataCenter.SoutheastAsia, "https://sg1prod-dacsvc.azure.com/DACWebService.svc" }, 
                { DataCenter.JapanWest, "https://os1-1prod-dacsvc.azure.com/dacwebservice.svc" }, 
                { DataCenter.JapanEast, "https://kw1-1prod-dacsvc.azure.com/dacwebservice.svc" }, 
                { DataCenter.CentralUS, "https://dm1-1prod-dacsvc.azure.com/dacwebservice.svc" }, 
                { DataCenter.NorthCentralUS, "https://ch1prod-dacsvc.azure.com/DACWebService.svc" }, 
                { DataCenter.SouthCentralUS, "https://sn1prod-dacsvc.azure.com/DACWebService.svc" }, 
                { DataCenter.WestUS, "https://by1prod-dacsvc.azure.com/DACWebService.svc" },
                { DataCenter.EastUS, "https://bl2prod-dacsvc.azure.com/DACWebService.svc" },
                { DataCenter.EastUS2, "https://bn1prod-dacsvc.azure.com/dacwebservice.svc" },
                { DataCenter.NorthEurope, "https://db3prod-dacsvc.azure.com/DACWebService.svc" },
                { DataCenter.WestEurope, "https://am1prod-dacsvc.azure.com/DACWebService.svc" },
                { DataCenter.BrazilSouth, "https://cq1-1prod-dacsvc.azure.com/dacwebservice.svc" },
            };

        private readonly string certThumbprint;

        public TimeSpan Timeout { get; set; }

        private string UserAgent { get; set; }


        public AzureSQLGateway(string certThumbprint)
        {
            this.certThumbprint = certThumbprint;
            Timeout = TimeSpan.FromSeconds(60);
            UserAgent = "servicestack.net azuresql v1";
        }

        protected virtual string Send(string relativeUrl, string method, string body, string baseUrl = BaseUrl)
        {
            try
            {
                var url = baseUrl.CombineWith(relativeUrl);

                var response = url.SendStringToUrl(method: method, requestBody: body, requestFilter: req =>
                {
                    req.Accept = MimeTypes.Xml;

                    // Add a x-ms-version header to specify API version.
                    req.Headers.Add("x-ms-version", MSVersion);

                    // Generate a request ID that can be used to identify this request in the service logs.
                    string clientRequestId = Guid.NewGuid().ToString();
                    req.Headers.Add("x-ms-client-request-id", clientRequestId);

                    // Create a reference to the My certificate store.
                    X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

                    // Open the store.
                    certStore.Open(OpenFlags.ReadOnly);

                    // Find the certificate that matches the thumbprint.
                    X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certThumbprint, false);
                    certStore.Close();

                    // Verify the certificate was added to the collection.
                    if (0 == certCollection.Count)
                        throw new Exception("Error: No certificate found with thumbprint " + certThumbprint);

                    // Create an X509Certificate2 object using the matching certificate.
                    X509Certificate2 certificate = certCollection[0];

                    req.ClientCertificates.Add(certificate);


                    //if (method == HttpMethods.Post || method == HttpMethods.Put)
                    req.ContentType = MimeTypes.FormUrlEncoded;

                    PclExport.Instance.Config(req,
                        userAgent: UserAgent,
                        timeout: Timeout,
                        preAuthenticate: true);
                });

                return response;
            }
            catch (WebException ex)
            {
                string errorBody = ex.GetResponseBody();
                var errorStatus = ex.GetStatus() ?? HttpStatusCode.BadRequest;

                if (ex.IsAny400())
                {
                    var result = errorBody.FromXml<AzureErrors>();
                    throw new AzureException(result.Error)
                    {
                        StatusCode = errorStatus
                    };
                }

                throw;
            }
        }

        public T Send<T>(IReturn<T> request, string method, bool sendRequestBody = true)
        {
            var baseUrl = (request is IDacRequest) ? DacUrls[((IDacRequest)request).DataCenter] : BaseUrl;

            var relativeUrl = request.ToUrl(method);

            var serializer = new XmlSerializer(typeof(T));
            
            string body = null;
            if (sendRequestBody)
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    serializer.Serialize(textWriter, request);
                    body = serializer.ToString();
                }
            }

            var xml = Send(relativeUrl, method, body, baseUrl);

            using (StringReader reader = new StringReader(xml))
                return (T)serializer.Deserialize(reader);
        }

        public T Send<T>(IReturn<T> request)
        {
            var method = request is IPost ?
                  HttpMethods.Post
                : request is IPut ?
                  HttpMethods.Put
                : request is IDelete ?
                  HttpMethods.Delete :
                  HttpMethods.Get;

            return Send(request, method, sendRequestBody: false);
        }

        public T Get<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Get, sendRequestBody: false);
        }

        public T Post<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Post);
        }

        public T Put<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Put);
        }

        public T Delete<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Delete, sendRequestBody: false);
        }
    }
}
