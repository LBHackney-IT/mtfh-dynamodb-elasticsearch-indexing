using System;
using System.Threading.Tasks;

using Nest;

namespace DynamoDBIndexing.Gateways
{
    public class ElasticSearchGateway
    {
        private readonly string _indexNodeHost;
        private readonly string _indexName;

        public ElasticSearchGateway(string indexNodeHost, string indexName)
        {
            _indexNodeHost = indexNodeHost;
            _indexName = indexName;
        }

        public IndexResponse IndexDocument(Object doc)
        {
            var node = new Uri(_indexNodeHost);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            var response = client.Index(doc, idx => idx.Index(_indexName));

            return response;
        }
    }
}
