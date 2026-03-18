using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitGIS
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitGIS(AsnArt myAsnArt, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = GIS.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "GENERAL INDICATOR";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = GIS.Name.ToUpper();
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;


            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = GIS.Name;
            ese.Description = "GENERAL INDICATOR";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = GIS.Name.ToUpper();
            ese.SortId = 1;
            ese.Kennung = ese.Name + " | " + GIS.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Name;
            esef.Name = "Despatch advice date";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = "DTM";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = NAD.Name;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Name;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);



            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = GIS_C529.Kennung;
            ese.Description = "PROCESSING INDICATOR ";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = GIS_C529.Kennung;
            ese.SortId = 1;
            ese.Kennung = ese.Name + " | " + GIS_C529.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = GIS_C529.Kennung_7365;
            esef.Name = "Processing indicator, coded";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "37 Complete information";
            esef.constValue = "37";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = GIS_C529.Kennung_7365;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + GIS_C529.Kennung_7365;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = GIS_C529.Kennung_1131;
            esef.Name = "Code list qualifier";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = GIS_C529.Kennung_1131;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + GIS_C529.Kennung_1131;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = GIS_C529.Kennung_3055;
            esef.Name = "Code list responsible agency";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = GIS_C529.Kennung_3055;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + GIS_C529.Kennung_3055;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = GIS_C529.Kennung_7187;
            esef.Name = "Process type identification";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = GIS_C529.Kennung_7187;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + GIS_C529.Kennung_7187;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);
        }
    }

}
