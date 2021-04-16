namespace DynamoDBIndexing.Domain
{
    public class DynamoDBIndexingInput
    {
        public string DynamoTable { get; set; }
        public string IndexNodeHost { get; set; }
        public string IndexName { get; set; }
    }
}
