resource "aws_sns_topic" "sync_notification" {
  name = "mtfh-dynamodb-elasticsearch-indexing"
}
