namespace DynamoDBIndexingCore.Domain
{
    public class TenureForAsset
    {
        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public string Type { get; set; }
        public string StartOfTenureDate { get; set; }
        public string EndOfTenureDate { get; set; }
    }
}
