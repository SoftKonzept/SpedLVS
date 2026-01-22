using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace LVS
{
    public class clsMessages
    {

        //---Dispo/Fahrerlite 
        private string _DispoFahrer_EmptyFahrerListe;
        //  private string _DispoDragDrop_GewichtsCheck;
        //--ctrAuftrag-AuftragPDF existiert
        private string _ctrAuftrag_AuftragPDFnotExists;

        //---KommiCtr 
        private string _SetKommiPosition_GesamtgewichtZuHoch;
        private string _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo;


        /*************************************************************************************************
         *                      MailingList  -  EMailverteiler
         * ***********************************************************************************************/
        ///<summary>clsMessages/FrmMain_Close</summary>
        ///<remarks>Abfrage ob das Programm wirklich beendet werden soll</remarks>
        public static bool FrmMain_Close()
        {
            string Text = "Soll das Programm wirklich beendet werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /*************************************************************************************************
         *                      MailingList  -  EMailverteiler
         * ***********************************************************************************************/
        ///<summary>clsMessages/MailingList_MailingListNameExist</summary>
        ///<remarks>Info, wenn bereits eine MailingList mit der Bezeichnung für einen Adressdatensatz in 
        ///         einem Arbeitsbereich hinterlegt wurde.</remarks>
        public static void MailingList_MailingListNameExist()
        {
            string Text = "Es existiert bereits ein E-Mailverteiler mit der selben Bezeichnung. Bitte verwenden Sie eine andere Bezeichnung.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages/MailingList_Delete</summary>
        ///<remarks>Checkabfrage Löschung</remarks>
        public static bool MailingList_Delete()
        {
            string Text = "Soll der ausgewählte Datensatz wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /************************************************************************************************
         *                          MAIL
         * *********************************************************************************************/
        ///<summary>clsMessages/Mail_MailReceiverMissing</summary>
        ///<remarks></remarks>
        public static void Mail_MailReceiverMissing()
        {
            string Text = "Die E-Mail kann nicht versendet werden, da kein E-Mailempfänger ausgewählt wurde.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        /************************************************************************************************
         *                                  Print
         * *********************************************************************************************/
        ///<summary>clsMessages/Mail_MailReceiverMissing</summary>
        ///<remarks></remarks>
        public static void Print_Fail_ReportAssignment()
        {
            string Text = "Der Druckvorgang kann nicht durchgeführt werden. Es wurde kein Dokument zugewiesen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //***********************************++ Fahrzeuge *************************************************
        //
        //------------- Fahrzeug Daten ---------------------------
        //
        public static bool Fahrzeug_DeleteDatenSatz()
        {
            string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //--------- KFZ Kennzeichen ist bereits vorhanden --------
        //
        public static void Fahrzeug_KennzeichenBereitsVorhanden()
        {
            string Text = "Das Kennzeichen ist bereits in der Datenbank vorhanden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        //
        //
        /*****************************************************************************************************
         *                                              Personal                                              
         *                                              
         * ***************************************************************************************************/
        //
        //------------- Personal Daten ---------------------------
        //
        public static bool Personal_DeleteDatenSatz()
        {
            string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //-------------   Liste Dispo Fahrer -----------------------------
        //
        public string DispoFahrer_EmptyFahrerListe
        {
            get
            {
                _DispoFahrer_EmptyFahrerListe = "Keine Fahrer in  der Personaldatenbank gefunden!";
                return _DispoFahrer_EmptyFahrerListe;
            }
            set { _DispoFahrer_EmptyFahrerListe = value; }

        }
        //
        //
        //
        /*************************************************************************************************************
         * 
         *                                               Tarife
         * ***********************************************************************************************************/
        ///<summary>clsMessages / Tarife_KonditionenSaved</summary>
        ///<remarks>Tarif löschen - Info</remarks>
        public static void Tarife_KonditionenSaved()
        {
            string Text = "Die gewählten Frachtkonditionen wurden gespeichert!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages / Tarife_DeleteTarif</summary>
        ///<remarks>Tarif löschen - Abfrage</remarks>
        public static bool Tarife_DeleteTarif()
        {
            string Text = "Soll der Tarif wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / Tarife_DeleteDeniedofMasterTarifPos</summary>
        ///<remarks></remarks>
        public static void Tarife_DeleteDeniedofMasterTarifPos()
        {
            string Text = "Der gewählte Tarifdatensatz ist eine Master-Tarifposition und kann nicht gelöscht werden. Der Tarifdatensatz wird inaktiv gesetzt.";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        ///<summary>clsMessages / Tarife_DeleteDeniedofMasterTarifPos</summary>
        ///<remarks></remarks>
        public static void Tarife_EingabeFehlerStaffelPreis()
        {
            string Text = "Die Eingabe beim Feld €/Einheit ist fehlerhaft.";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        ///<summary>clsMessages / Tarife_DeleteTarifPosition</summary>
        ///<remarks>Tarifposition löschen - Abfrage</remarks>
        public static bool Tarife_DeleteTarifPosition()
        {
            string Text = "Soll die Tarifposition wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / Tarife_DeleteStaffelTarifPosition</summary>
        ///<remarks>Selectierte Staffel-Tarifposition löschen - Abfrage</remarks>
        public static bool Tarife_DeleteSelectedStaffelTarifPosition()
        {
            string Text = "Soll die gewählte Staffelposition gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / Tarife_DeleteStaffelTarifPosition</summary>
        ///<remarks>Tarifposition löschen - Abfrage</remarks>
        public static bool Tarife_DeleteStaffelTarifPosition()
        {
            string Text = "Sollen alle  Staffelpositionen wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /***********************************************************************************************************
        *                               Call
        * ********************************************************************************************************/
        ///<summary>clsMessages / FreeForCall_FreigabeVolumenNull</summary>
        ///<remarks>Freigabe Volumen für die Freigabe der Abrufe nicht gesetzt</remarks>
        public static bool Call_CreateCall()
        {
            string Text = "Soll der Abruf mit den angegebenen Daten erstellt bzw. geändert werden?";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / FreeForCall_FreigabeVolumenNull</summary>
        ///<remarks>Freigabe Volumen für die Freigabe der Abrufe nicht gesetzt</remarks>
        public static bool Call_Activate()
        {
            string Text = "Der Abruf ist deaktiviert. Soll der Abruf aktiviert werden?";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / FreeForCall_FreigabeVolumenNull</summary>
        ///<remarks></remarks>
        public static bool Call_DeleteCall()
        {
            string Text = "Soll der Abruf unwiderruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /***********************************************************************************************************
        *                                Freigabe für Abruf / Free For Call
        * ********************************************************************************************************/
        ///<summary>clsMessages / FreeForCall_FreigabeVolumenNull</summary>
        ///<remarks>Freigabe Volumen für die Freigabe der Abrufe nicht gesetzt</remarks>
        public static void FreeForCall_FreigabeVolumenNull()
        {
            string Text = "Das Freigabe-Volumen wurde nicht festgesetzt. Die Funktion kann nicht ausgeführt werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        /************************************************************************************************************
         * 
         * *********************************************************************************************************/
        ///<summary>clsMessages / RL_DoRL</summary>
        ///<remarks></remarks>
        public static bool RL_DoRL()
        {
            string Text = "Soll die Rücklieferung für diesen Artikel ausgeführt werden? (ein Artikel je RL)";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /***********************************************************************************************************
         *                                  Umbuchung
         * ********************************************************************************************************/
        ///<summary>clsMessages / UB_Ok</summary>
        ///<remarks></remarks>
        public static void UB_Ok()
        {
            string Text = "Die Umbuchung wurde erfolgreich durchgeführt!";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void UB_Failed()
        {
            string Text = "Die Umbuchung konnte nicht korrekt durchgeführt werden!";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /***************************************************************************************************
         * 
         * *************************************************************************************************/
        ///<summary>clsMessages / Artikel_GetAllGArtenData</summary>
        ///<remarks></remarks>
        public static bool Artikel_GetAllGArtenData()
        {
            string Text = "Sollen die Daten (Abmessung, Gewicht) der gewählten Güterart dem Artikel hinterlegt werden?";
            DialogResult result = MessageBox.Show(Text, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /***********************************************************************************************************
         *                                          Abrechnung / Fakturierung
         * 
         * *********************************************************************************************************/
        ///<summary>clsMessages / ExistRGId</summary>
        ///<remarks>Check, ob die ID existiert.</remarks>
        public static void Fakturierung_LagerRGInsertFailed()
        {
            string Text = "Die Rechnung konnte nicht erstellt und gespeichert werden. Ein Fehlerprotokoll wurde angelegt. Bitte setzen Sie sich mit dem Support der Firma ComTEC-Nöker in Verbindung.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ///<summary>clsMessages / Fakturierung_Delete</summary>
        ///<remarks>Check, ob das Dokument gelöscht werden soll.</remarks>
        public static bool Fakturierung_Delete()
        {
            string Text = "Soll die Rechnung/Gutschrift unwiederruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / Fakturierung_Delete</summary>
        ///<remarks>Check, ob das Dokument gelöscht werden soll.</remarks>
        public static bool Fakturierung_Korrektur()
        {
            string Text = "Soll die Rechnung korrigiert werden?";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //
        public static void Fakturierung_DeniedLöschenRechnung()
        {
            string Text = "Es können nur manuelle Rechnungen / Gutschriften gelöscht werden. Alle anderen Rechnungen müssen storniert werden.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        //
        public static void Fakturierung_LesenFrachtdaten()
        {
            string Text = "Die Frachtdaten konnten nicht gelesen werden. Die Auftragspositions-ID fehlt!";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        //
        public static void Fakturierung_LesenFrachtdatenByRGID()
        {
            string Text = "Die Frachtdaten konnten nicht gelesen werden. Die RG-/ GS-Nummer fehlt!";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        //
        //
        //
        public void Fakturierung_EingabenFehlerhaft(string Text)
        {
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        public static void Fakturierung_KmAngabeFehlt()
        {
            string Text = "Aufgrund der fehlenden KM-Angabe kann die Frachtberechnung nicht durchgeführt werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Fakturierung_MargeFalsch()
        {
            string Text = "Der Margenwert in € ist nicht korrekt!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static bool Fakturierung_GSDatum()
        {
            string Text = "Ist das Gutschriftsdatum wirklich das heutige Datum? Bestätigen Sie mit OK, wenn das Datum korrekt ist.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsMessages / Fakturierung_RGBookNORG</summary>
        ///<remarks>Die Meldun wenn bereits alle Rechnungen im Rechnungsbuch gedruckt worden sind</remarks>
        public static void Fakturierung_RGBookNoRG()
        {
            string Text = "Es liegen keine neuen Rechnungen vor.";
            DialogResult result = MessageBox.Show(Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        ///<summary>clsMessages / Fakturierung_RGBookNORG</summary>
        ///<remarks>Die Meldun wenn bereits alle Rechnungen im Rechnungsbuch gedruckt worden sind</remarks>
        public static void Fakturierung_RGBookPrinted()
        {
            string Text = "Diese Rechung wurde bereits für das Rechnungsbuch ausgedruckt!";
            DialogResult result = MessageBox.Show(Text, "AcCHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages / Fakturierung_RGBookNORG</summary>
        ///<remarks>Die Meldun wenn bereits alle Rechnungen im Rechnungsbuch gedruckt worden sind</remarks>
        public static bool Fakturierung_RGBookPrintAgain()
        {
            string Text = "Das Rechnungsbuch wurde bereits gedruckt. Soll das Rechnungsbuch nocheinmal gedruckt werden?";
            DialogResult result = MessageBox.Show(Text, "AcCHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        //-------------------- bereits gedruckt RG / GS soll noch einmal gedruckt werden ------------------
        //
        public static bool Fakturierung_RGGSPrintAgain()
        {
            string Text = "Die Rechnung / Gutschrift wurde schon gedruckt. Soll noch einmal gedruckt werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //
        //----------- Check Auftragszuweisung Auftrags- und Auftragspositionsnummer -----------------
        //
        public static void Fakturierung_AuftragszuweisungCheckAuftrag()
        {
            string Text = "Ein Auftrag mit der eingegebenen Auftrags- und Auftragspositionsnummer wurde nicht gefunden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //----------- Check Auftragszuweisung Auftrags- und Auftragspositionsnummer -----------------
        //
        public static void Fakturierung_FrachtFehlt()
        {
            string Text = "Fracht für Rechnung / Gutschrift fehlt!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //----------- manuelle RG / GS Erstellung /kein Auftraggeber gewählt -----------------
        //
        public static void Fakturierung_AuftraggeberFehlt()
        {
            string Text = "Rechnung / Gutschrift kann nicht erstellt werden, da kein Rechnungs-/Gutschriftsempfänger ausgewählt wurde.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static void Fakturierung_DatenfeldArtikelIsNull()
        {
            string Text = "Berechnung nicht korrekt. Im Tarif ist das Feld Artikel-Datenfeld nicht korrekt ausgewählt." +
                          "Korrigieren Sie den Tarif. Bei Fragen setzen Sie sich bitte mit dem Support der Firma comTEC-Nöker GmbH in Verbidnung.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //+++++++++++++ Auftragsliste +++++++++++++
        //
        public static void Auftragsliste_ZeitraumAuswahlFalsch()
        {
            string Text = "Das Startdatum der Suche ist größer wie das Enddatum der Suche. Dies ist nicht möglich. Bitte erneut auswählen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                //return true;
            }
            else
            {
                //return false;
            }
        }
        public static void Auftragsliste_SULIste_ZeitraumAuswahlFalsch()
        {
            string Text = "Das Startdatum der Suche darf nicht in der Zukunft liegen. Dies ist nicht möglich. Bitte erneut auswählen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                //return true;
            }
            else
            {
                //return false;
            }
        }
        public static void Auftragsliste_AuftragPosLoeschenNichtMoeglich()
        {
            string Text = "Die Auftragposition = 0  kann nicht aufgelöst werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        public static void Auftragserfassung_ADRTakeOverDistanceCenter()
        {
            string Text = "Es müssen die Versand- und Empfangsadresse ausgewählt sein.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        /***********************************************************************************************************
         *                                          Schaäden
         * 
         * *********************************************************************************************************/
        //
        public static void Schaeden_Exist()
        {
            string Text = "Es existiert bereits ein Schaden mit der Schadensbezeichung. Bitte änndern Sie die Eingabe entsprechend.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        public static void Schaeden_DeletDenied()
        {
            string Text = "Der Schaden kann nicht gelöscht werden, da der Datensatz verwendet wird. Der Schaden kann nur passiv gesetzt werden.";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static bool Schaeden_Delete()
        {
            string text = "Soll der Schaden aus dem Artikeldatensatz gelöscht werden?";
            DialogResult result = MessageBox.Show(text, "INFORMATION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        /***********************************************************************************************************
         *                                          Direct Delivery 
         * ********************************************************************************************************/
        //
        public static void DirectDelivery_KeineArtikeldatenVorhanden()
        {
            string Text = "Für die Direktanlieferung sind keine Artikel vorhanden.";
            DialogResult result = MessageBox.Show(Text, "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //
        //
        /***********************************************************************************************************
  *                                          Einheiten
  * ********************************************************************************************************/
        //
        public static void Einheiten_BezeichnungExist()
        {
            string Text = "Es existiert bereits eine Einheit mit der Bezeichung. Bitte änndern Sie die Eingabe entsprechend.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //
        /******************************************************************************************************
         *                                      DIsposition
         *
         * ****************************************************************************************************/
        //
        //------------- Abfrage erhöhtes Gewicht   ----------------------
        //
        public static bool Disposition_DragDropGewichtsCheck(string Ladungsgewicht, string maxZuladung)
        {
            string text = "Gewicht Transportauftrag [kg]: " + Ladungsgewicht + "\r\n" +
                          "max. Zuladungsgewicht [kg]: " + maxZuladung + "\r\n" +
                          "Das aktuelle Ladungsgewicht überschreitet das max. Zuladungsgewicht. Soll der Auftrag dennoch disponiert werden?";
            DialogResult result = MessageBox.Show(text, "Check Ladungsgewicht!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void Disposition_RecourcenFehlen(string fehlendeRecource)
        {
            string text = "Transportauftrag kann nicht disponiert werden, da folgende Recourcen noch nicht zugwiesen wurden. \r\n " +
                          fehlendeRecource;
            MessageBox.Show(text, "Check Ressourcen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public string ctrAuftrag_AuftragPDFnotExists
        {
            get
            {
                _ctrAuftrag_AuftragPDFnotExists = "Zu diesem Auftrag kann keine Datei gefunden werden";
                return _ctrAuftrag_AuftragPDFnotExists;
            }
            set { _ctrAuftrag_AuftragPDFnotExists = value; }
        }
        //
        //
        public static void Disposition_AuftragNichtDisponierbar()
        {
            string Text = "Dieser Auftrag kann nicht disponiert werden. Es fehlen Auftragsdaten oder Auftrag ist hahon disponiert bzw. durchgeführt.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                //return true;
            }
            else
            {
                //return false;
            }
        }
        //
        //
        //
        public static void Disposition_RecourcenEndZeitZuKlein()
        {
            string Text = "Die Resourcenendzeit kann nicht vor der Resourcenstartzeit liegen. Die Resourcenendzeit wurde automatisch geändert auf Resourcenstartzeit + 3 Stunden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                //return true;
            }
            else
            {
                //return false;
            }
        }
        public static void Disposition_MinimaleKommissionsanzahlinTourErreicht()
        {
            string Text = "Es muss mindestens eine Kommission einer Tour zugewiesen sein. Falls die komplette Tour entfernt werden soll, kann dies über den Dispoplan erreicht werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        public static void Disposition_EntladeZeitZuKlein()
        {
            string Text = "Die Entladezeit kann nicht vor der Beladezeit liegen. Bitte die Eingabe noch einmal überprüfen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                //return true;
            }
            else
            {
                //return false;
            }

        }
        //
        //------------------ Änderung Recourcenentzeit - Gewähltes Datum falsch
        //
        public static void Disposition_RecourcenentzeitDatumFalsch()
        {
            string Text = "Das gewählte Datum liegt vor dem min. Recourcenentzeitpunkt. Der neue Recourcenentzeitpunkt muss geändert werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        //
        //
        /*****************************************************************************************************+
         *                                            Kommission    
         *                                                                                        
         ******************************************************************************************************/
        //-------- KommiCtr
        //
        //
        public string SetKommiPosition_GesamtgewichtZuHoch
        {
            get
            {
                _SetKommiPosition_GesamtgewichtZuHoch = "Die Gewicht der beiden Aufträge ist zu groß! Sollen die Aufträge dennoch kombiniert werden?";
                return _SetKommiPosition_GesamtgewichtZuHoch;
            }
            set { _SetKommiPosition_GesamtgewichtZuHoch = value; }
        }
        public string SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo
        {
            get
            {
                _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo = "Die Gewicht der beiden Aufträge ist zu groß und kann nicht kombiniert werden!";
                return _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo;
            }
            set { _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo = value; }
        }
        //
        //------------- Gewicht überschritten - Abfrag ob Kommission wieder entfernt werden soll --------------
        //
        public static bool AuftragEingabe_Gewialue_Abbruch()
        {
            string Text = "Das eingegebenen Gewicht ist gleich 0.\n Bitte Abbrechen um den Speichervorgang zu beenden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //********************************************************* Auftragerfassung *******************************************************************
        //
        //
        public static bool AuftragEingabe_GewichtDefaultValue_Abbruch()
        {
            string Text = "Das eingegebenen Gewicht ist gleich 0.\n Bitte Abbrechen um den Speichervorgang zu beenden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Auftragserfassung_ADRCheck(string Text)
        {
            DialogResult result = MessageBox.Show(Text, "Check Angabe Adressen", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static void Auftragserfassung_AuftragGespeichert()
        {
            string Text = "Die Auftragsdaten wurden erfolgreich gespeichert!";
            DialogResult result = MessageBox.Show(Text, "Check Angabe Adressen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //************************************************************ Auftragssplitting *****************************************************************
        //
        //
        public static void Auftragssplitting_SplittingGewichtZuHoch()
        {
            string text = "Es können nicht alle Artikel ausgewählt werden! Bitte Auswahl ändern.";
            MessageBox.Show(text, "Check Auftragssplitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //-- wenn das gesplittete Gewicht zu groß für eine Ladung (ca 25 to ) ist ------
        //
        public static bool Auftragsplitting_SplittingGewichtIstKeinLadungsgewicht()
        {
            string Text = "Das ausgewählte Gewicht überschreitet das normale Ladungsgewicht von 25 to! Soll das Splitting dennoch vorgenommen werden?";
            DialogResult result = MessageBox.Show(Text, "Check Auftragssplitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //-- wenn das gesplittete Gewicht zu groß für eine Ladung (ca 25 to ) ist ------
        //
        public static bool Auftragsplitting_CancelAuftragPos()
        {
            string Text = "Soll die gewählte Auftragsposition wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "Löschen Auftragsposition", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //-- wenn das gesplittete Gewicht zu groß für eine Ladung (ca 25 to ) ist ------
        //
        public static bool Auftragsplitting_NoArtikelSelected()
        {
            string Text = "Für eine neue Auftragposition muss mindestens ein Artikel ausgewählt werden!";
            DialogResult result = MessageBox.Show(Text, "Check Auftragssplitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //
        public static void Auftragserfassung_KeineMandantenAuswahl()
        {
            string Text = "Es wurde kein Mandant ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "Mandantencheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //************************************************************ Retoure Einlagerung *****************************************************************
        //
        //
        public static void Retoure_NoArtikelSelected()
        {
            string text = "Es wurde kein Artikel ausgewählt. Bitte prüfen.";
            MessageBox.Show(text, "Check Artikelauswahl", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        /*****************************************************************************************************+
         *                                             RECOURCE   
         *                                                                                        
         ******************************************************************************************************/
        //
        //
        //
        public static bool Recource_IsUsed()
        {
            string Text = "Die Ressource wird zu diesem Zeitpunkt bereits verwendet!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //
        //ZM ist bereits die entsprechende Resource zugewiesen
        //
        public static void Recource_TruckUsedTheRecource()
        {
            string Text = "Dem Fahrzeug ist für die angegebene Zeit bereits ein entsprechender Recourcentyp zugewiesen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //************************************************************ OrderPosRectangle ******************************************************************
        //
        //
        //
        public bool OderPosRectangle_Delete()
        {
            bool delete = false;
            string Text = "Soll die Markierung des Unterauftrags gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                string Text1 = "Die Markierung wird nun unwiderruflich gelöscht! \n Bitte mit OK bestätigen.";
                DialogResult result1 = MessageBox.Show(Text1, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result1 == DialogResult.OK)
                {
                    delete = true;
                }
            }
            return delete;
        }

        //
        //---------- löschen Kontakteintrag  ------------
        //
        public static bool Kontakte_KontaktDelete()
        {
            bool delete = false;
            string Text = "Soll der Kontakt gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                string Text1 = "Der Kontakt wird nun unwiderruflich gelöscht! \n Bitte mit OK bestätigen.";
                DialogResult result1 = MessageBox.Show(Text1, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result1 == DialogResult.OK)
                {
                    delete = true;
                }
            }
            return delete;
        }
        //
        /****************************************************************************************************
         *                                      StyleSheet
         * **************************************************************************************************/
        //
        public static bool StyleSheet_Delete()
        {
            bool delete = false;
            string Text = "Soll die Formatvorlage gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                delete = true;
            }
            return delete;
        }
        //
        //
        /*****************************************************************************************************
         *                                                  Extra Charge                                               
         *****************************************************************************************************/
        //
        //
        public static bool ExtraCharge_DeleteDatenSatz()
        {
            string Text = " Soll der Datensatz wirklich gelöscht werden? Dabei werden auch alle kundenbezogenen Preise gelöscht!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //
        /*****************************************************************************************************
         *                                                  ADR                                               
         *****************************************************************************************************/
        //
        //
        //------------- ADR löschen ---------------------------
        //
        public static bool ADR_DeleteDatenSatz()
        {
            string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden? Dabei werden auch alle Kontakt-/Kundendaten dieser Adresse gelöscht!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //------------- ViewID = KD_ID Check ViewID -------------------
        //
        public static void ADR_ViewIDisNotNo()
        {
            string Text = "Der Matchcode darf nur Ziffern beinhalten, wenn er als Kundennummer verwendet werden soll!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static void ADRText_TextForTextModulIsEmty()
        {
            string Text = "Für das Textmodul wurde kein Text eingegeben. Daten können nicht gespeichert werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static void ADRText_NoDocumtenArtSelection()
        {
            string Text = "Es wurde keine Dokumentenart ausgewählt. Bitte weisen Sie eine Dokumentenart zu.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void ADR_ViewIDnotZero()
        {
            string Text = "Der Matchcode darf nicht leer oder den Wert 0 aufweisen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------------- Check VIEWID---------------------------
        //
        public static void ADR_ViewIDIsUsed()
        {
            string Text = "Der Matchcode für diese Adresse wird bereits verwendet. Bitte den Matchcode ändern!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------------- ADR Änderung in ctArtDetails -----------
        //
        public static void ADR_ADRChangeFailed(string ADRBezeichnung)
        {
            string Text = "Die Adresse für den " + ADRBezeichnung + " konnte nicht geändert werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void ADR_FalscheEingabeKtoOrBLZ()
        {
            string Text = "Die Eingabe der Konto- oder BLZ - Nummer enthält Buchstaben. Die Eingabe wurde auf 0 gesetzt und gespeichert.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //--------------- Kundennummer wird schon verwendet ----------------
        //
        public static void ADR_KundenNummerIsUsed()
        {
            string Text = "Die Kundennummer ist bereits vergeben. Bitte den Kundenummer ändern!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //--------------- Kundennummer wird schon verwendet ----------------
        //
        public static void ADR_ADRgeloescht()
        {
            string Text = "Der Adressdatensatz und die jeweiligen Kunden-, Kontakt-, Tarif- und Frachtkonditionen wurden gelöscht.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        //--------------- Kundennummer wird schon verwendet ----------------
        //
        public static void ADR_KeineAdressenIdVorhanden()
        {
            string Text = "Die Ansicht, konnte nicht angezeigt werden, da keine Adresse ausgewählt wurde!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        /***********************************************************************************************
         * 
         * ********************************************************************************************/
        //
        public static void Tarife_StaffelBeschreibungFehlt()
        {
            string Text = "Es muss ein Staffelname vergeben sein, da dieser anschließend in der Rechnung aufgeführt wird.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /*******************************************************************************************************
         *                                    Kunden / Kundennummern
         */
        //
        public static void Kunde_FalscheEingabeKDNr()
        {
            string Text = "Die Eingabe der Kundennummer enthält Buchstaben. Die Eingabe wurde zurückgesetzt.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static void Kunde_SaveKundenDataOK()
        {
            string Text = "Die Kundendanten wurden erfolgreich gespeichert.";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        //
        //
        //
        /***********************************************************************************************************
         * 
         *                                                  Lager
         * 
         * ********************************************************************************************************/
        //
        //--------------- Lagereingang - DGV ist leer ------------------
        //
        public static void Lager_DGVleer()
        {
            string Text = "Es liegen keien Artikel zur Ein- / Auslagerung vor!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------------ Dateneingabe ADR - fehlen -------------------
        //
        public static void Lager_ArtDeleteFailed()
        {
            string Text = "Der Lagereingang ist bereits abgeschlossen. Der Artikel kann nicht gelöscht werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static DialogResult Lager_Artikel_LVSNRNotExist(string myPlusText)
        {
            string Text = "Die angegebene LVSNR ist in diesem Arbeitsbereich nicht vorhanden!";
            if (!myPlusText.Equals(string.Empty))
            {
                Text += Environment.NewLine + myPlusText;
                Text += Environment.NewLine + "Wollen Sie direkt zum gesuchten Artikel wechseln, dann bestättigen Sie mit OK.";
            }

            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return result;
        }

        //
        //------------ Dateneingabe ADR - fehlen -------------------
        //
        public static void Lager_ADRfehlen()
        {
            string Text = "Adressangaben fehlen! Bitte Adressen auswählen.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //-------------- Keine Einlagerungsdaten ---------------
        //
        public static void Lager_EindgangsIDExistiertNicht()
        {
            string Text = "Die angegebene EingangsID ist in diesem Arbeitsbereich nicht vorhanden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages/Lager_SPL_StoreOut</summary>
        ///<remarks></remarks>
        public static bool Lager_DirectTransferToUBArtikel()
        {
            string Text = "Der Artikel wurde umgebucht und Sie werden direkt zum neuen Datensatz weitergeleitet. " +
                          "Wenn Sie nicht direkt weitergeleitet werden wollen, dann betätigen Sie den Button Abbrechen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Lager_AusgangasIDExistiertNicht()
        {
            string Text = "Die angegebene AusgangsID ist in diesem Arbeitsbereich nicht vorhanden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        public static void Auftrag_AuftragNummerExistiertNicht()
        {
            string Text = "Die angegebene Auftragsnummer konnte in diesem Arbeitsbereich nicht gefunden werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        public static void Lager_DeleteLagerplatzFailed()
        {
            string Text = "Der Löschvorgang konnte nicht durchgeführt werden. Ein oder mehrere Lagerplätze " +
                          "sind noch belegt. Sobald die entsprechenden Lagerplätze frei sind, können diese " +
                          "gelöscht werden.";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void Lager_KeineArtikelFuerAuslagerung()
        {
            string Text = "Es wurden keine Artikel der zur Auslagerung ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static bool Lager_AuslagerungKomplett()
        {
            string Text = "Der Artikel wird ausgelagert und der Lagerausgang abgeschlossen.";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Lager_KeineArtikelFuerUB()
        {
            string Text = "Es wurden keine Artikel der zur Umbuchung ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void Lager_LagerPrintNeutralMissingADR()
        {
            string Text = "Die neutralen Adressen wurden nocht nicht hinertlegt. Sobald die Adressen hinterlegt wurden kann das Dokument erstellt werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //
        public static void Lager_CheckAllArtikelAndEingangFailed()
        {
            string Text = "Der Eingang konnte nicht kompeltt abgeschlossen werden, da noch nicht alle Artikel im Lager platziert wurden.";
            DialogResult result = MessageBox.Show(Text, "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        public static void Lager_EingangOffenKeineUmbuchung()
        {
            string Text = "Der Eingang ist noch nicht abgeschlossen und kann somit nicht umgebucht werden!";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void Lager_IdentischeBestandsbesitzer()
        {
            string Text = "Die Umbuchung ist unnötig. Der Quell- und der Zielbestand sind idetnisch!";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /**************************************************************************************************
         *                              Sperrlager SPL
         * ************************************************************************************************/
        public static bool Sperrlager_add()
        {
            string Text = "Soll der Artikel ins Sperrlager umgebucht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        public static void Sperrlager_addAuto()
        {
            string Text = "Aufgrund der Schadenszuweisung wurde der Artikel automatisch ins Sperrlager gebucht!";
            DialogResult result = MessageBox.Show(Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void Sperrlager_CheckOutOK(List<string> myMesList)
        {
            string Text = "Die Artikel wurden erfolgreich aus dem Sperrlager ausgebucht." + Environment.NewLine;
            if (myMesList != null && myMesList.Count > 0)
            {
                Text += "Die folgenden Artikel wurden nicht aus dem SPL ausgebucht werden: " + Environment.NewLine;
                foreach (string mes in myMesList)
                {
                    Text += mes + Environment.NewLine;
                }
            }
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        public static void Sperrlager_CheckOutFailed(List<string> myMesList)
        {
            string Text = "Bei der Sperrlagerausbuchung ist ein Fehler aufgetreten. Die Artikel konnten nicht ausgebucht werden." + Environment.NewLine;
            if (myMesList != null && myMesList.Count > 0)
            {
                //Text += "Die folgenden Artikel wurden nicht aus dem SPL ausgebucht werden: " + Environment.NewLine;
                foreach (string mes in myMesList)
                {
                    Text += mes + Environment.NewLine;
                }
            }
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        //************************************** ARTIKEL / Güterart *******************************************
        //
        //------------- Artikeldatensatz löschen ---------------------------
        //
        public static bool Artikel_DeleteDatenSatz()
        {
            string Text = "Soll der Artikeldatensatz unwiderrufliche gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //------------- Artikeldaten wurden geändert - Soll die Änderung gespeichert werden -------------
        //
        public static bool Artikel_SaveChangedArtikeldaten()
        {
            string Text = "Die Artikeldaten wurden geändert. Soll die Änderung gespeichert werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Artikel_UseKorrStorVerfahren()
        {
            string Text = "Soll das Stornier- und Korrekturverfahren jetzt auf den ausgewählten Artikel angewendet werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //
        //------------- im Datensatz nicht ausgewählt ---------------------------
        //
        public static void Artikel_ArtikelGutLeer()
        {
            string Text = "Im Artikeldatensatz müssen mindestens Gut, gemeldetes Gewicht und Brutto > 0 kg angegeben werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        public static void Artikel_ArtikelProduktionsnummerExist()
        {
            string Text = "Diese Produktionsnummer existiert bereits !";
            DialogResult result = MessageBox.Show(Text, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //------------- Check VIEWID---------------------------
        //
        public static void Gut_ViewIDIsUsed()
        {
            string Text = "Der Matchcode für diese Güterart wird bereits verwendet. Bitte den Matchcode ändern!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static void Gut_GueterartIsInUse()
        {
            string Text = "Die Der Datensatz ist im System in Verwendung und kann nicht gelöscht werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //+++++++++++++++++++++++++++++++++++++++++ Relation  +++++++++++++++++++++++++++++++++++++++++++
        //
        //
        //
        //------------- Relation löschen ---------------------------
        //
        public static bool Relation_DeleteDatenSatz()
        {
            string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //------------- Check VIEWID---------------------------
        //
        public static void Relation_RelationIsUsed()
        {
            string Text = "Eine Relation mit diesem Namen existiert bereits. Bitte Namen ändern!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //******************************************* FRachtvergabe an SU ************************
        //
        //
        public static void Frachtvergabe_SUfehlt()
        {
            string Text = "Es wurde kein Subunternehmer ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        //
        //
        //
        /**************************************************************************************************
         *                                  Userverwaltung
         * 
         * ************************************************************************************************/
        //
        public static void Userverwaltung_PassNichtIdentisch()
        {
            string Text = "Die eingegebenen Passwörter sind nicht identisch. Bitte die Eingabe wiederholen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //User noch eingeloggt
        public static void Userverwaltung_UserIsLoggedIn()
        {
            string Text = "Der zu löschende User ist noch im System angemeldet und kann nicht gelöscht werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }


        //
        //------------ LOGIN - Check false
        //
        public static void Userverwaltung_LOGIN()
        {
            string Text = "Die Zugangsdaten sind nicht korrekt. Bitte die Eingabe wiederholen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //-------- Nachfrage, ob User gelöscht werden soll ---------------------
        //
        public static bool User_Delete(string name)
        {
            string Text = "Soll der User [" + name + "] unwiderruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //
        //
        public static void User_DeleteNOT(string name)
        {
            string Text = "Der User [" + name + "] kann nicht gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        //
        //
        public static void User_NoMailadress()
        {
            string Text = "Dem aktuelle User ist keine eigene E-Mailadresse zugewiesen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        //---------------- Keine Berechtigung -------------------------
        //
        public static void User_NoAuthen()
        {
            string Text = "Für diesen Vorgang haben Sie keine Berechtigung!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        /*********************************************************
         *                                SCANNER 
         *                                ***********************/
        //
        //
        public static void SCAN_FalschesBetriebssystem()
        {
            string Text = "Scan kann unter diesem Betriebssystem noch nicht ausgeführt werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        public static void SCAN_Allgemein()
        {
            string Text = "Beim Scan ist ein Fehler aufgetreten. Bitte prüfen Sie ob der Scanner ordnungsgemäß angeschlossen ist. Besteht das Problem weiterhin, setzen Sie sich im ComTEC Nöker GmbH in Verbindung!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void SCAN_AuftragIDnichtKorrekt()
        {
            string Text = "Die übergebene Auftragsnummer ist nicht korrekt. Bitte probieren Sie es noch einmal.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void SCAN_ScannerDeviceNichtErkannt()
        {
            string Text = "Es konnte kein Scanner erkannt werden. Bitte prüfen Sie, ob das Gerät korrekt angeschlossen ist.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        /****************************************************************************************************
         *                                      Allgemein
         * 
         * **************************************************************************************************/
        //
        //
        public static void Print_NeutrADR_NotSelected()
        {
            string Text = "Es wurde keine neutralen Adressen ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Print_NoDocArtSelected()
        {
            string Text = "Es wurde kein Dokument ausgewählt!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static bool Print_DocToPrint()
        {
            string Text = "Sollen der Dokumentendruck jetzt durchgeführt werden?";
            DialogResult result = MessageBox.Show(Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //
        //
        public static bool Allgemein_ChangesToSave()
        {
            string Text = "Sollen die vorgenommenen Änderungen gespeichert werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //
        //
        //
        public static void Allgemein_ConvertFehlerDateTime()
        {
            string Text = "Bei Datumumwandlung ist ein Fehler aufgetreten!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_FunctionOnConstruction()
        {
            string Text = "Diese Funktion ist in Arbeit!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_PflichtfeldNichtLeer(string strFeld)
        {
            string Text = "Das Feld " + strFeld + " ist ein Pflichtfeld und darf nicht leer sein.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        //------------- Allgemein löschen verweigert  ----------------------------------------------
        //
        public static void DeleteDenied()
        {
            string Text = "Der Datensatz wird verwendet und kann deshalb nicht gelöscht werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {

            }
            else
            {
            }
        }
        //
        //------------------- Allgemein -  -------------------
        //
        public static void Allgemein_FehlerDistanceEingabe(string strText)
        {
            string Text = strText;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Allgemein_KeinDatensatzAusgewaehlt()
        {
            string Text = "Es wurde kein Datensatz ausgewählt!. Aktion kann nicht durchgeführt werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Allgemein_ERRORTextShow(string strText)
        {
            string Text = strText;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Allgemein_InfoTextShow(string strText)
        {
            string Text = strText;
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool Allgemein_SelectionInfoTextShow(string strText)
        {
            string Text = strText;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Allgemein_SelectionInfoTextShow(string strText, MessageBoxIcon myIcon)
        {
            string Text = strText;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, myIcon);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Allgemein_ModulNotInstalled()
        {
            string Text = "Dieses Modul ist aktuell nicht freigeschaltet. Setzen Sie sich bitte mit dem Support ComTEC Nöker GmbH in Verbindung.";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static bool Allgemein_ModulNotInstalled_WatchDog()
        {
            string Text = "Dieses Modul WatchDog ist aktuell nicht freigeschaltet. Setzen Sie sich bitte mit dem Support ComTEC Nöker GmbH in Verbindung.";
            DialogResult result = MessageBox.Show(Text, "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        //
        //----------- Check auf Eingabe INT32 ist negativ -----------------
        //
        public static void Allgemein_EingabeIstKeineGanzzahl()
        {
            string Text = "Es dürfen nur ganze Zahlen ohne Nachkommastellen eingegeben werden. Bitte Eingabe wiederholen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //----------- falsche Übergabewert -----------------
        //
        public static void Allgemein_FalscheUeberabeParameter(string strHelp)
        {
            string Text = "Es wurden die falschen Parameter übergeben.(" + strHelp + ")" + Environment.NewLine +
                          "Bitte wenden Sie sich an das Service-Team der ComTEC Nöker GmbH";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        public static void Allgemein_EingabeIstKeineDecimalzahl()
        {
            string Text = "Zahlenwert bitte in folgendem Format eingeben: 1000,00 ";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_EingabeIstFehlerhaft()
        {
            string Text = "Die Eingabe ist fehlerhaft oder es konnten zur Eingabe keine Daten gefunden werden!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        public static void Allgemein_FunktionInBearbeitung()
        {
            string Text = "Diese Funktion ist noch in Bearbeitung bzw. nicht freigeschaltet!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_WertZuGross()
        {
            string Text = "Der eingebene Wert ist zu groß. Eingabe bitte wiederholen.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        public static void Allgemein_FileCanNotOpen()
        {
            string Text = "Die Datei kann leider nicht geöffnet werden.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        //
        public static void Allgemein_BruttoKleinerNetto()
        {
            string Text = "Der eingebene Bruttowert ist kleiner als der eingegebene Nettowert. Bitte überprüfen Sie die Eingabe.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_MandantFehlt()
        {
            string Text = "Es wurde kein Mandanten für diese Aktion ausgewählt. Bitte wählen Sie einen Mandanten aus.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_EingabeFormatFehlerhaft()
        {
            string Text = "Das Eingabeformat ist falsch. Die Eingabe kann nicht verarbeitet werden. Bitte Wiederholen Sie die Eingabe!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void Allgemein_EingabeDatenFehlerhaft(string strFehlermeldung)
        {
            string Text = "Folgende Eingaben sind fehlerhaft:";
            Text = Text + Environment.NewLine + strFehlermeldung;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        public static void Allgemein_EingabefeldZuLang(string strFehlermeldung)
        {
            string Text = "Folgende Eingaben sind fehlerhaft:";
            Text = Text + Environment.NewLine + strFehlermeldung;
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        //------- Datum Check  -----------------------
        //
        public static void DateCheck_DateToBeInPast()
        {
            string Text = "Das Datum liegt in der Vergangenheit. Bitte Datum neu wählen.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------- Datum Check  -----------------------
        //
        public static void DateCheck_DateToBeInPastLiefertermin()
        {
            string Text = "Das Liefertermin / Entladedatum liegt in der Vergangenheit. Bitte Datum neu wählen.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------- Datum Check  -----------------------
        //
        public static void DateCheck_DateToBeInFuture()
        {
            string Text = "Das Datum liegt in der Zukunft. Bitte Datum neu wählen.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //
        //------- Datum Check  -----------------------
        //
        public static void DateCheck_LTistkleinerVSB()
        {
            string Text = "Der Liefertermin darf nicht vor der Versandbereitschaft liegen. Bitte prüfen!";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //------------- Allgemein löschen -----------------------
        //
        public static bool DeleteAllgemein()
        {
            string Text = "Soll der Datensatz / Dokument unwiederruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void DeleteOKAllgemein()
        {
            string Text = "Der Datensatz wurde gelöscht?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        //------------- Allgemein löschen -----------------------
        //
        public static bool DoProzess()
        {
            string Text = "Soll der Prozess nun durchgeführt werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /****************************************************************************************************
         *                                      Print Center
         * **************************************************************************************************/
        //
        //
        //
        public static void PrintCenter_ChangedArtikelDaten()
        {
            string Text = "Die Artikeldaten wurden verändert und nicht gespeichert. Bitte speichern Sie die Änderung.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        //
        //
        public static void PrintCenter_ChangedAuftragsDaten()
        {
            string Text = "Die Auftragsdaten wurden verändert und nicht gespeichert. Bitte speichern Sie die Änderung.";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages / ExistRGId</summary>
        ///<remarks>Check, ob die ID existiert.</remarks>
        public static void Error_ContactToSupport()
        {
            string Text = "Es ist ein Fehler aufgetreten. Es wurde eine Fehlerbeschreibung erstellt. Bitte setzen Sie sich mit dem Support der Firma ComTEC-Nöker in Verbindung.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /***************************************************************************************************
         *                              Export an Excel, PDF, FIBU usw.
         * ************************************************************************************************/
        public static bool Export_OpenFileInExcel()
        {
            string text = "Soll die Exceldatei nun geöffnet werden?";
            DialogResult result = MessageBox.Show(text, "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /***************************************************************************************************
         *                              Druck
         * ************************************************************************************************/
        ///<summary>clsMessages / Print_NoView</summary>
        ///<remarks></remarks>
        public static void Print_NoView()
        {
            string Text = "Es konnte nicht gedruckt werden. Es wurden keine passende Druckansicht gefunden.";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /***************************************************************************************************
         *                              Druck
         * ************************************************************************************************/
        ///<summary>clsMessages / Print_NoView</summary>
        ///<remarks></remarks>
        public static bool ADRVerweis_Delete()
        {
            string Text = "Soll der Verweis unwiederruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsMessages/MailingList_Delete</summary>
        ///<remarks>Checkabfrage Löschung</remarks>
        public static bool Lager_Delete()
        {
            string Text = "Soll der ausgewählte Datensatz wirklich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        ///<summary>clsMessages / ADRVerweis_SenderFehlt</summary>
        ///<remarks></remarks>
        public static void ADRVerweis_SenderFehlt()
        {
            string Text = "Es wurde kein Sender eingetragen!";
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ///<summary>clsMessages / ADRVerweis_EingabeIstKeineDecimalzahl</summary>
        ///<remarks></remarks>
        public static void ADRVerweis_EingabeIstKeineDecimalzahl()
        {
            string Text = "Zahlenwert bitte in folgendem Format eingeben: 1000";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        ///<summary>clsMessages / ADRVerweis_InputError</summary>
        ///<remarks></remarks>
        public static void ADRVerweis_InputError(string strError)
        {
            string Text = "Es sind folgende Fehler aufgetreten:\n";
            Text += strError;
            DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ///<summary>clsMessages / Allgemein_Arbeitsbereichswechsel</summary>
        ///<remarks></remarks>
        public static void Allgemein_Arbeitsbereichswechsel(string AbName)
        {
            string Text = "Der Wechsel in den Arbeitsbereich [" + AbName + "] wurde erfolgreich vorgenommen!";
            DialogResult result = MessageBox.Show(Text, "Wechsel des Arbeitsbereichts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static void Lager_Ausgang_SPL()
        {
            string Text = "Es wurde mindestens ein Artikel aus dem Sperrlager ausgewählt! Diese Artikel können keinen AUsgang Hinzugefügt werden";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //if (result == DialogResult.Cancel)
            //{
            //   return false;
            //}
            //else
            //{
            //   return true;
            //}
        }
        ///<summary>clsMessages/Lager_SPL_StoreOut</summary>
        ///<remarks></remarks>
        public static bool Lager_SPL_StoreOut()
        {
            string Text = "Sollen die ausgewählte Artikel wirklich ausgelagert werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsMessages/MailingList_Delete</summary>
        ///<remarks>Checkabfrage Löschung</remarks>
        public static bool Lager_SPL_Out()
        {
            string Text = "Soll der ausgewählte Artikel wirklich aus dem Sperrlager genommen werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /*****************************************************************************************************
         *                                      ASN / DFU
         * ***************************************************************************************************/
        //------------- Artikeldaten wurden geändert - Soll die Änderung gespeichert werden -------------
        //
        public static bool ASN_ResendASN()
        {
            string Text = "Sollen die kompletten Lagerausgangsmeldungen noch einnmal gesendet werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /*****************************************************************************************************+
         *                                             Inventur   
         *                                                                                        
         ******************************************************************************************************/
        //
        //
        //
        public static bool Inventory_Add()
        {
            string Text = "Soll der der Inventurdatensatz angelegt werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Inventory_Delete()
        {
            string Text = "Soll der der Inventurdatensatz unwiderruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Inventory_SetStatus()
        {
            string Text = "Soll der der Inventurdatensatz unwiderruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CustomProcesses_DeleteItem()
        {
            string Text = "Soll der der Prozess unwiderruflich gelöscht werden?";
            DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
