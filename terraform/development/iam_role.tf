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
                        "sns:GetTopicAttributes",
                        "sns:ListSubscriptionsByTopic",
                        "sns:Subscribe"
                     ],
            "Resource": "${aws_sns_topic.sync_notification.arn}"
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
