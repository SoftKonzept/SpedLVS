using Common.Models;


namespace Common.Views
{
    public class AddressReferenceView
    {
        /// <summary>
        ///             Verwendet für Dgv in CtrAdrVerweis.cs
        /// </summary>
        /// 
        public AddressReferenceView(AddressReferences myAdrRef, Workspaces myWorkspace)
        {
            AdrReference = myAdrRef;
            Workspace = myWorkspace;
        }

        public AddressReferences AdrReference { get; set; }
        public Workspaces Workspace { get; set; }


        public int Id
        {
            get
            {
                int iTmp = 0;
                if (AdrReference != null)
                {
                    iTmp = AdrReference.Id;
                }
                return iTmp;
            }
        }
        public int SenderAdrId
        {
            get
            {
                int iTmp = 0;
                if (AdrReference != null)
                {
                    iTmp = AdrReference.SenderAdrId;
                }
                return iTmp;
            }
        }
        public int VerweisAdrId
        {
            get
            {
                int iTmp = 0;
                if (AdrReference != null)
                {
                    iTmp = AdrReference.VerweisAdrId;
                }
                return iTmp;
            }
        }

        public int MandantenId
        {
            get
            {
                int iTmp = 0;
                if (AdrReference != null)
                {
                    iTmp = AdrReference.MandantenId;
                }
                return iTmp;
            }
        }
        public string Arbeitsbereich
        {
            get
            {
                string strTmp = string.Empty;
                if (Workspace != null)
                {
                    strTmp = Workspace.Name;
                }
                return strTmp;
            }
        }

        public string SenderReference
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.SenderReference;
                }
                return strTmp;
            }
        }

        public string SupplierRefrence
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.SupplierReference;
                }
                return strTmp;
            }
        }
        public string SupplierNo
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.SupplierNo;
                }
                return strTmp;
            }
        }

        public string ReferenceArt
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.ReferenceArt;
                }
                return strTmp;
            }
        }
        public string Verweis
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.Reference;
                }
                return strTmp;
            }
        }
        public string ASNFileTyp
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.ASNFileTyp;
                }
                return strTmp;
            }
        }
        public bool Activ
        {
            get
            {
                bool bActiv = false;
                if (AdrReference != null)
                {
                    bActiv = AdrReference.IsActive;
                }
                return bActiv;
            }
        }


        public string Bemerkung
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.Remark;
                }
                return strTmp;
            }
        }
        public bool UseS712F04
        {
            get
            {
                bool bTmp = false;
                if (AdrReference != null)
                {
                    bTmp = AdrReference.UseS712F04;
                }
                return bTmp;
            }
        }

        public bool UseS713F13
        {
            get
            {
                bool bTmp = false;
                if (AdrReference != null)
                {
                    bTmp = AdrReference.UseS713F13;
                }
                return bTmp;
            }
        }

        public string Description
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.Description;
                }
                return strTmp;
            }
        }

        public string ReferencePart1
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.ReferencePart1;
                }
                return strTmp;
            }
        }
        public string ReferencePart2
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.ReferencePart2;
                }
                return strTmp;
            }
        }
        public string ReferencePart3
        {
            get
            {
                string strTmp = string.Empty;
                if (AdrReference != null)
                {
                    strTmp = AdrReference.ReferencePart3;
                }
                return strTmp;
            }
        }

    }
}
