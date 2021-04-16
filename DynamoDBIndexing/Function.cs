using System.Threading.Tasks;

using Amazon.Lambda.Core;

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

        public async Task FunctionHandler(DynamoDBIndexingInput input, ILambdaContext context)
        {
            await ProcessRecordAsync(input, context);
        }

        private async Task ProcessRecordAsync(DynamoDBIndexingInput input, ILambdaContext context)
        {
            context.Logger.LogLine($"dynamoTable: {input.DynamoTable}");
            context.Logger.LogLine($"IndexName: {input.IndexName}");

            SyncPersonData syncPersonData = new SyncPersonData();
            await syncPersonData.ExecuteSyncPersonData(input.DynamoTable, input.IndexNodeHost, input.IndexName, context);

            await Task.CompletedTask;
        }
    }
}
