namespace Play.Catalog.Service.Settings;

public class ServiceSettings
{
    public ServiceSettings(string serviceName)
    {
        ServiceName = serviceName;
    }

    public string ServiceName { get; init; }
}