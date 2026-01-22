using System.Data;
using System.Windows.Forms;

namespace Sped4.Settings
{
    public class frmAuftragFastSettings
    {

        ///<summary>Globals / FillcbRelation</summary>
        ///<remarks></remarks>
        public static void FillcbRelation(ref ComboBox cb, ref DataTable dtRelation)
        {
            DataRow row = dtRelation.NewRow();
            row["ID"] = 1;
            row["Relation"] = "";
            dtRelation.Rows.Add(row);

            cb.DataSource = dtRelation;
            cb.DisplayMember = "Relation";
            cb.ValueMember = "Relation";
            cb.SelectedValue = "";
        }
    }
}
