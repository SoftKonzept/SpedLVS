using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using LVS;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.Data;


namespace Sped4
{
    public partial class ctrVDA4905Details : UserControl
    {
        public Globals._GL_USER GLUser;
        internal DataRow VDA4905Row;
        internal  decimal AsnID = 0;
        internal clsGut Gut;
        internal clsASN AsnVDA4905;
        public ctrMenu _ctrMenu;

        internal string Werk = string.Empty;
        internal string Lieferant = string.Empty;
        internal string AsnDate = string.Empty;
        internal string ASNUENr = string.Empty;
        internal Int32 StartFortschrittzahl = 0;
        public frmTmp _frmTmp;

        ///<summary>ctrVDA4905Details / ctrVDA4905Details</summary>
        ///<remarks></remarks>
        public ctrVDA4905Details(DataRow myRow)
        {
            InitializeComponent();
            VDA4905Row = myRow;
        }
        ///<summary>ctrVDA4905Details / ctrVDA4905Details_Load</summary>
        ///<remarks></remarks>
        private void ctrVDA4905Details_Load(object sender, EventArgs e)
        {
            if (VDA4905Row is DataRow)
            {
                decimal decTmp = 0;
                decimal.TryParse(VDA4905Row["ASNID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.AsnID = decTmp;
                }
                this.Gut = null;
                this.Gut = (clsGut)VDA4905Row["GArt"];
                this.Lieferant = VDA4905Row["Lieferant"].ToString();



                if (
                    (this.Gut is clsGut) &&
                    (this.AsnID > 0) &&
                    (this._ctrMenu is ctrMenu)
                   )
                {
                    this.AsnVDA4905 = new clsASN();
                    this.AsnVDA4905.InitClass(this._ctrMenu._frmMain.GL_System, this.GLUser);
                    InitDGV();

                }
            }
        }
        ///<summary>ctrVDA4905Details / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            DataTable dtTmp = this.AsnVDA4905.GetASNByASNId(this.AsnID);
            DataTable dt4905 = new DataTable();
            dt4905.Columns.Add("Abrufdatum", typeof(string));
            dt4905.Columns.Add("Menge", typeof(Int32));
            dt4905.Columns.Add("kum. Menge", typeof(Int32));
            DataRow NewRow;

            string strTmpDate = string.Empty;
            Int32 iTmpValue = 0;
            if (dtTmp.Rows.Count > 0)
            {
                bool IsSearchCallDetails = false;
                bool IsTimePeriode = false;   // 55555
                //ASNTeile für die bestimmte Güterart herausfiltern
                foreach (DataRow row in dtTmp.Rows)
                {
                    Int32 iTmp = 0;

                    string strKennung = row["Kennung"].ToString();
                    switch (strKennung)
                    {
                        case clsASN.const_VDA4905SatzField_SATZ511F05:
                            this.ASNUENr = row["Value"].ToString();
                            break;

                        case clsASN.const_VDA4905SatzField_SATZ511F06:
                            this.ASNUENr +="/"+ row["Value"].ToString();
                            break;

                        case clsASN.const_VDA4905SatzField_SATZ512F01:
                            if (IsSearchCallDetails)
                            {
                                IsSearchCallDetails = false;
                                break;
                            }
                            break;

                        //Check Lieferwerk -> wenn nicht korrekt weiter zur Nächsten Satz SATZ512F03 mit der nächsten Liefeinteilung
                        case clsASN.const_VDA4905SatzField_SATZ512F03:
                            Werk = row["Value"].ToString();
                            break;

                        //Datum Lieferabruf  Spalte 4905
                        case clsASN.const_VDA4905SatzField_SATZ512F05:
                            DateTime dateVDA4905ASN = Globals.DefaultDateTimeMinValue;
                            string strTmp = row["Value"].ToString();
                            DateTime.TryParseExact(strTmp, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateVDA4905ASN);
                            AsnDate = dateVDA4905ASN.ToShortDateString();
                            break;

                        // check Güterart
                        case clsASN.const_VDA4905SatzField_SATZ512F08:
                            string strSachnummerKunde = row["Value"].ToString();
                            IsSearchCallDetails = (Gut.Verweis.Trim().Equals(strSachnummerKunde.Trim()));
                            break;

                        /**************************************************
                        *    Summieren der Liefermengen
                        * ************************************************/
                        //Fortschrittszahl  FZ dazu
                        case clsASN.const_VDA4905SatzField_SATZ513F07:                            
                            if (IsSearchCallDetails)
                            {
                                iTmp = 0;
                                Int32.TryParse(row["Value"].ToString(), out iTmp);
                                StartFortschrittzahl = iTmp;
                            }
                            break;

                        // Abrufedatum 
                        case clsASN.const_VDA4905SatzField_SATZ513F08:
                        case clsASN.const_VDA4905SatzField_SATZ513F10:
                        case clsASN.const_VDA4905SatzField_SATZ513F12:
                        case clsASN.const_VDA4905SatzField_SATZ513F14:
                        case clsASN.const_VDA4905SatzField_SATZ513F16:
                        case clsASN.const_VDA4905SatzField_SATZ514F03:
                        case clsASN.const_VDA4905SatzField_SATZ514F05:
                        case clsASN.const_VDA4905SatzField_SATZ514F07:
                        case clsASN.const_VDA4905SatzField_SATZ514F09:
                        case clsASN.const_VDA4905SatzField_SATZ514F11:
                        case clsASN.const_VDA4905SatzField_SATZ514F13:
                        case clsASN.const_VDA4905SatzField_SATZ514F15:
                        case clsASN.const_VDA4905SatzField_SATZ514F17:
                            if (IsSearchCallDetails)
                            {
                                strTmpDate = string.Empty;
                                strTmpDate = row["Value"].ToString();
                                switch (strTmpDate)
                                {
                                    case LVS.ASN.clsVDA4905.const_AbrufDatum_000000:  //ENDE
                                        IsTimePeriode = false;
                                        break;

                                    case LVS.ASN.clsVDA4905.const_AbrufDatum_222222:  //kein Bedarf
                                        NewRow = dt4905.NewRow();
                                        NewRow["Abrufdatum"] = strTmpDate;
                                        NewRow["Menge"] = 0;
                                        dt4905.Rows.Add(NewRow);
                                        break;
                                    case LVS.ASN.clsVDA4905.const_Menge_999999:  //Rest(Vorschaumenge mehrere Monate
                                    case LVS.ASN.clsVDA4905.const_AbrufDatum_333333:
                                    case LVS.ASN.clsVDA4905.const_AbrufDatum_444444:
                                        break;

                                    case LVS.ASN.clsVDA4905.const_AbrufDatum_555555:  //Zeitraumangabe (Woche Monat)
                                        IsTimePeriode = true;
                                        break;

                                    default:
                                        //Datumswert aus VDA4905 ASN
                                        if (IsTimePeriode)
                                        {
                                            //zeitraum bezogen / Value wert muss umgewandelt werden
                                            strTmpDate = LVS.ASN.clsVDA4905.ConvertValueForTimePeriode(strTmpDate);
                                        }
                                        else
                                        {
                                            DateTime dateTmp = Globals.DefaultDateTimeMinValue;
                                            DateTime.TryParseExact(strTmpDate, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTmp);
                                            strTmpDate = dateTmp.ToShortDateString();
                                        }
                                        break;
                                }//swicht
                            }//if
                            break;

                        // Abrufe die ggf. zur Fortschrittszahl addiert werden müssen
                        case clsASN.const_VDA4905SatzField_SATZ513F09:
                        case clsASN.const_VDA4905SatzField_SATZ513F11:
                        case clsASN.const_VDA4905SatzField_SATZ513F13:
                        case clsASN.const_VDA4905SatzField_SATZ513F15:
                        case clsASN.const_VDA4905SatzField_SATZ513F17:
                        case clsASN.const_VDA4905SatzField_SATZ514F04:
                        case clsASN.const_VDA4905SatzField_SATZ514F06:
                        case clsASN.const_VDA4905SatzField_SATZ514F08:
                        case clsASN.const_VDA4905SatzField_SATZ514F10:
                        case clsASN.const_VDA4905SatzField_SATZ514F12:
                        case clsASN.const_VDA4905SatzField_SATZ514F14:
                        case clsASN.const_VDA4905SatzField_SATZ514F16:
                        case clsASN.const_VDA4905SatzField_SATZ514F18:
                            if (IsSearchCallDetails)
                            {
                                iTmp = 0;
                                Int32.TryParse(row["Value"].ToString(), out iTmp);
                                iTmpValue = iTmp;
                                NewRow = dt4905.NewRow();
                                NewRow["Abrufdatum"] = strTmpDate;
                                NewRow["Menge"] = iTmpValue;
                                dt4905.Rows.Add(NewRow);
                                strTmpDate = string.Empty;
                                iTmpValue = 0;
                            }
                            break;
                    }
                }
            }
            Int32 iKumMenge = this.StartFortschrittzahl;
            foreach (DataRow row in dt4905.Rows)
            {
                iKumMenge = iKumMenge + (Int32)row["Menge"];
                row["kum. Menge"] = iKumMenge;
            }
            this.dgv.DataSource = dt4905;
            //foreach (GridViewColumn col in this.dgv.Columns)
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                this.dgv.Columns[i].HeaderTextAlignment = ContentAlignment.MiddleCenter;
                this.dgv.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                this.dgv.Columns[i].Width = (this.dgv.Width - 25) / 3;
                switch (this.dgv.Columns[i].Name)
                {
                    case "Abrufdatum":
                        break;

                    case "Menge":
                    case "kum. Menge":
                        this.dgv.Columns[i].FormatString ="{0:N0}";
                        break;
                }
            }
            FillInfoBox();
        }
        ///<summary>ctrVDA4905Details / FillInfoBox</summary>
        ///<remarks></remarks>
        private void FillInfoBox()
        {
            StringFormat stringFormat = new StringFormat();
            float[] tabs = { 150, 100, 100, 100 };
            stringFormat.SetTabStops(0, tabs);

            string strInfo = string.Empty;
            strInfo += String.Format("{0}\t{1}", "DFÜ4905:", ((Int32)this.AsnID).ToString())+ Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "DFÜ Ü-Nr.:", this.ASNUENr) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Datum:", this.AsnDate.ToString()) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "Lieferant:",this.Lieferant) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Werk:", this.Werk) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Güterart:", this.Gut.ViewID + " - "+ this.Gut.Bezeichnung ) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Verweis:", this.Gut.Verweis.Trim()) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "Fortschrittszahl:", this.StartFortschrittzahl.ToString()) + Environment.NewLine;
            tbInfoBox.Text = strInfo;
        }
        ///<summary>ctrVDA4905Details / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrVDA4905Details / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            this.InitDGV();
        }

        private void tbInfoBox_TextChanged(object sender, EventArgs e)
        {

        }
        ///<summary>ctrVDA4905Details / dgv_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgv_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                string strToolTip = string.Empty;
                string strCellValue = cell.Value.ToString();
                e.ToolTipText = string.Empty;

                if (cell.ColumnInfo.Name == "Abrufdatum")
                {
                    switch (strCellValue)
                    {
                        case LVS.ASN.clsVDA4905.const_AbrufDatum_000000:
                            strToolTip = "Letztes Abruf-Feld für die vorliegende Liefereinteilung!";
                            break;
                        case LVS.ASN.clsVDA4905.const_AbrufDatum_222222:
                            strToolTip = "Es liegt kein weiterer Bedarf vor!";
                            break;
                        case LVS.ASN.clsVDA4905.const_AbrufDatum_333333:
                            strToolTip = "Rückstand!";
                            break;
                        case LVS.ASN.clsVDA4905.const_AbrufDatum_444444:
                            strToolTip = "Sorfortbedarf!!!";
                            break;
                        case LVS.ASN.clsVDA4905.const_AbrufDatum_555555:
                            strToolTip = "zeitraumbezogener Abruf";
                            break;

                        default:

                            break;
                    }
                }
                if (cell.ColumnInfo.Name == "Abrufdatum")
                {
                    switch (strCellValue)
                    {
                        case LVS.ASN.clsVDA4905.const_Menge_999999:
                            strToolTip = "Restmenge!";
                            break;
                        default:

                            break;
                    }
                }
                e.ToolTipText = strToolTip;

            }
        }
    }
}
