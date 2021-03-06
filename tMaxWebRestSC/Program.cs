﻿using System;
using Grpc.Core;
using Starcounter;
using System.Linq;
using System.Diagnostics;
using DBTM;

namespace tMaxWebRestSC
{
    class Program
    {
        public static Stopwatch sw = new Stopwatch();

        static void Main()
        {
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "AAA").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPRD_Idx ON PPRD");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "FRT_FrtId").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX FRT_FrtId ON FRT (FRTID)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "OPM_OpmId").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX OPM_OpmId ON OPM (OPMID)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "OPH_OphId").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX OPH_OphId ON OPH (OPHID)");


            Server server = new Server
            {
                Services = { Rest.RestService.BindService(new RestServiceImpl()) },
                Ports = { new ServerPort("127.0.0.1", 50055, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("217.160.13.102", 50055, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Rest server listening on port 50055");

            Handle.GET("/tMaxWebRestSCStop", () =>
            {
                //Task.Run(async () => { await server.ShutdownAsync(); }).Wait();
                server.ShutdownAsync().Wait();
                return "ShutDown gRPC Server OK";
            });


            Handle.GET("/d1", () =>
            {
                int nor = 0;
                decimal vm3 = 0;

                sw.Restart();
                foreach(var h in Db.SQL<OPH>("select r from OPH r"))// where r.OPHID < ?", 2_000_000))
                {
                    nor++;
                    vm3 += h.VM3;

                }
                sw.Stop();

                return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
            });


        }
    }
}