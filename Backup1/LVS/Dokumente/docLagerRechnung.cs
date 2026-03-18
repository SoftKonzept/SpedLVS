namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class docLagerRechnung : Telerik.Reporting.Report
    {
        internal clsRechnung Rechnung;
        public Globals._GL_USER _GL_User;
        internal DataTable dtStaffelPositionen;
        internal Int32 Count = -1;
        internal DataSet dsDetails;
        internal DataSet dsBestand;


        public docLagerRechnung()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            dsDetails = new DataSet();
            dsBestand = new DataSet();
            dtStaffelPositionen = new DataTable();
        }
        ///<summary>docLagerRechnung / InitDoc</summary>
        ///<remarks>Setz die Daten auf das Dokument</remarks>
        public void InitDoc(decimal myRGNr)
        {
            if (myRGNr > 0)
            {
                //detailBK.Dispose();

                Rechnung = new clsRechnung();
                Rechnung._GL_User = this._GL_User;
                Rechnung.ID = myRGNr;
                Rechnung.Fill();

                //Briefkopf
                //InitPageHeader();
                InitDetail();
                SetSumme();
            }
        }
        /***************************************************************************************
         *                          Ini Briefkopf
         * ***********************************************************************************/
        ///<summary>docLagerRechnung / InitPageHeader</summary>
        ///<remarks>Setzt alle Daten für den Briefkopf</remarks>
        private void InitPageHeader()
        {
            /***
            //REchnungsempfänger
            SetRGReceiverADR();
            string strDocName = string.Empty;
            if (Rechnung.GS)
            {
                if (Rechnung.Storno)
                {
                    strDocName = "STORNO - GUTSCHRIFT";
                }
                else
                {
                    strDocName = "GUTSCHRIFT";
                }
            }
            else
            {
                if (Rechnung.Storno)
                {
                    strDocName="STORNO - RECHNUNG";
                }
                else
                {
                    strDocName = "RECHNUNG";
                }
            }
            tbDocName.Value = strDocName;
            tbRGNr.Value = Rechnung.RGNr.ToString();
            tbDatum.Value = Rechnung.Datum.ToShortDateString();
            tbKDNr.Value = string.Empty;
             * ****/
        }
        ///<summary>docLagerRechnung / SetRGReceiverADR</summary>
        ///<remarks></remarks>
        private void SetRGReceiverADR()
        {
            /***
            tbADRAnrede.Value = Rechnung.ADR_RGEmpfaenger.FBez;
            tbADRName1.Value = Rechnung.ADR_RGEmpfaenger.Name1;
            tbADRName2.Value = Rechnung.ADR_RGEmpfaenger.Name2;
            tbADRzHd.Value = string.Empty;
            tbADRStr.Value=Rechnung.ADR_RGEmpfaenger.Str +" " +Rechnung.ADR_RGEmpfaenger.HausNr;
            tbADRPLZ.Value=Rechnung.ADR_RGEmpfaenger.PLZ;
            tbADROrt.Value=Rechnung.ADR_RGEmpfaenger.Ort;
             * ***/
        }
        /**********************************************************************************
         *                     Init Details  
         * *******************************************************************************/
        ///<summary>docLagerRechnung / docLagerRechnung_NeedDataSource</summary>
        ///<remarks></remarks>
        private void docLagerRechnung_NeedDataSource(object sender, EventArgs e)
        {
            dtStaffelPositionen.Clear();
            dtStaffelPositionen = Rechnung.dtRechnungsPositionen.DefaultView.ToTable(true, "RGText");
            this.tableRGPositionen.DataSource = dtStaffelPositionen;
        }
        ///<summary>docLagerRechnung / SetSumme</summary>
        ///<remarks>Set die Summenbeträge der Rechnung</remarks>
        private void SetSumme()
        {
            tbNetto.Value = Functions.FormatDecimal(Rechnung.NettoBetrag);
            tbMwStSatz.Value = Functions.FormatDecimal(Rechnung.MwStSatz);
            tbMwStBetrag.Value = Functions.FormatDecimal(Rechnung.MwStBetrag);
            tbBrutto.Value = Functions.FormatDecimal(Rechnung.BruttoBetrag);
        }
        ///<summary>docLagerRechnung / InitDetail</summary>
        ///<remarks>Set die Summenbeträge der Rechnung</remarks>
        private void InitDetail()
        {
            tbAbrZeitraum.Value = Rechnung.AbrZeitraumVon.ToShortDateString() + " - " + Rechnung.AbrZeitraumBis.ToShortDateString();
            tbTarif.Value = Rechnung.AbrechnungsTarifName;
        }
        ///<summary>docLagerRechnung / textBox2_ItemDataBound</summary>
        ///<remarks></remarks>
        private void textBox2_ItemDataBound(object sender, EventArgs e)
        {
            //if (Rechnung.dtRechnungsPositionen.Rows.Count > 0)
            //{
            tablePosDetails_NeedDataSource(sender, e);
            listBestand_NeedDataSource(sender, e);
            //RemoveRows();
            //}
        }
        ///<summary>docLagerRechnung / tablePosDetails_NeedDataSource</summary>
        ///<remarks></remarks>
        private void tablePosDetails_NeedDataSource(object sender, EventArgs e)
        {
            if (Rechnung.dtRechnungsPositionen.Rows.Count > 0)
            {
                Count++;
                string strFilter = "RGText ='" + ((Telerik.Reporting.Processing.TextBox)(sender)).Value.ToString() + "'";
                string dtName = "dt_" + Count.ToString();
                DataTable dtTmp = Rechnung.dtRechnungsPositionen;
                dtTmp.TableName = dtName;
                dtTmp.DefaultView.RowFilter = strFilter;
                DataTable dtTmp2 = dtTmp.DefaultView.ToTable();
                dsDetails.Tables.Add(dtTmp2);
                tablePosDetails.DataSource = dsDetails.Tables[Count];
            }
            else
            {

            }
        }
        ///<summary>docLagerRechnung / listBestand_NeedDataSource</summary>
        ///<remarks></remarks>
        private void listBestand_NeedDataSource(object sender, EventArgs e)
        {
            if (Rechnung.dtRechnungsPositionen.Rows.Count > 0)
            {
                DataTable dtTmp = dsDetails.Tables[Count];
                DataTable dtTmp2 = dtTmp.DefaultView.ToTable(true, "Abrechnungsart", "Anfangsbestand", "Abgang", "Zugang", "Endbestand");
                dtTmp2.TableName = "Bestand_" + Count.ToString();
                dtTmp2.DefaultView.RowFilter = "Abrechnungsart='Lagerkosten'";
                DataTable dtTmp3 = dtTmp2.DefaultView.ToTable();
                dsBestand.Tables.Add(dtTmp3);
                listBestand.DataSource = dsBestand.Tables[Count];
            }
            else
            {

            }
        }


        private void RemoveRows()
        {
            for (Int32 i = 0; i <= dsDetails.Tables[Count].Rows.Count - 1; i++)
            {
                decimal decID = (decimal)dsDetails.Tables[Count].Rows[i]["ID"];
                Int32 j = 0;
                while (j <= Rechnung.dtRechnungsPositionen.Rows.Count - 1)
                {
                    decimal decVrglID = (decimal)Rechnung.dtRechnungsPositionen.Rows[j]["ID"];
                    if (decID == decVrglID)
                    {
                        Rechnung.dtRechnungsPositionen.Rows.RemoveAt(j);
                        j = Rechnung.dtRechnungsPositionen.Rows.Count;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }

        private void textBox2_ItemDataBinding(object sender, EventArgs e)
        {
            if (Rechnung.dtRechnungsPositionen.Rows.Count == 0)
            {
                textBox2.Dispose();
            }
        }

    }
}