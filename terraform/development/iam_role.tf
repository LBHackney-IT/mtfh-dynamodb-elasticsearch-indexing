resource "aws_iam_policy" "lambda_dynamodb_sns_policy" {
    name                  = "lambda-dynamodb-elasticsearch-indexing"
    description           = "A policy allowing read operations on person dynamoDB for the elasticsearch indexing"
    path                  = "/mtfh-dynamodb-elasticsearch-indexing/"

    policy                = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                        "dynamodb:BatchGet*",
                        "dynamodb:DescribeStream",
                        "dynamodb:DescribeTable",
                        "dynamodb:Get*",
                        "dynamodb:Query",
                        "dynamodb:Scan"
                     ],
            "Resource": "arn:aws:dynamodb:eu-west-2:${data.aws_caller_identity.current.account_id}:table/Persons"
        },
        {
            "Effect": "Allow",
            "Action": [
                "ecr:GetAuthorizationToken",
                "ecr:BatchCheckLayerAvailability",
                "ecr:GetDownloadUrlForLayer",
                "ecr:BatchGetImage",
                "logs:CreateLogStream",
                "logs:PutLogEvents",
                "S3:*"
            ],
            "Resource": "*"
        },
        {
            "Effect": "Allow",
            "Action": [
              "ecs:DescribeContainerInstances",
              "ecs:DescribeTasks",
              "ecs:ListTasks",
              "ecs:UpdateContainerAgent",
              "ecs:StartTask",
              "ecs:StopTask",
              "ecs:RunTask"
            ],
            "Condition": {
                "ArnEquals": {
                    "ecs:cluster": "${aws_ecs_cluster.cluster.arn}"
                }
            },
            "Resource": "arn:aws:ecs:eu-west-2:364864573329:task-definition/mtfh-dynamodb-elasticsearch-indexing-development-task:15"
        }
    ]
}
EOF

  tags = {
    Name              = "mtfh-dynamodb-elasticsearch-indexing-${var.environment_name}"
    Environment       = var.environment_name
    terraform-managed = true
    project_name      = var.project_name
  }
}

data "aws_iam_policy_document" "assume_role_policy" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ecs-tasks.amazonaws.com"]
    }
  }
}
