using LvsScan.Portable.ViewModels.TestWizard;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Wizard;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.TestWizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tw2_wizHost : ContentPage
    {
        public tw2_wizHost()
        {
            var wizardPageItem1 = new WizardItemViewModel("Schritt 1 von 2", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            //var wizardPageItem1 = new WizardItemViewModel("Schritt 1 von 3", typeof(tw2w1_wizView), new tw2w1_wizViewModel());
            var wizardPageItem2 = new WizardItemViewModel("Schritt 2 von 2", typeof(tw2w2_wizView), new tw2w2_wizViewModel());
            //var wizardPageItem3 = new WizardItemViewModel("Schritt 3 von 3", typeof(eaw2w3_wizView), new eaw2w3_wizViewModel());


            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>() { wizardPageItem1, wizardPageItem2 } //, wizardPageItem3 }
                            , false
                            , "CHECK"
                            , "<"
                            , "ZUR ARTIKELLISTE"
                            , "<<"
                            , Color.Blue
                        );

            InitializeComponent();

        }
    }
}