using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBIndexingCore.Gateways
{
    public class DynamoDBGateway
    {
        public async IAsyncEnumerable<Document> ScanDynamoDBTable(string tableName)
        {
            AmazonDynamoDBClient _client = new AmazonDynamoDBClient();
            Table dynamoDBTable = Table.LoadTable(_client, tableName);
            ScanFilter scanFilter = new ScanFilter();
            Search search = dynamoDBTable.Scan(scanFilter);

            do
            {
                List<Document> documentList = await search.GetNextSetAsync();
                foreach (var document in documentList)
                {
                    Console.WriteLine(document.Keys);
                    yield return document;
                }
            } while (!search.IsDone);
        }
    }
}
