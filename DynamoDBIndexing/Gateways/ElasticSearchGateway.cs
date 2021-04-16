using System;

using Nest;

namespace DynamoDBIndexing.Gateways
{
    public class ElasticSearchGateway
    {
        private readonly string _indexNodeHost;
        private readonly string _indexName;
        private readonly ElasticClient _client;

        public ElasticSearchGateway(string indexNodeHost, string indexName)
        {
            _indexNodeHost = indexNodeHost;
            _indexName = indexName;
            Uri node = new Uri(_indexNodeHost);
            ConnectionSettings settings = new ConnectionSettings(node);
            _client = new ElasticClient(settings);
        }

        public IndexResponse IndexDocument(Object doc)
        {
            var response = _client.Index(doc, idx => idx.Index(_indexName));

            return response;
        }
    }
}
