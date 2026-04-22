using LVS;
using System;


namespace Sped4
{
    public partial class frmGueterArten : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        internal string[,] GArtArray = null;
        internal clsGut Gut = new clsGut();

        public ctrGueterArtListe _ctrGut = new ctrGueterArtListe();
        public bool _update;
        public decimal gaID;

        //public frmGueterArten(ctrGueterArtListe ctrGut, bool update)
        public frmGueterArten()
        {
            InitializeComponent();
            //_ctrGut = ctrGut;
            // _update = update;
            // initForm();
        }
        ///<summary>frmGueterArten/ frmGueterArten_Load</summary>
        ///<remarks></remarks>
        private void frmGueterArten_Load(object sender, EventArgs e)
        {
            this._update = this._ctrGut.bUpdateGArtDaten;
            this.Gut = this._ctrGut.Gut;
            initForm();
        }
        ///<summary>frmGueterArten/ initForm</summary>
        ///<remarks></remarks>
        private void initForm()
        {
            ResetFrm();
            InitGArtArray();
            //update
            if (_update)
            {
                /****
              ArrayList arrayL = new ArrayList();
              arrayL = _ctrGut.GueterArtenDaten();
              gaID=Convert.ToDecimal(arrayL[0].ToString());
              tbViewID.Text       = arrayL[1].ToString();
              tbBezeichung.Text   = arrayL[2].ToString();
              tbDicke.Text        = arrayL[3].ToString();
              tbBreite.Text       = arrayL[4].ToString();
              tbLaenge.Text       = arrayL[5].ToString();
              tbHoehe.Text        = arrayL[6].ToString();
              ***/
                gaID = Gut.ID;
                tbViewID.Text = Gut.ViewID;
                tbBezeichung.Text = Gut.Bezeichnung;
                tbDicke.Text = Functions.FormatDecimal(Gut.Dicke);
                tbBreite.Text = Functions.FormatDecimal(Gut.Breite);
                tbLaenge.Text = Functions.FormatDecimal(Gut.Laenge);
                tbHoehe.Text = Functions.FormatDecimal(Gut.Hoehe);

                //MassAnzahl wird geändert
                nudMassAnzahl.Value = Convert.ToDecimal(Gut.MassAnzahl);
            }
        }
        ///<summary>frmGueterArten/ InitGArtArray</summary>
        ///<remarks></remarks>
        private void InitGArtArray()
        {
            string[,] tmpArray = {
                                  {"Coil", "2"},
                                  {"Bandstahl", "2"},


                                  {"Blech", "3" },
                                  {"Rohr", "3" },

                                  {"Palette", "4" },
                                  {"Gibo", "4" },
                                  {"Gitter", "4" },
                                  {"Box", "4" },
                                  {"Behälter", "4" },
                                  {"Platinen", "4" }
                              };
            GArtArray = tmpArray;
        }

        ///<summary>frmGueterArten/ AssignValue</summary>
        ///<remarks></remarks>
        private void AssignValue()
        {
            if ((tbBezeichung.Text != "") & (tbViewID.Text != ""))
            {
                Gut = new clsGut();
                Gut.BenutzerID = GL_User.User_ID;
                Gut.Bezeichnung = tbBezeichung.Text;
                Gut.ViewID = tbViewID.Text;
                Gut.Dicke = Convert.ToDecimal(tbDicke.Text);
                Gut.Breite = Convert.ToDecimal(tbBreite.Text);
                Gut.Laenge = Convert.ToDecimal(tbLaenge.Text);
                Gut.Hoehe = Convert.ToDecimal(tbHoehe.Text);
                Gut.MassAnzahl = Convert.ToInt32(nudMassAnzahl.Value);

                if (_update)
                {
                    Gut.ID = gaID;
                    Gut.UpdateGueterArt();
                    _update = false;
                    this._ctrGut.InitDGV();
                    this.Close();
                }
                else
                {
                    Gut.Add();
                }
            }
        }
        ///<summary>frmGueterArten/ tsbSpeichern_Click</summary>
        ///<remarks></remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Gut)
            {
                if (!clsGut.ViewIDExists(this.GL_User, tbViewID.Text))
                {
                    AssignValue();
                    initForm();
                    _ctrGut.InitDGV();
                }
                else
                {
                    //
                    if (clsGut.ViewIDExistsByID(this.GL_User, tbViewID.Text, gaID))
                    {
                        AssignValue();
                        initForm();
                        _ctrGut.InitDGV();
                    }
                    else
                    {
                        clsMessages.Gut_ViewIDIsUsed();
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>frmGueterArten/ tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmGueterArten/ ResetFrm</summary>
        ///<remarks></remarks>
        private void ResetFrm()
        {
            string defNull = "0,0";
            tbBezeichung.Text = string.Empty;
            tbViewID.Text = string.Empty;
            tbBreite.Text = defNull;
            tbDicke.Text = defNull;
            tbHoehe.Text = defNull;
            tbLaenge.Text = defNull;
            nudMassAnzahl.Value = 1;
            ResetEnableTbBox();
        }
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks></remarks>
        private void ResetEnableTbBox()
        {
            tbBreite.Enabled = true;
            tbDicke.Enabled = false;
            tbLaenge.Enabled = false;
            tbHoehe.Enabled = false;
        }
        ///<summary>frmGueterArten/ tbDicke_Validated</summary>
        ///<remarks></remarks>
        private void tbDicke_Validated(object sender, EventArgs e)
        {
            tbDicke.Text = tbDicke.Text.Trim();
            Decimal decTmp = 0.0M;
            if (!decimal.TryParse(tbDicke.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
            }
            tbDicke.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>frmGueterArten/ tbBreite_Validated</summary>
        ///<remarks></remarks>
        private void tbBreite_Validated(object sender, EventArgs e)
        {
            tbBreite.Text = tbBreite.Text.Trim();
            Decimal decTmp = 0.0M;
            if (!decimal.TryParse(tbBreite.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
            }
            tbBreite.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>frmGueterArten/ tbLaenge_Validated</summary>
        ///<remarks></remarks>
        private void tbLaenge_Validated(object sender, EventArgs e)
        {
            tbLaenge.Text = tbLaenge.Text.Trim();
            Decimal decTmp = 0.0M;
            if (!decimal.TryParse(tbLaenge.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
            }
            tbLaenge.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>frmGueterArten/ tbHoehe_Validated</summary>
        ///<remarks></remarks>
        private void tbHoehe_Validated(object sender, EventArgs e)
        {
            tbHoehe.Text = tbHoehe.Text.Trim();
            Decimal decTmp = 0.0M;
            if (!decimal.TryParse(tbHoehe.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
            }
            tbHoehe.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>frmGueterArten/ tbViewID_Validated</summary>
        ///<remarks></remarks>
        private void tbViewID_Validated(object sender, EventArgs e)
        {
            tbViewID.Text = tbViewID.Text.Trim();
        }
        ///<summary>frmGueterArten/ tbBezeichung_Validated</summary>
        ///<remarks></remarks>
        private void tbBezeichung_Validated(object sender, EventArgs e)
        {
            tbBezeichung.Text = tbBezeichung.Text.Trim();
            //Check der Eingabe, damit die MassAnzahl voreingestellt werden kann
            SetMassAnzahl();
        }
        ///<summary>frmGueterArten/ nudMassAnzahl_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudMassAnzahl_ValueChanged(object sender, EventArgs e)
        {
            Int32 i = 0;
            i = Convert.ToInt32(nudMassAnzahl.Value);
            ResetEnableTbBox();
            switch (i)
            {
                case 1:
                    tbBreite.Enabled = true;
                    break;
                case 2:
                    tbBreite.Enabled = true;
                    tbDicke.Enabled = true;
                    break;

                case 3:
                    tbBreite.Enabled = true;
                    tbDicke.Enabled = true;
                    tbLaenge.Enabled = true;
                    break;

                case 4:
                    tbBreite.Enabled = true;
                    tbDicke.Enabled = true;
                    tbLaenge.Enabled = true;
                    tbHoehe.Enabled = true;
                    break;
            }
        }
        ///<summary>frmGueterArten/ SetMassAnzahl</summary>
        ///<remarks></remarks>
        private void SetMassAnzahl()
        {
            /******************************************************************
             * Liste Massanzahl:
             *  2:  Coils, Bandstahl
             *  3:  Bleche, Rohre
             *  4:  Paletten, Gibo, GItterboxen, Behählter aller Art, Boxen
             ******************************************************************/
            string strBez = tbBezeichung.Text;
            Int32 iValue = 4;
            if (GArtArray != null)
            {
                Int32 iArCount = (GArtArray.Length / 2);
                for (Int32 i = 0; i <= iArCount - 1; i++)
                {
                    string tmp1 = string.Empty;
                    string tmp2 = string.Empty;
                    tmp1 = strBez.ToUpper();
                    tmp2 = GArtArray[i, 0].ToString().ToUpper();
                    Int32 g = tmp1.IndexOf(tmp2);
                    if (g >= 0)
                    {
                        iValue = Convert.ToInt32(GArtArray[i, 1].ToString());
                        //damit die Schleife beendet wird
                        i = (GArtArray.Length / 2);
                    }
                }
            }
            nudMassAnzahl.Value = iValue;
        }


    }
}
