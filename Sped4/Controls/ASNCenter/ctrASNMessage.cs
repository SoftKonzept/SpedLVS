using LVS;
using LVS.ASN;
using Sped4.Controls.ASNCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrASNMessage : UserControl
    {
        internal clsASNWizzard asnWizz;

        internal ctrMenu _ctrMenu;
        public Int32 SearchButton = 0;
        public clsADR AdrAuftraggeber;
        public clsADR AdrEmpf;
        internal ctrASNActionSelectToCopy _ctrASNActionSelectToCopy;
        DataTable dtSourceQueue;
        internal clsASNTransfer AsnTransfer;
        internal Dictionary<decimal, clsJobs> DictJobs;

        ///<summary>ctrASNMessageTest / ctrASNMessageTest</summary>
        ///<remarks></remarks>
        public ctrASNMessage(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
            asnWizz = myAsnWizz;
            asnWizz.Arbeitsbereich = _ctrMenu._frmMain.system.AbBereich.Copy();
            this.cbMesToReceiver.Checked = true;
            this.cbMesToSupporter.Checked = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNMessageTest_Load(object sender, EventArgs e)
        {
            InitDgvQueue();

            //Combo ASNTest
            cbASNAction.DataSource = clsASNAction.GetASNAktionList();
            cbASNAction.DisplayMember = "Aktion";
            cbASNAction.ValueMember = "ID";

            //comboArbeitsbereich
            cbArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(_ctrMenu._frmMain.system._GL_User.User_ID);
            cbArbeitsbereich.DisplayMember = "Arbeitsbereich";
            cbArbeitsbereich.ValueMember = "ID";

            Functions.SetComboToSelecetedValue(ref cbArbeitsbereich, this.asnWizz.Arbeitsbereich.ID.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvQueue()
        {
            clsQueue tmpQueue = new clsQueue();
            tmpQueue.GL_User = this.asnWizz.AsnAction.GL_User;
            dtSourceQueue = clsQueue.GetQueue(this.asnWizz.AsnAction.GL_User);
            dgvQueue.DataSource = dtSourceQueue;
            dgvQueue.BestFitColumns();
        }

        private void InitDgvActionView()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshQueue_Click(object sender, EventArgs e)
        {
            InitDgvQueue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            decimal.TryParse(this.cbArbeitsbereich.SelectedValue.ToString(), out decTmp);
            if (decTmp > 0)
            {
                this.asnWizz.Arbeitsbereich.ID = (decimal)this.cbArbeitsbereich.SelectedValue;
                this.asnWizz.Arbeitsbereich.Fill();
            }
            //check combo Auswahl
            if ((cbASNAction.SelectedIndex > -1) && (this.asnWizz.Arbeitsbereich.ID > 0) && (this.nudArtikelId.Value > 0))
            {
                if (clsArtikel.ExistsArtikelTableID(this.asnWizz.AsnAction.GL_User.User_ID, this.nudArtikelId.Value))
                {
                    clsLager Lager = new LVS.clsLager();
                    Lager.InitClass(this.asnWizz.AsnAction.GL_User, this.asnWizz.AsnAction.GL_System, this._ctrMenu._frmMain.system);

                    Lager.Artikel.ID = this.nudArtikelId.Value;
                    Lager.Artikel.GetArtikeldatenByTableID();
                    Lager.Artikel.listArt.Clear();
                    Lager.Artikel.listArt.Add(Lager.Artikel);

                    Lager.ASNAction = new clsASNAction();
                    Lager.ASNAction.InitClassByAction(ref Lager, (int)cbASNAction.SelectedValue);

                    AsnTransfer = new clsASNTransfer();
                    AsnTransfer.IsCreateByASNMessageTestCtr = true;
                    AsnTransfer.QueueDescription = tbQueueDescription.Text.Trim();
                    AsnTransfer.MessageForSupporter = this.cbMesToSupporter.Checked;
                    AsnTransfer.MessageForReceiver = this.cbMesToReceiver.Checked;

                    if ((cbMesToSupporter.Checked) && (!cbMesToReceiver.Checked))
                    {
                        //nur Lieferant
                        AsnTransfer.iCommunicationPartnerRange = 1;
                    }
                    if ((cbMesToReceiver.Checked) && (!cbMesToSupporter.Checked))
                    {
                        //nur Empfänger
                        AsnTransfer.iCommunicationPartnerRange = 2;
                    }

                    if (AsnTransfer.DoASNTransfer(this.asnWizz.AsnAction.GL_System, this.asnWizz.Arbeitsbereich.ID, this.asnWizz.Arbeitsbereich.MandantenID))
                    {
                        switch (cbASNAction.SelectedValue)
                        {
                            case clsASNAction.const_ASNAction_Eingang:
                            case clsASNAction.const_ASNAction_StornoKorrektur:
                            case clsASNAction.const_ASNAction_SPLIn:
                            case clsASNAction.const_ASNAction_SPLOut:
                                Lager.Eingang = Lager.Artikel.Eingang.Copy();
                                DictJobs = AsnTransfer.GetDictJobLM(Lager.Eingang.Auftraggeber,
                                                                    Lager.Eingang.Empfaenger,
                                                                    Lager.Eingang.AbBereichID,
                                                                    Lager.Eingang.MandantenID,
                                                                    Lager.Eingang.LEingangDate,
                                                                    Lager.ASNAction,
                                                                    AsnTransfer.iCommunicationPartnerRange);
                                break;

                            case clsASNAction.const_ASNAction_Ausgang:
                                Lager.Ausgang = Lager.Artikel.Ausgang.Copy();
                                DictJobs = AsnTransfer.GetDictJobLM(Lager.Ausgang.Auftraggeber,
                                                                    Lager.Ausgang.Empfaenger,
                                                                    Lager.Ausgang.AbBereichID,
                                                                    Lager.Ausgang.MandantenID,
                                                                    Lager.Ausgang.LAusgangsDate,
                                                                    Lager.ASNAction,
                                                                    AsnTransfer.iCommunicationPartnerRange);
                                break;

                            case clsASNAction.const_ASNAction_RücklieferungSL:
                                Lager.Eingang = Lager.Artikel.Eingang.Copy();
                                Lager.Ausgang = Lager.Artikel.Ausgang.Copy();
                                DictJobs = AsnTransfer.GetDictJobLM(Lager.Eingang.Auftraggeber,
                                                                    Lager.Eingang.Empfaenger,
                                                                    Lager.Eingang.AbBereichID,
                                                                    Lager.Eingang.MandantenID,
                                                                    Lager.Eingang.LEingangDate,
                                                                    Lager.ASNAction,
                                                                    AsnTransfer.iCommunicationPartnerRange);
                                break;

                            case clsASNAction.const_ASNAction_Umbuchung:
                                Lager.Eingang = Lager.Artikel.Eingang.Copy();
                                Lager.Ausgang = Lager.Artikel.Ausgang.Copy();
                                DictJobs = AsnTransfer.GetDictJobLM(Lager.Eingang.Auftraggeber,
                                                                    Lager.Eingang.Empfaenger,
                                                                    Lager.Eingang.AbBereichID,
                                                                    Lager.Eingang.MandantenID,
                                                                    Lager.Eingang.LEingangDate,
                                                                    Lager.ASNAction,
                                                                    AsnTransfer.iCommunicationPartnerRange);
                                break;

                        }
                        if (Lager.ASNAction.ASNActionProcessNr > 0)
                        {
                            AsnTransfer.CreateLM(ref Lager);
                        }
                        InitDgvQueue();
                    }
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbArbeitsbereich.SelectedIndex > -1)
            {
                if (this.cbArbeitsbereich.SelectedValue != null)
                {
                    //decimal decTmp = 0;
                    //decimal.TryParse(((int)this.cbArbeitsbereich.SelectedValue).ToString(), out decTmp);
                    //if (decTmp > 0)
                    //{
                    //    this.asnWizz.Arbeitsbereich.ID = (decimal)this.cbArbeitsbereich.SelectedValue;
                    //    this.asnWizz.Arbeitsbereich.Fill();
                    //}
                }

            }
        }
    }
}
