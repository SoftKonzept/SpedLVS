using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitCPS
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitCPS(AsnArt myAsnArt, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = CPS.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "CONSIGNMENT PACKING SEQUENCE";
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
            ese.Name = CPS.Name;
            ese.Description = "EQUIPMENT DETAILS";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + CPS.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = CPS.Name;
            esef.Name = "CONSIGNMENT PACKING SEQUENCE";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = "CPS";
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
            ese.Name = CPS.Kennung_7164;
            ese.Description = "Hierarchical id. number";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + CPS.Kennung_7164;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = CPS.Kennung_7164;
            esef.Name = "Hierarchical id. number";
            esef.Status = "R";
            esef.Format = "a12";
            esef.Description = string.Empty;
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
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = CPS.Kennung_7166;
            ese.Description = "Hierarchical parent id.";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + CPS.Kennung_7166;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = CPS.Kennung_7166;
            esef.Name = "Hierarchical parent id.";
            esef.Status = "M";
            esef.Format = "c12";
            esef.Description = "Hierarchical parent id.";
            esef.constValue = string.Empty;
            esef.Position = 2;
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
            ese.Name = CPS.Kennung_7075;
            ese.Description = "Packaging level, coded ";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + CPS.Kennung_7075;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = CPS.Kennung_7075;
            esef.Name = "Packaging level, coded ";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "Packaging level, coded";
            esef.constValue = string.Empty;
            esef.Position = 2;
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
