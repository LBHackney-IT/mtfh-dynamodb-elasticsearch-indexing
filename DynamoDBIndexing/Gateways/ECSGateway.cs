using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon.ECS;
using Amazon.ECS.Model;
using KeyValuePair = Amazon.ECS.Model.KeyValuePair;
using Task = System.Threading.Tasks.Task;

namespace DynamoDBIndexing.Gateways
{
    public class ECSGateway
    {
        public async Task ECSRunTask(string dynamoTable, string indexNodeHost, string indexName)
        {
            string awsEcsTaskArn = Environment.GetEnvironmentVariable("AWS_ECS_TASK_ARN");
            string awsNetworkSubnet = Environment.GetEnvironmentVariable("AWS_NETWORK_SUBNET");
            string ecrRepoName = Environment.GetEnvironmentVariable("ECR_REPO_NAME");
            string EcsClusterName = Environment.GetEnvironmentVariable("ECS_CLUSTER_NAME");
            var AmazonECS = new AmazonECSClient();

            ContainerOverride containerOverride = new ContainerOverride()
            {
                Name = ecrRepoName,
                Environment = new List<KeyValuePair>()
                            {
                                new KeyValuePair() {Name = "DYNAMODB_TABLE", Value = dynamoTable},
                                new KeyValuePair() {Name = "ELASTICSEARCH_HOST", Value = indexNodeHost},
                                new KeyValuePair() {Name = "ELASTICSEARCH_INDEX", Value = indexName},
                            }
            };

            RunTaskResponse results = await AmazonECS.RunTaskAsync(new RunTaskRequest()
            {
                Cluster = EcsClusterName,
                LaunchType = LaunchType.FARGATE,
                TaskDefinition = awsEcsTaskArn,
                Count = 1,
                NetworkConfiguration = new NetworkConfiguration()
                {
                    AwsvpcConfiguration = new AwsVpcConfiguration()
                    {
                        Subnets = new List<string>() { awsNetworkSubnet },
                        AssignPublicIp = AssignPublicIp.DISABLED,
                    },
                },
                Overrides = new TaskOverride()
                {
                    ContainerOverrides = new List<ContainerOverride>()
                        {
                            containerOverride
                        }
                }
            });
        }
    }
}
