using System.Collections.Generic;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBIndexingCore.Gateways
{
    public class DynamoDBGateway
    {
        public async IAsyncEnumerable<Document> ScanDynamoDBTable(string TableName)
        {
            AmazonDynamoDBClient _client = new AmazonDynamoDBClient();
            Table dynamoDBTable = Table.LoadTable(_client, TableName);
            ScanFilter scanFilter = new ScanFilter();
            Search search = dynamoDBTable.Scan(scanFilter);

            List<Document> documentList = new List<Document>();

            do
            {
                documentList = await search.GetNextSetAsync();
                foreach (var document in documentList)
                {
                    yield return document;
                }
            } while (!search.IsDone);
        }
    }
}
