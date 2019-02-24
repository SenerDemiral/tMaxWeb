using System;
using System.Threading.Tasks;
using Grpc.Core;
using Rest;
using Starcounter;
using DBTM;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace tMaxWebRestSC
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
                            FRT row = Db.SQL<FRT>("select r from FRT r where r.FrtId = ?", prxy.FRTID).FirstOrDefault();

                            if (row == null)    // Kayit yok, Insert
                            {
                                row = new FRT();
                                ProxyHelper.FromProxy2<FrtProxy, FRT>(prxy, row);
                            }
                            else
                            {
                                ProxyHelper.FromProxy2<FrtProxy, FRT>(prxy, row);
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
                            OPM row = Db.SQL<OPM>("select r from OPM r where r.OPMID = ?", prxy.OPMID).FirstOrDefault();

                            if (row == null)    // Kayit yok, Insert
                            {
                                row = new OPM();
                                ProxyHelper.FromProxy2<OpmProxy, OPM>(prxy, row);
                            }
                            else
                            {
                                ProxyHelper.FromProxy2<OpmProxy, OPM>(prxy, row);
                            }
                            row.SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault();
                            row.CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault();
                            row.ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault();
                            row.CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault();
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
                            OPH row = Db.SQL<OPH>("select r from OPH r where r.OPHID = ?", prxy.OPHID).FirstOrDefault();
                            
                            if (row == null)    // Kayit yok, Insert
                            {
                                row = new OPH();
                                ProxyHelper.FromProxy2<OphProxy, OPH>(prxy, row);
                            }
                            else
                            {
                                ProxyHelper.FromProxy2<OphProxy, OPH>(prxy, row);
                            }

                            row.OPM = Db.SQL<OPM>("select r from OPM r where r.OPMID = ?", prxy.OPMID).FirstOrDefault();
                            row.SHP = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.SHPID).FirstOrDefault();
                            row.CNE = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CNEID).FirstOrDefault();
                            row.ACC = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.ACCID).FirstOrDefault();
                            row.MNF = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.MNFID).FirstOrDefault();
                            row.NFY = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.NFYID).FirstOrDefault();
                            row.CRR = Db.SQL<FRT>("select r from FRT r where r.FRTID = ?", prxy.CRRID).FirstOrDefault();
                            
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

        public static TDatabase FromProxy2<TProxy, TDatabase>(TProxy proxy, TDatabase row) //where TDatabase : class, new()
        {
            //TDatabase row = null;
            Type proxyType = typeof(TProxy);
            Type databaseType = typeof(TDatabase);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();
            PropertyInfo[] databaseProperties = databaseType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            //ulong pk = (ulong)proxy.GetType().GetProperty(keyName)?.GetValue(proxy);    // FRTID
            //if (pk > 0)
            //    row = Db.FromId<TDatabase>(pk);
            //else
            //    row = new TDatabase();

            foreach (PropertyInfo databaseProperty in databaseProperties)
            {
                PropertyInfo proxyProperty = proxyProperties.FirstOrDefault(x => x.Name == databaseProperty.Name);

                if (proxyProperty != null)
                {
                    object value = proxyProperty.GetValue(proxy);

                    if (value != null && databaseProperty.PropertyType.GetTypeInfo().IsClass && databaseProperty.PropertyType != typeof(string))
                        continue;// databaseProperty.SetValue(row, Db.FromId((ulong)value)); //v.GetObjectNo());  //Db.FromId(2));
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

            if (databaseType == typeof(string))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    return null;
                return value;
            }
            else if (databaseType == typeof(decimal))
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
                if (v == 0) // Null ise Client'dan 0 geliyor
                    return null;
                return (DateTime?)(new DateTime(v.Value));
            }

            return value;
        }

        public static DateTime? ConvertToNullableDateTime(long value)
        {
            if (value == 0)
                return null;
            return (DateTime?)(new DateTime(value));
        }

    }


}
