using System.Threading.Tasks;

using Amazon.DynamoDBv2.DocumentModel;

using Nest;

using DynamoDBIndexingCore.Domain;
using DynamoDBIndexingCore.Factories;
using DynamoDBIndexingCore.Gateways;

namespace DynamoDBIndexingCore.UseCase
{
    public class SyncTenureData
    {
        public async Task ExecuteSyncTenureData(string tableName, string indexNodeHost, string indexName)
        {
            ElasticSearchGateway elasticSearchGateway = new ElasticSearchGateway(indexNodeHost, indexName);
            DynamoDBGateway dynamoDBGateway = new DynamoDBGateway();

            await foreach (Document document in dynamoDBGateway.ScanDynamoDBTable(tableName))
            {
                Tenure TransformedTenure = document.ToDomainTenure();
                elasticSearchGateway.IndexDocument(TransformedTenure);
            }
        }
    }
}
