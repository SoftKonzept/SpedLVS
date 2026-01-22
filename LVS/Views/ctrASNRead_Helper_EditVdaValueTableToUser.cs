using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrASNRead_Helper_EditVdaValueTableToUser
    {
        public DataTable dtOrg { get; set; } = new DataTable();
        public ctrASNRead_Helper_EditVdaValueTableToUser(DataTable myDtAsn)
        {
            dtOrg = myDtAsn.DefaultView.ToTable();
            dtOrg.Rows.Clear();
            System.Data.DataTable dtReCreatedASNValue = new System.Data.DataTable();

            if (myDtAsn.Rows.Count > 0)
            {
                //Gruppierung nach ASNID muss vorgenommen werden
                System.Data.DataTable dtAsnID = myDtAsn.DefaultView.ToTable(true, "ASNID");
                foreach (DataRow rowCount in dtAsnID.Rows)
                {
                    Int32 iAsnID = 0;
                    Int32.TryParse(rowCount["ASNID"].ToString(), out iAsnID);
                    myDtAsn.DefaultView.RowFilter = "ASNID=" + iAsnID;

                    System.Data.DataTable dtAsnValueSource = myDtAsn.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID

                    //DataTable dtTmpValue = dt.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID
                    System.Data.DataTable dtSplitt = new System.Data.DataTable();  //beinhaltet die Value für die gefilterte ASNID
                    dtSplitt = dtAsnValueSource.Clone();

                    try
                    {
                        //Test für 712
                        List<System.Data.DataTable> ListTmpValue = new List<System.Data.DataTable>();
                        // Datatable müssen aufgeteilt werden 
                        //- je ein Datatable je neuem Transport SATZ712
                        // gesammelt in der List
                        bool bFindS71201 = false;
                        //foreach (DataRow r in dtAsnValueSource.Rows)
                        for (int x = 0; x <= dtAsnValueSource.Rows.Count - 1; x++)
                        {
                            DataRow r = dtAsnValueSource.Rows[x];
                            int iFieldId = 0;
                            if (int.TryParse(r["ASNFieldID"].ToString(), out iFieldId))
                            {
                                if (iFieldId > 0)
                                {
                                    if (iFieldId.Equals(107))
                                    {
                                        string str = string.Empty;
                                    }
                                    if ((iFieldId.Equals(13)) && (bFindS71201))
                                    {
                                        ListTmpValue.Add(dtSplitt);
                                        dtSplitt = new System.Data.DataTable();
                                        dtSplitt = dtAsnValueSource.Clone();
                                        //dtSplitt = dtAsnValueSource.DefaultView.ToTable();
                                        //dtSplitt.Rows.Clear();
                                        dtSplitt.ImportRow(r);
                                    }
                                    else if ((iFieldId.Equals(13)) && (!bFindS71201))
                                    {
                                        bFindS71201 = true;
                                        dtSplitt.ImportRow(r);
                                    }
                                    else
                                    {
                                        dtSplitt.ImportRow(r);
                                    }
                                }

                                if (x == (dtAsnValueSource.Rows.Count - 1))
                                {
                                    ListTmpValue.Add(dtSplitt);
                                }
                            }
                        }

                        System.Data.DataTable dtTmpASNValue = new System.Data.DataTable();        //beinhaltet die neuaufgebaute Table der ASNID
                        List<System.Data.DataTable> ListTmpASNValue = new List<System.Data.DataTable>();

                        // --- die einzelnen Datatable der Liste wird durchlaufen und
                        // --- der Neuaufbau der ASNValue Tabelle wird durchgeführt
                        // --- die Liste ListTmpValue enthält hier die nach Satz712
                        // --- getrennten ASNValue - Sätze

                        foreach (System.Data.DataTable dtTmpValue in ListTmpValue)
                        {
                            dtTmpASNValue = new System.Data.DataTable(); //beinhaltet die neuaufgebaute Table der ASNID
                            dtTmpASNValue = dtTmpValue.Clone();

                            System.Data.DataTable dtVDA4913ElemLfs = new System.Data.DataTable("ElementsLfs");
                            dtVDA4913ElemLfs.Columns.Add("ID713", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count714", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count715", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count716", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count717", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count718", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("LfsNr", typeof(string));

                            DataRow tmpImpRow = dtVDA4913ElemLfs.NewRow();
                            Int32 iAsnValueTableID713 = 0;
                            Int32 iCount714 = 0;
                            Int32 iCount715 = 0;
                            Int32 iCount716 = 0;
                            Int32 iCount717 = 0;
                            Int32 iCount718 = 0;

                            // --- 712 Datensatz hinzufügen
                            dtTmpValue.DefaultView.RowFilter = "SatzKennung=712";
                            System.Data.DataTable dt712 = dtTmpValue.DefaultView.ToTable();
                            dtTmpASNValue = dt712.Copy();
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            dtTmpValue.DefaultView.RowFilter = "SatzKennung<>711 AND SatzKennung<>712";
                            System.Data.DataTable dtCountElements = dtTmpValue.DefaultView.ToTable();
                            dtCountElements.Columns.Add("ID713F01", typeof(int));
                            dtCountElements.Columns.Add("LfsNr", typeof(string));
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            string strKennungLastItem = dtCountElements.Rows[dtCountElements.Rows.Count - 1]["Kennung"].ToString().Trim();
                            string tmp713F03_Lfs = string.Empty;
                            iAsnValueTableID713 = 0;
                            //LfsNr füllen
                            for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
                            {
                                string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
                                switch (strKenn)
                                {
                                    case "SATZ713F01":
                                        //1. 713er Satz
                                        if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
                                        {
                                            tmp713F03_Lfs = string.Empty;
                                            if (dtCountElements.Rows[i + 2]["Value"] != null)
                                            {
                                                tmp713F03_Lfs = dtCountElements.Rows[i + 2]["Value"].ToString();
                                            }
                                        }
                                        break;
                                    case "SATZ719F01":
                                        tmp713F03_Lfs = string.Empty;
                                        iAsnValueTableID713 = 0;

                                        break;
                                }
                                dtCountElements.Rows[i]["ID713F01"] = iAsnValueTableID713;
                                dtCountElements.Rows[i]["LfsNr"] = tmp713F03_Lfs;
                            }


                            for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
                            {
                                string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
                                switch (strKenn)
                                {
                                    case "SATZ713F01":
                                        tmpImpRow = dtVDA4913ElemLfs.NewRow();
                                        iAsnValueTableID713 = 0;
                                        tmp713F03_Lfs = string.Empty;

                                        //713er Satz
                                        if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
                                        {

                                        }
                                        tmp713F03_Lfs = dtCountElements.Rows[i]["LfsNr"].ToString();
                                        tmpImpRow["ID713"] = iAsnValueTableID713;
                                        tmpImpRow["LfsNr"] = tmp713F03_Lfs;

                                        if (iAsnValueTableID713 > 0)
                                        {
                                            int iTmp = 0;
                                            if (int.TryParse(tmp713F03_Lfs, out iTmp))
                                            {
                                                iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                            }
                                            else
                                            {
                                                iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                            }

                                            tmpImpRow["Count714"] = iCount714;
                                            tmpImpRow["Count715"] = iCount715;
                                            tmpImpRow["Count716"] = iCount716;
                                            tmpImpRow["Count717"] = iCount717;
                                            tmpImpRow["Count718"] = iCount718;

                                            dtVDA4913ElemLfs.Rows.Add(tmpImpRow);
                                            iCount714 = 0;
                                            iCount715 = 0;
                                            iCount716 = 0;
                                            iCount717 = 0;
                                            iCount718 = 0;
                                            //tmpImpRow = dtVDA4913ElemLfs.NewRow();
                                        }
                                        break;

                                }
                            }

                            dtTmpValue.DefaultView.RowFilter = "SatzKennung=713";
                            System.Data.DataTable dt713 = dtTmpValue.DefaultView.ToTable();
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            for (Int32 x = 0; x <= dtVDA4913ElemLfs.Rows.Count - 1; x++)
                            {
                                Int32 iID713 = 0;
                                Int32 iID713Next = 0;

                                string strID = dtVDA4913ElemLfs.Rows[x]["ID713"].ToString();
                                string strIDNext = string.Empty;
                                //prüfen und Anzahl ermitteln
                                iCount714 = 0;
                                string str714 = dtVDA4913ElemLfs.Rows[x]["Count714"].ToString();
                                Int32.TryParse(str714, out iCount714);

                                iCount715 = 0;
                                string str715 = dtVDA4913ElemLfs.Rows[x]["Count715"].ToString();
                                Int32.TryParse(str715, out iCount715);

                                iCount716 = 0;
                                string str716 = dtVDA4913ElemLfs.Rows[x]["Count716"].ToString();
                                Int32.TryParse(str716, out iCount716);

                                iCount717 = 0;
                                string str717 = dtVDA4913ElemLfs.Rows[x]["Count717"].ToString();
                                Int32.TryParse(str717, out iCount717);

                                iCount718 = 0;
                                string str718 = dtVDA4913ElemLfs.Rows[x]["Count718"].ToString();
                                Int32.TryParse(str718, out iCount718);

                                //den 713er Satz zur Tabelle hinzufügen
                                bool b713 = true;
                                Int32 rCount713 = 0;
                                string strKennung = string.Empty;
                                string oldKennung = string.Empty;
                                while (b713)
                                {
                                    if (dt713.Rows.Count > 0)
                                    {
                                        if (rCount713 <= (dt713.Rows.Count - 1))
                                        {
                                            DataRow r713 = dt713.Rows[rCount713];
                                            strKennung = r713["Kennung"].ToString();

                                            if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ713F01) && (oldKennung != string.Empty))
                                            {
                                                b713 = !(clsASN.const_VDA4913SatzField_SATZ713F01 == strKennung);
                                            }
                                            if (b713)
                                            {
                                                dtTmpASNValue.ImportRow(r713);
                                                dt713.Rows.RemoveAt(rCount713);
                                                //rCount713++;
                                                //rCount714++;
                                            }
                                            oldKennung = strKennung;
                                        }
                                        else
                                        {
                                            b713 = false;
                                        }
                                    }
                                    else
                                    {
                                        b713 = false;
                                    }
                                }

                                //prüfen existiert eine weitere Zeile
                                if (x + 1 <= dtVDA4913ElemLfs.Rows.Count - 1)
                                {
                                    strIDNext = dtVDA4913ElemLfs.Rows[x + 1]["ID713"].ToString();
                                }
                                if (Int32.TryParse(strID, out iID713))
                                {
                                    string strFilter = "ID>=" + iID713.ToString();
                                    if (Int32.TryParse(strIDNext, out iID713Next))
                                    {
                                        if (iID713Next > 0)
                                        {
                                            strFilter = strFilter + " AND ID<" + iID713Next.ToString();
                                        }
                                    }
                                    dtTmpValue.DefaultView.RowFilter = strFilter;
                                    System.Data.DataTable dtTmpLfsValue = dtTmpValue.DefaultView.ToTable();
                                    //dtTmpLfsValue enthält nun die VDAElemente eines LIeferscheins

                                    //Aufteilen der Sätze 714 bis 718
                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=714";
                                    System.Data.DataTable dt714 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=715";
                                    System.Data.DataTable dt715 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=716";
                                    System.Data.DataTable dt716 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=717";
                                    System.Data.DataTable dt717 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=718";
                                    System.Data.DataTable dt718 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;
                                    Int32 iCountArt = iCount714;
                                    if (iCount714 < iCount715)
                                    {
                                        iCountArt = iCount715;
                                    }
                                    if (iCount714 < iCount716)
                                    {
                                        iCountArt = iCount716;
                                    }
                                    if (iCount714 < iCount717)
                                    {
                                        //iCountArt = iCount717;   // 28.02.2024 mr test
                                    }
                                    if (iCount714 < iCount718)
                                    {
                                        iCountArt = iCount718;
                                    }

                                    //714 Satz muss für die Verarbeitung je Artikel einmal vorhanden sein
                                    Int32 iCount = 1; //da der 714 Satz schon einmal vorhanden ist
                                    System.Data.DataTable dtTmp714 = dt714.DefaultView.ToTable();
                                    while (iCountArt > iCount)
                                    {
                                        //714
                                        foreach (DataRow row1 in dtTmp714.Rows)
                                        {
                                            dt714.ImportRow(row1);
                                        }
                                        iCount++;
                                    }

                                    //kommt pro Artikel nur einmal vor, muss aber hier für jeden Artikel eingefügt werden
                                    System.Data.DataTable dtTmp716 = dt716.DefaultView.ToTable();
                                    iCount = 1;
                                    while (iCountArt > iCount)
                                    {
                                        //716
                                        foreach (DataRow row2 in dtTmp716.Rows)
                                        {
                                            dt716.ImportRow(row2);
                                        }
                                        iCount++;
                                    }
                                    Int32 iTmp = dt714.Rows.Count;

                                    //Tabellen zu einer zusammensetzen
                                    for (Int32 i = 0; i <= iCountArt - 1; i++)
                                    {
                                        //Satz714
                                        bool b714 = true;
                                        Int32 rCount714 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b714)
                                        {
                                            if (dt714.Rows.Count > 0)
                                            {
                                                if (rCount714 <= (dt714.Rows.Count - 1))
                                                {
                                                    DataRow r714 = dt714.Rows[rCount714];
                                                    strKennung = r714["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ714F01) && (oldKennung != string.Empty))
                                                    {
                                                        b714 = !(clsASN.const_VDA4913SatzField_SATZ714F01 == strKennung);
                                                    }
                                                    if (b714)
                                                    {
                                                        dtTmpASNValue.ImportRow(r714);
                                                        dt714.Rows.RemoveAt(rCount714);
                                                        //rCount714++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b714 = false;
                                                }
                                            }
                                            else
                                            {
                                                b714 = false;
                                            }
                                        }
                                        //Satz715
                                        bool b715 = true;
                                        Int32 rCount715 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b715)
                                        {
                                            if (dt715.Rows.Count > 0)
                                            {
                                                if (rCount715 <= (dt715.Rows.Count - 1))
                                                {
                                                    DataRow r715 = dt715.Rows[rCount715];
                                                    strKennung = r715["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ715F01) && (oldKennung != string.Empty))
                                                    {
                                                        b715 = !(clsASN.const_VDA4913SatzField_SATZ715F01 == strKennung);
                                                    }
                                                    if (b715)
                                                    {
                                                        dtTmpASNValue.ImportRow(r715);
                                                        dt715.Rows.RemoveAt(rCount715);
                                                        //rCount715++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b715 = false;
                                                }
                                            }
                                            else
                                            {
                                                b715 = false;
                                            }
                                        }
                                        //Satz716
                                        bool b716 = true;
                                        Int32 rCount716 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b716)
                                        {
                                            if (dt716.Rows.Count > 0)
                                            {
                                                if (rCount716 <= (dt716.Rows.Count - 1))
                                                {
                                                    DataRow r716 = dt716.Rows[rCount716];
                                                    strKennung = r716["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ716F01) && (oldKennung != string.Empty))
                                                    {
                                                        b716 = !(clsASN.const_VDA4913SatzField_SATZ716F01 == strKennung);
                                                    }
                                                    if (b716)
                                                    {
                                                        dtTmpASNValue.ImportRow(r716);
                                                        dt716.Rows.RemoveAt(rCount716);
                                                        //rCount716++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b716 = false;
                                                }
                                            }
                                            else
                                            {
                                                b716 = false;
                                            }
                                        }
                                        //Satz717
                                        bool b717 = true;
                                        Int32 rCount717 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b717)
                                        {
                                            if (dt717.Rows.Count > 0)
                                            {
                                                if (rCount717 <= (dt717.Rows.Count - 1))
                                                {
                                                    DataRow r717 = dt717.Rows[rCount717];
                                                    strKennung = r717["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ717F01) && (oldKennung != string.Empty))
                                                    {
                                                        b717 = !(clsASN.const_VDA4913SatzField_SATZ717F01 == strKennung);
                                                    }
                                                    if (b717)
                                                    {
                                                        dtTmpASNValue.ImportRow(r717);
                                                        dt717.Rows.RemoveAt(rCount717);
                                                        //rCount717++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b717 = false;
                                                }
                                            }
                                            else
                                            {
                                                b717 = false;
                                            }
                                        }


                                        //Satz718
                                        bool b718 = true;
                                        Int32 rCount718 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b718)
                                        {
                                            if (dt718.Rows.Count > 0)
                                            {
                                                if (rCount718 <= (dt718.Rows.Count - 1))
                                                {
                                                    DataRow r718 = dt718.Rows[rCount717];
                                                    strKennung = r718["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ718F01) && (oldKennung != string.Empty))
                                                    {
                                                        b718 = !(clsASN.const_VDA4913SatzField_SATZ718F01 == strKennung);
                                                    }
                                                    if (b718)
                                                    {
                                                        dtTmpASNValue.ImportRow(r718);
                                                        dt718.Rows.RemoveAt(rCount718);
                                                        //rCount717++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b718 = false;
                                                }
                                            }
                                            else
                                            {
                                                b718 = false;
                                            }
                                        }
                                        iCount++;
                                    }
                                }
                            }
                            ListTmpASNValue.Add(dtTmpASNValue);
                        }// Ende List

                        dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=711";
                        System.Data.DataTable dt711 = dtAsnValueSource.DefaultView.ToTable();

                        //-- 711 hinzufügen
                        dtReCreatedASNValue = dt711.Copy();


                        // --- Datatable dtReCreatedASNValue füllen mit den Datensätzen 
                        // --- aus der ListTmpASNVAlue
                        // --- 712 bis 718 je Transport
                        foreach (System.Data.DataTable dtTmp in ListTmpASNValue)
                        {
                            foreach (DataRow row in dtTmp.Rows)
                            {
                                dtReCreatedASNValue.ImportRow(row);
                            }
                        }

                        dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=719";
                        System.Data.DataTable dt719 = dtAsnValueSource.DefaultView.ToTable();

                        //Satz 719 hinzufügen
                        foreach (DataRow row in dt719.Rows)
                        {
                            dtReCreatedASNValue.ImportRow(row);
                        }

                        //Spalte LfdNr -> Zähler
                        if (!dtOrg.Columns.Contains("LfdNr"))
                        {
                            dtOrg.Columns.Add("LfdNr", typeof(Int32));
                        }
                        if (!dtReCreatedASNValue.Columns.Contains("LfdNr"))
                        {
                            dtReCreatedASNValue.Columns.Add("LfdNr", typeof(Int32));
                        }

                        //Rows aus der TMPTable dtASNIDValue
                        Int32 iLfdNr = 1;
                        foreach (DataRow rowImp in dtReCreatedASNValue.Rows)
                        {
                            rowImp["LfdNr"] = iLfdNr;
                            dtOrg.ImportRow(rowImp);
                            iLfdNr++;
                        }
                        dtTmpASNValue.Rows.Clear();

                    }
                    catch (Exception ex)
                    {
                        string st = ex.ToString();
                    }
                }
            }

            dtOrg.Columns.Add("ASNSender", typeof(decimal));
            dtOrg.Columns.Add("ASNReceiver", typeof(decimal));
        }



    }
}
