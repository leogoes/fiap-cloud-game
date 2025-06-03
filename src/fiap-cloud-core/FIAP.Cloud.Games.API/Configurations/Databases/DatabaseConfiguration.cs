using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using FIAP.Cloud.Games.Domain.Libraries.Entities;
using FIAP.Cloud.Games.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Cloud.Games.API.Configurations.Databases
{
    public static class DatabaseConfiguration
    {
        public static void AddMongodbConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(configuration["MONGO_DB_CONNECTION"]);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(configuration["MONGO_DB_NAME"]);
            });


            services.AddSingleton(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var database = client.GetDatabase(configuration["MONGO_DB_NAME"]);

                return database.GetCollection<Library>("library");
            });

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        public static void AddMysqlConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

            services.AddDbContext<GameContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(configuration["MYSQL_DB_CONNECTION"], serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
        }
    }
}
