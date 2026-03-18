using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LVS
{
    public class clsASNTransfer
    {
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public clsLagerMeldungen LM;
        public clsASN ASN;
        public clsASNTyp ASNTyp;

        public string QueueDescription { get; set; }
        public bool IsCreateByASNMessageTestCtr = false;
        public bool MessageForSupporter = true;
        public bool MessageForReceiver = true;
        public int iCommunicationPartnerRange = 0;


        /*************************************************************************
         * 
         * **********************************************************************/
        ///<summary>clsASNTransfer / DoASNTransfer</summary>
        ///<remarks>.</remarks>
        public bool DoASNTransfer(Globals._GL_SYSTEM myGLSystem, decimal myAbBereich, decimal myMandand)
        {
            bool reVal = false;
            this.GLSystem = myGLSystem;
            //prüfen, ob für diesen Abreitsbereich der ASNTransfer frei ist
            if (
                (this.GLSystem.sys_ArbeitsbereichID == myAbBereich) &
                (this.GLSystem.sys_MandantenID == myMandand) &
                (this.GLSystem.sys_Arbeitsbereich_ASNTransfer)
               )
            {
                reVal = true;
            }
            else
            {
                if (this.GLSystem.sys_Arbeitsbereich_ASNTransfer)
                {
                    string strMes = "Aus folgenden Gründen kann keine Meldung erzeugt werden:" + Environment.NewLine;
                    if ((this.GLSystem.sys_ArbeitsbereichID != myAbBereich))
                    {
                        strMes += "Angemeldeter Arbeitsbereich: [" + this.GLSystem.sys_ArbeitsbereichID + "] - " + this.GLSystem.sys_Arbeitsbereichsname + Environment.NewLine;
                        strMes += "Artikel - Arbeitsbereich: [" + myAbBereich + "] - " + "" + Environment.NewLine;
                    }
                    if ((this.GLSystem.sys_MandantenID != myMandand))
                    {
                        strMes += "Angemeldeter Mandant: [" + this.GLSystem.sys_MandantenID + "] - " + this.GLSystem.sys_Arbeitsbereichsname + Environment.NewLine;
                        strMes += "Artikel - Mandant: [" + myMandand + "] - " + Environment.NewLine;
                    }
                    //if (!this.GLSystem.sys_Arbeitsbereich_ASNTransfer)
                    //{
                    //    strMes += "ASN Transfer ist deaktiviert ! " + Environment.NewLine;
                    //}
                    clsMessages.Allgemein_InfoTextShow(strMes);
                }
            }
            return reVal;
        }
        ///<summary>clsASNTransfer / CreateLM</summary>
        ///<remarks></remarks>
        public void CreateLM(ref clsLager myLager)
        {
            if (myLager.ASNAction != null) // && (myLager.ASNAction.ListASNActionASNTypActiv.Count>0))
            {
                //Ermitteln der einzelnen Lagermeldungen, die erstellt werden müssen
                clsASNAction ASNAction = myLager.ASNAction;
                if (ASNAction.ASNActionProcessNr > 0)
                {
                    ASNAction.InitClassByAction(ref myLager, ASNAction.ASNActionProcessNr);
                    //Sender ist hier anzusehen als Auftraggeber
                    decimal decAdrAuftraggeber = ASNAction.Auftraggeber;
                    decimal decAdrEmpfaenger = ASNAction.Empfaenger;

                    if ((ASNAction.DictASNAction.Count > 0) && (ASNAction.ListASNActionASNTypActiv.Count > 0))
                    {
                        Dictionary<decimal, clsJobs> DictLM;
                        //List Meldungen vom Empfänger und SL aus der Jobs
                        if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Ausgang)
                        {
                            DictLM = GetDictJobLM(decAdrAuftraggeber,
                                                  decAdrEmpfaenger,
                                                  myLager.Ausgang.AbBereichID,
                                                  myLager.Ausgang.MandantenID,
                                                  myLager.Ausgang.LAusgangsDate,
                                                  ASNAction,
                                                  iCommunicationPartnerRange);
                        }
                        else
                        {
                            DictLM = GetDictJobLM(decAdrAuftraggeber,
                                                  decAdrEmpfaenger,
                                                  myLager.Eingang.AbBereichID,
                                                  myLager.Eingang.MandantenID,
                                                  myLager.Eingang.LEingangDate,
                                                  ASNAction,
                                                  iCommunicationPartnerRange);
                        }

                        List<clsArtikel> listArtToUpdate = new List<clsArtikel>();
                        foreach (KeyValuePair<decimal, clsASNAction> item in ASNAction.DictASNAction)
                        {
                            clsASNAction tmpAction;
                            this.QueueDescription = string.Empty;

                            if (ASNAction.DictASNAction.TryGetValue(item.Key, out tmpAction))
                            {
                                //TEST übergabe virt File
                                myLager.ASNAction = tmpAction.Copy();

                                decimal ASNTypID = tmpAction.ASNTypID;
                                //Prüfen, ob der ASNTyp in der ListLM enthalten ist
                                if (DictLM.ContainsKey(ASNTypID))
                                {
                                    clsJobs tmpJob;
                                    if (DictLM.TryGetValue(ASNTypID, out tmpJob))
                                    {
                                        //die einzelnen Artikel des Eingangs ermitteln
                                        List<clsArtikel> listArt = new List<clsArtikel>();
                                        //zuweisung listArt
                                        switch (ASNAction.ASNActionProcessNr)
                                        {
                                            case clsASNAction.const_ASNAction_Eingang:
                                                listArt = GetArtList(myLager.Eingang.dtArtInLEingang);
                                                break;

                                            case clsASNAction.const_ASNAction_Umbuchung:
                                                switch (tmpAction.ASNTyp.Typ)
                                                {
                                                    case clsASNTyp.const_string_ASNTyp_EML:
                                                    case clsASNTyp.const_string_ASNTyp_EME:
                                                    case clsASNTyp.const_string_ASNTyp_BML:
                                                    case clsASNTyp.const_string_ASNTyp_BME:
                                                    case clsASNTyp.const_string_ASNTyp_STE:
                                                    case clsASNTyp.const_string_ASNTyp_STL:
                                                    case clsASNTyp.const_string_ASNTyp_AML:
                                                    case clsASNTyp.const_string_ASNTyp_AME:
                                                    case clsASNTyp.const_string_ASNTyp_AVL:
                                                    case clsASNTyp.const_string_ASNTyp_AVE:
                                                    case clsASNTyp.const_string_ASNTyp_RLL:
                                                    case clsASNTyp.const_string_ASNTyp_RLE:
                                                        listArt = myLager.Artikel.listArt;
                                                        break;

                                                    case clsASNTyp.const_string_ASNTyp_UBE:
                                                    case clsASNTyp.const_string_ASNTyp_UBL:
                                                        listArt = myLager.Artikel.listArtUB;
                                                        //listArt = myLager.Artikel.listArt;
                                                        //INFO für UB hinterlegen                                                        
                                                        break;

                                                }
                                                break;

                                            case clsASNAction.const_ASNAction_Ausgang:
                                            case clsASNAction.const_ASNAction_RücklieferungSL:
                                                listArt = GetArtList(myLager.Ausgang.dtArtInLAusgang);
                                                break;

                                            case clsASNAction.const_ASNAction_SPLOut:
                                            case clsASNAction.const_ASNAction_SPLIn:
                                            case clsASNAction.const_ASNAction_StornoKorrektur:
                                                listArt = myLager.Artikel.listArt;
                                                break;
                                            default:
                                                listArt = new List<clsArtikel>();
                                                break;
                                        }

                                        List<clsArtikel> tmpArtGutEdiIgnore = new List<clsArtikel>();

                                        switch ((Int32)ASNTypID)
                                        {

                                            case clsASNTyp.const_ASNTyp_LSL:
                                                break;

                                            //****************************************************************************** EM
                                            case clsASNTyp.const_ASNTyp_EME:
                                                //Bei allen anderen Aktion wie EIngang muss die Einheit Artikel lauten
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Eingang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);

                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Eingang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (artCheck.IsKorStVerUse)
                                                            {
                                                                if (!artCheck.GArt.IgnoreEdi)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                }
                                                                else
                                                                {
                                                                    CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                {
                                                                    if (!artCheck.Lagermeldungen.LM_EME)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                    else if (this.IsCreateByASNMessageTestCtr)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (art.IsKorStVerUse)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                }
                                                                else
                                                                {
                                                                    if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                    {
                                                                        if (!art.Lagermeldungen.LM_EME)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;
                                            //******************************************************************************  Bestandsmeldung
                                            case clsASNTyp.const_ASNTyp_BME:
                                                //Bei allen anderen Aktion wie EIngang muss die Einheit Artikel lauten
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Eingang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);
                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Eingang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (artCheck.IsKorStVerUse)
                                                            {
                                                                AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                            }
                                                            else
                                                            {
                                                                if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                {
                                                                    if (!artCheck.Lagermeldungen.LM_BME)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                    else if (this.IsCreateByASNMessageTestCtr)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (art.IsKorStVerUse)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                }
                                                                else
                                                                {
                                                                    if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                    {
                                                                        if (!art.Lagermeldungen.LM_BME)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;
                                            case clsASNTyp.const_ASNTyp_EML:
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Eingang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);
                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Eingang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                            {
                                                                if (!artCheck.Lagermeldungen.LM_EML)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                }
                                                                else if (this.IsCreateByASNMessageTestCtr)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (!art.Lagermeldungen.LM_EML)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                }
                                                                else
                                                                {
                                                                    if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                    {
                                                                        if (!art.Lagermeldungen.LM_EML)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                        }
                                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;

                                            case clsASNTyp.const_ASNTyp_BML:
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Eingang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);
                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Eingang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                            {
                                                                if (!artCheck.Lagermeldungen.LM_BML)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                }
                                                                else if (this.IsCreateByASNMessageTestCtr)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (!art.Lagermeldungen.LM_BML)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                }
                                                                if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
                                                                {
                                                                    if (!art.Lagermeldungen.LM_BML)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                    }
                                                                    else if (this.IsCreateByASNMessageTestCtr)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;

                                            //********************************************************************************** AM 
                                            case clsASNTyp.const_ASNTyp_AME:
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Ausgang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);
                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Ausgang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Umbuchung)
                                                            {
                                                                AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                            }
                                                            else
                                                            {
                                                                if (artCheck.IsKorStVerUse)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                }
                                                                else
                                                                {
                                                                    if (!artCheck.Lagermeldungen.LM_AME)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                    else if (this.IsCreateByASNMessageTestCtr)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, artCheck);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Umbuchung)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                }
                                                                else
                                                                {
                                                                    if (art.IsKorStVerUse)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (!art.Lagermeldungen.LM_AME)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;
                                            case clsASNTyp.const_ASNTyp_AML:
                                                if (ASNAction.ASNActionProcessNr != clsASNAction.const_ASNAction_Ausgang)
                                                {
                                                    tmpJob.EinheitLM = "Artikel";
                                                }
                                                //AddItemToQueue(tmpJob, ASNAction, ref myLager, listArt);
                                                switch (tmpJob.EinheitLM)
                                                {
                                                    case "Ausgang":
                                                        tmpArtGutEdiIgnore = new List<clsArtikel>();
                                                        tmpArtGutEdiIgnore = listArt.Where(x => x.GArt.IgnoreEdi == true).ToList();
                                                        if (tmpArtGutEdiIgnore.Count == 0)
                                                        {
                                                            clsArtikel artCheck = (clsArtikel)listArt[0];
                                                            if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Umbuchung)
                                                            {
                                                                AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                            }
                                                            else
                                                            {
                                                                if (artCheck.IsKorStVerUse)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                }
                                                                else
                                                                {
                                                                    if (!artCheck.Lagermeldungen.LM_AML)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                    }
                                                                    else if (this.IsCreateByASNMessageTestCtr)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, artCheck);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        }

                                                        //CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                        break;
                                                    case "Artikel":
                                                        foreach (clsArtikel art in listArt)
                                                        {
                                                            if (!art.GArt.IgnoreEdi)
                                                            {
                                                                if (ASNAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Umbuchung)
                                                                {
                                                                    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                }
                                                                else
                                                                {
                                                                    if (art.IsKorStVerUse)
                                                                    {
                                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (!art.Lagermeldungen.LM_AML)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                        }
                                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                                        {
                                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                            }
                                                        }
                                                        break;
                                                }
                                                break;

                                            //*********************************************************************************** Avissierung / AA-VW
                                            case clsASNTyp.const_ASNTyp_AVE:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if (art.Lagermeldungen.LM_EME)
                                                    //{
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //listArtToUpdate.Add(art);
                                                    //}

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;
                                            case clsASNTyp.const_ASNTyp_AVL:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //listArtToUpdate.Add(art);
                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }

                                                break;

                                            //********************************************************************************* Rücklieferung
                                            case clsASNTyp.const_ASNTyp_RLL:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //Lieferant auch check auf EM????
                                                    //if (art.Lagermeldungen.LM_EML)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //    listArtToUpdate.Add(art);
                                                    //}
                                                    //else if (this.IsCreateByASNMessageTestCtr)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //}
                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        //Lieferant auch check auf EM????
                                                        if (art.Lagermeldungen.LM_EML)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                            listArtToUpdate.Add(art);
                                                        }
                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;
                                            case clsASNTyp.const_ASNTyp_RLE:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if (art.Lagermeldungen.LM_EME)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //    listArtToUpdate.Add(art);
                                                    //}
                                                    //else if (this.IsCreateByASNMessageTestCtr)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //}

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        if (art.Lagermeldungen.LM_EME)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                            listArtToUpdate.Add(art);
                                                        }
                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;

                                            //********************************************************************************* Storno
                                            case clsASNTyp.const_ASNTyp_STL:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if (art.Lagermeldungen.LM_EML)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //    listArtToUpdate.Add(art);
                                                    //}
                                                    //else if (this.IsCreateByASNMessageTestCtr)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //}
                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        if (art.Lagermeldungen.LM_EML)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                            listArtToUpdate.Add(art);
                                                        }
                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;
                                            case clsASNTyp.const_ASNTyp_STE:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if (art.Lagermeldungen.LM_EME)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //    listArtToUpdate.Add(art);
                                                    //}
                                                    //else if (this.IsCreateByASNMessageTestCtr)
                                                    //{
                                                    //    AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //}
                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        if (art.Lagermeldungen.LM_EME)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                            listArtToUpdate.Add(art);
                                                        }
                                                        else if (this.IsCreateByASNMessageTestCtr)
                                                        {
                                                            AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;

                                            //******************************************************************************* TS Transportschaden
                                            case clsASNTyp.const_ASNTyp_TSL:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //listArtToUpdate.Add(art);

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;

                                            case clsASNTyp.const_ASNTyp_TSE:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //listArtToUpdate.Add(art);

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;

                                            //******************************************************************************* UB Umbuchung
                                            case clsASNTyp.const_ASNTyp_UBL:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if ((art.LVSNrBeforeUB > 0) && (art.UBAltArtID > 0))
                                                    //{
                                                    //    this.QueueDescription = "UB Artikel: LVSNr alt [" + art.LVSNrBeforeUB.ToString() + "] / ID alt [" + art.UBAltArtID.ToString() + "]";
                                                    //}
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                    //listArtToUpdate.Add(art);

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        if ((art.LVSNrBeforeUB > 0) && (art.UBAltArtID > 0))
                                                        {
                                                            this.QueueDescription = "UB Artikel: LVSNr alt [" + art.LVSNrBeforeUB.ToString() + "] / ID alt [" + art.UBAltArtID.ToString() + "]";
                                                        }
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrAuftraggeber, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;

                                            case clsASNTyp.const_ASNTyp_UBE:
                                                foreach (clsArtikel art in listArt)
                                                {
                                                    //if ((art.LVSNrBeforeUB > 0) && (art.UBAltArtID > 0))
                                                    //{
                                                    //    this.QueueDescription = "UB Artikel: LVSNr alt [" + art.LVSNrBeforeUB.ToString() + "] / ID alt [" + art.UBAltArtID.ToString() + "]";
                                                    //}
                                                    //AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                    //listArtToUpdate.Add(art);

                                                    if (!art.GArt.IgnoreEdi)
                                                    {
                                                        if ((art.LVSNrBeforeUB > 0) && (art.UBAltArtID > 0))
                                                        {
                                                            this.QueueDescription = "UB Artikel: LVSNr alt [" + art.LVSNrBeforeUB.ToString() + "] / ID alt [" + art.UBAltArtID.ToString() + "]";
                                                        }
                                                        AddToQueue(tmpJob, myLager, ASNTypID, decAdrEmpfaenger, art);
                                                        listArtToUpdate.Add(art);
                                                    }
                                                    else
                                                    {
                                                        CreateErrorEdiMail(tmpJob.EinheitLM, ref myLager, listArt);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }//Check Dict auf Key

                            }
                        }// foreach Dict
                        //Update der Artikel bei Korrektur Stornierverfahren, wenn alle Meldungen erstellt sind
                        switch (ASNAction.ASNActionProcessNr)
                        {
                            case clsASNAction.const_ASNAction_StornoKorrektur:
                                if (listArtToUpdate.Count > 0)
                                {
                                    for (Int32 x = 0; x <= listArtToUpdate.Count - 1; x++)
                                    {
                                        clsArtikel tmpArt = (clsArtikel)listArtToUpdate[x];
                                        tmpArt.UpdateArtikelForKorrekturStorVerfahren(false);
                                    }
                                }
                                break;
                        }
                    }

                }//ASNAction>0
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEinheit"></param>
        /// <param name="myLager"></param>
        /// <param name="myArtList"></param>
        private void CreateErrorEdiMail(string myEinheit, ref clsLager myLager, List<clsArtikel> myArtList)
        {
            string strMes = string.Empty;
            strMes += "System: " + this.GLSystem.client_MatchCode + "-" + this.GLSystem.client_CompanyName + " - [" + this.GLSystem.sys_ArbeitsbereichID + "] - " + this.GLSystem.sys_Arbeitsbereichsname + Environment.NewLine;
            strMes += " -> Error EDI Message - Gut.IgnoreEDI: " + Environment.NewLine;
            strMes += Environment.NewLine;
            strMes += string.Format("{0} \t\t{1}", "Job - Einheit:", myEinheit) + Environment.NewLine;
            strMes += string.Format("{0} \t\t{1}", "Eingang:", myLager.Eingang.LEingangID.ToString() + "[" + myLager.Eingang.LEingangTableID + "]") + Environment.NewLine;
            strMes += string.Format("{0} \t\t{1}", "E-Datum:", myLager.Eingang.LEingangDate.ToString("dd.MM.yyyy")) + Environment.NewLine;
            if ((myLager.Ausgang is clsLAusgang) && (myLager.Ausgang.LAusgangTableID > 0))
            {
                strMes += string.Format("{0} \t\t{1}", "Ausgang:", myLager.Ausgang.LAusgangID.ToString() + "[" + myLager.Ausgang.LAusgangTableID + "]") + Environment.NewLine;
                strMes += string.Format("{0} \t\t{1}", "A-Datum:", myLager.Ausgang.LAusgangsDate.ToString("dd.MM.yyyy")) + Environment.NewLine;
            }
            strMes += Environment.NewLine;
            strMes += string.Format("{0}", "Artikel:") + Environment.NewLine;
            foreach (clsArtikel art in myArtList)
            {
                strMes += string.Format("{0} \t{1}", " - LVSNr:", art.LVS_ID + "[" + art.ID + "] - Gut Id [" + art.GArtID + "] -> " + art.GArt.ViewID + " - " + art.Gut) + Environment.NewLine;
            }

            clsMail ErrorMail = new clsMail();
            ErrorMail.InitClass(this.GL_User, new clsSystem());
            ErrorMail.Subject = "clsASNTransfer.CreateLM - Info Güterart IgnorEDI";
            ErrorMail.Message = strMes;
            ErrorMail.SendError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myJob"></param>
        /// <param name="myAsnAction"></param>
        /// <param name="myLager"></param>
        /// <param name="myArtList"></param>
        //private void AddItemToQueue(clsJobs myJob, clsASNAction myAsnAction, ref clsLager myLager, List<clsArtikel> myArtList)
        //private void AddItemToQueue(clsJobs myJob, clsASNAction myAsnAction, ref clsLager myLager, List<clsArtikel> myArtList)
        //{
        //    clsArtikel artCheck = (clsArtikel)myArtList[0]; 
        //    switch (myJob.EinheitLM)
        //    {
        //        case "Eingang":

        //            break;
        //        case "Artikel":
        //            foreach (clsArtikel art in myArtList)
        //            {
        //                //if (art.IsKorStVerUse)
        //                //{
        //                //    AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Eingang.Empfaenger, art);
        //                //}
        //                //else
        //                //{
        //                //    if (myAsnAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Eingang)
        //                //    {
        //                //        if (!art.Lagermeldungen.LM_BME)
        //                //        {
        //                //            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Eingang.Empfaenger, art);
        //                //        }
        //                //        else if (this.IsCreateByASNMessageTestCtr)
        //                //        {
        //                //            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Eingang.Empfaenger, art);
        //                //        }
        //                //    }
        //                //    else
        //                //    {
        //                //        AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Eingang.Empfaenger, art);
        //                //    }
        //                //}


        //            }
        //            break;

        //        case "Ausgang":
        //            //clsArtikel artCheck = (clsArtikel)myArtList[0];
        //            //    if (myAsnAction.ASNActionProcessNr == clsASNAction.const_ASNAction_Umbuchung)
        //            //{
        //            //    AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //            //}
        //            //else
        //            //{
        //            //    if (artCheck.IsKorStVerUse)
        //            //    {
        //            //        AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //            //    }
        //            //    else
        //            //    {
        //            //        if (!artCheck.Lagermeldungen.LM_AME)
        //            //        {
        //            //            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //            //        }
        //            //        else if (this.IsCreateByASNMessageTestCtr)
        //            //        {
        //            //            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //            //        }
        //            //    }
        //            //}
        //            var tmpArticleAusgang = myArtList.Select(x => x.GArt.IgnoreEdi == true).ToList();
        //            if (tmpArticleAusgang.Count == 0)
        //            {
        //                if (!artCheck.GArt.IgnoreEdi)
        //                {
        //                    if (artCheck.IsKorStVerUse)
        //                    {
        //                        AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //                    }
        //                    else
        //                    {
        //                        if (!artCheck.Lagermeldungen.LM_AME)
        //                        {
        //                            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //                        }
        //                        else if (this.IsCreateByASNMessageTestCtr)
        //                        {
        //                            AddToQueue(myJob, myLager, myJob.ASNTypID, myLager.Ausgang.Empfaenger, artCheck);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    CreateErrorEdiMail(myJob.EinheitLM, ref myLager, myArtList);
        //                }
        //            }
        //            else
        //            {
        //                CreateErrorEdiMail(myJob.EinheitLM, ref myLager, myArtList);
        //            }
        //            break;
        //    }           

        //}


        ///<summary>clsASNTransfer / AddToQueue</summary>
        ///<remarks>.</remarks>
        private void AddToQueue(clsJobs myJob, clsLager myLager, decimal myASNTypID, decimal myAdrId, clsArtikel myArt)
        {
            clsQueue que = new clsQueue();
            que.GL_User = this.GL_User;
            que.Datum = DateTime.Now;
            que.activ = true;
            que.ASNTypID = myASNTypID;
            que.AdrVerweisID = myAdrId;
            que.ASNAction = myLager.ASNAction.ASNActionProcessNr;
            que.IsVirtFile = myLager.ASNAction.IsVirtFile;
            que.ASNActionTableID = (int)myLager.ASNAction.ID;
            que.UseOldPropertyValue = myLager.ASNAction.UseOldPropertyValue;
            que.AbBereichID = (int)myLager.ASNAction.AbBereichID;
            que.IsCreateByASNMesTestCtr = this.IsCreateByASNMessageTestCtr;
            if (this.QueueDescription is null)
            {
                this.QueueDescription = string.Empty;
            }
            que.Description = this.QueueDescription;
            que.ASNArtId = (int)myJob.ASNArtID;

            switch (myJob.EinheitLM)
            {
                case "Eingang":
                    que.TableName = "LEingang";
                    //test mr 12.07.2023
                    if (myLager.Eingang.LEingangTableID > 0)
                    {
                        que.TableID = myLager.Eingang.LEingangTableID;
                    }
                    else
                    {
                        que.TableID = myLager.LEingangTableID;
                    }
                    que.ASNID = myLager.Eingang.ASN;
                    que.Description += "LEingangTableId:" + myLager.LEingangTableID.ToString();
                    que.Add();
                    break;

                case "Ausgang":
                    que.TableName = "LAusgang";
                    que.TableID = myLager.Ausgang.LAusgangTableID;
                    que.ASNID = 0;
                    que.Add();
                    break;

                case "Artikel":
                    que.TableName = "Artikel";
                    que.TableID = myArt.ID;
                    que.ASNID = myLager.Eingang.ASN;
                    que.Add();
                    break;
            }
        }
        ///<summary>clsASNTransfer / DoASNTransfer</summary>
        ///<remarks>.</remarks>
        public void CreateLM_Eingang(ref clsLager myLager)
        {
            //ASNTypID
            //Dictionary<decimal, string> dictASNTyp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            Dictionary<Int32, string> dictASNTyp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            decimal decASNTypID_EML = dictASNTyp.FirstOrDefault(x => x.Value == "EML").Key;
            decimal decASNTypID_BML = dictASNTyp.FirstOrDefault(x => x.Value == "BML").Key;
            decimal decASNTypID_EME = dictASNTyp.FirstOrDefault(x => x.Value == "EME").Key;
            decimal decASNTypID_BME = dictASNTyp.FirstOrDefault(x => x.Value == "BME").Key;
            List<decimal> listTypID = new List<decimal>()
            {
                decASNTypID_EML,
                decASNTypID_EME,
                decASNTypID_BML,
                decASNTypID_BME
            };
            bool bLM_EML = false;
            bool bLM_EME = false;
            bool bLM_BML = false;
            bool bLM_BME = false;

            //Sender ist hier anzusehen als Auftraggeber
            decimal decAdrSender = myLager.Eingang.Auftraggeber;
            //empf - > EML ; ist auch der Auftraggeber
            decimal decAdrVerweis_Lieferant = myLager.Eingang.Auftraggeber;
            //empf -> EME 
            decimal decAdrVerweis_Empfaenger = myLager.Eingang.Empfaenger;

            //Jobs ermitteln für folngeden Infos
            // - ASNID            
            DataTable dtJob = GetJob(decAdrSender, string.Join(",", listTypID.ToArray()), myLager.Eingang.AbBereichID, myLager.Eingang.MandantenID, myLager.Eingang.LEingangDate);
            if (dtJob.Rows.Count > 0)
            {
                string strEinheit_EML = string.Empty;
                string strEinheit_EME = string.Empty;
                string strEinheit_BML = string.Empty;
                string strEinheit_BME = string.Empty;

                //ASNID
                for (Int32 i = 0; i <= dtJob.Rows.Count - 1; i++)
                {
                    decimal decTypID = (decimal)dtJob.Rows[i]["ASNTypID"];
                    switch (decTypID.ToString())
                    {
                        case "2":
                            //EML=2 
                            //check ob die Mdlung schon versandt wurde
                            //AsnID_EML = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_EML = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_EML = true;
                            break;
                        case "3":
                            //EME=3
                            //AsnID_EME = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_EME = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_EME = true;
                            break;
                        case "15":
                            //EML=2 : BML = 15
                            //check ob die Mdlung schon versandt wurde
                            //AsnID_EML = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_BML = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_BML = true;
                            break;
                        case "16":
                            //EME=3 : BME=16
                            //AsnID_EME = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_BME = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_BME = true;
                            break;

                    }
                }
                //die einzelnen Artikel des Eingangs ermitteln
                List<clsArtikel> listArt = GetArtList(myLager.Eingang.dtArtInLEingang);
                /***************************************************************************
                 *                  EML - Auftraggeber / Lieferant
                 * ************************************************************************/

                //erst alle EML
                if ((bLM_EML) || (bLM_BML))
                {
                    clsQueue que = new clsQueue();
                    que.GL_User = this.GL_User;
                    string strEinheit = string.Empty;
                    if (bLM_EML)
                    {
                        strEinheit = strEinheit_EML;
                    }
                    else
                    {
                        strEinheit = strEinheit_BML;
                    }

                    if (strEinheit.Equals(string.Empty))
                    {
                        strEinheit = "Artikel";
                    }


                    switch (strEinheit)
                    {
                        case "Eingang":
                            //Check, ob die Meldungen schon versandt wurden
                            clsArtikel artCheck = (clsArtikel)listArt[0];
                            if (bLM_EML)
                            {
                                if (!artCheck.Lagermeldungen.LM_EML)
                                {
                                    que.TableName = "LEingang";
                                    que.TableID = myLager.Eingang.LEingangTableID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_EML;
                                    que.AdrVerweisID = decAdrVerweis_Lieferant;
                                    que.ASNID = myLager.Eingang.ASN;
                                    que.Add();
                                }
                            }
                            if (bLM_BML)
                            {
                                if (!artCheck.Lagermeldungen.LM_BML)
                                {
                                    que.TableName = "LEingang";
                                    que.TableID = myLager.Eingang.LEingangTableID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_BML;
                                    que.AdrVerweisID = decAdrVerweis_Lieferant;
                                    que.ASNID = myLager.Eingang.ASN;
                                    que.Add();
                                }
                            }
                            break;

                        case "Artikel":
                            foreach (clsArtikel art in listArt)
                            {
                                if (bLM_EML)
                                {
                                    if (!art.Lagermeldungen.LM_EML)
                                    {
                                        que = new clsQueue();
                                        que.GL_User = this.GL_User;
                                        que.TableName = "Artikel";
                                        que.TableID = art.ID;
                                        que.Datum = DateTime.Now;
                                        que.activ = true;
                                        que.ASNTypID = decASNTypID_EML;
                                        que.AdrVerweisID = decAdrVerweis_Lieferant;
                                        que.ASNID = myLager.Eingang.ASN;
                                        que.Add();
                                    }
                                }
                                if (bLM_BML)
                                {
                                    if (!art.Lagermeldungen.LM_BML)
                                    {
                                        que = new clsQueue();
                                        que.GL_User = this.GL_User;
                                        que.TableName = "Artikel";
                                        que.TableID = art.ID;
                                        que.Datum = DateTime.Now;
                                        que.activ = true;
                                        que.ASNTypID = decASNTypID_BML;
                                        que.AdrVerweisID = decAdrVerweis_Lieferant;
                                        que.ASNID = myLager.Eingang.ASN;
                                        que.Add();
                                    }
                                }
                            }
                            break;
                    }
                }
                /***************************************************************************
                 *                  EME - Empfänger
                 * ************************************************************************/
                //check ob der Partner (Auftraggeber / Lieferant) einen Verweis für ASNTransfer hat
                //Jetzt alle EME
                if (bLM_EME)
                {
                    clsQueue que = new clsQueue();
                    que.GL_User = this.GL_User;
                    switch (strEinheit_EME)
                    {
                        case "Eingang":
                            //Check, ob die Meldungen schon versandt wurden
                            if (listArt.Count > 0)
                            {
                                clsArtikel artCheck = (clsArtikel)listArt[0];
                                if (!artCheck.Lagermeldungen.LM_EML)
                                {
                                    que.TableName = "LEingang";
                                    que.TableID = myLager.Eingang.LEingangTableID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_EME;
                                    que.AdrVerweisID = decAdrVerweis_Empfaenger;
                                    que.ASNID = myLager.Eingang.ASN;
                                    que.Add();
                                }
                            }
                            break;

                        case "Artikel":
                            foreach (clsArtikel art in listArt)
                            {
                                if (!art.Lagermeldungen.LM_EML)
                                {
                                    que = new clsQueue();
                                    que.GL_User = this.GL_User;
                                    que.TableName = "Artikel";
                                    que.TableID = art.ID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_EML;
                                    que.AdrVerweisID = decAdrVerweis_Empfaenger;
                                    que.ASNID = myLager.Eingang.ASN;
                                    que.Add();
                                }
                            }
                            break;
                    }
                }
            }
        }
        ///<summary>clsASNTransfer / CreateLM_Ausgang</summary>
        ///<remarks>.</remarks>
        public void CreateLM_Ausgang(ref clsLager myLager)
        {
            //ASNTypID
            //Dictionary<decimal, string> dictASNTyp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            Dictionary<Int32, string> dictASNTyp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            decimal decASNTypID_AML = dictASNTyp.FirstOrDefault(x => x.Value == "AML").Key;
            decimal decASNTypID_AME = dictASNTyp.FirstOrDefault(x => x.Value == "AME").Key;
            List<decimal> listTypID = new List<decimal>()
            {
                decASNTypID_AML,
                decASNTypID_AME
            };
            bool bLM_AML = false;
            bool bLM_AME = false;
            //Sender ist hier anzusehen als Auftraggeber
            decimal decAdrSender = myLager.Ausgang.Auftraggeber;
            //empf - > EML ; ist auch der Auftraggeber
            decimal decAdrVerweis_AML = myLager.Ausgang.Auftraggeber;
            //empf -> EME 
            decimal decAdrVerweis_AME = myLager.Ausgang.Empfaenger;

            //Jobs ermitteln für folngeden Infos
            // - ASNID            
            DataTable dtJob = GetJob(decAdrSender, string.Join(",", listTypID.ToArray()), myLager.Ausgang.AbBereichID, myLager.Ausgang.MandantenID, myLager.Ausgang.LAusgangsDate);
            if (dtJob.Rows.Count > 0)
            {
                //decimal AsnID_AML = 0;
                //decimal AsnID_AME = 0;
                string strEinheit_AML = string.Empty;
                string strEinheit_AME = string.Empty;
                //ASNID
                for (Int32 i = 0; i <= dtJob.Rows.Count - 1; i++)
                {
                    decimal decTypID = (decimal)dtJob.Rows[i]["ASNTypID"];
                    switch (decTypID.ToString())
                    {
                        case "4":
                            //EML
                            //check ob die Mdlung schon versandt wurde
                            //AsnID_EML = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_AML = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_AML = true;

                            break;
                        case "5":
                            //EME
                            //AsnID_EME = (decimal)dtJob.Rows[i]["ASNArtID"];
                            strEinheit_AME = dtJob.Rows[i]["EinheitLM"].ToString();
                            bLM_AME = true;
                            break;
                    }
                }
                //die einzelnen Artikel des Eingangs ermitteln
                List<clsArtikel> listArt = GetArtList(myLager.Ausgang.dtArtInLAusgang);
                /***************************************************************************
                 *                  EML - Auftraggeber / Lieferant
                 * ************************************************************************/

                //erst alle EML
                if (bLM_AML)
                {
                    clsQueue que = new clsQueue();
                    que.GL_User = this.GL_User;

                    //clsLEingang tmpE = new clsLEingang();  // AML Sollte auf dem Ausgang Basieren 
                    clsLAusgang tmpE = new clsLAusgang();
                    tmpE._GL_User = this.GL_User;

                    switch (strEinheit_AML)
                    {
                        case "Ausgang":
                            //Check, ob die Meldungen schon versandt wurden
                            if (listArt.Count > 0)
                            {
                                clsArtikel artCheck = (clsArtikel)listArt[0];
                                if (!artCheck.Lagermeldungen.LM_EML)
                                {
                                    que.TableName = "LAusgang";
                                    que.TableID = myLager.Ausgang.LAusgangTableID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_AML;
                                    que.AdrVerweisID = decAdrVerweis_AML;
                                    /*
                                     tmpE.LEingangTableID = artCheck.LEingangTableID;
                                     tmpE.FillEingang(); 
                                     */
                                    tmpE.LAusgangTableID = artCheck.LAusgangTableID;
                                    tmpE.FillAusgang();
                                    que.ASNID = tmpE.ASN;
                                    que.Add();
                                }
                            }
                            break;

                        case "Artikel":
                            foreach (clsArtikel art in listArt)
                            {
                                if (!art.Lagermeldungen.LM_EML)
                                {
                                    que = new clsQueue();
                                    que.GL_User = this.GL_User;
                                    que.TableName = "Artikel";
                                    que.TableID = art.ID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_AML;
                                    que.AdrVerweisID = decAdrVerweis_AML;
                                    /*
                                     tmpE.LAusgangTableID = art.LEingangTableID;
                                     tmpE.FillEingang();
                                     */
                                    tmpE.LAusgangTableID = art.LAusgangTableID;
                                    tmpE.FillAusgang();
                                    que.ASNID = tmpE.ASN;
                                    que.Add();

                                }
                            }
                            break;
                    }
                }

                /***************************************************************************
                 *                  EME - Empfänger
                 * ************************************************************************/
                //check ob der Partner (Auftraggeber / Lieferant) einen Verweis für ASNTransfer hat
                //Jetzt alle EME
                if (bLM_AME)
                {
                    clsQueue que = new clsQueue();
                    que.GL_User = this.GL_User;
                    switch (strEinheit_AME)
                    {
                        case "Ausgang":
                            //Check, ob die Meldungen schon versandt wurden
                            clsArtikel artCheck = (clsArtikel)listArt[0];
                            if (!artCheck.Lagermeldungen.LM_EML)
                            {
                                que.TableName = "LAusgang";
                                que.TableID = myLager.Eingang.LEingangTableID;
                                que.Datum = DateTime.Now;
                                que.activ = true;
                                que.ASNTypID = decASNTypID_AME;
                                que.AdrVerweisID = decAdrVerweis_AME;
                                que.ASNID = myLager.Ausgang.ASN;
                                que.Add();
                            }
                            break;

                        case "Artikel":
                            foreach (clsArtikel art in listArt)
                            {
                                if (!art.Lagermeldungen.LM_EML)
                                {
                                    que = new clsQueue();
                                    que.GL_User = this.GL_User;
                                    que.TableName = "Artikel";
                                    que.TableID = art.ID;
                                    que.Datum = DateTime.Now;
                                    que.activ = true;
                                    que.ASNTypID = decASNTypID_AML;
                                    que.AdrVerweisID = decAdrVerweis_AML;
                                    que.ASNID = myLager.Eingang.ASN;
                                    que.Add();
                                }
                            }
                            break;
                    }
                }

            }
        }
        ///<summary>clsASNTransfer / GetArtList</summary>
        ///<remarks>.</remarks>
        private List<clsArtikel> GetArtList(DataTable myDT)
        {
            List<clsArtikel> listArt = new List<clsArtikel>();
            for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(myDT.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsArtikel art = new clsArtikel();
                    art.InitClass(this.GL_User, this.GLSystem);
                    art.ID = decTmp;
                    art.GetArtikeldatenByTableID();

                    listArt.Add(art);
                }
            }
            return listArt;
        }
        ///<summary>clsASNTransfer / clsJobs</summary>
        ///<remarks></remarks>
        private DataTable GetJob(decimal myAdrID, string myTypID, decimal myABereich, decimal myMandant, DateTime myValidFrom)
        {
            string strSQL = string.Empty;
            strSQL = "Select a.* " +
                                "From Jobs a " +
                                "INNER JOIN ASNTyp b ON b.ID=a.ASNTypID " +
                                " WHERE " +
                                        "a.activ=1 " +
                                        "AND a.Direction='OUT' " +
                                        "AND a.ASNTypID IN (" + myTypID + ") " +
                                        "AND a.ArbeitsbereichsID=" + myABereich + " " +
                                        "AND a.MandantenID=" + myMandant + " " +
                                        "AND a.AdrVerweisID=" + myAdrID + " " +
                                        "AND DATEDIFF(day, a.validFrom, '" + myValidFrom + "')>=0 ";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Jobs");
            return dt;
        }
        ///<summary>clsASNTransfer / GetListJobLM</summary>
        ///<remarks></remarks>
        public Dictionary<decimal, clsJobs> GetDictJobLM(decimal myAdrAuftraggeber,
                                                         decimal myAdrEmpf,
                                                         decimal myABereich,
                                                         decimal myMandant,
                                                         DateTime myValidFrom,
                                                         clsASNAction myASNAction,
                                                         int myRange = 0)
        {
            Dictionary<decimal, clsJobs> RetDIct = new Dictionary<decimal, clsJobs>();
            Dictionary<decimal, clsJobs> DictEmpf = new Dictionary<decimal, clsJobs>();
            Dictionary<decimal, clsJobs> DictAuft = new Dictionary<decimal, clsJobs>();

            switch (myRange)
            {
                //nur Lieferant
                case 1:
                    DictAuft = GetDictJobLMByAdr(myAdrAuftraggeber, myABereich, myMandant, myValidFrom, myASNAction.ListASNActionASNTypActiv);
                    RetDIct = DictAuft;
                    break;
                //nur Empfänger
                case 2:
                    DictEmpf = GetDictJobLMByAdr(myAdrEmpf, myABereich, myMandant, myValidFrom, myASNAction.ListASNActionASNTypActiv);
                    RetDIct = DictEmpf;
                    break;
                default:
                    DictEmpf = GetDictJobLMByAdr(myAdrEmpf, myABereich, myMandant, myValidFrom, myASNAction.ListASNActionASNTypActiv);
                    DictAuft = GetDictJobLMByAdr(myAdrAuftraggeber, myABereich, myMandant, myValidFrom, myASNAction.ListASNActionASNTypActiv);
                    RetDIct = DictEmpf;

                    foreach (KeyValuePair<decimal, clsJobs> item in DictAuft)
                    {
                        if (!RetDIct.ContainsKey(item.Key))
                        {
                            RetDIct.Add(item.Key, item.Value);
                        }
                    }
                    break;
            }
            return RetDIct;
        }
        ///<summary>clsASNTransfer / GetDictJobLMByAdr</summary>
        ///<remarks></remarks>
        private Dictionary<decimal, clsJobs> GetDictJobLMByAdr(decimal myADRID, decimal myABereich, decimal myMandant, DateTime myValidFrom, List<int> myASNActionASNTypActiv)
        {
            Dictionary<decimal, clsJobs> RetDIct = new Dictionary<decimal, clsJobs>();
            string strSQL = string.Empty;
            strSQL = "Select a.* " +
                                "From Jobs a " +
                                "INNER JOIN ASNTyp b ON b.TypID=a.ASNTypID " +
                                " WHERE " +
                                "a.activ=1 " +
                                "AND a.Direction='OUT' ";
            if (myASNActionASNTypActiv.Count > 0)
            {
                strSQL += " AND ASNTypID IN(" + string.Join(",", myASNActionASNTypActiv.ToArray()) + ") ";
            }
            //"AND a.ASNTypID IN (" + myTypID + ") " +
            strSQL += "AND a.ArbeitsbereichsID=" + myABereich + " " +
                                "AND a.MandantenID=" + myMandant + " " +
                                "AND a.AdrVerweisID=" + myADRID + " ";

            if (!this.IsCreateByASNMessageTestCtr)
            {
                strSQL += " AND DATEDIFF(day, a.validFrom, '" + myValidFrom.ToString("dd.MM.yyyy") + "')>=0 ";
            }

            strSQL += " order by ASNTypID, validFrom desc";

            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Jobs");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsJobs TmpJob = new clsJobs();
                    TmpJob.InitClass(this.GLSystem, this.GL_User, false);
                    TmpJob.ID = decTmp;
                    TmpJob.Fill();

                    if (!RetDIct.ContainsKey(TmpJob.ASNTypID))
                    {
                        RetDIct.Add(TmpJob.ASNTypID, TmpJob);
                    }
                }
            }
            return RetDIct;
        }
    }
}
