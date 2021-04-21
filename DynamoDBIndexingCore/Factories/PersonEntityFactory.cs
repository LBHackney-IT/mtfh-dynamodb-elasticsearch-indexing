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
                AssetLlpgRef = getStringDynamoEntry(databaseEntity, "assetLlpgRef"),
                AssetId = getStringDynamoEntry(databaseEntity, "assetId")
            };
        }
        public static Person ToDomainPerson(this Document databaseEntity)
        {
            return new Person
            {
                Id = databaseEntity["id"],
                Title = databaseEntity["title"],
                PreferredFirstname = databaseEntity["preferredFirstname"],
                PreferredSurname = databaseEntity["preferredSurname"],
                Firstname = databaseEntity["firstname"],
                MiddleName = databaseEntity["middlename"],
                Surname = databaseEntity["surname"],
                DateOfBirth = databaseEntity["dateOfBirth"],
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
