using LVS;
using System;
using System.Data;
using System.Windows.Forms;
namespace Sped4
{
    public partial class ctrDispoKommiDetails : UserControl
    {
        public Globals._GL_USER GL_User;

        public clsKommission Kommission = new clsKommission();
        public clsTour Tour = new clsTour();
        frmDispoKalender Kalender;
        frmKommiDetailsPanel mdiDetails;
        public string docText = string.Empty;
        public string fahrerText = string.Empty;
        public string neueInfo = string.Empty;
        public string strKFZ = string.Empty;

        public ctrDispoKommiDetails(decimal KommiID, frmDispoKalender _Kalender, frmKommiDetailsPanel _mdiDetails)
        {
            InitializeComponent();
            Kalender = _Kalender;
            mdiDetails = _mdiDetails;
            Kommission.ID = KommiID;
            Kommission.FillByID();
            initForm();
        }
        //
        //
        //
        private void initForm()
        {
            Kommission.FillByID();
            Kommission.GetKontaktInfo();
            tbKontaktZeit.Text = Functions.FormatShortDateTime(DateTime.Now);
            tbAlteInfos.Text = Kommission.KontaktInfo;
            //Auftragsdaten in Form anzeigen
            FillAuftragsInfo();
        }
        //
        //----------- Info Feld der Auftragsdaten wird gefüllt -------------------
        //
        private void FillAuftragsInfo()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Col1", typeof(String));
            dt.Columns.Add("Col2", typeof(String));

            FillTable(ref dt);

            dgvInfo.DataSource = dt;
            dgvInfo.Columns["Col1"].Width = 110;
            dgvInfo.Columns["Col2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInfo.Columns["Col2"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
            {
                dgvInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }
        //
        //
        private DataTable FillTable(ref DataTable dt)
        {
            strKFZ = clsFahrzeuge.GetKFZByID(this.GL_User, Tour.KFZ_ZM);
            string strDefault = string.Empty;

            DataRow row1 = dt.NewRow();
            row1[0] = "aktuelles Fahrzeug";
            row1[1] = strKFZ;
            dt.Rows.Add(row1);

            DataRow row2 = dt.NewRow();
            row2[0] = "Auftrag / Auftragsposition:";
            string strAuftrag = string.Empty;
            if ((Kommission.AuftragID > 0) & (Kommission.AuftragPosTableID >= 0))
            {
                strAuftrag = Kommission.AuftragID.ToString() + "/" +
                           Kommission.AuftragPos.ToString();
            }
            else
            {
                strAuftrag = string.Empty;
            }
            row2[1] = strAuftrag;
            dt.Rows.Add(row2);

            DataRow row3 = dt.NewRow();
            row3[0] = "Beladestelle:";
            string strBADR = string.Empty;
            if (Kommission.B_ID > 0)
            {
                strBADR = clsADR.GetADRStringMZ(Kommission.B_ID);
            }
            else
            {
                strBADR = strDefault;
            }
            row3[1] = strBADR;
            dt.Rows.Add(row3);

            DataRow row4 = dt.NewRow();
            row4[0] = "Entladestelle:";
            string strEADR = string.Empty;
            if (Kommission.B_ID > 0)
            {
                strEADR = clsADR.GetADRStringMZ(Kommission.E_ID);
            }
            else
            {
                strEADR = strDefault;
            }
            row4[1] = strEADR;
            dt.Rows.Add(row4);

            DataRow row5 = dt.NewRow();
            row5[0] = "Gut:";
            string strGut = string.Empty;
            if (Kommission.B_ID > 0)
            {
                //strGut = clsArtikel.GetAllArtikelString(Kommission.AuftragID, Kommission.AuftragPos);
            }
            else
            {
                strGut = strDefault;
            }
            row5[1] = strGut;
            dt.Rows.Add(row5);

            DataRow row6 = dt.NewRow();
            row6[0] = "Gewicht [kg]:";
            string strGewicht = string.Empty;
            if (Kommission.Menge > 0)
            {
                strGewicht = Functions.FormatDecimal(Kommission.Menge);
            }
            else
            {
                strGewicht = Convert.ToString("0");
            }
            row6[1] = strGewicht;
            dt.Rows.Add(row6);

            return dt;
        }
        //
        //
        private void AssignValue()
        {
            if (tbNeueInfo.Text == "")
            {
                neueInfo = tbNeueInfo.Text;
            }
            else
            {
                neueInfo = tbNeueInfo.Text + "\r\n";
            }

            string alteInfo = tbAlteInfos.Text.ToString() + "\r\n";

            neueInfo = docText + fahrerText + neueInfo;
            string InsertInfo = neueInfo;
            //Update FahrerInfo und Papiere
            Kommission.KontaktInfo = InsertInfo;
            Kommission.InsertFahrerInfo();
        }
        //
        //--------------- speichern -------------------
        //
        private void tsBtnSpeichern_Click(object sender, EventArgs e)
        {
            AssignValue();
            //Kalender.KalenderRefresh();
            //mdiDetails.Close();
            mdiDetails.CloseFrmKommiDetailsPanel();
        }
        //
        //------------ schliessen--------------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            mdiDetails.CloseFrmKommiDetailsPanel();
        }




    }
}
