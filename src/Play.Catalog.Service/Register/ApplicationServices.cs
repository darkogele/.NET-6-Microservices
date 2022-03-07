namespace Play.Catalog.Service.Register;

public static partial class Register
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Built in .net Services
        services.AddControllers(opt => opt.SuppressAsyncSuffixInActionNames = false);

        services.AddEndpointsApiExplorer();

        // DI Services

        return services;
    }
}