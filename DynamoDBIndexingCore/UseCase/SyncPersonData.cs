using System.Threading.Tasks;

using Amazon.DynamoDBv2.DocumentModel;

using Nest;

using DynamoDBIndexingCore.Domain;
using DynamoDBIndexingCore.Factories;
using DynamoDBIndexingCore.Gateways;

namespace DynamoDBIndexingCore.UseCase
{
    public class SyncPersonData
    {
        public async Task ExecuteSyncPersonData(string TableName, string IndexNodeHost, string IndexName)
        {
            ElasticSearchGateway elasticSearchGateway = new ElasticSearchGateway(IndexNodeHost, IndexName);
            DynamoDBGateway dynamoDBGateway = new DynamoDBGateway();

            await foreach (Document document in dynamoDBGateway.ScanDynamoDBTable(TableName))
            {
                Person TransformedPerson = document.ToDomainPerson();
                IndexResponse response = elasticSearchGateway.IndexDocument(TransformedPerson);
            }
        }
    }
}
