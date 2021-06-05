using System;
using System.Collections.Generic;

namespace DynamoDBIndexingCore.Domain
{
    public class Person
    {
        public string Id { get; set; }
        #nullable enable
        public string? Title { get; set; }
        #nullable enable
        public string? PreferredTitle { get; set; }
        #nullable enable
        public string? PreferredFirstname { get; set; }
        #nullable enable
        public string? PreferredMiddleName { get; set; }
        #nullable enable
        public string? PreferredSurname { get; set; }
        public string Firstname { get; set; }
        #nullable enable
        public string? MiddleName { get; set; }
        public string Surname { get; set; }
        #nullable enable
        public string? DateOfBirth { get; set; }
        #nullable enable
        public string? NationalInsuranceNo { get; set; }
        #nullable enable
        public IEnumerable<Identification>? Identifications { get; set; }
        #nullable enable
        public IEnumerable<Tenure>? Tenures { get; set; }
        #nullable enable
        public IEnumerable<String>? PersonTypes { get; set; }
    }
}
