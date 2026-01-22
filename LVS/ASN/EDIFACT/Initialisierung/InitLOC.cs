using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitLOC
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitLOC(AsnArt myAsnArt, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = LOC.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "Place of discharge";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = myCode;
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;


            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = LOC.Name;
            ese.Description = "Place of discharge";
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
            esef.Shortcut = LOC.Kennung_3227;
            esef.Name = "Location function code qualifier";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "Place/port of discharge";
            esef.constValue = "11";
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
            ese.Name = LOC_C517.Kennung_C517;
            ese.Description = "Location identification ";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = LOC_C517.Kennung_3225;
            esef.Name = "Location identifier";
            esef.Status = "O";
            esef.Format = "a35";
            esef.Description = "dentity on Place of Discharge";
            esef.constValue = string.Empty;
            esef.Position = 1;
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
            esef.Shortcut = LOC_C517.Kennung_1131;
            esef.Name = "Line item number";
            esef.Status = "O";
            esef.Format = "a6";
            esef.Description = "not used";
            esef.constValue = string.Empty;
            esef.Position = 1;
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
            esef.Shortcut = LOC_C517.Kennung_3055;
            esef.Name = "Code list responsible agency code";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "Assigned by buyer or buyer's agent";
            esef.constValue = "";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = "92";
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
