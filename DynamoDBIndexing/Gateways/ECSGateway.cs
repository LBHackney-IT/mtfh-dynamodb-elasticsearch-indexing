using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon.ECS;

namespace DynamoDBIndexing.Gateways
{
    public class ECSGateway
    {
        public ECSGateway()
        {

        }
        public async Task<Amazon.ECS.Model.RunTaskResponse> ECSRunTask(string DynamoTable, string IndexNodeHost, string IndexName)
        {
            string aws_ecs_task_arn = Environment.GetEnvironmentVariable("AWS_ECS_TASK_ARN");
            var AmazonECS = new AmazonECSClient();

            Amazon.ECS.Model.ContainerOverride containerOverride = new Amazon.ECS.Model.ContainerOverride()
            {
                Name = "mfth-dynamodb-elasticsearch-indexing",
                Environment = new List<Amazon.ECS.Model.KeyValuePair>()
                            {
                                new Amazon.ECS.Model.KeyValuePair() {Name = "DYNAMODB_TABLE", Value = DynamoTable},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_HOST", Value = IndexNodeHost},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_INDEX", Value = IndexName},
                            }
            };

            Amazon.ECS.Model.RunTaskResponse results = await AmazonECS.RunTaskAsync(new Amazon.ECS.Model.RunTaskRequest()
            {
                Cluster = "mfth-dynamodb-elasticsearch-indexing",
                LaunchType = LaunchType.FARGATE,
                TaskDefinition = aws_ecs_task_arn,
                Count = 1,
                // NetworkConfiguration = new Amazon.ECS.Model.NetworkConfiguration()
                // {
                //     AwsvpcConfiguration = new Amazon.ECS.Model.AwsVpcConfiguration()
                //     {
                //         Subnets = new List<string>() { "subnet-0140d06fb84fdb547", "subnet-05ce390ba88c42bfd" },
                //         AssignPublicIp = AssignPublicIp.DISABLED,
                //     },
                // },
                Overrides = new Amazon.ECS.Model.TaskOverride()
                {
                    ContainerOverrides = new List<Amazon.ECS.Model.ContainerOverride>()
                        {
                            containerOverride
                        }
                }
            });

            return results;
        }
    }
}
