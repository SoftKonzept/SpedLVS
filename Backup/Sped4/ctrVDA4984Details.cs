using LVS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrVDA4984Details : UserControl
    {
        public Globals._GL_USER GLUser;
        internal DataRow VDA4984Row;
        internal clsEdiVDA4984Value vda4984;


        internal decimal AsnID = 0;
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
        public ctrVDA4984Details(DataRow myRow)
        {
            InitializeComponent();
            VDA4984Row = myRow;
        }
        ///<summary>ctrVDA4905Details / ctrVDA4905Details_Load</summary>
        ///<remarks></remarks>
        private void ctrVDA4905Details_Load(object sender, EventArgs e)
        {
            if (VDA4984Row is DataRow)
            {
                this.Gut = null;
                this.Gut = (clsGut)VDA4984Row["GArt"];
                this.Lieferant = VDA4984Row["Lieferant"].ToString();

                decimal decTmp = 0;
                decimal.TryParse(VDA4984Row["LE#"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    vda4984 = new clsEdiVDA4984Value();
                    vda4984.InitClass(ref this.GLUser, (int)decTmp);
                    InitDGV();
                }
            }
        }
        ///<summary>ctrVDA4905Details / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            this.vda4984.dtVDA4984_DocNo.DefaultView.RowFilter = "ArtikelVerweis LIKE '" + this.Gut.Verweis + "'";
            DataTable dtSource = this.vda4984.dtVDA4984_DocNo.DefaultView.ToTable();
            this.dgv.DataSource = dtSource;
            foreach (GridViewColumn col in this.dgv.Columns)
            {
                GridViewDataColumn tmpDataCol;
                switch (col.Name)
                {
                    case "Id":
                        col.IsVisible = true; // Functions.CheckUserForAdminComtec(this.GL_User);
                        break;

                    case "DiffQTY":
                    case "EfzQTY":
                    case "DeliveryQTY":
                        //col.ImageLayout = ImageLayout.Center;
                        //col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        break;

                    case "DeliveryDate":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:d}";
                        break;
                    default:
                        col.IsVisible = false;
                        break;
                }
            }
            this.dgv.BestFitColumns();
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
            strInfo += String.Format("{0}", "vda4984") + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "Doc-No:", this.vda4984.DocNo.ToString("000000")) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Datum:", this.vda4984.DocDate.ToString("dd.MM.yyyy")) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "Lieferant:", this.Lieferant) + Environment.NewLine;
            //strInfo += String.Format("{0}\t\t{1}", "Werk:", this.Werk) + Environment.NewLine;
            strInfo += String.Format("{0}\t\t{1}", "Güterart:", this.Gut.ViewID + " - " + this.Gut.Bezeichnung) + Environment.NewLine;
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
                e.ToolTipText = "noch leer";

                //if (cell.ColumnInfo.Name == "Abrufdatum")
                //{
                //    switch (strCellValue)
                //    {
                //        case LVS.ASN.clsVDA4905.const_AbrufDatum_000000:
                //            strToolTip = "Letztes Abruf-Feld für die vorliegende Liefereinteilung!";
                //            break;
                //        case LVS.ASN.clsVDA4905.const_AbrufDatum_222222:
                //            strToolTip = "Es liegt kein weiterer Bedarf vor!";
                //            break;
                //        case LVS.ASN.clsVDA4905.const_AbrufDatum_333333:
                //            strToolTip = "Rückstand!";
                //            break;
                //        case LVS.ASN.clsVDA4905.const_AbrufDatum_444444:
                //            strToolTip = "Sorfortbedarf!!!";
                //            break;
                //        case LVS.ASN.clsVDA4905.const_AbrufDatum_555555:
                //            strToolTip = "zeitraumbezogener Abruf";
                //            break;

                //        default:

                //            break;
                //    }
                //}
                //if (cell.ColumnInfo.Name == "Abrufdatum")
                //{
                //    switch (strCellValue)
                //    {
                //        case LVS.ASN.clsVDA4905.const_Menge_999999:
                //            strToolTip = "Restmenge!";
                //            break;
                //        default:

                //            break;
                //    }
                //}
                e.ToolTipText = strToolTip;

            }
        }


    }
}
