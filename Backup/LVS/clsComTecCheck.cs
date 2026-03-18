using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsComTecCheck
    {

        public clsSystem System;
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;



        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public Int32 MaxCount { get; set; }
        public Int32 MinCount { get; set; }
        public string InfoTxt { get; set; }



        ///<summary>clsComTecCheck / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(clsSystem mySys, ref Globals._GL_USER myUser, ref Globals._GL_SYSTEM myGLSystem)
        {
            this.System = mySys;
            this._GL_User = myUser;
            this._GL_System = myGLSystem;
        }
        ///<summary>clsComTecCheck / DoLagerOrtCheck</summary>
        ///<remarks></remarks>
        public void DoLagerOrtCheck(bool bArtInStore)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = "SELECT a.ID FROM Artikel a ";
            if (bArtInStore)
            {
                strSql = strSql + "WHERE a.LAusgangTableID=0 ";
            }
            else
            {
                strSql = strSql + "WHERE a.LAusgangTableID>0 ";
            }

            strSql = strSql + " Order BY a.ID DESC";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this._GL_User.User_ID, "Artikel");
            this.MinCount = 0;
            this.MaxCount = dt.Rows.Count;

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsLager Lager = new clsLager();
                    Lager.InitClass(this._GL_User, this._GL_System, this.System);
                    Lager.Artikel.ID = decTmp;
                    //214.09.2014 Test
                    //Lager.Artikel.GetArtikeldatenByTableIDonlyFillDaten();
                    Lager.Artikel.GetArtikeldatenByTableID();

                    if (bArtInStore)
                    {
                        if (Lager.Artikel.LAusgangTableID == 0)
                        {
                            Lager.LagerOrt.ArtikelID = Lager.Artikel.ID;
                            Lager.LagerOrt.LagerPlatzID = Lager.Artikel.LagerOrt;
                            Lager.LagerOrt.LOTable = Lager.Artikel.LagerOrtTable;

                            if (Lager.LagerOrt.LagerPlatzID > 0)
                            {
                                Lager.LagerOrt.InitLagerPlatz();
                            }
                            Lager.Artikel.Werk = Lager.LagerOrt.WerkBezeichnung;
                            Lager.Artikel.Halle = Lager.LagerOrt.HalleBezeichnung;
                            Lager.Artikel.Reihe = Lager.LagerOrt.ReiheBezeichnung;
                            Lager.Artikel.Ebene = Lager.LagerOrt.EbeneBezeichnung;
                            Lager.Artikel.Platz = Lager.LagerOrt.PlatzBezeichnung;

                            Lager.Artikel.UpdateArtikelLagerOrt();
                        }
                    }
                    else
                    {
                        if (Lager.Artikel.LAusgangTableID > 0)
                        {
                            Lager.LagerOrt.ArtikelID = Lager.Artikel.ID;
                            Lager.LagerOrt.LagerPlatzID = Lager.Artikel.LagerOrt;
                            Lager.LagerOrt.LOTable = Lager.Artikel.LagerOrtTable;

                            if (Lager.LagerOrt.LagerPlatzID > 0)
                            {
                                Lager.LagerOrt.InitLagerPlatz();
                            }
                            Lager.Artikel.Werk = Lager.LagerOrt.WerkBezeichnung;
                            Lager.Artikel.Halle = Lager.LagerOrt.HalleBezeichnung;
                            Lager.Artikel.Reihe = Lager.LagerOrt.ReiheBezeichnung;
                            Lager.Artikel.Ebene = Lager.LagerOrt.EbeneBezeichnung;
                            Lager.Artikel.Platz = Lager.LagerOrt.PlatzBezeichnung;

                            Lager.Artikel.UpdateArtikelLagerOrt();
                        }
                    }
                    //infoAusgabe
                    InfoTxt = "ID= " + Lager.Artikel.ID.ToString() + " -> " + Lager.LagerOrt.LagerPlatzBezeichungKomplett;
                }
            }
            //clsMessages.Allgemein_ERRORTextShow("Lagerortcheck wurde durchgeführt!");
        }
        ///<summary>clsComTecCheck / DoLagerOrtCheck</summary>
        ///<remarks></remarks>
        public void CheckADRAndASNAction()
        {
            clsASNAction aAction = new clsASNAction();
            aAction.InitClass(ref this._GL_User);
            aAction.MandantenID = this.System.AbBereich.MandantenID;
            aAction.AbBereichID = this.System.AbBereich.ID;

            //Ermitteln der Standard Listen
            List<clsASNAction> listAAction = aAction.GetASNActionbyADR(this.System.Client.DefaultASNParnter_Emp);
            if (listAAction.Count > 0)
            {
                //Adressen mit bereits vorhandenem Eintrag in ASNAction
                List<Int32> listADRInASNAction = clsASNAction.GetAdrIDWithAssignASNAction(this._GL_User, this.System.Client.DefaultASNParnter_Emp);
                DataTable dtAdressen = clsADR.GetADRList(this._GL_User.User_ID);
                for (Int32 i = 0; i <= dtAdressen.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dtAdressen.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        //prüfen ob bereits Einträge zur Adresse vorhanden sind
                        if (!listADRInASNAction.Contains((Int32)decTmp))
                        {
                            //Eintrag vornehmen anhand der ListAAction
                            for (Int32 x = 0; x <= listAAction.Count - 1; x++)
                            {
                                aAction = (clsASNAction)listAAction[x];
                                //Werte setzen
                                aAction.ID = 0;
                                aAction.Auftraggeber = decTmp;
                                switch ((Int32)aAction.ASNTypID)
                                {
                                    case 3:
                                    case 5:
                                    case 9:
                                    case 19:
                                        aAction.activ = true;
                                        break;

                                    default:
                                        aAction.activ = false;
                                        break;
                                }
                                aAction.Add();
                            }
                        }
                    }
                }
            }
        }

    }
}
