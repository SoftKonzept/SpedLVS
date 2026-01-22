namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Drawing;
    using System.IO;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;


    /// <summary>
    /// Summary description for repLieferschein.
    /// </summary>
    public partial class docBriefkofpHeisiep : Report
    {
        public Globals._GL_USER GL_User;
        public bool SetKontaktDaten = false;

        internal decimal _MandantenID = 0;

        public docBriefkofpHeisiep()
        {
            InitializeComponent();
            //InitBriefkopf();
        }
        //
        //--------------- Initialisierung Briefkopf / Fussdaten --------------------
        //
        public void InitBriefkopf()
        {
            if (_MandantenID > 0)
            {
                clsBriefkopfdaten bk = new clsBriefkopfdaten();
                bk.MandantenID = _MandantenID;
                bk.InitBriefkopfDaten();
                //Absender
                tbFirmaAbsender.Value = bk.Absender;

                //Briefkopfadresse
                tbBKFirma1.Value = bk.Name1BK;
                tbBKFirma2.Value = bk.Name2BK;
                tbBKStr.Value = bk.StrBK;
                //tbADRzHd.Value über Transportdaten
                tbBKPLZOrt.Value = bk.PLZBK + " - " + bk.OrtBK;
                tbBKUST.Value = bk.USTBK;
                tbBKSteuer.Value = bk.SteuerBK;

                //Ort Datum
                tbOrt.Value = bk.OrtBK + ", ";
                tbDatum.Value = DateTime.Today.Date.ToShortDateString();

                //Kontakt
                if (SetKontaktDaten)
                {
                    SetKontaktdateToDoc();
                }
                else
                {
                    SetKontaktNeutral();
                }
                //LOGO

                if (bk.LogoPfad != null)
                {
                    if (File.Exists(@bk.LogoPfad))
                    {
                        pbLogo.Value = Image.FromFile(bk.LogoPfad);
                        pbLogo.Sizing = ImageSizeMode.ScaleProportional;
                    }
                }
                if (bk.ZertPfad != null)
                {
                    if (File.Exists(@bk.ZertPfad))
                    {
                        pbZert.Value = Image.FromFile(bk.ZertPfad);
                        pbZert.Sizing = ImageSizeMode.ScaleProportional;
                    }
                }
                //pbLogo.Value = Image.FromFile(bk.LogoPfad);
                //pbLogo.Sizing = ImageSizeMode.ScaleProportional;
                //pbZert.Value = Image.FromFile(bk.ZertPfad);
                //pbZert.Sizing = ImageSizeMode.ScaleProportional;

                //Fusskopf
                tbBank1.Value = bk.Bank1;
                tbBank2.Value = bk.Bank2;
                tbBank3.Value = bk.Bank3;
                tbKt1.Value = bk.Kto1;
                tbKt2.Value = bk.Kto2;
                tbKt3.Value = bk.Kto3;
                tbBLZ1.Value = bk.BLZ1;
                tbBLZ2.Value = bk.BLZ2;
                tbBLZ3.Value = bk.BLZ3;

                tbHR.Value = bk.HR;
                tbText.Value = bk.Text;
            }
        }
        //
        //----------------- Ansprechpartner Daten neutralisieren --------
        //
        public void SetKontaktNeutral()
        {
            tbKontakt.Value = string.Empty;
            tbAnsprechpartner.Value = string.Empty;
            tbTel.Value = string.Empty;
            tbMail.Value = string.Empty;
            tbFax.Value = string.Empty;
        }
        //
        //
        //
        public void SetKontaktdateToDoc()
        {
            tbAnsprechpartner.Value = GL_User.Name + ", " + GL_User.Vorname;
            tbTel.Value = "Tel.: " + GL_User.Telefon;
            tbFax.Value = "Fax: " + GL_User.Fax;
            tbMail.Value = "E-Mail: " + GL_User.Mail;
        }
    }
}