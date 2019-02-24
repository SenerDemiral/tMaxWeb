using System;
using System.Threading.Tasks;
using Grpc.Core;
using Rest;
using Starcounter;
using DBTM;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace tMaxWebRestServer
{
    class RestServiceImpl : RestService.RestServiceBase
    {
        List<FrtProxy> FrtProxyList = new List<FrtProxy>();
        List<OpmProxy> OpmProxyList = new List<OpmProxy>();
        List<OphProxy> OphProxyList = new List<OphProxy>();

        public override Task<StatusReply> FrtPut(FrtProxy request, ServerCallContext context)
        {

            if (request.FRTID != int.MaxValue)
            {
                FrtProxyList.Add(request);
            }
            else
            {
                Scheduling.RunTask(() =>
                {
                    Db.Transact(() =>
                    {
                        foreach (var prxy in FrtProxyList)
                        {
                            FRT frt = Db.SQL<FRT>("select r from FRT r where r.FrtId = ?", prxy.FRTID).FirstOrDefault();

                            if (frt == null)    // Kayit yok, Insert
                            {
                                // Proxy FB'den geldigi icin SC alanlarina donusmesi lazim. Manuel yap. Relation ID ye gore SC ye cevrilmeli
                                // FRT row = ProxyHelper.FromProxy<FrtProxy, FRT>(request);

                                frt = new FRT
                                {
                                    FRTID = prxy.FRTID,
                                    ADN = prxy.ADN,
                                    AD = prxy.AD,
                                    LOCID = prxy.LOCID,
                                    PWD = prxy.PWD,
                                };
                            }
                            else
                            {
                                frt.ADN = prxy.ADN;
                                frt.AD = prxy.AD;
                                frt.LOCID = prxy.LOCID;
                                frt.PWD = prxy.PWD;
                            }
                        }
                    });
                }).Wait();

                FrtProxyList.Clear();
            }
            return Task.FromResult(new StatusReply { ErrNo = 0 });
        }

        public override Task<StatusReply> OpmPut(OpmProxy request, ServerCallContext context)
        {
            if (request.OPMID != int.MaxValue)
            {
                OpmProxyList.Add(request);
            }
            else
            {
                Scheduling.RunTask(() =>
                {
                    Db.Transact(() =>
                    {
                        foreach (var prxy in OpmProxyList)
                        {
                            OPM opm = Db.SQL<OPM>("select r from OPM r where r.OPMID = ?", prxy.OPMID).FirstOrDefault();

                            if (opm == null)    // Kayit yok, Insert
                            {
                                // Proxy FB'den geldigi icin SC alanlarina donusmesi lazim. Manuel yap. Relation ID ye gore SC ye cevrilmeli
                                // FRT row = ProxyHelper.FromProxy<FrtProxy, FRT>(request);

                                opm = new OPM
                                {
                                    SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault(),
                                    CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault(),
                                    ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault(),
                                    CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault(),

                                    OPMID = prxy.OPMID,
                                    SHPID = prxy.SHPID,
                                    CNEID = prxy.CNEID,
                                    ACCID = prxy.ACCID,
                                    CRRID = prxy.CRRID,
                                };
                            }
                            else
                            {
                                opm.OPMID = prxy.OPMID;
                                opm.SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault();
                                opm.CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault();
                                opm.ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault();
                                opm.CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault();

                                opm.OPMID = prxy.OPMID;
                                opm.SHPID = prxy.SHPID;
                                opm.CNEID = prxy.CNEID;
                                opm.ACCID = prxy.ACCID;
                                opm.CRRID = prxy.CRRID;
                            }
                        }
                    });
                }).Wait();

                OphProxyList.Clear();
            }
            return Task.FromResult(new StatusReply { ErrNo = 0 });
        }

        public override Task<StatusReply> OphPut(OphProxy request, ServerCallContext context)
        {
            if (request.OPHID != int.MaxValue)
            {
                OphProxyList.Add(request);
            }
            else
            {
                Scheduling.RunTask(() =>
                {
                    Db.Transact(() =>
                    {
                        foreach (var prxy in OphProxyList)
                        {
                            OPH oph = Db.SQL<OPH>("select r from OPH r where r.OPHID = ?", prxy.OPHID).FirstOrDefault();

                            if (oph == null)    // Kayit yok, Insert
                            {
                                // Proxy FB'den geldigi icin SC alanlarina donusmesi lazim. Manuel yap. Relation ID ye gore SC ye cevrilmeli
                                // FRT row = ProxyHelper.FromProxy<FrtProxy, FRT>(request);

                                oph = new OPH
                                {
                                    OPM = Db.SQL<OPM>("select r from OPM r where r.OPMID = ?", prxy.OPMID).FirstOrDefault(),
                                    SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault(),
                                    CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault(),
                                    ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault(),
                                    MNF = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.MNFID).FirstOrDefault(),
                                    NFY = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.NFYID).FirstOrDefault(),
                                    CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault(),

                                    OPHID = prxy.OPHID,
                                    OPMID = prxy.OPMID,
                                    SHPID = prxy.SHPID,
                                    CNEID = prxy.CNEID,
                                    ACCID = prxy.ACCID,
                                    MNFID = prxy.MNFID,
                                    NFYID = prxy.NFYID,
                                    CRRID = prxy.CRRID,
                                };
                            }
                            else
                            {
                                oph.OPM = Db.SQL<OPM>("select r from OPM r where r.OPMID = ?", prxy.OPMID).FirstOrDefault();
                                oph.SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault();
                                oph.CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault();
                                oph.ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault();
                                oph.MNF = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.MNFID).FirstOrDefault();
                                oph.NFY = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.NFYID).FirstOrDefault();
                                oph.CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault();

                                oph.OPMID = prxy.OPMID;
                                oph.SHPID = prxy.SHPID;
                                oph.CNEID = prxy.CNEID;
                                oph.ACCID = prxy.ACCID;
                                oph.MNFID = prxy.MNFID;
                                oph.NFYID = prxy.NFYID;
                                oph.CRRID = prxy.CRRID;
                            }
                        }
                    });
                }).Wait();

                OphProxyList.Clear();
            }
            return Task.FromResult(new StatusReply { ErrNo = 0 });
        }

    }

    public static class ProxyHelper
    {

        public static TProxy ToProxy<TProxy, TDatabase>(TDatabase row) where TProxy : class, new()
        {
            TProxy proxy = new TProxy();
            Type proxyType = typeof(TProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            // only take if proxyProperty exists in databaseProperties 
            // proxy can be subset of database
            // proxy can have own properties
            // database can have computed/ReadOnly properties, copy also
            foreach (PropertyInfo proxyProperty in proxyProperties)
            {
                var dbP = row.GetType().GetProperty(proxyProperty.Name); //?.GetValue(row);

                if (dbP != null)
                {
                    object value = dbP.GetValue(row);

                    if (value != null)
                    {
                        if (value != null && dbP.PropertyType.GetTypeInfo().IsClass && dbP.PropertyType != typeof(string))
                            proxyProperty.SetValue(proxy, value.GetObjectNo()); //v.GetObjectNo());  //Db.FromId(2));
                        else
                        {
                            value = ConvertToProxyValue(dbP.PropertyType, value);
                            proxyProperty.SetValue(proxy, value);
                        }
                    }
                }
            }
            // Should every proxy hase Key property? Maybe not
            proxy.GetType().GetProperty("RowKey")?.SetValue(proxy, row.GetObjectNo());

            return proxy;
        }

        public static TDatabase FromProxy<TProxy, TDatabase>(TProxy proxy) where TDatabase : class, new()
        {
            TDatabase row = null;
            Type proxyType = typeof(TProxy);
            Type databaseType = typeof(TDatabase);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();
            PropertyInfo[] databaseProperties = databaseType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            ulong pk = (ulong)proxy.GetType().GetProperty("RowKey")?.GetValue(proxy);

            if (pk > 0)
                row = Db.FromId<TDatabase>(pk);
            else
                row = new TDatabase();

            foreach (PropertyInfo databaseProperty in databaseProperties)
            {
                PropertyInfo proxyProperty = proxyProperties.FirstOrDefault(x => x.Name == databaseProperty.Name);

                if (proxyProperty != null)
                {
                    object value = proxyProperty.GetValue(proxy);

                    if (value != null && databaseProperty.PropertyType.GetTypeInfo().IsClass && databaseProperty.PropertyType != typeof(string))
                        databaseProperty.SetValue(row, Db.FromId((ulong)value)); //v.GetObjectNo());  //Db.FromId(2));
                    else
                    {
                        value = ConvertToDatabaseValue(databaseProperty.PropertyType, value);
                        databaseProperty.SetValue(row, value);
                    }
                }
            }

            return row;
        }

        public static TDatabase FromProxy2<TProxy, TDatabase>(TProxy proxy, string keyName) where TDatabase : class, new()
        {
            TDatabase row = null;
            Type proxyType = typeof(TProxy);
            Type databaseType = typeof(TDatabase);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();
            PropertyInfo[] databaseProperties = databaseType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            ulong pk = (ulong)proxy.GetType().GetProperty(keyName)?.GetValue(proxy);    // FRTID

            if (pk > 0)
                row = Db.FromId<TDatabase>(pk);
            else
                row = new TDatabase();

            foreach (PropertyInfo databaseProperty in databaseProperties)
            {
                PropertyInfo proxyProperty = proxyProperties.FirstOrDefault(x => x.Name == databaseProperty.Name);

                if (proxyProperty != null)
                {
                    object value = proxyProperty.GetValue(proxy);

                    if (value != null && databaseProperty.PropertyType.GetTypeInfo().IsClass && databaseProperty.PropertyType != typeof(string))
                        databaseProperty.SetValue(row, Db.FromId((ulong)value)); //v.GetObjectNo());  //Db.FromId(2));
                    else
                    {
                        value = ConvertToDatabaseValue(databaseProperty.PropertyType, value);
                        databaseProperty.SetValue(row, value);
                    }
                }
            }

            return row;
        }

        public static object ConvertToProxyValue(Type databaseType, object value)
        {
            if (value == null)
            {
                return value;
            }

            if (databaseType == typeof(decimal))
            {
                return Convert.ToDouble(value);
            }
            else if (databaseType == typeof(decimal?))
            {
                decimal? v = value as decimal?;
                return (double?)Convert.ToDouble(v.Value);
            }
            else if (databaseType == typeof(DateTime))
            {
                DateTime v = (DateTime)value;
                return v.Ticks;
            }
            else if (databaseType == typeof(DateTime?))
            {
                DateTime? v = value as DateTime?;
                return (long?)(v.Value.Ticks);
            }

            return value;
        }

        public static object ConvertToDatabaseValue(Type databaseType, object value)
        {
            if (value == null)
            {
                return value;
            }

            if (databaseType == typeof(decimal))
            {
                return Convert.ToDecimal(value);
            }
            else if (databaseType == typeof(decimal?))
            {
                double? v = value as double?;
                return (decimal?)Convert.ToDecimal(v.Value);
            }
            else if (databaseType == typeof(DateTime))
            {
                long v = (long)value;
                return new DateTime(v);
            }
            else if (databaseType == typeof(DateTime?))
            {
                long? v = value as long?;
                return (DateTime?)(new DateTime(v.Value));
            }

            return value;
        }


    }

}
