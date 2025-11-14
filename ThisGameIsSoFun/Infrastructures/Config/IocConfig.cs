using ThisGameIsSoFun.Services;

namespace ThisGameIsSoFun.Infrastructures.Config
{
    public static class IocConfig
    {
        public static void IocConfiguration(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMongoTestService, MongoTestService>();
        }
    }
}
