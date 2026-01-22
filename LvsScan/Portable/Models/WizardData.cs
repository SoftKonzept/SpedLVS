using Common.Enumerations;
using Common.Models;
using Common.Views;
using System;
using System.Collections.ObjectModel;

namespace LvsScan.Portable.Models
{
    internal class WizardData
    {
        public WizardData()
        { }
        public static WizardData Init(enumAppProcess appProcess, WizardData myWizData, Users myUser)
        {
            WizardData tmpReturn = myWizData.Copy();
            tmpReturn.LoggedUser = myUser;

            switch (appProcess)
            {
                case enumAppProcess.Inventory:
                    tmpReturn.Wiz_Inventory = new wizInventory();
                    if (myWizData.Wiz_Inventory != null)
                    {
                        tmpReturn.Wiz_Inventory = myWizData.Wiz_Inventory.Copy();
                    }
                    break;
                case enumAppProcess.StoreIn:
                    tmpReturn.Wiz_StoreIn = new wizStoreIn();
                    if (myWizData.Wiz_StoreIn != null)
                    {
                        tmpReturn.Wiz_StoreIn = myWizData.Wiz_StoreIn.Copy();
                    }
                    break;
                case enumAppProcess.StoreOut:
                    tmpReturn.Wiz_StoreOut = new wizStoreOut();
                    if (myWizData.Wiz_StoreOut != null)
                    {
                        tmpReturn.Wiz_StoreOut = myWizData.Wiz_StoreOut.Copy();
                    }
                    break;

                case enumAppProcess.StoreLocationChange:
                    tmpReturn.Wiz_StoreLocationChange = new wizStoreLocationChanged();
                    if (myWizData.Wiz_StoreLocationChange != null)
                    {
                        tmpReturn.Wiz_StoreLocationChange = myWizData.Wiz_StoreLocationChange.Copy();
                    }
                    break;
                default:
                    break;
            }
            return tmpReturn;
        }

        public string Teststring { get; set; } = string.Empty;
        public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;
        public wizInventory Wiz_Inventory { get; set; } = new wizInventory();
        public wizStoreLocationChanged Wiz_StoreLocationChange { get; set; } = new wizStoreLocationChanged();

        public wizStoreOut Wiz_StoreOut { get; set; } = new wizStoreOut();
        public wizStoreIn Wiz_StoreIn { get; set; } = new wizStoreIn();

        public Users LoggedUser { get; set; }


        public ObservableCollection<AsnArticleView> SaveAsnArticleList { get; set; } = new ObservableCollection<AsnArticleView>();
        public ObservableCollection<AsnLfsView> SaveAsnLfsList { get; set; } = new ObservableCollection<AsnLfsView>();

        public DateTime SaveAsnListTimeStamp { get; set; } = new DateTime(1900, 1, 1);

        public WizardData Copy()
        {
            return (WizardData)this.MemberwiseClone();
        }
    }
}
