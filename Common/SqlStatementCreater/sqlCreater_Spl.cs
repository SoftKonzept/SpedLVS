using Common.Models;
using System;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_Spl
    {
        public Sperrlager Spl { get; set; }
        public Articles Article { get; set; }

        public sqlCreater_Spl()
        {
            Spl = new Sperrlager();
            Article = new Articles();
        }
        public sqlCreater_Spl(Sperrlager mySpl) : this()
        {
            Spl = mySpl.Copy();
        }
        public sqlCreater_Spl(Articles myArticle) : this()
        {
            Article = myArticle.Copy();
        }
        public sqlCreater_Spl(Sperrlager mySpl, Articles myArticle) : this()
        {
            Article = myArticle.Copy();
            Spl = mySpl.Copy();
        }

        public string AddStrSQL(bool bEinbuchung)
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                if (bEinbuchung)
                {
                    Spl.BKZ = "IN";
                    Spl.SPLIDIn = 0;
                }
                else
                {
                    Spl.BKZ = "OUT";
                }
                Spl.Datum = DateTime.Now;
                strSql = " INSERT INTO Sperrlager (ArtikelID, UserID, Datum, BKZ, SPLIDIn, DefWindungen, Sperrgrund, Vermerk, IsCustomCertificateMissing ) VALUES ("
                                                            + (Int32)Spl.ArtikelID +
                                                            "," + Spl.UserID +
                                                            ",'" + Spl.Datum + "'" +
                                                            ",'" + Spl.BKZ + "'" +
                                                            "," + (Int32)Spl.SPLIDIn +
                                                            ", " + Spl.DefWindungen +
                                                            ",'" + Spl.Sperrgrund + "'" +
                                                            ",'" + Spl.Vermerk + "'" +
                                                            "," + Convert.ToInt32(Spl.IsCustomCertificateMissing) +
                                                            "); ";
                strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            }
            return strSql;
        }
        /// <summary>
        ///             Update
        /// </summary>
        public string sql_Update()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "Update Sperrlager " +
                   "SET DefWindungen = " + Spl.DefWindungen +
                        ", Sperrgrund='" + Spl.Sperrgrund + "'" +
                        ", Vermerk ='" + Spl.Vermerk + "'" +
                   " WHERE ID=" + Spl.SPLIDIn + " ;";
            }
            return strSql;
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetItem()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT * FROM Sperrlager WHERE ID=" + Spl.Id + "; ";
            }
            return strSql;
        }
        /// <summary>
        ///             GET 
        ///             Ermittelt den letzten Eintrag IN für den Artikel
        /// </summary>
        public string sql_GetLastINByArtikelID(bool bComplete = true)
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT TOP(1) * FROM Sperrlager WHERE ArtikelID=" + Spl.ArtikelID + " AND BKZ='IN' ORDER BY Datum DESC;";
            }
            return strSql;
        }
        /// <summary>
        ///             GET 
        ///             Ermittelt den letzten Eintrag IN für den Artikel für Artikelzertifikat
        /// </summary>
        public string sql_GetLastINByArtikelIDByCustomCertificate(bool bComplete = true)
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT TOP(1) * FROM Sperrlager ";
                strSql += "WHERE ArtikelID=" + Spl.ArtikelID;
                strSql += " AND BKZ='IN' ";
                strSql += " AND IsCustomCertificateMissing=1 ";
                strSql += " ORDER BY Datum DESC;";
            }
            return strSql;
        }
        /// <summary>
        ///             GET 
        ///             Ermittelt den letzten Eintrag OUT für den Artikel für Artikelzertifikat
        /// </summary>
        public string sql_GetLastOUTByArtikelID()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT TOP(1) * FROM Sperrlager WHERE ArtikelID=" + Spl.ArtikelID + " AND BKZ='OUT' ORDER BY Datum DESC;";
            }
            return strSql;
        }
        /// <summary>
        ///             GET 
        ///             Ermittelt alle Artikel im SPL (fehlende Zertifikate)
        /// </summary>
        public string sql_GetArticlesInSPLMissingCertificates()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT * FROM Sperrlager " +
                                    "WHERE BKZ = 'IN' AND ID NOT IN " +
                                    "(SELECT DISTINCT SPLIDIn FROM Sperrlager WHERE SPLIDIn>0) AND IsCustomCertificateMissing=1 Order by ArtikelID, ID; ";
            }
            return strSql;
        }
        /// <summary>
        ///             CHeck 
        ///             Check, ob der Artikel sich im SPL befindet
        /// </summary>
        public string sql_CheckArtikelInSPL()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT * FROM Sperrlager " +
                                    "WHERE BKZ = 'IN' AND ID NOT IN " +
                                    "(SELECT DISTINCT SPLIDIn FROM Sperrlager WHERE SPLIDIn>0) AND ArtikelID=" + Spl.ArtikelID + ";";
            }
            return strSql;
        }

        /// <summary>
        ///             Ermittelt den letzten Eintrag des Artikels in SPL
        /// </summary>
        /// <returns></returns>
        public string sql_FillLastINByArtikelId()
        {
            string strSql = string.Empty;
            if ((Article != null) && (Article.Id > 0))
            {
                strSql = strSql = "SELECT TOP(1) * FROM Sperrlager WHERE ArtikelID=" + Article.Id + " AND BKZ='IN' ORDER BY Datum DESC;";
            }
            return strSql;
        }

        /// <summary>
        ///             Exist 
        ///             Check, ob der Artikel sich im SPL befindet
        /// </summary>
        public string sql_ExistSPLID()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "SELECT ID FROM Sperrlager WHERE ID=" + Spl.SPLIDIn + ";";
            }
            return strSql;
        }
        ////
        ///
        public string sql_DeleteDoubleBookingRecord()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                //-- Delete double booking IN Records
                strSql = "DELETE FROM Sperrlager ";
                strSql += "where";
                strSql += " ArtikelID = " + Spl.ArtikelID;
                strSql += " AND BKZ='IN' ";
                strSql += " AND Sperrgrund = '" + Spl.Sperrgrund + "' ";
                strSql += " AND IsCustomCertificateMissing = " + Convert.ToInt16(Spl.IsCustomCertificateMissing);
                strSql += " AND ID NOT IN (" + Spl.Id + "); ";

                //-- DELETE double booking OUT Recourd for booking IN record

                strSql += "DELETE FROM Sperrlager ";
                strSql += "where";
                strSql += " ArtikelID = " + Spl.ArtikelID;
                strSql += " AND BKZ='OUT' ";
                strSql += " AND SPLIDIn = " + Spl.Id;
                strSql += " ;";

                //-- Update Vermerk bearbeitet
                strSql += " Update Sperrlager ";
                strSql += " SET ";
                strSql += " Vermerk='Doppelbuchungen gelöscht' ";
                strSql += " WHERE ID=" + Spl.Id;

                //-- Sperrlager OUT Einträge in ArtikelVita löschen
                strSql += "DELETE FROM ArtikelVita ";
                strSql += "where";
                strSql += " TableID = " + Spl.ArtikelID;
                strSql += " And TableName='Artikel' ";
                strSql += " AND Aktion = 'SperrlagerOUT' ";
                strSql += " AND Beschreibung LIKE 'Artikelfreigabe durch Zertifikat:%' ";
                strSql += " ;";
            }
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string sql_Delete()
        {
            string strSql = string.Empty;
            if (Spl != null)
            {
                strSql = "DELETE FROM Sperrlager ";
                strSql += "where";
                strSql += " ID = " + Spl.Id + "; ";
            }
            return strSql;
        }
    }
}
