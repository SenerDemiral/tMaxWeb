using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    public static class ProxyHelper
    {
        public static void RowToProxy(DataRow row, object obj)
        {
            string colTyp = "";
            object colVal = null;

            for (int c = 0; c < row.ItemArray.Length; c++)
            {
                //colName = row[c].
                colVal = row[c];
                colTyp = row[c].GetType().Name;

                if (colVal != DBNull.Value)
                {
                    if (colTyp == "DateTime")
                        colVal = Convert.ToDateTime(colVal).Ticks;
                    else if (colTyp == "Decimal")
                        colVal = Convert.ToDouble(colVal);

                    //?? obj.GetType().GetProperty(colName)?.SetValue(obj, colVal);
                }
            }
        }


        /*
        public static void ProxyToRow(DataTable tbl, DataRow row, object obj)
        {
            string colName = "";
            object objVal = null;

            for (int c = 0; c < tbl.Columns.Count; c++)
            {
                colName = tbl.Columns[c].ColumnName;
                objVal = obj.GetType().GetProperty(colName)?.GetValue(obj);

                if (objVal != null)
                {
                    if (tbl.Columns[c].DataType.Name == "DateTime")
                        row[c] = Convert.ToDateTime(new DateTime((long)objVal));
                    else if (tbl.Columns[c].DataType.Name == "Decimal")
                        row[c] = Convert.ToDecimal(objVal);
                    else
                        row[c] = objVal;
                }
            }
        }

        public static void RowToProxy(DataTable tbl, DataRow row, object obj)
        {
            string colName = "";
            object colVal = null;

            for (int c = 0; c < tbl.Columns.Count; c++)
            {
                colName = tbl.Columns[c].ColumnName;
                colVal = row[c];

                if (colVal != DBNull.Value)
                {
                    if (tbl.Columns[c].DataType.Name == "DateTime")
                        colVal = Convert.ToDateTime(colVal).Ticks;
                    else if (tbl.Columns[c].DataType.Name == "Decimal")
                        colVal = Convert.ToDouble(colVal);

                    obj.GetType().GetProperty(colName)?.SetValue(obj, colVal);
                }
            }
        }
        */
    }
}