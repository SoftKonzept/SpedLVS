using Common.ApiUri;
using Common.Models;
using LvsScan.Portable.Enumerations;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{
    public class apiSetting
    {

        public string ServerUrl
        {
            get
            {
                string strUrl = apiServerUri.GetApiServerUri();
                return strUrl;
            }
        }

        public Users LoggedUser { get; set; }
        public HttpClient client { get; set; }

        public HttpClient InitHttpClient()
        {
            HttpClient cl = new HttpClient();
            try
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        cl = new HttpClient(DependencyService.Get<Interfaces.IHTTPClientHandlerCreationService>().GetInsecureHandler());
                        break;

                    case Device.iOS:
                        cl = new HttpClient(new HttpClientHandler());
                        break;

                    case Device.UWP:
                        cl = new HttpClient(new HttpClientHandler());
                        cl.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                        cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                        break;
                    default:
                        //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                        cl = new HttpClient(new HttpClientHandler());
                        break;
                }
                SetAccessTokenToHttpClient(ref cl);
                ////AccessToken hinzufügen
                //if (Settings.InternalSettings.AccessGranted)
                //{
                //    cl.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.InternalSettings.AccessToken);
                //}
                //cl.DefaultRequestHeaders.Accept.Clear();
                //cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                string str = ex.Message;

                //cl = new HttpClient(new HttpClientHandler());
            }
            return cl;
        }

        public HttpClient InitHttpClient(string myDeviceRuntimePlattform)
        {
            HttpClient cl = new HttpClient();
            try
            {
                //AccessToken hinzufügen
                if (Settings.InternalSettings.AccessGranted)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.InternalSettings.AccessToken);
                }
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        cl = new HttpClient(DependencyService.Get<Interfaces.IHTTPClientHandlerCreationService>().GetInsecureHandler());
                        break;

                    case Device.iOS:
                        cl = new HttpClient(new HttpClientHandler());
                        break;

                    case Device.UWP:
                        cl = new HttpClient(new HttpClientHandler());
                        cl.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                        cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                        break;
                    default:
                        //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                        cl = new HttpClient(new HttpClientHandler());
                        break;
                }
                SetAccessTokenToHttpClient(ref cl);
            }
            catch (Exception ex)
            {
                string str = ex.Message;

                //cl = new HttpClient(new HttpClientHandler());
            }
            return cl;
        }
        public HttpClient InitHttpClient(string myDeviceRuntimePlattform, enumHTTPMethodeType myHTTPMethodeType)
        {
            HttpClient cl = new HttpClient();
            try
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        cl = new HttpClient(DependencyService.Get<Interfaces.IHTTPClientHandlerCreationService>().GetInsecureHandler());
                        break;

                    case Device.iOS:
                        cl = new HttpClient(new HttpClientHandler());
                        break;

                    case Device.UWP:
                        cl = new HttpClient(new HttpClientHandler());
                        cl.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                        cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                        break;
                    default:
                        //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                        cl = new HttpClient(new HttpClientHandler());
                        break;
                }
                SetAccessTokenToHttpClient(ref cl);
            }
            catch (Exception ex)
            {
                string str = ex.Message;

                //cl = new HttpClient(new HttpClientHandler());
            }
            return cl;
        }

        //  neu muss noch getestet werden
        //public HttpClient InitHttpClient(string myDeviceRuntimePlattform, enumHTTPMethodeType myHTTPMethodeType)
        //{
        //    HttpClient client;
        //    try
        //    {
        //        HttpMessageHandler handler = myDeviceRuntimePlattform switch
        //        {
        //            Device.Android => DependencyService.Get<Interfaces.IHTTPClientHandlerCreationService>().GetInsecureHandler(),
        //            Device.iOS => new HttpClientHandler(),
        //            Device.UWP => new HttpClientHandler(),
        //            _ => new HttpClientHandler()
        //        };

        //        client = new HttpClient(handler);

        //        if (myDeviceRuntimePlattform == Device.UWP)
        //        {
        //            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        //        }

        //        SetAccessTokenToHttpClient(ref client);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if necessary
        //        Console.WriteLine($"Error initializing HttpClient: {ex.Message}");
        //        client = new HttpClient(new HttpClientHandler());
        //    }

        //    return client;
        //}


        private void SetAccessTokenToHttpClient(ref HttpClient cl)
        {
            //AccessToken hinzufügen
            if (Settings.InternalSettings.AccessGranted)
            {
                string str = Settings.InternalSettings.AccessToken;
                cl.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.InternalSettings.AccessToken);
            }
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
