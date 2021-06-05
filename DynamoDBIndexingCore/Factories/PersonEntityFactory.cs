using System;
using System.Linq;
using System.Collections.Generic;

using Amazon.DynamoDBv2.DocumentModel;

using DynamoDBIndexingCore.Domain;

namespace DynamoDBIndexingCore.Factories
{
    public static class PersonEntityFactory
    {
        public static Identification ToDomainIdentification(this Document databaseEntity)
        {
            return new Identification
            {
                IdentificationType = databaseEntity["identificationType"],
                Value = databaseEntity["value"],
                IsOriginalDocumentSeen = (bool) databaseEntity["isOriginalDocumentSeen"],
                LinkToDocument = databaseEntity["linkToDocument"]
            };
        }
        public static Tenure ToDomainTenure(this Document databaseEntity)
        {
            return new Tenure
            {
                Id = databaseEntity["id"],
                Type = getStringDynamoEntry(databaseEntity, "type"),
                StartDate = getStringDynamoEntry(databaseEntity, "startDate"),
                EndDate = getStringDynamoEntry(databaseEntity, "endDate"),
                AssetFullAddress = getStringDynamoEntry(databaseEntity, "assetFullAddress"),
                Uprn = getStringDynamoEntry(databaseEntity, "uprn"),
                AssetId = getStringDynamoEntry(databaseEntity, "assetId")
            };
        }
        public static Person ToDomainPerson(this Document databaseEntity)
        {
            return new Person
            {
                Id = databaseEntity["id"],
                Title = databaseEntity["title"],
                PreferredTitle = databaseEntity.Contains("preferredTitle") ? databaseEntity["preferredTitle"] : "",
                PreferredFirstname = databaseEntity.Contains("preferredFirstname") ? databaseEntity["preferredFirstname"] : "",
                PreferredMiddleName = databaseEntity.Contains("preferredMiddleName") ? databaseEntity["preferredMiddleName"] : "",
                PreferredSurname = databaseEntity.Contains("preferredSurname") ? databaseEntity["preferredSurname"] : "",
                Firstname = databaseEntity.Contains("firstname") ? databaseEntity["firstname"] : "",
                MiddleName = databaseEntity.Contains("middleName") ? databaseEntity["middleName"] : "",
                Surname = databaseEntity.Contains("surname") ? databaseEntity["surname"] : "",
                DateOfBirth = databaseEntity["dateOfBirth"],
                NationalInsuranceNo = databaseEntity.Contains("nationalInsuranceNo") ? databaseEntity["nationalInsuranceNo"] : "",
                Identifications = ((List<Document>) databaseEntity["identifications"]).Select(p => p.ToDomainIdentification()),
                Tenures = ((List<Document>) databaseEntity["tenures"]).Select(p => p.ToDomainTenure()),
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
