using Android.Net;
using Javax.Net.Ssl;
using LvsScan.Android;
using System.Net.Http;
using Xamarin.Android.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(HTTPClientHandlerCreationService_Android))]

namespace LvsScan.Android
{
    public class HTTPClientHandlerCreationService_Android : LvsScan.Portable.Interfaces.IHTTPClientHandlerCreationService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            return new IgnoreSSLClientHandler();
        }
        internal class IgnoreSSLClientHandler : AndroidClientHandler
        {
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return SSLCertificateSocketFactory.GetInsecure(1000, null);
            }

            protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
            {
                return new IgnoreSSLHostnameVerifier();
            }
        }

        internal class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
            {
                return true;
            }
        }
    }
}