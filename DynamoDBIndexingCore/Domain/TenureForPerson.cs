namespace DynamoDBIndexingCore.Domain
{
    public class TenureForPerson
    {
        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AssetFullAddress { get; set; }
        public string Uprn { get; set; }
        public string PropertyReference { get; set; }
        public string AssetId { get; set; }
    }
}
