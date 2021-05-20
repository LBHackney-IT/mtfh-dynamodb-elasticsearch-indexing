variable "environment_name" {
    type = string
    default = "development"
}

variable "project_name" {
    type = string
    default = "Housing-Development"
}

variable "ecs_cluster_name" {
    type = string
    default = "mfth-dynamodb-elasticsearch-indexing"
}

variable "ecr_repo_name" {
    type = string
}
