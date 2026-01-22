using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsAppTest
{
    public partial class Form1 : Form
    {
        internal active_directory_wpf_msgraph_v2 msgraph_V2 { get; set; } = null;
        //public static IPublicClientApplication PublicClientApp { get; set; }
        
        //internal MsalCacheHelper cacheHelper { get; set; } = null;
        public Form1()
        {
            InitializeComponent();
            // Initialize the form and its components
            tbMobileNr.Text = "+4915510056631"; // Example mobile number
            tbMessage.Text = "Hello, this is a test message!"; // Example message

            //PublicClientApplication_LvsSped4 publicClientApplication_LvsSped4 = new PublicClientApplication_LvsSped4();
            //PublicClientApp = publicClientApplication_LvsSped4.PublicClientApp;

            ////BrokerOptions brokerOptions = new BrokerOptions(BrokerOptions.OperatingSystems.Windows);

            //cacheHelper = CreateCacheHelperAsync().GetAwaiter().GetResult();
            //cacheHelper.RegisterCache(PublicClientApp.UserTokenCache);

        }
        //private static async Task<MsalCacheHelper> CreateCacheHelperAsync()
        //{
        //    // Since this is a WPF application, only Windows storage is configured
        //    var storageProperties = new StorageCreationPropertiesBuilder(
        //                      System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".msalcache.bin",
        //                      MsalCacheHelper.UserRootDirectory)
        //                        .Build();

        //    MsalCacheHelper cacheHelper = await MsalCacheHelper.CreateAsync(
        //                storageProperties,
        //                new TraceSource("MSAL.CacheTrace"))
        //             .ConfigureAwait(false);

        //    return cacheHelper;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            //sendWhatsAppMessageAsync(tbMessage.Text, tbMobileNr.Text);
        }

        //private async Task sendWhatsAppMessageAsync(string message, string number)
        //{
        //    // Implement the logic to send a WhatsApp message here
        //    // You can use a library like Twilio or WhatsApp Business API
        //    // to send messages programmatically.
        //    try 
        //    {
        //        active_directory_wpf_msgraph_v2 msgraph_V2 = new active_directory_wpf_msgraph_v2(PublicClientApp);
        //        var graphClient = new GraphServiceClient(msgraph_V2.DeviceCodeCredential, msgraph_V2.scopes);

        //        var requestBody = new ChatMessage
        //        {
        //            Body = new ItemBody
        //            {
        //                Content = "Hello world",
        //            },
        //        };
        //        // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
        //        var result = await graphClient.Chats["{chat-id}"].Messages.PostAsync(requestBody);


        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during the sending process
        //        MessageBox.Show("Error: " + ex.Message);
        //    }   
        //}
    }
}
