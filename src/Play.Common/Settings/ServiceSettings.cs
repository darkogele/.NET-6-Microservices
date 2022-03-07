namespace Play.Common.Settings;

public class ServiceSettings
{
    public ServiceSettings(string serviceName)
    {
        ServiceName = serviceName;
    }

    public string ServiceName { get; init; }
}