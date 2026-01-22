using Common.Enumerations;
using Common.Models;
using Common.Views;
using System.Collections.Generic;
using System.Linq;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_ScanSearchArticle_Helper
    {
        public wiz_ScanSearchArticle_Helper()
        {
        }
        public wiz_ScanSearchArticle_Helper(enumAppProcess myAppProcess, enumStoreInArt myStoreInArt, enumStoreOutArt myStoreOutArt) : this()
        {
            AppProcess = myAppProcess;
            StoreInArt = myStoreInArt;
            StoreOutArt = myStoreOutArt;

            switch (AppProcess)
            {
                case enumAppProcess.StoreIn:
                    IsLvsInputVisible = false;
                    IsProductionnoInputVisible = true;
                    StoreOutArt = enumStoreOutArt.NotSet;
                    break;
                case enumAppProcess.StoreOut:
                    IsLvsInputVisible = true;
                    IsProductionnoInputVisible = true;
                    StoreInArt = enumStoreInArt.NotSet;
                    break;
                case enumAppProcess.Inventory:
                case enumAppProcess.StoreLocationChange:
                    IsLvsInputVisible = true;
                    IsProductionnoInputVisible = false;
                    StoreInArt = enumStoreInArt.NotSet;
                    StoreOutArt = enumStoreOutArt.NotSet;
                    break;
            }
        }
        internal enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;
        internal enumStoreInArt StoreInArt { get; set; } = enumStoreInArt.NotSet;
        internal enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;

        public bool IsLvsInputVisible { get; set; } = true;
        public bool IsProductionnoInputVisible { get; set; } = true;

        public AsnArticleView SearchedAsnArticleView { get; set; }

        public bool IsBaseNextEnabeld(bool myExistLvsNr, bool myExistProductionNo)
        {
            bool bReturn = false;
            switch (AppProcess)
            {
                case enumAppProcess.StoreIn:
                    switch (StoreInArt)
                    {
                        case enumStoreInArt.open:
                        case enumStoreInArt.edi:
                            if ((!IsLvsInputVisible) && (IsProductionnoInputVisible))
                            {
                                //Beispiel Einlagerung, da hat man nur die Produktionsnummer
                                bReturn = !(myExistProductionNo);
                            }
                            break;
                    }
                    break;
                case enumAppProcess.StoreOut:
                    if ((IsLvsInputVisible) && (IsProductionnoInputVisible))
                    {
                        bReturn = !(myExistLvsNr && myExistProductionNo);
                    }
                    break;
                case enumAppProcess.StoreLocationChange:
                case enumAppProcess.Inventory:
                    if ((IsLvsInputVisible) && (!IsProductionnoInputVisible))
                    {
                        //Beipiel Umlagerung
                        bReturn = !(myExistLvsNr);
                    }
                    break;

            }
            return bReturn;
        }

        public bool ExistProductionNo(string myValue, List<AsnArticleView> myArticles)
        {
            bool bReturn = false;
            if (IsProductionnoInputVisible)
            {
                AsnArticleView tmpArt = myArticles.FirstOrDefault(x => x.Produktionsnummer == myValue);
                if ((tmpArt != null) && (tmpArt.AsnId > 0))
                {
                    SearchedAsnArticleView = tmpArt.Copy();
                    bReturn = true;
                }
            }
            return bReturn;
        }

        public bool ExistProductionNo(string myValue, List<Articles> myArticles)
        {
            bool bReturn = false;
            if (IsProductionnoInputVisible)
            {
                Articles tmpArt = myArticles.FirstOrDefault(x => x.Produktionsnummer == myValue);
                if ((tmpArt != null) && (tmpArt.Id > 0))
                {
                    bReturn = true;
                }
            }
            return bReturn;
        }

        public bool ExistLvsNo(int myValue, List<Articles> myArticles)
        {
            bool bReturn = false;
            if (IsProductionnoInputVisible)
            {
                Articles tmpArt = myArticles.FirstOrDefault(x => x.LVS_ID == myValue);
                if ((tmpArt != null) && (tmpArt.Id > 0))
                {
                    bReturn = true;
                }
            }
            return bReturn;
        }
    }
}
