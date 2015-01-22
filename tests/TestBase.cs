﻿using System;
using System.IO;
using System.Net;

using ServiceStack;
using ServiceStack.AzureSQL;
using ServiceStack.AzureSQL.Types;

namespace AzureSQL.Test
{
    public class TestsBase
    {
        const string AzureSQLSubscriptionId = "";
        const string LicenseTextPath = @"C:\src\appsettings.license.txt";
        const string FingerPrint = "sk_test_23KlmQohLKD4dfmAvxYESZ2z";
        protected readonly AzureSQLGateway gateway = new AzureSQLGateway(FingerPrint);

        public TestsBase()
        {
            if (File.Exists(LicenseTextPath))
                Licensing.RegisterLicenseFromFile(LicenseTextPath);
        }

        protected ServerName CreateServer()
        {
            var server = gateway.Post(new CreateServer
            {
                SubscriptionId = AzureSQLSubscriptionId, 
                AdministratorLogin = "test",
                AdministratorPassword = "test",
                Location = DataCenter.EastUS,
            });
            return server;
        }


        protected Database GetOrCreateDatabase()
        {
            try
            {
                return gateway.Get(new GetDatabase 
                    { 
                        SubscriptionId = AzureSQLSubscriptionId,  
                        ServerName = "SERVER-DB-01", 
                        DatabaseName = "TEST-DB-01" 
                    });
            }
            catch (AzureException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return CreateDatabase();

                throw;
            }
        }


        protected Database CreateDatabase()
        {
            var database = gateway.Post(new CreateDatabase
            {
                SubscriptionId = AzureSQLSubscriptionId,
                Name = "TEST-DB-01", 
                Edition = "Basic",
                ServerName = "SERVER-DB-01",
            });
            return database;
        }
    }
}
