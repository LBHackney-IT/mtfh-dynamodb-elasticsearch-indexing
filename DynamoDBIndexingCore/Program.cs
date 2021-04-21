﻿using System;
using System.Threading.Tasks;

using DynamoDBIndexingCore.Domain;
using DynamoDBIndexingCore.UseCase;

namespace NetCore.Docker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string DynamoTable = Convert.ToString(args[0]);
            string IndexNodeHost = Convert.ToString(args[1]);
            string IndexName = Convert.ToString(args[2]);

            SyncPersonData syncPersonData = new SyncPersonData();
            await syncPersonData.ExecuteSyncPersonData(DynamoTable, IndexNodeHost, IndexName);

            await Task.CompletedTask;
        }
    }
}
