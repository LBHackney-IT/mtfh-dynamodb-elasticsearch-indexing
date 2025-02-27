# resource "aws_ecs_cluster" "cluster" {
#   name               = var.ecs_cluster_name
#   capacity_providers = ["FARGATE", "FARGATE_SPOT"]

#   tags = {
#     Name              = var.ecr_repo_name
#     Environment       = var.environment_name
#     terraform-managed = true
#     project_name      = var.project_name
#   }
# }

# resource "aws_ecs_task_definition" "app" {
#   family                   = "${var.ecr_repo_name}-task"
#   execution_role_arn       = aws_iam_role.ecs_task_role.arn
#   task_role_arn            = aws_iam_role.ecs_task_role.arn
#   network_mode             = "awsvpc"
#   requires_compatibilities = ["FARGATE"]
#   cpu                      = 256
#   memory                   = 512
#   container_definitions    = jsonencode(
#   [
#     {
#       name      = var.ecr_repo_name
#       image     = "${var.ecr_host}/${var.ecr_repo_name}:${var.ecr_image_sha1}"
#       cpu       = 256
#       memory    = 512
#       essential = true
#       logConfiguration = {
#         logDriver = "awslogs"
#         options   = {
#           "awslogs-group"         = aws_cloudwatch_log_group.log_group.name,
#           "awslogs-region"        = "eu-west-2",
#           "awslogs-stream-prefix" = "ecs"
#         }
#       }
#     }
#   ])

#   tags = {
#     Name              = var.ecr_repo_name
#     Environment       = var.environment_name
#     terraform-managed = true
#     project_name      = var.project_name
#   }
# }

# resource "aws_cloudwatch_log_group" "log_group" {
#   name = "/ecs/${var.ecr_repo_name}-task"

#   tags = {
#     Name              = var.ecr_repo_name
#     Environment       = var.environment_name
#     terraform-managed = true
#     project_name      = var.project_name
#   }
# }
