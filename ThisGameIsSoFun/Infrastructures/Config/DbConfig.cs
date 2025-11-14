using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ThisGameIsSoFun.Infrastructures.Models;

namespace ThisGameIsSoFun.Infrastructures.Config
{
    public static class DbConfig
    {
        public static void MongoDbConfig(IHostApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));

            builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<MongoDbConfig>>().Value;
                return new MongoClient(config.ConnectionString);
            });
        }
    }
}
