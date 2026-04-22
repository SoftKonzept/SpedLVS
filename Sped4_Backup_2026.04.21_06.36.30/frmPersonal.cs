using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmPersonal : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;

        public bool update = false;
        internal Image imgPassbild;
        internal DateTime BeschaeftigtBis = DateTime.MaxValue;
        public decimal ID;
        ctrPersonal_List _ctrPersonalList = new ctrPersonal_List();

        public frmPersonal(ctrPersonal_List ctrPersonalList, bool _update)
        {
            InitializeComponent();
            ID = 0;
            _ctrPersonalList = ctrPersonalList;
            GL_User = _ctrPersonalList.GL_User;
            update = _update;
        }


        //
        //
        //
        private void frmPersonal_Load(object sender, EventArgs e)
        {
            cbAbteilung.DataSource = Enum.GetNames(typeof(enumAbteilung));
            cbBeruf.DataSource = Enum.GetNames(typeof(enumBeruf));
            cbAnrede.DataSource = Enum.GetNames(typeof(enumAnrede));
        }
        //
        //-------------------------------------- schließen der Form  ---------------
        //
        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            ID = 0;
            this.Close();
        }
        //
        //-------------------------------- add new Item to DB  -----------------
        //
        private void btn1_Click(object sender, EventArgs e)
        {
            //------ Eingabeüberprüfung der Pflichtfelder-----
            if (CheckInput())
            {
                AssignVar();

                //-- Eingabemaske auf Null setzen
                CleanFrm();
                //-- Aktualisierung DataGrid
                _ctrPersonalList.initList();
                _ctrPersonalList.Refresh();
            }
        }
        //
        //--------------------------------------------------- Clean Form  ----------------------------------
        //
        private void CleanFrm()
        {
            //---------- Zusweisung der Werte
            tbName.Text = "";
            tbVorname.Text = "";
            tbStr.Text = "";
            tbPLZ.Text = "";
            tbOrt.Text = "";
            tbTel.Text = "";
            tbMail.Text = "";
            //dtpSeit.Value = DateTime.Today.Date;
            //dtpBis.Value = DateTime.Today.Date;
            tbNotiz.Text = "";
        }
        //
        //------------------------------------------------------- Trim und Zuweisen der Inputdaten -------------
        //
        private void AssignVar()
        {
            clsPersonal Pers = new clsPersonal();
            Pers.BenutzerID = GL_User.User_ID;

            //--------- Leerzeichen werden abgeschnitten
            tbName.Text = tbName.Text.ToString().Trim();
            tbVorname.Text = tbVorname.Text.ToString().Trim();
            tbStr.Text = tbStr.Text.ToString().Trim();
            tbPLZ.Text = tbPLZ.Text.ToString().Trim();
            tbOrt.Text = tbOrt.Text.ToString().Trim();
            tbTel.Text = tbTel.Text.ToString().Trim();
            tbMail.Text = tbMail.Text.ToString().Trim();


            //---------- Zusweisung der Werte
            Pers.Name = tbName.Text;
            Pers.Vorname = tbVorname.Text;
            Pers.Str = tbStr.Text;
            Pers.PLZ = tbPLZ.Text;
            Pers.Ort = tbOrt.Text;
            Pers.Telefon = tbTel.Text;
            Pers.Mail = tbMail.Text;
            Pers.Abteilung = cbAbteilung.Text;
            Pers.Beruf = cbBeruf.Text;
            Pers.seit = DateTime.Today;     //nachträglich rausgenommen
            Pers.bis = BeschaeftigtBis;
            Pers.Notiz = tbNotiz.Text;
            Pers.Anrede = cbAnrede.Text;
            //

            if (!update)
            {
                if (GL_User.write_Personal)
                {
                    // --- Eintrag in DB ----
                    Pers.AddItem();
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
            else
            {
                if (GL_User.write_Relation)
                {
                    //---- update in DB
                    Pers.ID = ID;
                    if (cbBis.Checked == true)
                    {
                        Pers.bis = BeschaeftigtBis;
                    }
                    Pers.updatePersonal();
                    this.Close();
                }
            }

        }
        //
        //--------------------- Check der Inputdaten ------------------
        //
        private bool CheckInput()
        {
            bool status = true;

            string strHelp;
            strHelp = "Folgende Eingaben sind nicht korrekt:\n";
            char[] ad = { '@' };
            char[] tele = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '/' };
            char[] bst = { 'a', 'A', 'b', 'c', 'C', 'd', 'D', 'e', 'E', 'F', 'f', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };
            char[] Uml = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü' };
            char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //---------------- Check der Pflichtfelder ------------------
            if (tbName.Text == "")
            {
                strHelp = strHelp + "Name \n";
                status = false;
            }
            if (tbVorname.Text == "")
            {
                strHelp = strHelp + "Vorname \n";
                status = false;
            }
            //---------------- Check der Kann-Felder ------------------
            //---- Prüfung auf korrekte Eingabe der Felder ------------
            if (tbTel.Text != "")
            {
                if (tbTel.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Telefonnummer enthält Buchstaben \n";
                    status = false;
                }
            }


            if (tbMail.Text != String.Empty)
            {
                if (tbMail.Text.ToString().IndexOfAny(ad) == -1)
                {
                    strHelp = strHelp + "E-Mail beinhaltet kein '@' \n";
                    status = false;
                }

                if (tbMail.Text.ToString().IndexOfAny(Uml) != -1)
                {
                    strHelp = strHelp + "E-Mail beinhaltet Umlaute \n";
                    status = false;
                }
            }
            if (status)
            {
                return true;
            }
            else
            {
                MessageBox.Show(strHelp);
                return false;
            }
        }
        //
        //----------------------------- setzt den zu ändernen Datensatz  --------------
        //
        public void SetUpdateItem(DataSet ds)
        {
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                tbName.Text = ds.Tables[0].Rows[i]["Name"].ToString();
                tbVorname.Text = ds.Tables[0].Rows[i]["Vorname"].ToString();
                tbStr.Text = ds.Tables[0].Rows[i]["Str"].ToString();
                tbPLZ.Text = ds.Tables[0].Rows[i]["PLZ"].ToString();
                tbOrt.Text = ds.Tables[0].Rows[i]["Ort"].ToString();
                tbTel.Text = ds.Tables[0].Rows[i]["Telefon"].ToString();
                tbMail.Text = ds.Tables[0].Rows[i]["Mail"].ToString();
                cbAbteilung.Text = ds.Tables[0].Rows[i]["Abteilung"].ToString();
                cbBeruf.Text = ds.Tables[0].Rows[i]["Beruf"].ToString();
                DateTime tmpDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["bis"].ToString());
                if (tmpDate.Date == DateTime.MaxValue.Date)
                {
                    cbBis.Checked = false;
                }
                else
                {
                    cbBis.Checked = true;
                }
                tbNotiz.Text = ds.Tables[0].Rows[i]["Notiz"].ToString();
                cbAnrede.Text = ds.Tables[0].Rows[i]["Anrede"].ToString();

                GetPassbild(ID);
            }
        }
        //
        //
        //
        private void cbBis_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBis.Checked == true)
            {
                BeschaeftigtBis = DateTime.Today.Date;
            }
            else
            {
                BeschaeftigtBis = DateTime.MaxValue;
            }
        }
        //
        //
        //
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                clsImages img = new clsImages();

                string filename = string.Empty;
                filename = ofd.FileName;
                if (filename.ToString() != "")
                {
                    img.ImageIn = Image.FromFile(filename);
                    if (ID > 0)
                    {
                        img.WriteToPersonalImage(ID);
                        GetPassbild(ID);
                    }
                    else
                    {
                        imgPassbild = img.ImageIn;
                        pictureBox1.Image = imgPassbild;
                        pictureBox1.Refresh();
                    }
                }
            }
        }
        //
        //
        //
        private void GetPassbild(decimal _ID)
        {
            clsPersonal Personal = new clsPersonal();
            Personal.BenutzerID = GL_User.User_ID;
            Personal.ID = _ID;
            if (Personal.Passbild != null)
            {
                pictureBox1.Image = Personal.Passbild;
                pictureBox1.Refresh();
            }
        }
        //
        //---------------- Speichern Formulardaten ---------------
        //
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //------ Eingabeüberprüfung der Pflichtfelder-----
            if (CheckInput())
            {
                AssignVar();

                //-- Eingabemaske auf Null setzen
                CleanFrm();
                //-- Aktualisierung DataGrid
                _ctrPersonalList.initList();
                _ctrPersonalList.Refresh();
            }
        }
        //
        //----------------- schliessen des Formular --------------
        //
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            ID = 0;
            this.Close();
        }
    }
}
