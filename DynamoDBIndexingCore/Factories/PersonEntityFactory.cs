using System;
using System.Linq;
using System.Collections.Generic;

using Amazon.DynamoDBv2.DocumentModel;

using DynamoDBIndexingCore.Domain;

namespace DynamoDBIndexingCore.Factories
{
    public static class PersonEntityFactory
    {
        public static TenureForPerson ToDomainTenureForPerson(this Document databaseEntity)
        {
            return new TenureForPerson
            {
                Id = databaseEntity["id"],
                PaymentReference = getStringDynamoEntry(databaseEntity, "paymentReference"),
                Type = getStringDynamoEntry(databaseEntity, "type"),
                StartDate = getStringDynamoEntry(databaseEntity, "startDate"),
                EndDate = getStringDynamoEntry(databaseEntity, "endDate"),
                AssetFullAddress = getStringDynamoEntry(databaseEntity, "assetFullAddress"),
                Uprn = getStringDynamoEntry(databaseEntity, "uprn"),
                PropertyReference = getStringDynamoEntry(databaseEntity, "propertyReference"),
                AssetId = getStringDynamoEntry(databaseEntity, "assetId")
            };
        }
        public static Person ToDomainPerson(this Document databaseEntity)
        {
            return new Person
            {
                Id = databaseEntity["id"],
                Title = databaseEntity.Contains("title") ? getStringDynamoEntry(databaseEntity, "title") : "",
                PreferredTitle = databaseEntity.Contains("preferredTitle") ? getStringDynamoEntry(databaseEntity, "preferredTitle") : "",
                PreferredFirstname = databaseEntity.Contains("preferredFirstName") ? databaseEntity["preferredFirstName"] : "",
                PreferredMiddleName = databaseEntity.Contains("preferredMiddleName") ? databaseEntity["preferredMiddleName"] : "",
                PreferredSurname = databaseEntity.Contains("preferredSurname") ? databaseEntity["preferredSurname"] : "",
                Firstname = databaseEntity.Contains("firstName") ? databaseEntity["firstName"] : "",
                MiddleName = databaseEntity.Contains("middleName") ? databaseEntity["middleName"] : "",
                Surname = databaseEntity.Contains("surname") ? databaseEntity["surname"] : "",
                DateOfBirth = databaseEntity.Contains("dateOfBirth") ? getStringDynamoEntry(databaseEntity, "dateOfBirth") : "",
                PlaceOfBirth = databaseEntity.Contains("placeOfBirth") ? databaseEntity["placeOfBirth"] : "",
                Tenures = databaseEntity.Contains("tenures") ? ((List<Document>) databaseEntity["tenures"]).Select(p => p.ToDomainTenureForPerson()) : null,
                PersonTypes = (List<String>) databaseEntity["personTypes"]
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
