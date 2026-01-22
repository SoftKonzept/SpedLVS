using LVS;
using LVS.Dokumente;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFKalenderItemKommi : AFKalenderItem
    {
        public const string const_ctrName = "KalenderItemKommi";
        public new Globals._GL_USER GL_User;
        public delegate void ThreadCtrInvokeEventHandler();
        public frmDispoKalender Kalender;
        public clsKommission Kommission = new clsKommission();
        internal clsDispoCheck DispoCheck = new clsDispoCheck();
        public bool ToRefresh = false;
        ctrAufträge AuftragCtr;
        public ctrMenu ctrMenu;

        //public delegate void ctrAuftragRefreshEventHandler();
        //public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        //public AFKalenderItemKommi(frmDispoKalender _Kalender)
        public AFKalenderItemKommi()
        {
            InitializeComponent();
            this.Name = AFKalenderItemKommi.const_ctrName;
        }

        private void AFKalenderItemKommi_Load(object sender, EventArgs e)
        {
            Int32 iStatus = 0;
            this.ctrMenu = this.Kalender.menue;
            this.Kommission.FillByID();
            iStatus = Kommission.Status;
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.AP_ID = Kommission.AuftragPosTableID;
            iStatus = ast.GetAuftragsstatus();

            if (iStatus == 7)
            {
                this.ColorFrom = Color.FromArgb(255, 128, 0);
                this.ColorTo = Color.WhiteSmoke;
            }
            else if (iStatus == 8)
            {
                this.ColorFrom = Color.GhostWhite;
                this.ColorTo = Color.WhiteSmoke;
            }
            else
            {
                this.ColorFrom = Color.FromArgb(55, 210, 47);
                this.ColorTo = Color.FromArgb(17, 67, 14);
            }

            if (this.Kommission.IsActiv)
            {
                this.ColorFrom = Color.Yellow;
                this.ColorTo = Color.FromArgb(17, 67, 14);
            }

            SetImagesForInfo();
        }
        //
        //
        //
        private void SetImagesForInfo()
        {
            if (this.Kommission.Dokuments(ref this.Kommission))
            {
                this.ImagePicRight = Sped4.Properties.Resources.form_green_edit;
            }
            else
            {
                this.ImagePicRight = null;
            }
            if (this.Kommission.FahrerKontakt)
            {
                this.ImagePicLeft = Sped4.Properties.Resources.schoolboy;
            }
            else
            {
                this.ImagePicLeft = null;
            }
        }
        //
        //
        //
        public void myKalenderItemKommi_Refresh()
        {
            SetImagesForInfo();
            base.Refresh(); //Refresh AFKalenderItem wegen den Images
            this.Refresh();

        }
        //
        //
        private void AFKalenderItemKommi_Paint(object sender, PaintEventArgs e)
        {
            Rectangle oRectText = new Rectangle(25, 0, this.Width - (2 * 25), this.Height);
            StringFormat DrawFormat = new StringFormat();
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.FormatFlags = StringFormatFlags.LineLimit;
            DrawFormat.Alignment = StringAlignment.Near;
            oRectText.X++;
            oRectText.Y++;

            // Daten auf das Rectangel geschrieben
            e.Graphics.DrawString(Kommission.Beladestelle, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            oRectText.X--;
            oRectText.Y--;
            e.Graphics.DrawString(Kommission.Beladestelle, myFontStyle, Brushes.White, oRectText, DrawFormat);
            DrawFormat.Alignment = StringAlignment.Far;
            oRectText.X++;
            oRectText.Y++;
            e.Graphics.DrawString(Kommission.Entladestelle, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            oRectText.X--;
            oRectText.Y--;
            e.Graphics.DrawString(Kommission.Entladestelle, myFontStyle, Brushes.White, oRectText, DrawFormat);

        }
        //
        //


        //
        //------------------- Zeigt Infos zur Auftrag an, wenn Maus drüber ist  ----------------------
        //
        private void AFKalenderItemKommi_MouseHover(object sender, EventArgs e)
        {
            /*****
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            string strKFZ = string.Empty;
            DateTime dtBeladeZeit = DateTime.MinValue;
            DateTime dtEntladeZeit = DateTime.MaxValue;
            
            AFKalenderItemKommi KommiCtr = (AFKalenderItemKommi)sender;
            DataSet ds = new DataSet();

            if(clsKommission.IsAuftragPositionIn(this.GL_User, KommiCtr.Kommission.AuftragPos_ID));
            {
                if (Kommission.Brutto > 0.00m)
                {
                  ds = clsAuftragPos.GetAuftragPosRecByID(KommiCtr.Kommission.AuftragPos_ID, true);
                }
                else
                {
                  ds = clsAuftragPos.GetAuftragPosRecByID(KommiCtr.Kommission.AuftragPos_ID, false);

                }

                strKFZ = clsKommission.GetZM_KFZByAP_ID(KommiCtr.Kommission.AuftragPos_ID);
                dtBeladeZeit =Convert.ToDateTime(clsKommission.GetBeladedatumByAP_ID(KommiCtr.Kommission.AuftragPos_ID, GL_User.User_ID));
                dtEntladeZeit = Convert.ToDateTime(clsKommission.GetEntladedatumByAP_ID(KommiCtr.Kommission.AuftragPos_ID, GL_User.User_ID));


                for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strInfo = strInfo + "Auftrag / Pos: " + ds.Tables[0].Rows[i]["Auftrag_ID"].ToString() + "/" + ds.Tables[0].Rows[i]["AuftragPos"].ToString() + " \n";
                    DateTime AD = (DateTime) ds.Tables[0].Rows[i]["ADate"];
                    strInfo = strInfo + "Auftragsdatum : "+ AD.ToShortDateString() + " \n";
                    strInfo = strInfo + "Auftraggeber : " +ds.Tables[0].Rows[i]["Auftraggeber"].ToString() + " \n \n";
                    strInfo = strInfo + "Beladestelle: " + ds.Tables[0].Rows[i]["Beladestelle"].ToString() + " \n" +
                                                           ds.Tables[0].Rows[i]["B_Strasse"].ToString() + " \n" +    
                                                           ds.Tables[0].Rows[i]["B_PLZ"].ToString() + " " + ds.Tables[0].Rows[i]["B_Ort"].ToString() + " \n \n";
                    strInfo = strInfo + "Entladestelle: " + ds.Tables[0].Rows[i]["Entladestelle"].ToString() + " \n" +
                                                            ds.Tables[0].Rows[i]["E_Strasse"].ToString() + " \n" + 
                                                            ds.Tables[0].Rows[i]["E_PLZ"].ToString() + " " + ds.Tables[0].Rows[i]["E_Ort"].ToString() + " \n \n";
                    DateTime LT = (DateTime)ds.Tables[0].Rows[i]["T_Date"];
                    DateTime ZF = (DateTime)ds.Tables[0].Rows[i]["ZF"];
                    DateTime VSB = (DateTime)ds.Tables[0].Rows[i]["VSB"];
                    strInfo = strInfo + "Liefertermin: " + LT.ToShortDateString() + " \n";
                    strInfo = strInfo + "Zeitfenter: " + ZF.ToShortTimeString() + " \n";
                    strInfo = strInfo + "Versandbereit: " + VSB.ToShortDateString() + "\n \n";
                    strInfo = strInfo + "Gut: " + ds.Tables[0].Rows[i]["Gut"].ToString() + "\n";
                    strInfo = strInfo + "Gewicht: " + ds.Tables[0].Rows[i]["Gewicht"].ToString() + "[kg] \n";
                    strInfo = strInfo + "Ladenummer: " + ds.Tables[0].Rows[i]["Ladenummer"].ToString() + " \n";
                    strInfo = strInfo + Environment.NewLine;
                    strInfo = strInfo + "--- Dispositionsinformation ---" + Environment.NewLine;
                    strInfo = strInfo + "Fahrzeug: " + strKFZ + Environment.NewLine;
                    strInfo = strInfo + "Beladezeit: " + dtBeladeZeit.ToString() + Environment.NewLine;
                    strInfo = strInfo + "Entladezeit: " + dtEntladeZeit.ToString() + Environment.NewLine;
                }
                info.SetToolTip(this,strInfo);
            }
             * ***/
        }

        //
        //------------------ Delete --------------------------------
        //
        private void miDelete_Click(object sender, EventArgs e)
        {
            //Baustelle
            /***
           DeleteKommissionFromDispoKalender();
             * ***/
        }
        //
        public void DeleteKommissionFromDispoKalender()
        {
            GL_User = Kalender.GL_User;
            if (GL_User.write_Disposition)
            {
                clsKommission Kommi = new clsKommission();
                Kommi.BenutzerID = GL_User.User_ID;

                if (Kommission.AuftragPosTableID > 0)
                {
                    if (clsAuftragPos.GetStatusByAuftragPosTableID(this.GL_User, Kommission.AuftragPosTableID) < 5)
                    {
                        Kommission.ID = Kommission.GetIDfromKommission();
                        Kommission.DeleteKommission();

                        //Status zurück auf 2 setzen
                        clsAuftragsstatus ast = new clsAuftragsstatus();
                        ast.AP_ID = Kommission.AuftragPosTableID;
                        ast.SetStatusBackFromDispo();

                        //löschen aus DB DispoCheck
                        DispoCheck.DeleteDispoCheck();

                        Kalender.KalenderRefresh();

                        //löschen Lieferscheine
                        if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPosTableID))
                        {
                            clsLieferscheine.DeleteLieferscheinByAP_ID(Kommission.AuftragPosTableID);
                        }

                        if (Functions.IsCtrAlreadyOpen(ref this.AuftragCtr._ctrMenu) != null)
                        {
                            AuftragCtr.InitDGV();
                            //Thread workerThread = new Thread(AuftragCtr.init);
                            //workerThread.Start();       
                        }

                    }

                }

            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        /*******************+ Start TEST ********************/
        //
        private void ReSetStatus()
        {
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.Auftrag_ID = Kommission.AuftragID;
            ast.AuftragPos = Kommission.AuftragPos;
            ast.SetStatusBackFromDispo();
        }
        //
        private void DeleteLieferscheine()
        {
            //Baustelle Lieferscheine
            //löschen Lieferscheine
            if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPosTableID))
            {
                clsLieferscheine.DeleteLieferscheinByAP_ID(Kommission.AuftragPosTableID);
            }
        }

        //
        public void RefreshKommiDaten()
        {
            //clsKommission upKommission = new clsKommission();
            //upKommission.ID = this.Kommission.ID;
            //upKommission.FillByID();
            //this.Kommission = upKommission;
            this.Kommission.FillByID();
            myKalenderItemKommi_Refresh();
        }
        //---------------- Details --------------------------
        //beinhaltet auch Info zu Fahrer und Papiere 
        private void miDetails_Click(object sender, EventArgs e)
        {
            if ((this.Kalender != null) || (this.ctrMenu != null))
            {
                this.ctrMenu.OpenFrmKommiDetailsPanel(this.Kalender, this);
            }
        }
        //
        //
        //
        private void AFKalenderItemKommi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            base.bMouseClick = true;
            if ((GL_User.write_Disposition) || (GL_User.read_Disposition))
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.ctrMenu.OpenFrmKommiDetailsPanel(this.Kalender, this);

                }
            }
        }
        //
        //
        //

        private void AFKalenderItemKommi_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                base.bMouseClick = true;
                //MessageBox.Show("Show");
                AFKalenderItemKommi KommiCtr = (AFKalenderItemKommi)sender;
                Kommission.AuftragID = KommiCtr.Kommission.AuftragID;
                Kommission.AuftragPos = KommiCtr.Kommission.AuftragPos;
                Kommission.AuftragPosTableID = KommiCtr.Kommission.AuftragPosTableID;
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            if (e.Button == MouseButtons.Left)
            {
                if (this.Kommission.IsActiv)
                {
                    this.Kommission.IsActiv = false;
                }
                else
                {
                    this.Kommission.IsActiv = true;
                }
                this.Refresh();
            }
        }
        //
        //----------- Print _________________
        //
        private void dokumenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPrintCenter(this);
        }


        public void OpenPrintCenter(AFKalenderItemKommi kommi)
        {
            DataSet ds = new DataSet(); //Leer

            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPrintCenter)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmPrintCenter));
            }
            frmPrintCenter pC = new frmPrintCenter();
            pC._AuftragID = Kommission.AuftragID;
            pC._AuftragPos = Kommission.AuftragPos;
            pC._AuftragPosTableID = Kommission.AuftragPosTableID;
            pC.GL_User = Kalender.GL_User;
            pC._AFKalenderItemKommi = kommi;
            pC.Show();
            pC.BringToFront();
        }

        //
        //
        //
        private void dokumentScannenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this.AuftragCtr._ctrMenu.OpenScanFrm(this.Kommission.AuftragTableID,this.Kommission.AuftragPos_ID, 0, 0, this);
        }

    }
}

