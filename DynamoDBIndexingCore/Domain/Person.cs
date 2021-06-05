using System;
using System.Collections.Generic;

namespace DynamoDBIndexingCore.Domain
{
    public class Person
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? PreferredTitle { get; set; }
        public string? PreferredFirstname { get; set; }
        public string? PreferredMiddleName { get; set; }
        public string? PreferredSurname { get; set; }
        public string Firstname { get; set; }
        public string? MiddleName { get; set; }
        public string Surname { get; set; }
        public string? DateOfBirth { get; set; }
        public string? NationalInsuranceNo { get; set; }
        public IEnumerable<Identification>? Identifications { get; set; }
        public IEnumerable<Tenure>? Tenures { get; set; }
        public IEnumerable<String>? PersonTypes { get; set; }
    }
}
