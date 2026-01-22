using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.Views.Login;
using System;
using Xamarin.Forms;

namespace LvsScan.Portable
{
    [QueryProperty("WizardData", "wizData")]
    public partial class App : Application
    {
        internal WizardData WizardData = new WizardData();
        internal Users LoggedUser = new Users();
        public App()
        {

            // init services
            DependencyService.Register<IMessageService, MessageService>();

            InitializeComponent();

            try
            {
                WizardData = new WizardData();
                WizardData.Teststring = "App" + Environment.NewLine;

                //MainPage = new MainPage();
                //MainPage = new FlyoutMenuPage();
                MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts


        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
