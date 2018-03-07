using NServiceBus;
using NServiceBus.Logging;

#pragma warning disable 618

public class EndpointConfig :
    IConfigureThisEndpoint
{
    static EndpointConfig()
    {
        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Debug);
    }

    public void Customize(EndpointConfiguration endpointConfiguration)
    {
        var persistence = endpointConfiguration.UsePersistence<AzureStoragePersistence>();
        persistence.ConnectionString("DefaultEndpointsProtocol=https;AccountName=carnbusstorage;AccountKey=XGoQFAa/nGH7/lCC2NuEL2X4OLZWzCDS4+h8iAb0AFKmk+g3zXfkdHT/1lV0nWLVHbQkVfeZGl6mWTMKm9LMQg==;EndpointSuffix=core.windows.net");
        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString("Endpoint=sb://carnbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SLblu/gBByZHDFwssItk9lGg5FixAzXc9wk1SmCU/68=");
        transport.UseEndpointOrientedTopology();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        endpointConfiguration.EnableInstallers();
    }
}

#pragma warning restore 618