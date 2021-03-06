﻿using System;
using Grpc.Core;
using Starcounter;
using System.Linq;

namespace tMaxWebRestServer
{
    class Program
    {
        const int Port = 50055;

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