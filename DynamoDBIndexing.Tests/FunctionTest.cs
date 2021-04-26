using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.TestUtilities;

using DynamoDBIndexing;

namespace DynamoDBIndexing.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestSQSEventLambdaFunction()
        {
            DynamoDBIndexingInput input = new DynamoDBIndexingInput();
            input.DynamoTable = "Persons";
            input.IndexNodeHost = "index host";
            input.IndexName = "persons_index";

            var logger = new TestLambdaLogger();
            var context = new TestLambdaContext
            {
                Logger = logger
            };

            var function = new Function();
            await function.FunctionHandler(input, context);

            Assert.Contains("Processed record foobar", logger.Buffer.ToString());
        }
    }
}
