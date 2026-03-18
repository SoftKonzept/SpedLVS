using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitRFF
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitRFF(AsnArt myAsnArt, string myRFF_C506_1153, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = RFF.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "REFERENCE";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = myCode;
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (ediSegmentVD.EdiSegment.Code)
                    {
                        //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_NAD_DP:
                        case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_RFF_AFV:
                            //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_DTM_171:
                            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement;
                            break;
                    }
                    break;
                default:
                    ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;
                    break;
            }
            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = RFF.Name;
            ese.Description = "REFERENCE";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = RFF.Name; ;
            esef.Name = "REFERENCE";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "RFF";
            esef.constValue = "RFF";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = RFF_C506.Kennung;
            ese.Description = "Reference  / separator";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + RFF_C506.Kennung; ;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = RFF_C506.Kennung_1153;
            esef.Name = "Referenz, Qualifier";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "Vorgabe";
            esef.constValue = myRFF_C506_1153;
            esef.Position = 2;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = RFF_C506.Kennung_1154;
            esef.Name = "Referenz, Identifikation";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "713F03 Lieferschein";
            esef.constValue = "";
            esef.Position = 3;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = RFF_C506.Kennung_1156;
            esef.Name = "Document line identifier";
            esef.Status = "O";
            esef.Format = "a6";
            esef.Description = "";
            esef.constValue = "#Blank#";
            esef.Position = 4;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);
        }
    }

}
