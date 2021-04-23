using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon.Lambda.Core;
using Amazon.ECS;

using DynamoDBIndexing.Domain;

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

            var AmazonECS = new AmazonECSClient();

            Amazon.ECS.Model.ContainerOverride containerOverride = new Amazon.ECS.Model.ContainerOverride()
            {
                Environment = new List<Amazon.ECS.Model.KeyValuePair>()
                            {
                                new Amazon.ECS.Model.KeyValuePair() {Name = "DYNAMODB_TABLE", Value = input.DynamoTable},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_HOST", Value = input.IndexNodeHost},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_INDEX", Value = input.IndexName},
                            }
            };

            var results = await AmazonECS.RunTaskAsync(new Amazon.ECS.Model.RunTaskRequest()
            {
                Cluster = "mfth-dynamodb-elasticsearch-indexing",
                LaunchType = LaunchType.FARGATE,
                TaskDefinition = "arn:aws:ecs:eu-west-2:364864573329:task-definition/mfth-dynamodb-elasticsearch-indexing-development-task:7",
                Count = 1,
                NetworkConfiguration = new Amazon.ECS.Model.NetworkConfiguration()
                {
                    AwsvpcConfiguration = new Amazon.ECS.Model.AwsVpcConfiguration()
                    {
                        Subnets = new List<string>() { "subnet-0140d06fb84fdb547", "subnet-05ce390ba88c42bfd" },
                        AssignPublicIp = AssignPublicIp.DISABLED,
                    },
                },
                Overrides = new Amazon.ECS.Model.TaskOverride()
                {
                    ContainerOverrides = new List<Amazon.ECS.Model.ContainerOverride>()
                        {
                            containerOverride
                        }
                }
            });

            await Task.CompletedTask;
        }
    }
}
