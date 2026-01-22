using System;
using System.Linq;
using System.Threading.Tasks;


namespace WhatsAppTest
{
    public class active_directory_wpf_msgraph_v2
    {
        //Set the API Endpoint to Graph 'me' endpoint. 
        // To change from Microsoft public cloud to a national cloud, use another value of graphAPIEndpoint.
        // Reference with Graph endpoints here: https://docs.microsoft.com/graph/deployments#microsoft-graph-and-graph-explorer-service-root-endpoints
        internal string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";

        //Set the scope for API call to user.read
        public string[] scopes = new string[] { "user.read" };
        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use 8cb10d0a-483d-4455-b82d-72fd18e176e2
        //   - for Microsoft Personal account, use consumers
        //private static string ClientId = "daabb35e-0b44-4d1d-a980-e5b2a7913760";

        //// Note: Tenant is important for the quickstart.
        //private static string Tenant = "8cb10d0a-483d-4455-b82d-72fd18e176e2";
        //private static string Instance = "https://login.microsoftonline.com/";
        //private IPublicClientApplication _clientApp;
        //public IPublicClientApplication PublicClientApp { get; set; }

        //private IAccount _firstAccount;
        //internal IAccount firstAccount { get { return _firstAccount; } set { _firstAccount = value; } }
        //public bool Authenticated { get; set; } = false;  
        //private DeviceCodeCredentialOptions deviceCodeCredentialOptions { get; set; }
        //public DeviceCodeCredential DeviceCodeCredential { get; set; } = null;

        //public active_directory_wpf_msgraph_v2(IPublicClientApplication myPublicClientApp)
        //{
        //    if(myPublicClientApp!=null)
        //    {
        //        PublicClientApp = myPublicClientApp;

        //        // if the user signed-in before, remember the account info from the cache
        //        //IAccount firstAccount = (await app.GetAccountsAsync()).FirstOrDefault();

        //        var fAccount = GetAccount();
        //        firstAccount = (IAccount)fAccount;

        //        // otherwise, try witht the Windows account
        //        if (firstAccount == null)
        //        {
        //            firstAccount = PublicClientApplication.OperatingSystemAccount;
        //        }

        //        AuthenticationResult authResult = null;
        //        try
        //        {
        //            //var auth = GetAuth();
        //            //authResult = (AuthenticationResult)auth; // await app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();

        //            var auth = PublicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
        //            //authResult = auth;
        //        }
        //        catch (MsalUiRequiredException ex)
        //        {
        //            // A MsalUiRequiredException happened on AcquireTokenSilent. 
        //            // This indicates you need to call AcquireTokenInteractive to acquire a token
        //            System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

        //            //try
        //            //{
        //            //    authResult = await app.AcquireTokenInteractive(scopes)
        //            //        .WithAccount(firstAccount)
        //            //        .WithParentActivityOrWindow(new WindowInteropHelper(this).Handle) // optional, used to center the browser on the window
        //            //        .WithPrompt(Prompt.SelectAccount)
        //            //        .ExecuteAsync();
        //            //}
        //            //catch (MsalException msalex)
        //            //{
        //            //    ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            string Error = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
        //            return;
        //        }

        //        if (authResult != null)
        //        {
        //            Authenticated = true;
        //            //Authenticated = authResult.AccessToken
        //            //ResultText.Text = await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken);
        //            //DisplayBasicTokenInfo(authResult);
        //            //this.SignOutButton.Visibility = Visibility.Visible;

        //            // using Azure.Identity;
        //            deviceCodeCredentialOptions = new DeviceCodeCredentialOptions
        //            {
        //                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        //                ClientId = PublicClientApplication_LvsSped4.ClientId,
        //                TenantId = PublicClientApplication_LvsSped4.Tenant,
        //                // Callback function that receives the user prompt
        //                // Prompt contains the generated device code that user must
        //                // enter during the auth process in the browser
        //                DeviceCodeCallback = (code, cancellation) =>
        //                {
        //                    Console.WriteLine(code.Message);
        //                    return Task.FromResult(0);
        //                },
        //            };

        //            DeviceCodeCredential = new DeviceCodeCredential(deviceCodeCredentialOptions);
        //        }
        //    }
        //}



        /// <summary>
        /// Perform an HTTP GET request to a URL using an HTTP Authorization header
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="token">The token</param>
        /// <returns>String containing the results of the GET operation</returns>
        public async Task<string> GetHttpContentWithToken(string url, string token)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        //private async Task<IAccount> GetAccount()
        //{
        //    var vReturn = (await PublicClientApp.GetAccountsAsync()).FirstOrDefault();
        //    return (IAccount)vReturn;
        //}

        //private async Task<AuthenticationResult> GetAuth()
        //{
        //    var vReturn = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
        //    return (AuthenticationResult)vReturn;
        //}
        //private async Task<AcquireTokenSilentParameterBuilder> GetAuth()
        //{
        //    var vReturn = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
        //    //AuthenticationResult authReturn = new AuthenticationResult()
        //    return (AuthenticationResult)vReturn;
        //}
    }
}
