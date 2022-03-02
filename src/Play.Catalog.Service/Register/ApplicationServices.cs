using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Register;

public static partial class Register
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddControllers(opt => opt.SuppressAsyncSuffixInActionNames = false);

        services.AddEndpointsApiExplorer();

        var ServiceSettings = new ServiceSettings(config["ServiceSettings:ServiceName"]);

        services.AddSingleton(serviceProvider =>
        {
            var mongoDbSettings = config.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoClient.GetDatabase(ServiceSettings.ServiceName);
        });

        services.AddSingleton<IItemsRepository, ItemsRepository>();

        return services;
    }
}