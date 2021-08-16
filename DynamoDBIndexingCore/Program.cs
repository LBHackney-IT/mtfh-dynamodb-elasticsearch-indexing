using System;
using System.Threading.Tasks;

using DynamoDBIndexingCore.UseCase;

namespace NetCore.Docker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string dynamoTable = Convert.ToString(args[0]);
            string indexNodeHost = Convert.ToString(args[1]);
            string indexName = Convert.ToString(args[2]);

            if (dynamoTable == "Persons")
            {
                SyncPersonData syncPersonData = new SyncPersonData();
                await syncPersonData.ExecuteSyncPersonData(dynamoTable, indexNodeHost, indexName);
            }
            else if (dynamoTable == "TenureInformation")
            {
                SyncTenureData syncTenureData = new SyncTenureData();
                await syncTenureData.ExecuteSyncTenureData(dynamoTable, indexNodeHost, indexName);
            }
            else if (dynamoTable == "Assets")
            {
                SyncAssetData syncAssetData = new SyncAssetData();
                await syncAssetData.ExecuteSyncAssetData(dynamoTable, indexNodeHost, indexName);
            }
            else
            {
                Console.WriteLine("Invalid entity to sync!");
            }

            await Task.CompletedTask;
        }
    }
}
