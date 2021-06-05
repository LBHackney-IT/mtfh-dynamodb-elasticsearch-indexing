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
                Title = databaseEntity.Contains("title") ? databaseEntity["title"] : null,
                PreferredTitle = databaseEntity.Contains("preferredTitle") ? databaseEntity["preferredTitle"] : null,
                PreferredFirstname = databaseEntity.Contains("preferredFirstname") ? databaseEntity["preferredFirstname"] : null,
                PreferredMiddleName = databaseEntity.Contains("preferredMiddleName") ? databaseEntity["preferredMiddleName"] : null,
                PreferredSurname = databaseEntity.Contains("preferredSurname") ? databaseEntity["preferredSurname"] : null,
                Firstname = databaseEntity["firstname"],
                MiddleName = databaseEntity.Contains("middleName") ? databaseEntity["middleName"] : null,
                Surname = databaseEntity["surname"],
                DateOfBirth = databaseEntity.Contains("dateOfBirth") ? databaseEntity["dateOfBirth"] : null,
                NationalInsuranceNo = databaseEntity.Contains("nationalInsuranceNo") ? databaseEntity["nationalInsuranceNo"] : null,
                Identifications = ((List<Document>) (databaseEntity.Contains("identifications") ? databaseEntity["identifications"] : null)).Select(p => p.ToDomainIdentification()),
                Tenures = ((List<Document>) (databaseEntity.Contains("tenures") ? databaseEntity["tenures"] : null)).Select(p => p.ToDomainTenure()),
                PersonTypes = (List<String>) (databaseEntity.Contains("personTypes") ? databaseEntity["personTypes"] : null)
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
