namespace DynamoDBIndexingCore.Domain
{
    public class Asset
    {
        public string Id { get; set; }
        public string AssetId { get; set; }
        public string AssetType { get; set; }
        public AssetAddress AssetAddress { get; set; }
        public TenureForAsset Tenure { get; set; }
        public bool IsCautionaryAlerted { get; set; }
    }
}
