using Common.Views;
using LVS.ViewData;
using System.Collections.Generic;
using System.Data;


namespace LVS.ASN
{
    public class clsASNWizzard
    {
        internal Globals._GL_USER GLUser;
        internal Globals._GL_SYSTEM GLSystem;
        public clsADR AuftragggeberAdr;

        //public clsADRVerweis AdrVerweis;
        public AddressReferenceViewData AdrReferenceVD = new AddressReferenceViewData();

        public clsOrga Orga;
        public clsJobs Jobs;
        public clsASNAction AsnAction;
        public clsVDAClientValue VdaClientOut;
        public clsVDAClientWorkspaceValue VdaWorkspaceValue;
        public clsASNArt AsnArt;
        public clsASNArtFieldAssignment ASNArtFieldAssign;
        public clsArbeitsbereiche Arbeitsbereich;


        public clsVDACreate VdaCreate;

        //public DataTable dtAdrVerweise;
        public List<AddressReferenceView> ListAdrReferenceViews;
        public List<string> ListArtikelFieldsAndFunctions
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.AddRange(clsASNFormatFunctions.ListASNFormatFunktions());
                tmpList.AddRange(clsEdiVDAValueAlias.ListValue_Functions);
                tmpList.AddRange(clsEdiVDAValueAlias.ListValue_EA);
                tmpList.AddRange(clsEdiVDAValueAlias.ListValue_Artikel);

                return tmpList;
            }
        }

        public DataTable dtArtikelFieldsAndFunctions
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FieldOrFunktion", typeof(string));
                List<string> tmpList = this.ListArtikelFieldsAndFunctions;

                foreach (string str in tmpList)
                {
                    DataRow row = dt.NewRow();
                    row[0] = str;
                    dt.Rows.Add(row);
                }
                return dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public clsASNWizzard(decimal myAuftraggeberAdrId,
                             Globals._GL_USER myGLUser,
                             Globals._GL_SYSTEM myGLSys,
                             clsASNArt myASNArt)
        {
            GLUser = myGLUser;
            GLSystem = myGLSys;
            AsnArt = myASNArt;

            AuftragggeberAdr = new clsADR();
            AuftragggeberAdr.InitClass(GLUser, myGLSys, myAuftraggeberAdrId, false);

            //--- Fill classes for AdressId
            AdrReferenceVD = new AddressReferenceViewData((int)myAuftraggeberAdrId, (int)myGLUser.User_ID, true);
            ListAdrReferenceViews = AdrReferenceVD.ListAdrReferenceView;
            //AdrVerweis = new clsADRVerweis();
            //AdrVerweis.InitClass(myGLUser);
            //AdrVerweis.SenderAdrID = myAuftraggeberAdrId;
            //InitAdrVerweisList();

            //---ORga
            this.Orga = new clsOrga();
            this.Orga.AdrID = this.AuftragggeberAdr.ID;
            this.Orga.FillByAdrID();

            //--- ASNAction
            this.AsnAction = new clsASNAction();
            this.AsnAction.GL_System = this.GLSystem;
            this.AsnAction.InitClass(ref this.GLUser);
            this.AsnAction.Auftraggeber = this.AuftragggeberAdr.ID;

            //--- Jobs
            this.Jobs = new clsJobs();
            this.Jobs.InitClass(this.GLSystem, this.GLUser, false);
            this.Jobs.AdrVerweisID = this.AuftragggeberAdr.ID;

            //--- VDAClientOut
            this.VdaClientOut = new clsVDAClientValue();
            this.VdaClientOut.InitClass(this.GLUser, this.AuftragggeberAdr.ID, this.AsnArt);

            //----VDAClientWorkspaceValue
            VdaWorkspaceValue = new clsVDAClientWorkspaceValue();
            this.VdaWorkspaceValue.InitClass(this.GLUser, this.AuftragggeberAdr.ID, this.GLSystem.sys_ArbeitsbereichID);

            //--- ASNArtFieldAssignment
            this.ASNArtFieldAssign = new clsASNArtFieldAssignment();
            this.ASNArtFieldAssign.InitClass(this.GLUser, this.GLSystem);

            //--- VdaCreate
            this.VdaCreate = new clsVDACreate();

            //---Arbeitsbereich
            this.Arbeitsbereich = new clsArbeitsbereiche();
            this.Arbeitsbereich.InitCls(this.GLUser, this.GLSystem.sys_ArbeitsbereichID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdressVerweisId"></param>
        public void InitAdressVerweisById(decimal myAdressVerweisId)
        {
            AdrReferenceVD = new AddressReferenceViewData((int)myAdressVerweisId, (int)GLUser.User_ID);
            //AdrVerweis = new clsADRVerweis();
            //AdrVerweis.InitClass(GLUser);
            //AdrVerweis.ID = myAdressVerweisId;
            //AdrVerweis.Fill();
        }

        public void InitAdrVerweisList()
        {
            //ListAdrReferenceViews = new List<AddressReferenceView>();
            AdrReferenceVD.GetADRVerweiseList();
            ListAdrReferenceViews = AdrReferenceVD.ListAdrReferenceView;

            //dtAdrVerweise = AdrVerweis.GetADRVerweiseList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myOrgaId"></param>
        public void InitOrgaById(decimal myOrgaId)
        {
            this.Orga = new clsOrga();
            this.Orga.ID = myOrgaId;
            this.Orga.Fill();
        }

    }
}
