using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon.ECS;

namespace DynamoDBIndexing.Gateways
{
    public class ECSGateway
    {
        public async Task<Amazon.ECS.Model.RunTaskResponse> ECSRunTask(string DynamoTable, string IndexNodeHost, string IndexName)
        {
            string awsEcsTaskArn = Environment.GetEnvironmentVariable("AWS_ECS_TASK_ARN");
            string awsNetworkSubnet = Environment.GetEnvironmentVariable("AWS_NETWORK_SUBNET");
            string ecrRepoName = Environment.GetEnvironmentVariable("ECR_REPO_NAME");
            string EcsClusterName = Environment.GetEnvironmentVariable("ECS_CLUSTER_NAME");
            var AmazonECS = new AmazonECSClient();

            Amazon.ECS.Model.ContainerOverride containerOverride = new Amazon.ECS.Model.ContainerOverride()
            {
                Name = ecrRepoName,
                Environment = new List<Amazon.ECS.Model.KeyValuePair>()
                            {
                                new Amazon.ECS.Model.KeyValuePair() {Name = "DYNAMODB_TABLE", Value = DynamoTable},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_HOST", Value = IndexNodeHost},
                                new Amazon.ECS.Model.KeyValuePair() {Name = "ELASTICSEARCH_INDEX", Value = IndexName},
                            }
            };

            Amazon.ECS.Model.RunTaskResponse results = await AmazonECS.RunTaskAsync(new Amazon.ECS.Model.RunTaskRequest()
            {
                Cluster = EcsClusterName,
                LaunchType = LaunchType.FARGATE,
                TaskDefinition = awsEcsTaskArn,
                Count = 1,
                NetworkConfiguration = new Amazon.ECS.Model.NetworkConfiguration()
                {
                    AwsvpcConfiguration = new Amazon.ECS.Model.AwsVpcConfiguration()
                    {
                        Subnets = new List<string>() { awsNetworkSubnet },
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

            return results;
        }
    }
}
