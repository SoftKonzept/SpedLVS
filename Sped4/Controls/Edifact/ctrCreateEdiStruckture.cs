using Common.Models;
using LVS;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls.Edifact
{
    public partial class ctrCreateEdiStruckture : UserControl
    {
        public ctrMenu _ctrMenu;
        public int SearchButton = 0;
        public AddressViewData adrVD = new AddressViewData();

        public ctrCreateEdiStruckture()
        {
            InitializeComponent();
        }

        public void InitCtr()
        {
            ////-- comboASNArt
            comboAsnArt.DataSource = clsASNArt.GetASNArtList(this._ctrMenu._frmMain.GL_User.User_ID);
            comboAsnArt.ValueMember = "ID";
            comboAsnArt.DisplayMember = "Typ";
            comboAsnArt.SelectedIndex = -1;

            ////comboArbeitsbereich
            comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(1);
            comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
            comboArbeitsbereich.ValueMember = "ID";
            comboArbeitsbereich.SelectedIndex = -1;

            adrVD = new AddressViewData();
            SetCreateButtonEnabled();
        }

        private void btnCreateStruckture_Click(object sender, EventArgs e)
        {
            int iAsnArtId = 0;
            int.TryParse(comboAsnArt.SelectedValue.ToString(), out iAsnArtId);

            int iWorkspaceId = 0;
            int.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out iWorkspaceId);

            if (
                (adrVD.Address.Id > 0) &&
                (iAsnArtId > 0) &&
                (iWorkspaceId > 0)
              )
            {
                AsnArtViewData artVD = new AsnArtViewData(iAsnArtId, (int)_ctrMenu._frmMain.GL_User.User_ID, false);

                switch (artVD.AsnArt.Typ.ToString())
                {
                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D97A:
                        Default_DESADV_D97A defDesadv = new Default_DESADV_D97A(adrVD.Address.Id, iAsnArtId, iWorkspaceId, _ctrMenu._frmMain.GL_User);
                        break;
                    case constValue_AsnArt.const_Art_EDIFACT_INVRPT_D96A:
                        Default_INVRPTD96A invprt = new Default_INVRPTD96A(adrVD.Address.Id, iAsnArtId, iWorkspaceId, _ctrMenu._frmMain.GL_User);
                        break;
                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                        Default_DESADV_D07A desadvD07A = new Default_DESADV_D07A(adrVD.Address.Id, iAsnArtId, iWorkspaceId, _ctrMenu._frmMain.GL_User);
                        break;
                    default:
                        string strTxt = "Für die gewählte ASN-Art liegt keine entsprechende Funktion vor!";
                        clsMessages.Allgemein_InfoTextShow(strTxt);
                        break;
                }
                nudAdrDirect.Value = 0;
                comboAsnArt.SelectedIndex = -1;
                comboArbeitsbereich.SelectedIndex = -1;
                SetCreateButtonEnabled();
            }
        }

        private void nudAdrDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrDirect.Value > 0)
            {
                //this.IsReceiverSearch = true;
                TakeOverAdrID((int)nudAdrDirect.Value);
            }
        }
        public void TakeOverAdrID(int myAdrId)
        {
            adrVD = new AddressViewData(myAdrId, (int)_ctrMenu._frmMain.GL_User.User_ID);
            if (adrVD.Address.Id == myAdrId)
            {
                tbAdrMatchCode.Text = adrVD.Address.ViewId.ToString();
                tbAdrShort.Text = adrVD.Address.AddressStringShort.ToString();
                nudAdrDirect.Value = adrVD.Address.Id;
            }
            else
            {
                nudAdrDirect.Value = myAdrId;
            }
            SetCreateButtonEnabled();
        }

        private void SetCreateButtonEnabled()
        {
            btnCreation.Enabled = (
                                                ((adrVD.Address is Addresses) && (adrVD.Address.Id > 0)) &&
                                                (comboArbeitsbereich.SelectedIndex > -1) &&
                                                (comboAsnArt.SelectedIndex > -1)
                                          );

            if (btnCreation.Enabled)
            {
                btnCreation.BackColor = Color.Green;
            }
            else
            {
                btnCreation.BackColor = Color.DarkGray;
            }
        }

        private void btnSearchAdr_Click(object sender, EventArgs e)
        {
            SearchButton = 0;
            _ctrMenu.OpenADRSearch(this);
        }

        private void comboAsnArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCreateButtonEnabled();
        }

        private void comboArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCreateButtonEnabled();
        }
    }
}
