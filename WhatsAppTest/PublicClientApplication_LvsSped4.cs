namespace WhatsAppTest
{
    public class PublicClientApplication_LvsSped4
    {
        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use 8cb10d0a-483d-4455-b82d-72fd18e176e2
        //   - for Microsoft Personal account, use consumers
        public static string ClientId = "daabb35e-0b44-4d1d-a980-e5b2a7913760";

        // Note: Tenant is important for the quickstart.
        //public static string Tenant = "8cb10d0a-483d-4455-b82d-72fd18e176e2";
        //private static string Instance = "https://login.microsoftonline.com/";
        //private static IPublicClientApplication _clientApp;

        //public IPublicClientApplication PublicClientApp { get; set; }

        //public PublicClientApplication_LvsSped4()
        //{
        //    BrokerOptions brokerOptions = new BrokerOptions(BrokerOptions.OperatingSystems.Windows);
        //    // Create a PublicClientApplication instance

        //    _clientApp = PublicClientApplicationBuilder.Create(ClientId)
        //                    .WithAuthority($"{Instance}{Tenant}")
        //                    .WithDefaultRedirectUri()
        //                    .WithBroker(brokerOptions)
        //                    .Build();

        //    PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
        //                    .WithAuthority($"{Instance}{Tenant}")
        //                    .WithDefaultRedirectUri()
        //                    .WithBroker(brokerOptions)
        //                    .Build();
        //}
    }
}
