using Common.Enumerations;
using Common.Models;

namespace Common.DatabaseTable
{
    public class Table_InventoryArticle
    {
        public static DatabaseTableProperties GetPropertyValue(enumInventoryArticle_Datafields myDatafield, string myValue)
        {
            DatabaseTableProperties tb = new DatabaseTableProperties();
            tb.TableName = enumDatabaseSped4_TableNames.InventoryArticle.ToString();
            tb.FieldName = myDatafield.ToString();
            tb.Value = myValue.ToString();

            return tb;
        }
    }
}
