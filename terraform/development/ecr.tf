resource "aws_ecr_repository" "repository" {
  name = "mtfh-dynamodb-elasticsearch-indexing-${var.environment_name}"

  tags = {
    Name              = "mtfh-dynamodb-elasticsearch-indexing-${var.environment_name}"
    Environment       = var.environment_name
    terraform-managed = true
    project_name      = var.project_name
  }
}

resource "aws_ecr_lifecycle_policy" "deletion_policy" {
  repository = aws_ecr_repository.repository.name

  policy = <<EOF
  {
    "rules": [
      {
        "rulePriority": 1,
        "description": "Delete the oldest image when there are more than 10 images",
        "selection": {
          "tagStatus": "any",
          "countType": "imageCountMoreThan",
          "countNumber": 10
        },
        "action": {
          "type": "expire"
        }
      }
    ]
  }
  EOF
}
