using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.DynamoDBv2.DocumentModel;

using Nest;

using DynamoDBIndexing.Domain;
using DynamoDBIndexing.Factories;
using DynamoDBIndexing.Gateways;

namespace DynamoDBIndexing.UseCase
{
    public class SyncPersonData
    {
        public async Task ExecuteSyncPersonData(string TableName, string IndexNodeHost, string IndexName, ILambdaContext context)
        {
            ElasticSearchGateway elasticSearchGateway = new ElasticSearchGateway(IndexNodeHost, IndexName);
            DynamoDBGateway dynamoDBGateway = new DynamoDBGateway();

            await foreach (Document document in dynamoDBGateway.ScanDynamoDBTable(TableName))
            {
                Person TransformedPerson = document.ToDomainPerson();
                context.Logger.LogLine($"Item ID: {document["id"]}");
                IndexResponse response = elasticSearchGateway.IndexDocument(TransformedPerson);
                context.Logger.LogLine($"ElasticSearch call response: {response.ToString()}");
            }
        }
    }
}
