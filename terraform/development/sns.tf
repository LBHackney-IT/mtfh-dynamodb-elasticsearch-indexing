resource "aws_sns_topic" "sync_notification" {
  name = "mtfh-dynamodb-elasticsearch-indexing"

  tags = {
    Name              = "mtfh-dynamodb-elasticsearch-indexing-${var.environment_name}"
    Environment       = var.environment_name
    terraform-managed = true
    project_name      = var.project_name
  }
}
