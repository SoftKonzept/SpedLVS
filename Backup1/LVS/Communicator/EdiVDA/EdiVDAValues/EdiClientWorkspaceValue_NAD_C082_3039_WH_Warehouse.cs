using LVS.ViewData;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse
    {
        public const string const_EdiClientWorkspaceValue_NAD_C082_3039_Warehouse = "#EdiClientWorkspaceValue_NAD_C082_3039_Warehouse#";
        public const string const_EdiClientWorkspaceValue_Property = "NAD+WH";
        public static string Execute(clsASN myAsn, clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            int iWorkspace = 0;
            int iAsnArtId = 0;
            int iAdrId = 0;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    if (myLager.Artikel.Eingang is clsLEingang)
                    {
                        iWorkspace = (int)myLager.Artikel.Eingang.AbBereichID;
                        iAsnArtId = (int)myAsn.ASNArt.ID;
                        iAdrId = (int)myLager.Artikel.Eingang.Auftraggeber;
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Artikel.Ausgang is clsLAusgang)
                    {
                        iWorkspace = (int)myLager.Artikel.Ausgang.AbBereichID;
                        iAsnArtId = (int)myAsn.ASNArt.ID;
                        iAdrId = (int)myLager.Artikel.Ausgang.Auftraggeber;
                    }
                    break;
            }

            if (
                    (iWorkspace > 0) &&
                    (iAsnArtId > 0) &&
                    (iAdrId > 0)
               )
            {
                EdiClientWorkspaceValueViewData eawaVD = new EdiClientWorkspaceValueViewData(iAdrId, iAsnArtId, iWorkspace, EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse.const_EdiClientWorkspaceValue_Property, (int)myAsn.BenutzerID);
                strTmp = eawaVD.AdrWorkspaceAssingment.Value.ToString();
            }
            return strTmp;
        }
    }
}
