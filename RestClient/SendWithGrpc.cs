using Grpc.Core;
using Rest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestClient
{
    public static class SendWithGrpc
    {
        public static Channel channel = new Channel($"127.0.0.1:50055", ChannelCredentials.Insecure);
        public static RestService.RestServiceClient client = new RestService.RestServiceClient(channel);

        private static DataSet1 dts = new DataSet1();

        private static DataSet1TableAdapters.FRTTableAdapter fta = new DataSet1TableAdapters.FRTTableAdapter();

        // Protobuf does not support DateTime and Decimal fields
        // Send DateTime as (long)Ticks
        // long objVal = Convert.ToDateTime(tblDateTimeField).Ticks;
        // DateTime field = Convert.ToDateTime(new DateTime((long)objVal));

        // Send Decimal as Double
        // double doubleVal = Convert.ToDouble(decimalVal);
        // decimal field = Convert.ToDecimal(doubleVal);

        // ProxyHelper KULLANMA

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



        public static void FrtSend(string typ)
        {
            int nor = fta.Fill(dts.FRT, typ);

            foreach (DataSet1.FRTRow frt in dts.FRT)
            {
                FrtProxy frtProxy = new FrtProxy();

                RowToProxy(dts.FRT, frt, frtProxy);

                StatusReply statu = client.FrtPut(frtProxy);
            }
            FrtProxy endProxy = new FrtProxy
            {
                FRTID = int.MaxValue,
            };
            client.FrtPut(endProxy);


        }
    }
}
