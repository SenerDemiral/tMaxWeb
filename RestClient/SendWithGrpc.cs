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
        public static Channel channel = new Channel($"127.0.0.1", 50055, ChannelCredentials.Insecure);
        public static RestService.RestServiceClient client = new RestService.RestServiceClient(channel);

        public static DataSet1 dts = new DataSet1();

        public static DataSet1TableAdapters.FRTTableAdapter frta = new DataSet1TableAdapters.FRTTableAdapter();
        public static DataSet1TableAdapters.OPMTableAdapter opma = new DataSet1TableAdapters.OPMTableAdapter();
        public static DataSet1TableAdapters.OPHTableAdapter opha = new DataSet1TableAdapters.OPHTableAdapter();

        // Protobuf does not support DateTime and Decimal fields
        // Send DateTime as (long)Ticks
        // long objVal = Convert.ToDateTime(tblDateTimeField).Ticks;
        // DateTime field = Convert.ToDateTime(new DateTime((long)objVal));

        // Send Decimal as Double
        // double doubleVal = Convert.ToDouble(decimalVal);
        // decimal field = Convert.ToDecimal(doubleVal);

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



        public static int FrtSend(string typ)
        {
            int nor = frta.Fill(dts.FRT, typ);

            foreach (DataSet1.FRTRow frt in dts.FRT)
            {
                FrtProxy frtProxy = new FrtProxy();
                RowToProxy(dts.FRT, frt, frtProxy);
                StatusReply statu = client.FrtPut(frtProxy);
            }
            var endProxy = new FrtProxy
            {
                FRTID = int.MaxValue,
            };
            client.FrtPut(endProxy);
            return nor;
        }

        public static int OpmSend(string typ)
        {
            int nor = opma.Fill(dts.OPM, typ);

            foreach (DataSet1.OPMRow opm in dts.OPM)
            {
                OpmProxy opmProxy = new OpmProxy();
                RowToProxy(dts.OPM, opm, opmProxy);
                StatusReply statu = client.OpmPut(opmProxy);
            }
            var endProxy = new OpmProxy
            {
                OPMID = int.MaxValue,
            };
            client.OpmPut(endProxy);
            return nor;
        }

        public static int OphSend(string typ)
        {
            int nor = opha.Fill(dts.OPH, typ);

            foreach (DataSet1.OPHRow oph in dts.OPH)
            {
                OphProxy ophProxy = new OphProxy();
                RowToProxy(dts.OPH, oph, ophProxy);
                StatusReply statu = client.OphPut(ophProxy);
            }
            var endProxy = new OphProxy
            {
                OPHID = int.MaxValue,
            };
            client.OphPut(endProxy);
            return nor;
        }
        /*
        public static async Task Frt2Send()
        {
            try
            {

                using (var call = client.Frt2Put())
                {
                    int nor = frta.Fill(dts.FRT, "F");

                    foreach (DataSet1.FRTRow frt in dts.FRT)
                    {
                        FrtProxy frtProxy = new FrtProxy();

                        RowToProxy(dts.FRT, frt, frtProxy);

                        await call.RequestStream.WriteAsync(frtProxy);

                        await Task.Delay(500);
                    }


                    await call.RequestStream.CompleteAsync();


                    var summary = await call.ResponseAsync;
                }
            }
            catch (RpcException e)
            {
            }
        }
        */
    }
}
