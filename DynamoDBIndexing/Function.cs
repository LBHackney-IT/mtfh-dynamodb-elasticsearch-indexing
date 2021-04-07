using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;

using Newtonsoft.Json;

using DynamoDBIndexing.Domain;
using DynamoDBIndexing.UseCase;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DynamoDBIndexing
{
    public class Function
    {
        public Function()
        {
        }

        public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
        {
            foreach (var record in evnt.Records)
            {
                await ProcessRecordAsync(record, context);
            }
        }

        private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed record {record.Sns.Message}");
            SnsMessage snsMessage = JsonConvert.DeserializeObject<SnsMessage>(record.Sns.Message);
            context.Logger.LogLine($"dynamoTable: {snsMessage.DynamoTable}");

            SyncPersonData syncPersonData = new SyncPersonData();
            await syncPersonData.ExecuteSyncPersonData(snsMessage.DynamoTable, snsMessage.IndexNodeHost, snsMessage.IndexName, context);

            await Task.CompletedTask;
        }
    }
}
