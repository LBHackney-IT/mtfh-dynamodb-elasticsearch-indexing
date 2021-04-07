using System;
using System.Linq;
using System.Collections.Generic;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

using DynamoDBIndexing.Domain;

namespace DynamoDBIndexing.Factories
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
                PersonTypes = (List<String>) databaseEntity["personTypes"]
            };
        }
    }
}
