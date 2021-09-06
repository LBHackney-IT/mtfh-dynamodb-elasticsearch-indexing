using System;

using Amazon.DynamoDBv2.DocumentModel;
using DynamoDBIndexingCore.Domain;

namespace DynamoDBIndexingCore.Factories
{
    public static class AssetEntityFactory
    {
        public static AssetAddress ToDomainAssetAddress(this Document databaseEntity)
        {
            return new AssetAddress
            {
                Uprn = databaseEntity.Contains("uprn") ? getStringDynamoEntry(databaseEntity, "uprn") : "",
                AddressLine1 = databaseEntity["addressLine1"],
                AddressLine2 = databaseEntity["addressLine2"],
                AddressLine3 = databaseEntity["addressLine3"],
                AddressLine4 = databaseEntity["addressLine4"],
                PostCode = databaseEntity["postCode"],
                PostPreamble = databaseEntity["postPreamble"]
            };
        }
        public static TenureForAsset ToDomainTenureForAsset(this Document databaseEntity)
        {
            return new TenureForAsset
            {
                Id = databaseEntity.Contains("id") ? getStringDynamoEntry(databaseEntity, "id") : "",
                PaymentReference = databaseEntity.Contains("paymentReference") ? getStringDynamoEntry(databaseEntity, "paymentReference") : "",
                Type = databaseEntity.Contains("type") ? getStringDynamoEntry(databaseEntity, "type") : "",
                StartOfTenureDate = databaseEntity.Contains("startOfTenureDate") ? getStringDynamoEntry(databaseEntity, "startOfTenureDate") : "",
                EndOfTenureDate = databaseEntity.Contains("endOfTenureDate") ? getStringDynamoEntry(databaseEntity, "endOfTenureDate") : ""
            };
        }
        public static Asset ToDomainAsset(this Document databaseEntity)
        {
            return new Asset
            {
                Id = databaseEntity["id"],
                AssetId = databaseEntity["assetId"],
                AssetType = databaseEntity["assetType"],
                AssetAddress = ((Document) databaseEntity["assetAddress"]).ToDomainAssetAddress(),
                Tenure = ((Document) databaseEntity["tenure"]).ToDomainTenureForAsset(),
                IsCautionaryAlerted = false
            };
        }
        public static string getStringDynamoEntry(Document doc, string fieldName)
        {
            string value = null;
            try
            {
                Primitive primitive = doc[fieldName] as Primitive;
                if (primitive != null)
                {
                    value = primitive.Value.ToString();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return value;
        }
    }
}
