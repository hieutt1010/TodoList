using Microsoft.EntityFrameworkCore;
using TodoApi.Context;

namespace TodoApi.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }

    public static class MongoDbConfig
    {
        public static void UseMongoDb(this IServiceCollection services, ConfigurationManager configuration)
        {
            var mongoDBSettings = configuration.GetSection("MongoDBSettings").Get<MongoDbSettings>();
            services.AddDbContext<MongoDbContext>(options =>
            options.UseMongoDB(mongoDBSettings.ConnectionString ?? "", mongoDBSettings.DatabaseName ?? ""));
        }
    }
}