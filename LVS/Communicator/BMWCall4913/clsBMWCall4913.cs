using LVS.Constants;
using System;
using System.IO;

namespace LVS
{
    public class clsBMWCall4913
    {
        internal clsASN ASN;
        internal string FilePath { get; set; }
        public string Errortext { get; set; }
        private decimal _BenutzerID;
        public string Filename
        {
            get
            {
                string strReturn = string.Empty;
                if (!this.FilePath.Equals(string.Empty))
                {
                    if (File.Exists(this.FilePath))
                    {
                        strReturn = Path.GetFileName(this.FilePath);
                    }
                }
                return strReturn;
            }
        }
        public string FileNameStored { get; set; }
        //Transportauftrag
        public bool IsTAA
        {
            get
            {
                bool bReturn = false;
                if (this.ASN is clsASN)
                {
                    if ((this.ASN.Job is clsJobs) && (this.ASN.Job.ID > 0))
                    {
                        if ((this.ASN.Job.AsnTyp is clsASNTyp) && (this.ASN.Job.AsnTyp.ID > 0))
                        {
                            bReturn = this.ASN.Job.AsnTyp.Typ.Equals("TAA");
                        }
                    }
                }
                return bReturn;
            }
        }

        public clsCompany Company
        {
            get
            {
                clsCompany tmpComp = new clsCompany();

                if (this.ASN is clsASN)
                {
                    if ((this.ASN.Job is clsJobs) && (this.ASN.Job.ID > 0))
                    {
                        tmpComp.InitCls(this.ASN.GL_User, (int)this.ASN.Job.ArbeitsbereichID, (int)this.ASN.Job.AdrVerweisID);
                    }
                }
                return tmpComp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myASN"></param>
        /// <param name="myFilePath"></param>
        public clsBMWCall4913(clsASN myASN, string myFilePath)
        {
            Errortext = string.Empty;
            this.FilePath = myFilePath;
            this.FileNameStored = this.Filename;
            if (myASN is clsASN)
            {
                this.ASN = myASN;
                if (helper_IOFile.CheckFile(this.FilePath))
                {
                    this.ASN.ASNArt = GetVDA4913Shadow();
                    this.ASN.FillListASN(this.ASN.ASNArt.ID);

                    string strLine = string.Empty;
                    try
                    {
                        strLine = helper_IOFile.ReadFileInLine(this.FilePath);
                        this.ASN.ASNArt.CreateASNStrings(strLine, this.ASN);

                        clsADRVerweis adrverweis = new clsADRVerweis();
                        adrverweis = ediHelper_AdrVerweis.GetAdrVerweis4913(this.ASN);
                        if (adrverweis.ID > 0)
                        {
                            clsASNCall call = CreateCall();

                            if (call.LVSNr > 0)
                            {
                                //Artikeldaten anhand der LVSNR / Arbeitsbereich ermitteln
                                clsArtikel art = GetCallArtikel(call.LVSNr);
                                if ((art is clsArtikel) && (art.ID > 0))
                                {
                                    call.Produktionsnummer = art.Produktionsnummer;
                                    call.Charge = art.Charge;
                                    call.Brutto = art.Brutto;

                                    //Adr-Daten ermitteln für Lieferant / Empfänger
                                    //eventuell über Eingang und Abgleich Lieferantennummer 
                                    //call.LiefAdrID = (int)this.ASN.Job.AdrVerweisID;
                                    call.LiefAdrID = (int)art.Eingang.Auftraggeber;

                                    //Empfänger ist hier immer BMW, das dies eine spezielle Customer-Function ist, als kann hier 
                                    //die VerweisAdr aus dem Job verwendet werden
                                    //call.EmpAdrID = (int)this.ASN.Job.AdrVerweisID;
                                    call.EmpAdrID = (int)art.Eingang.Empfaenger;

                                    //call.IsError = false;
                                    if (this.IsTAA)
                                    {
                                        //TAA - BMW sendet die alte LVSNR 
                                        // jetzt muss aus der alten LVSNR die neue LVSNR bzw. ArtikelID ermittelt werden
                                        if ((art.ArtIDAfterUB > 0) && (art.LVSNrAfterUB > 0))
                                        {
                                            call.ArtikelID = (int)art.ArtIDAfterUB;
                                            call.LVSNr = (int)art.LVSNrAfterUB;

                                            if (call.IsArtikelIdUniqueInCall())
                                            {
                                                try
                                                {
                                                    call.Add();
                                                    if (call.ID > 0)
                                                    {
                                                        this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                        call.ASNFile = this.FileNameStored;
                                                        call.Update();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.Errortext = ex.ToString();
                                                }
                                            }
                                            else
                                            {
                                                //if (call.IsArtikelToReaktivate())
                                                if (
                                                    (call.CallExist is clsASNCall) &&
                                                    (call.CallExist.Status.Equals(clsASNCall.const_Status_deactivated))
                                                  )
                                                {
                                                    //--- Abruf existiert bereits soll aber wieder aktiviert 
                                                    //--- und die neuen Daten entpsprechend gespeichert werden
                                                    call.ID = call.CallExist.ID;
                                                    call.Status = clsASNCall.const_Status_erstellt;
                                                    call.Description = "Abruf reaktiviert!" + Environment.NewLine + call.Description;
                                                    call.Erstellt = DateTime.Now;
                                                    call.ASNFile = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                    try
                                                    {
                                                        call.Update();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        this.Errortext = ex.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    call.FillbyArtikelID();
                                                    this.Errortext += "Doppelter Abruf - Abruf existiert bereits!!!" + Environment.NewLine;
                                                    this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                                    this.Errortext += CreateCallInfoString(ref call);
                                                    this.Errortext += Environment.NewLine;
                                                    this.Errortext += Environment.NewLine;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            call.ArtikelID = (int)art.ID;

                                            if (call.IsArtikelIdUniqueInCall())
                                            {
                                                try
                                                {
                                                    call.Add();
                                                    if (call.ID > 0)
                                                    {
                                                        this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                        call.ASNFile = this.FileNameStored;
                                                        call.Update();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.Errortext = ex.ToString();
                                                }
                                            }
                                            else
                                            {
                                                //if (call.IsArtikelToReaktivate())
                                                if (
                                                    (call.CallExist is clsASNCall) &&
                                                    (call.CallExist.Status.Equals(clsASNCall.const_Status_deactivated))
                                                  )
                                                {
                                                    //--- Abruf existiert bereits soll aber wieder aktiviert 
                                                    //--- und die neuen Daten entpsprechend gespeichert werden
                                                    call.ID = call.CallExist.ID;
                                                    call.Status = clsASNCall.const_Status_erstellt;
                                                    call.Description = "Abruf reaktiviert!" + Environment.NewLine + call.Description;
                                                    call.Erstellt = DateTime.Now;
                                                    call.ASNFile = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                    try
                                                    {
                                                        call.Update();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        this.Errortext = ex.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    call.FillbyArtikelID();
                                                    this.Errortext += "Doppelter Abruf - Abruf existiert bereits!!!" + Environment.NewLine;
                                                    this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                                    this.Errortext += CreateCallInfoString(ref call);
                                                    this.Errortext += Environment.NewLine;
                                                    this.Errortext += Environment.NewLine;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        call.ArtikelID = (int)art.ID;
                                        if (!call.Referenz.Equals(string.Empty))
                                        {
                                            if (call.IsArtikelIdUniqueInCall())
                                            {
                                                try
                                                {
                                                    call.Add();
                                                    //call.ID wurde in call.Add gesetzt (aktualisiert)
                                                    if (call.ID > 0)
                                                    {
                                                        this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                        call.ASNFile = this.FileNameStored;
                                                        call.Update();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.Errortext = ex.ToString();
                                                }
                                            }
                                            else
                                            {
                                                if (
                                                    (call.CallExist is clsASNCall) &&
                                                    (call.CallExist.Status.Equals(clsASNCall.const_Status_deactivated))
                                                  )
                                                {
                                                    //--- Abruf existiert bereits soll aber wieder aktiviert 
                                                    //--- und die neuen Daten entpsprechend gespeichert werden
                                                    call.ID = call.CallExist.ID;
                                                    call.Status = clsASNCall.const_Status_erstellt;
                                                    call.Description = "Abruf reaktiviert!" + Environment.NewLine + call.Description;
                                                    call.Erstellt = DateTime.Now;
                                                    call.ASNFile = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                    try
                                                    {
                                                        call.Update();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        this.Errortext = ex.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    call.FillbyArtikelID();
                                                    this.Errortext += "Doppelter Abruf - Abruf existiert bereits!!!" + Environment.NewLine;
                                                    this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                                    this.Errortext += CreateCallInfoString(ref call);
                                                    this.Errortext += Environment.NewLine;
                                                    this.Errortext += Environment.NewLine;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Es liegt ein Abruf vor, jedoch ohne PUS / AbrufRef
                                            this.Errortext += "PUS/AbrufRef FEHLT" + Environment.NewLine;
                                            this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                            this.Errortext += CreateCallInfoString(ref call);
                                        }
                                    }
                                    ////call.ID wurde in call.Add gesetzt (aktualisiert)
                                    //if (call.ID > 0)
                                    //{
                                    //    this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                    //    call.ASNFile = this.FileNameStored;
                                    //    call.Update();
                                    //}
                                }
                                else
                                {
                                    call.ArtikelID = 0;
                                    //call.IsError = true;
                                    call.Status = clsASNCall.const_Status_Error;
                                    if (!call.Referenz.Equals(string.Empty))
                                    {
                                        if (call.IsArtikelIdUniqueInCall())
                                        {
                                            try
                                            {
                                                call.Add();
                                                if (call.ID > 0)
                                                {
                                                    this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                                    call.ASNFile = this.FileNameStored;
                                                    call.Update();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                this.Errortext = ex.ToString();
                                            }
                                        }
                                        else
                                        {
                                            call.FillbyArtikelID();
                                            this.Errortext += "Doppelter Abruf - Abruf existiert bereits!!!" + Environment.NewLine;
                                            this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                            this.Errortext += CreateCallInfoString(ref call);
                                            this.Errortext += Environment.NewLine;
                                            this.Errortext += Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                    }

                                    //call.ID wurde in call.Add gesetzt (aktualisiert)
                                    //if (call.ID > 0)
                                    //{
                                    //    this.FileNameStored = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.Job.AsnTyp.Typ, call.ID.ToString()) + this.Filename;
                                    //    call.ASNFile = this.FileNameStored;
                                    //    call.Update();
                                    //}
                                }
                            }
                            else
                            {
                                //FEHLERMELDUNG
                                this.Errortext += "LVSNR FEHLT" + Environment.NewLine;
                                this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                                this.Errortext += CreateCallInfoString(ref call);
                            }
                        }
                        else
                        {
                            //FEHLERMELDUNG
                            this.Errortext += "AdrVerweis FEHLT - keine Zuordnung möglich!" + Environment.NewLine;
                            this.Errortext += "FILE: " + this.FileNameStored + Environment.NewLine;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Errortext = this.Errortext + Environment.NewLine + ex.ToString();
                    }
                }
                else
                {
                    this.Errortext = "Dateifehler - Datei nicht gefunden !!!" + Environment.NewLine + "Pfad: " + this.FilePath;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private clsASNArt GetVDA4913Shadow()
        {
            clsASNArt tmp = new clsASNArt();
            tmp.InitClass(ref this.ASN.GL_User, new clsSQLCOM());
            tmp.FillByAsnArt(constValue_AsnArt.const_Art_VDA4913);
            return tmp;
        }
        /// <summary>
        ///         - 711F08 = LVSNR
        ////        - 712F15 = PUSNr // AbrufRef
        ////        - 712F17 = Abrufdatum
        ////        - 712F18 = Abrufzeit
        ////        - 713F05 = Abladestelle ASN7 Konsi
        ////        - 713F11 = Abladestelle //geändert 22.08.2017 Abladestelle ASN 5
        ////        - 713F16 = Lieferantennummer
        ////        - 714F04 = Werksnummer
        ////        - 714F06 = Quantity / Abrufmenge
        ////        - 714F07 = Einheit
        /// </summary>
        /// <returns></returns>
        private clsASNCall CreateCall()
        {
            clsASNCall tmpCall = new clsASNCall();
            tmpCall.InitClass(this.ASN.GL_User, this.ASN.GLSystem, this.ASN.Sys);
            tmpCall.AbBereichID = (int)this.ASN.Job.ArbeitsbereichID;
            tmpCall.IsRead = false;
            tmpCall.ArtikelID = 0;
            tmpCall.LVSNr = 0;
            tmpCall.CompanyID = 0;
            tmpCall.CompanyName = string.Empty;
            if ((this.Company is clsCompany) && (this.Company.ID > 0))
            {
                tmpCall.CompanyID = this.Company.ID;
                tmpCall.CompanyName = this.Company.Shortname;
            }
            tmpCall.BenutzerID = 0;
            tmpCall.Benutzername = "EDI/ASN";
            tmpCall.Datum = DateTime.Now;
            tmpCall.IsCreated = true;
            tmpCall.Status = clsASNCall.const_Status_erstellt;
            tmpCall.Aktion = clsASNCall.const_AbrufAktion_Abruf;
            tmpCall.LiefAdrID = 0;
            tmpCall.EmpAdrID = 0;
            tmpCall.SpedAdrID = 0;

            tmpCall.ASNUnit = string.Empty;
            tmpCall.ASNFile = this.Filename;
            tmpCall.Description = "ASN Abruf/TA per EDI";

            if (this.ASN.ASNArt.asnSatz.ListASNValue.Count > 0)
            {
                int iTmp = 0;
                DateTime dtTmp = Globals.DefaultDateTimeMinValue;

                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.ListASNValue.Count - 1; i++)
                {
                    clsASNValue tmpASNVal = (clsASNValue)this.ASN.ASNArt.asnSatz.ListASNValue[i];
                    switch (tmpASNVal.Kennung)
                    {
                        //LVSNR
                        case clsASN.const_VDA4913SatzField_SATZ711F08:
                            iTmp = 0;
                            int.TryParse(tmpASNVal.Value.ToString().Trim(), out iTmp);
                            tmpCall.LVSNr = iTmp;
                            break;
                        //PUSNr // AbrufRef
                        case clsASN.const_VDA4913SatzField_SATZ712F15:
                            tmpCall.Referenz = tmpASNVal.Value.ToString().Trim();
                            break;
                        //Abrufdatum (Vorlage YYMMDD in DD.MM.YYYY konvertieren
                        case clsASN.const_VDA4913SatzField_SATZ712F17:
                            string strTmp = tmpASNVal.Value.ToString().Trim();
                            string yy = strTmp.Substring(0, 2);
                            string MM = strTmp.Substring(2, 2);
                            string DD = strTmp.Substring(4, 2);

                            strTmp = DD + "." + MM + ".20" + yy;
                            DateTime.TryParse(strTmp, out dtTmp);
                            tmpCall.EintreffDatum = dtTmp;
                            break;
                        //Abrufzeit (Vorlage HH:MM)
                        case clsASN.const_VDA4913SatzField_SATZ712F18:
                            strTmp = string.Empty;
                            strTmp = tmpASNVal.Value.ToString().Trim();
                            string HH = strTmp.Substring(0, 2);
                            string mm = strTmp.Substring(2, 2);

                            strTmp = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy") + " " + HH + ":" + mm;
                            DateTime.TryParse(strTmp, out dtTmp);
                            tmpCall.EintreffZeit = dtTmp;
                            break;

                        //Abladestelle ASN7 Konsi
                        case clsASN.const_VDA4913SatzField_SATZ713F05:
                            tmpCall.Abladestelle = tmpASNVal.Value.ToString().Trim();
                            break;
                        //Abladestelle 22.08.2017 Abladestelle ASN 5
                        case clsASN.const_VDA4913SatzField_SATZ713F11:
                            tmpCall.Abladestelle = tmpASNVal.Value.ToString().Trim();
                            break;

                        //Lieferantennummer
                        case clsASN.const_VDA4913SatzField_SATZ713F16:
                            tmpCall.ASNLieferant = tmpASNVal.Value.ToString().Trim();
                            break;

                        //Werksnummer
                        case clsASN.const_VDA4913SatzField_SATZ714F04:
                            tmpCall.Werksnummer = tmpASNVal.Value.ToString().Trim();
                            break;

                        //Quantity / Abrufmenge
                        case clsASN.const_VDA4913SatzField_SATZ714F06:
                            iTmp = 0;
                            int.TryParse(tmpASNVal.Value.ToString().Trim(), out iTmp);
                            tmpCall.ASNQuantity = iTmp;
                            break;

                        //Einheit
                        case clsASN.const_VDA4913SatzField_SATZ714F07:
                            tmpCall.ASNUnit = tmpASNVal.Value.ToString().Trim();
                            break;
                    }
                }
            }

            if (
                (tmpCall.ASNUnit.Equals("KG")) ||
                (tmpCall.ASNUnit.Equals("kg"))
               )
            {
                if (tmpCall.ASNQuantity > 0)
                {
                    tmpCall.ASNQuantity = tmpCall.ASNQuantity / 1000;
                }
            }
            return tmpCall;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private clsArtikel GetCallArtikel(int myLVSId)
        {
            clsArtikel tmpArt = new clsArtikel();
            decimal decTmp = 0;
            decimal.TryParse(myLVSId.ToString(), out decTmp);
            if (decTmp > 0)
            {
                decimal decArtikelID = clsArtikel.GetArtikelIDByLVSNrAndArbeitsbereich(this.ASN.GL_User, this.ASN.Sys, decTmp, this.ASN.Job.ArbeitsbereichID);
                tmpArt.InitClass(this.ASN.GL_User, this.ASN.GLSystem);
                tmpArt.ID = decArtikelID;
                tmpArt.GetArtikeldatenByTableID();
            }
            return tmpArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCall"></param>
        /// <returns></returns>
        private string CreateCallInfoString(ref clsASNCall myCall)
        {
            string strTmp = string.Empty;
            strTmp = "ID: " + myCall.ID.ToString() + Environment.NewLine +
                     "ArtikelID: " + myCall.ArtikelID.ToString() + Environment.NewLine +
                     "LVSNR: " + myCall.LVSNr.ToString() + Environment.NewLine +
                     "Charge: " + myCall.Charge.ToString() + Environment.NewLine +
                     "ArbeitsbereichID:" + myCall.AbBereichID.ToString() + Environment.NewLine +
                     "PUSNR: " + myCall.Referenz + Environment.NewLine +
                     "Termin: " + myCall.EintreffDatum.ToString("dd.MM.yyyy") +
                     "Zeit: " + myCall.EintreffZeit.ToString("HH:mm");


            strTmp = strTmp + Environment.NewLine + Environment.NewLine;
            foreach (string str in this.ASN.ASNArt.asnSatz.ListSatzString)
            {
                strTmp += str + Environment.NewLine;
            }

            return strTmp;
        }
    }
}
