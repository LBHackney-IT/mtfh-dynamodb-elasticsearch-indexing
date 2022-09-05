using System;
using System.Linq;
using System.Collections.Generic;

using Amazon.DynamoDBv2.DocumentModel;
using DynamoDBIndexingCore.Domain;

namespace DynamoDBIndexingCore.Factories
{
    public static class TenureEntityFactory
    {
        public static PersonForTenure ToDomainPersonForTenure(this Document databaseEntity)
        {
            return new PersonForTenure
            {
                Id = databaseEntity["id"],
                Type = getStringDynamoEntry(databaseEntity, "type"),
                FullName = getStringDynamoEntry(databaseEntity, "fullName"),
                isResponsible = (Boolean) databaseEntity["isResponsible"]
            };
        }
        public static AssetForTenure ToDomainAssetForTenure(this Document databaseEntity)
        {
            return new AssetForTenure
            {
                Id = databaseEntity["id"],
                Type = getStringDynamoEntry(databaseEntity, "type"),
                FullAddress = getStringDynamoEntry(databaseEntity, "fullAddress"),
                Uprn = getStringDynamoEntry(databaseEntity, "uprn")
            };
        }
        public static TenureType ToDomainTenureType(this Document databaseEntity)
        {
            return new TenureType
            {
                Code = databaseEntity["code"],
                Description = getStringDynamoEntry(databaseEntity, "description")
            };
        }
        public static Tenure ToDomainTenure(this Document databaseEntity)
        {
            return new Tenure
            {
                Id = databaseEntity["id"],
                PaymentReference = databaseEntity.Contains("paymentReference") ? databaseEntity["paymentReference"] : "",
                HouseholdMembers = ((List<Document>) databaseEntity["householdMembers"]).Select(p => p.ToDomainPersonForTenure()),
                TenuredAsset = databaseEntity.Contains("tenuredAsset") ? ((Document) databaseEntity["tenuredAsset"]).ToDomainAssetForTenure() : null,
                StartOfTenureDate = databaseEntity.Contains("startOfTenureDate") ? getStringDynamoEntry(databaseEntity, "startOfTenureDate") : "",
                EndOfTenureDate = databaseEntity.Contains("endOfTenureDate") ? getStringDynamoEntry(databaseEntity, "endOfTenureDate") : "",
                TenureType = ((Document) databaseEntity["tenureType"]).ToDomainTenureType()
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
