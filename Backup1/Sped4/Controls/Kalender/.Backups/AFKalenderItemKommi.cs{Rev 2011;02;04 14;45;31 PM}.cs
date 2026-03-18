using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sped4.Classes;

namespace Sped4.Controls
{
    public partial class AFKalenderItemKommi : Sped4.Controls.AFKalenderItem
    {
        public Globals._GL_USER GL_User;
        public clsKommission Kommission = new clsKommission();
        internal clsDispoCheck DispoCheck = new clsDispoCheck();
        public bool ToRefresh = false;
       // public Globals.DispoCheck _DispoCheck = new Globals.DispoCheck();
        frmDispoKalender Kalender;
        ctrAufträge AuftragCtr;

        public delegate void ctrAuftragRefreshEventHandler();
        public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        public AFKalenderItemKommi(frmDispoKalender _Kalender, ctrAufträge _AuftragCtr)
        {
            InitializeComponent();
            Kalender = _Kalender;
            GL_User = Kalender.GL_User;
            AuftragCtr = _AuftragCtr;
            this.Name = "KalenderItemKommi";
        }

        private void AFKalenderItemKommi_Load(object sender, EventArgs e)
        {
            //DispoCheck = Functions.InitDispoCheck(DispoCheck);
             
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.Auftrag_ID = Kommission.AuftragID;
            ast.AuftragPos = Kommission.AuftragPos;
           
           if (ast.GetAuftragsstatus() >4)
            {
              this.ColorFrom = Color.FromArgb(255, 128, 0);
              this.ColorTo = Color.WhiteSmoke;
            }
            else
            {
              this.ColorFrom = Color.FromArgb(55, 210, 47);
              this.ColorTo = Color.FromArgb(17, 67, 14);
            }
           if (this.Kommission.Dokuments(this.Kommission.AuftragPos_ID))
           {
               this.ImagePicRight = Sped4.Properties.Resources.form_green_edit;
           }
           if (this.Kommission.Kontakt(this.Kommission.AuftragPos_ID))
           {
               this.ImagePicLeft = Sped4.Properties.Resources.schoolboy;
           }
        }
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
        //------------------- Zeigt Infos zur Auftrag an, wenn Maus drüber ist  ----------------------
        //
        private void AFKalenderItemKommi_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            
            AFKalenderItemKommi KommiCtr = (AFKalenderItemKommi)sender;
            DataSet ds = new DataSet();
            if (Kommission.tatGewicht > 0.00m)
            {
              ds = clsAuftragPos.GetAuftragPosRecByID(KommiCtr.Kommission.AuftragPos_ID, true);
            }
            else
            {
              ds = clsAuftragPos.GetAuftragPosRecByID(KommiCtr.Kommission.AuftragPos_ID, false);
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
            }
            info.SetToolTip(this,strInfo);
        }
        //
        //--------------------- Menü rechte Maustaste ---------------------------------
        //
        private void AFKalenderItemKommi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                miDetails_Click(sender, e);
            }
        }
        //
        //------------------ Delete --------------------------------
        //
        private void miDelete_Click(object sender, EventArgs e)
        {
            /******
          GL_User = Kalender.GL_User;
          if (GL_User.Disposition)
          {
            clsKommission Kommi = new clsKommission();
            Kommi.BenutzerID = GL_User.User_ID;

            if (Kommission.AuftragPos_ID > 0)
            {
              if (clsAuftragPos.GetStatus(Kommission.AuftragID, Kommission.AuftragPos) < 5)
              {
                Kommission.ID = Kommission.GetIDfromKommission();
                Kommission.DeleteKommiPos();
                Kalender.KalenderRefresh();
                //ctrAufträge.myInstanceHandle.init();
                //Status zurück auf 2 setzen
                clsAuftragsstatus ast = new clsAuftragsstatus();
                ast.Auftrag_ID = Kommission.AuftragID;
                ast.AuftragPos = Kommission.AuftragPos;
                ast.SetStatusBackFromDispo();
                AuftragCtr.init();
                //ctrAuftragRefresh();

                //löschen aus DB DispoCheck
                DispoCheck.DeleteDispoCheck();

                //löschen Lieferscheine
                if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPos_ID))
                {
                  clsLieferscheine.DeleteLieferscheinByAP_ID(Kommission.AuftragPos_ID);
                }
              }
            }
          }
          else
          {
            clsMessages.User_NoAuthen();
          }
            ****/
            DeleteKommissionFromDispoKalender();
        }
        //
        public void DeleteKommissionFromDispoKalender()
        {
            GL_User = Kalender.GL_User;
            if (GL_User.Disposition)
            {
                clsKommission Kommi = new clsKommission();
                Kommi.BenutzerID = GL_User.User_ID;

                if (Kommission.AuftragPos_ID > 0)
                {
                    if (clsAuftragPos.GetStatus(Kommission.AuftragID, Kommission.AuftragPos) < 5)
                    {
                        Kommission.ID = Kommission.GetIDfromKommission();
                        Kommission.DeleteKommiPos();
                        Kalender.KalenderRefresh();
                        //ctrAufträge.myInstanceHandle.init();
                        //Status zurück auf 2 setzen
                        clsAuftragsstatus ast = new clsAuftragsstatus();
                        ast.Auftrag_ID = Kommission.AuftragID;
                        ast.AuftragPos = Kommission.AuftragPos;
                        ast.SetStatusBackFromDispo();
                        AuftragCtr.init();
                        //ctrAuftragRefresh();

                        //löschen aus DB DispoCheck
                        DispoCheck.DeleteDispoCheck();

                        //löschen Lieferscheine
                        if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPos_ID))
                        {
                            clsLieferscheine.DeleteLieferscheinByAP_ID(Kommission.AuftragPos_ID);
                        }
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //---------------- Details --------------------------
        //beinhaltet auch Info zu Fahrer und Papiere 
        private void miDetails_Click(object sender, EventArgs e)
        {
          if (Functions.IsFormAlreadyOpen(typeof(frmKommiDetailsPanel)) != null)
          {
            Functions.FormClose(typeof(frmKommiDetailsPanel));
          }
          frmKommiDetailsPanel Details = new frmKommiDetailsPanel(Kalender, AuftragCtr, this);
          Details.StartPosition = FormStartPosition.CenterScreen;
          Details.Show();
          Details.BringToFront();
        }
        //
        //
        //
        private void miUpdateEntladeZeit_Click(object sender, EventArgs e)
        {
            if (Functions.IsFormAlreadyOpen(typeof(frmDispoKommiChange)) != null)
            {
                Functions.FormClose(typeof(frmDispoKommiChange));
            }
            frmDispoKommiChange kc = new frmDispoKommiChange(Kommission, Kalender);
            kc.StartPosition = FormStartPosition.CenterScreen;
            kc.Show();
            kc.BringToFront();
          //}
        }
        //
        //------------------- Dokumente Abholschein ----------
        //
        private void miAbholschein_Click(object sender, EventArgs e)
        {
          if (Kommission.AuftragID>0)
          {
            Int32 AuftragID = Kommission.AuftragID;
            Int32 AuftragPos = Kommission.AuftragPos;
            DataSet ds = new DataSet(); //Leer
            //Panel für ADR CTR öffnen
            if (Functions.IsFormAlreadyOpen(typeof(frmReportViewer)) != null)
            {
              Functions.FormClose(typeof(frmReportViewer));
            }
            frmReportViewer reportview = new frmReportViewer(AuftragID, AuftragPos, ds, Globals.enumDokumentenart.Abholschein.ToString());
            reportview.GL_User = GL_User;
            reportview.StartPosition = FormStartPosition.CenterParent;
            reportview.Show();
            reportview.BringToFront();
          }
        }
        //
        //----------- Dokumente Lieferscheine ---------------------
        //
        private void miLieferschein_Click(object sender, EventArgs e)
        {
          if (Kommission.AuftragID > 0)
          {
            Int32 AuftragID = Kommission.AuftragID;
            Int32 AuftragPos = Kommission.AuftragPos;
            DataSet ds = new DataSet(); //Leer
            //Panel für ADR CTR öffnen
            if (Functions.IsFormAlreadyOpen(typeof(frmReportViewer)) != null)
            {
              Functions.FormClose(typeof(frmReportViewer));
            }
            frmReportViewer reportview = new frmReportViewer(AuftragID, AuftragPos, ds, Globals.enumDokumentenart.Lieferschein.ToString());
            reportview.GL_User = GL_User;
            reportview.StartPosition = FormStartPosition.CenterParent;
            reportview.Show();
            reportview.BringToFront();
          }
        }        
        //
        //
        //
        private void AFKalenderItemKommi_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //MessageBox.Show("Show");
                AFKalenderItemKommi KommiCtr = (AFKalenderItemKommi)sender;
                Kommission.AuftragID = KommiCtr.Kommission.AuftragID;
                Kommission.AuftragPos = KommiCtr.Kommission.AuftragPos;
                Kommission.AuftragPos_ID = KommiCtr.Kommission.AuftragPos_ID;
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }






    }
}

