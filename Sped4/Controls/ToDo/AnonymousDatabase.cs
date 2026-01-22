using LVS;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace Sped4.Controls.ToDo
{
    public class AnonymousDatabase
    {
        internal AddressViewData adrVD { get; set; } = new AddressViewData();
        internal AddressReferenceViewData adrRefVD { get; set; } = new AddressReferenceViewData();
        internal WorkspaceViewData wspVD { get; set; } = new WorkspaceViewData();
        internal ArticleViewData articleViewData { get; set; } = new ArticleViewData();
        internal clsMail mail { get; set; } = new clsMail();
        internal clsExtraCharge extraCharge { get; set; } = new clsExtraCharge();
        internal GoodstypeViewData goodtypeVD { get; set; } = new GoodstypeViewData();
        internal clsKontakte kontakte { get; set; } = new clsKontakte();
        internal MandantenViewData mandantVD { get; set; } = new MandantenViewData();
        internal clsRGVorlagenTxt rgVorlagenTxt { get; set; } = new clsRGVorlagenTxt();
        internal UsersViewData userVD { get; set; } = new UsersViewData();

        public List<string> Logs { get; set; } = new List<string>();
        public string LogText { get; set; } = string.Empty;
        public AnonymousDatabase(Globals._GL_USER myGLUser)
        {
            Logs.Clear();

            //--------- ADR
            LogText = "Tabelle ADR wird anonymisiert:" + Environment.NewLine;
            Logs.Add(LogText);

            //adrVD = new AddressViewData();
            //adrVD.GetAddresslist(Common.Enumerations.enumAppProcess.NotSet, 0);
            //if (adrVD.ListAddresses.Count > 0)
            //{
            //    LogText = " -> Anzahl Adressen:" + adrVD.ListAddresses.Count + Environment.NewLine;
            //    Logs.Add(LogText);
            //    foreach (var adr in adrVD.ListAddresses)
            //    {
            //        adr.ViewId = ((int)adr.Id).ToString();
            //        adr.Name1 = "Firma " + ((int)adr.Id).ToString();
            //        adr.Name2 = string.Empty;
            //        adr.Name3 = string.Empty;
            //        adr.Street = "Adr Strasse";
            //        if (!adr.ZIP.Equals(string.Empty))
            //        {
            //            adr.ZIP = adr.ZIP.Substring(0, 2) + "000";
            //        }
            //        if (adr.DunsNr > 0)
            //        {
            //            adr.DunsNr = adr.DunsNr - 118;
            //        }
            //        else
            //        {
            //            adr.DunsNr = 0;
            //        }

            //        AddressViewData tmpVD = new AddressViewData(adr, (int)myGLUser.User_ID);
            //        tmpVD.Update();
            //        LogText = "   > Adr [" + adr.Id + "] - wurde geändert!" + Environment.NewLine;
            //        Logs.Add(LogText);
            //    }
            //}

            //--------- ADRVerweis
            //LogText = "Tabelle ADRVerweis wird anonymisiert:" + Environment.NewLine;
            //Logs.Add(LogText);

            //adrRefVD = new AddressReferenceViewData();
            //adrRefVD.GetADRVerweiseListAll();
            //if (adrRefVD.ListAddressReferences.Count > 0)
            //{
            //    LogText = " -> Anzahl Adressverweise:" + adrRefVD.ListAddressReferences.Count + Environment.NewLine;
            //    Logs.Add(LogText);
            //    foreach (var adrRef in adrRefVD.ListAddressReferences)
            //    {
            //        adrRef.Remark = "Feld Bemerkung";

            //        AddressReferenceViewData tmpVD = new AddressReferenceViewData(adrRef, 1);
            //        tmpVD.Update();
            //        LogText = "   > AdrVerweis [" + adrRef.Id + "] - wurde geändert!" + Environment.NewLine;
            //        Logs.Add(LogText);
            //    }
            //}

            //--------- Workspace
            //LogText = "Tabelle Workspace wird anonymisiert:" + Environment.NewLine;
            //Logs.Add(LogText);

            //wspVD = new WorkspaceViewData();
            //wspVD.GetWorkspaceList();
            //if (wspVD.ListWorkspace.Count > 0)
            //{
            //    LogText = " -> Anzahl Workspaces:" + wspVD.ListWorkspace.Count + Environment.NewLine;
            //    Logs.Add(LogText);
            //    foreach (var wsp in wspVD.ListWorkspace)
            //    {
            //        wsp.Name = "Arbeitsbereich " + ((int)wsp.Id).ToString();
            //        wsp.Descrition = string.Empty;

            //        WorkspaceViewData tmpVD = new WorkspaceViewData(wsp);
            //        tmpVD.Update();
            //        LogText = "   > Arbeitsbereich [" + wsp.Id + "] - wurde geändert!" + Environment.NewLine;
            //        Logs.Add(LogText);
            //    }
            //}

            //--------- User
            LogText = "Tabelle User wird anonymisiert:" + Environment.NewLine;
            Logs.Add(LogText);

            userVD = new UsersViewData();
            userVD.GetUsersList();

            if (userVD.ListUsers.Count > 0)
            {
                LogText = " -> Anzahl Workspaces:" + userVD.ListUsers.Count + Environment.NewLine;
                Logs.Add(LogText);
                foreach (var user in userVD.ListUsers)
                {
                    if (!user.Name.Equals("Administrator"))
                    {
                        user.Name = "User " + ((int)user.Id).ToString();
                        user.LoginName = "User" + ((int)user.Id).ToString();
                        user.Tel = string.Empty;
                        user.Fax = string.Empty;
                        user.Mail = string.Empty;
                        user.SMTPUser = string.Empty;
                        user.SMTPPasswort = string.Empty;
                        user.SMTPServer = string.Empty;

                        UsersViewData tmpVD = new UsersViewData(user, myGLUser);
                        tmpVD.Update();
                        LogText = "   > User [" + user.Id + "] - wurde geändert!" + Environment.NewLine;
                        Logs.Add(LogText);
                    }
                }
            }

            //--------- Güterart - goodstype
            LogText = "Tabelle Güterart wird anonymisiert:" + Environment.NewLine;
            Logs.Add(LogText);

            goodtypeVD = new GoodstypeViewData();
            goodtypeVD.GetGoodtypeListAll();
            if (goodtypeVD.ListGueterarten.Count > 0)
            {
                LogText = " -> Anzahl Güterarten:" + goodtypeVD.ListGueterarten.Count + Environment.NewLine;
                Logs.Add(LogText);
                foreach (var gt in goodtypeVD.ListGueterarten)
                {
                    gt.Bezeichnung = "Güterart " + ((int)gt.Id).ToString();

                    GoodstypeViewData tmpVD = new GoodstypeViewData(gt, (int)myGLUser.User_ID);
                    tmpVD.Update();
                    LogText = "   > Güterart [" + gt.Id + "] - wurde geändert!" + Environment.NewLine;
                    Logs.Add(LogText);
                }
            }
        }
    }
}
