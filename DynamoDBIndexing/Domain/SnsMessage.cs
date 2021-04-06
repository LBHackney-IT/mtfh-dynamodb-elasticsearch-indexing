using System;
using System.Collections.Generic;

namespace DynamoDBIndexing.Domain
{
    public class SnsMessage
    {
        public string DynamoTable { get; set; }
        public string IndexNodeHost { get; set; }
        public string IndexName { get; set; }
    }
}
