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
        public async Task ExecuteSyncPersonData(string tableName, string indexNodeHost, string indexName)
        {
            ElasticSearchGateway elasticSearchGateway = new ElasticSearchGateway(indexNodeHost, indexName);
            DynamoDBGateway dynamoDBGateway = new DynamoDBGateway();

            await foreach (Document document in dynamoDBGateway.ScanDynamoDBTable(tableName))
            {
                Person TransformedPerson = document.ToDomainPerson();
                elasticSearchGateway.IndexDocument(TransformedPerson);
            }
        }
    }
}
