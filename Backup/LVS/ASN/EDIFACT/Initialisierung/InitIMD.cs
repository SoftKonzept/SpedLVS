using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitIMD
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitIMD(AsnArt myAsnArt, string myIMD_7077_7077, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = IMD.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "ITEM DESCRIPTION";
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
            ese.Name = IMD.Name;
            ese.Description = "Item description type";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = myCode;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD.Name;
            esef.Name = "Article Description";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "Article Description ";
            esef.constValue = myIMD_7077_7077;
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = IMD.Kennung_7077;
            ese.Description = "tem description type, coded";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = myCode;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name; ;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD.Kennung_7077;
            esef.Name = "tem description type, coded";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = IMD.Kennung_7081;
            ese.Description = "Item characteristic, coded ";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = myCode;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name; ;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD.Kennung_7081;
            esef.Name = "Item characteristic, coded";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = IMD_C273.Kennung;
            ese.Description = "ITEM DESCRIPTION ";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = myCode;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name; ;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD_C273.Kennung_7009;
            esef.Name = "Item description identification";
            esef.Status = "C";
            esef.Format = "a17";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD_C273.Kennung_1131;
            esef.Name = "Item description identification";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD_C273.Kennung_3055;
            esef.Name = "Code list responsible agency";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD_C273.Kennung_7008;
            esef.Name = "Item description ";
            esef.Status = "C";
            esef.Format = "a35";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = IMD.Kennung_7383;
            ese.Description = "Surface/layer indicator, coded";
            ese.Position = 4;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = myCode;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name; ;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = IMD.Kennung_7383;
            esef.Name = "Surface/layer indicator, coded";
            esef.Status = "C";
            esef.Format = "a3";
            esef.Description = "";
            esef.constValue = "";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = 0;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);
        }
    }

}
