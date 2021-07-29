using System.Collections.Generic;

namespace DynamoDBIndexingCore.Domain
{
    public class Tenure
    {
        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public IEnumerable<PersonForTenure> HouseholdMembers { get; set; }
        // public AssetForTenure TenuredAsset { get; set; }
        public string StartOfTenureDate { get; set; }
        public string EndOfTenureDate { get; set; }
        // public TenureType TenureType { get; set; }
    }
}
