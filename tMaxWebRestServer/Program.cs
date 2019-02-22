using System;
using System.Threading.Tasks;
using Grpc.Core;
using Rest;
using Starcounter;
using DBTM;
using System.Linq;
using System.Collections.Generic;

namespace tMaxWebRestServer
{
    class RestImpl : RestService.RestServiceBase
    {
        List<FrtProxy> lFrt = new List<FrtProxy>();

        // Server side handler of the SayHello RPC
        public override Task<StatusReply> FrtPut(FrtProxy request, ServerCallContext context)
        {

            if (request.FRTID != int.MaxValue)
            {
                lFrt.Add(request);
            }
            else
            {
                Scheduling.RunTask(() =>
                {
                    Db.Transact(() =>
                    {
                        foreach (var prxy in lFrt)
                        {
                            FRT frt = Db.SQL<FRT>("select r from FRT r where r.FrtId = ?", prxy.FRTID).FirstOrDefault();

                            if (frt == null)    // Kayit yok, Insert
                            {
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

                lFrt.Clear();
            }
            return Task.FromResult(new StatusReply { ErrNo = 0 });
        }
    }

    class Program
    {
        const int Port = 50055;

        static void Main()
        {
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "AAA").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPRD_Idx ON PPRD");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "FRT_FrtId").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX FRT_FrtId ON FRT (FrtId)");


            Server server = new Server
            {
                Services = { Rest.RestService.BindService(new RestImpl()) },
                Ports = { new ServerPort("127.0.0.1", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("217.160.13.102", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Rest server listening on port " + Port);

            Handle.GET("/tMaxWebRestServerStop", () =>
            {
                //Task.Run(async () => { await server.ShutdownAsync(); }).Wait();
                server.ShutdownAsync().Wait();
                return "ShutDown gRPC Server OK";
            });

        }
    }
}