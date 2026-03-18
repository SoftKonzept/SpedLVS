using LVS.Constants;
using System.Collections.Generic;

namespace LVS
{
    public class clsVDAClientOutVDA4987DefaultSchema
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CreateVDA4987DefaultClientSet(int myDestAdrID, Globals._GL_USER myGLUser)
        {
            bool bReturn = false;
            clsEdiSegmentElementField EdiFields = new clsEdiSegmentElementField();
            EdiFields.InitClass(ref myGLUser, null);

            List<clsEdiSegmentElementField> ListFields = EdiFields.GetDefaultEdiFieldsList(constValue_AsnArt.const_Art_EdifactVDA4987);
            if (ListFields.Count > 0)
            {
                List<clsVDAClientValue> ListClient = new List<clsVDAClientValue>();

                foreach (clsEdiSegmentElementField itm in ListFields)
                {
                    clsVDAClientValue cv = new clsVDAClientValue();
                    cv.InitClass(myGLUser, myDestAdrID, null);
                    cv.AdrID = myDestAdrID;
                    cv.ASNFieldID = itm.ID;

                    if (!itm.constValue.Equals(string.Empty))
                    {
                        cv.ValueArt = "const";
                        cv.Value = itm.constValue;
                    }
                    else
                    {
                        cv.ValueArt = "const";
                        cv.Value = "#Empty#";
                    }
                    cv.Fill0 = false;
                    cv.aktiv = true;
                    cv.NextSatz = 0;
                    cv.IsArtSatz = false;
                    cv.FillValue = "#BLANKS#";
                    cv.FillLeft = false;
                    cv.ASNArtId = itm.EdiSegmentElement.EdiSegment.ASNArtId;
                    if (!ListClient.Contains(cv))
                    {
                        ListClient.Add(cv);
                    }
                }

                string strSql = string.Empty;
                foreach (clsVDAClientValue itm in ListClient)
                {
                    strSql += itm.AddSqlString();
                }
                if (!strSql.Equals(string.Empty))
                {
                    bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "EDIFACTAdd", myGLUser.User_ID);
                }
            }
            return bReturn;
        }





    }
}
