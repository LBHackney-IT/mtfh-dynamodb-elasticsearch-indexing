resource "aws_ecs_cluster" "cluster" {
  name               = var.ecs_cluster_name
  capacity_providers = ["FARGATE", "FARGATE_SPOT"]

  tags = {
    Name              = var.ecr_repo_name
    Environment       = var.environment_name
    terraform-managed = true
    project_name      = var.project_name
  }
}
