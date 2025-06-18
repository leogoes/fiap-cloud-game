using Aspire.Hosting;
using Aspire.Hosting.MongoDB;
using Aspire.Hosting.RabbitMQ;
using Aspire.Hosting.Redis;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
                   .WithEnvironment("MYSQL_ROOT_PASSWORD", "root")
                   .WithVolume("mysql_data", "/var/lib/mysql");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
                      .WithVolume("rabbitmq_data", "/var/lib/rabbitmq");

var redis = builder.AddRedis("redis")
                   .WithVolume("redis_data", "/data");

var mongodb = builder.AddMongoDB("mongodb")
                     .WithVolume("./data/db", "/data/db");


builder.AddContainer("keycloak", "quay.io/keycloak/keycloak:latest")
       .WithContainerName("Keycloak")
       .WithEnvironment("KC_HEALTH_ENABLED", "true")
       .WithEnvironment("KEYCLOAK_ADMIN", "admin")
       .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
       .WithVolume("./.containers/identity", "/opt/keycloak/data");

builder.AddProject<Projects.FIAP_Cloud_Games_API>("fiap-cloud-games-api")
                        .WithReference(mysql)
                        .WithReference(rabbitmq)
                        .WithReference(mongodb)
                        .WithReference(redis);

builder.AddProject<Projects.FIAP_Cloud_Games_Identity_API>("fiap-cloud-games-identity-api")
                        .WithReference(mysql);

builder.Build().Run();
