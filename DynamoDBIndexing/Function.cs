using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.ECS;

using DynamoDBIndexing.Gateways;

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

            ECSGateway ecsGateway = new ECSGateway();

            await ecsGateway.ECSRunTask(input.DynamoTable, input.IndexNodeHost, input.IndexName);
        }
    }
}
