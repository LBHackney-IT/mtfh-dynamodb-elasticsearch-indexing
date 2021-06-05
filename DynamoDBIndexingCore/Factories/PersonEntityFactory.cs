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
                Id = getValueDynamoEntry(databaseEntity, "id"),
                Title = getValueDynamoEntry(databaseEntity, "title"),
                PreferredTitle = getValueDynamoEntry(databaseEntity, "preferredTitle"),
                PreferredFirstname = getValueDynamoEntry(databaseEntity, "preferredFirstname"),
                PreferredMiddleName = getValueDynamoEntry(databaseEntity, "preferredMiddleName"),
                PreferredSurname = getValueDynamoEntry(databaseEntity, "preferredSurname"),
                Firstname = getValueDynamoEntry(databaseEntity, "firstname"),
                MiddleName = getValueDynamoEntry(databaseEntity, "middleName"),
                Surname = getValueDynamoEntry(databaseEntity, "surname"),
                DateOfBirth = getValueDynamoEntry(databaseEntity, "dateOfBirth"),
                NationalInsuranceNo = getValueDynamoEntry(databaseEntity, "nationalInsuranceNo"),
                Identifications = ((List<Document>) getValueDynamoEntry(databaseEntity, "identifications")).Select(p => p.ToDomainIdentification()),
                Tenures = ((List<Document>) getValueDynamoEntry(databaseEntity, "tenures")).Select(p => p.ToDomainTenure()),
                PersonTypes = (List<String>) getValueDynamoEntry(databaseEntity, "personTypes")
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
        public static string getValueDynamoEntry(Document doc, string fieldName)
        {
            return doc.Contains(fieldName) ? doc[fieldName] : null;
        }
    }
}
