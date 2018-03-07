using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

public static class VerificationLogger
{
    static CloudTable table;
    static object locker = new object();

    static VerificationLogger()
    {
        var cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=carnbuslogger;AccountKey=jScREBJGpeUw8PG0REr2VIm1qFUovjMSCZ6BrIkVfW6rpB0zbXHJkfOwsjy/gZMA1I0PyedZUXYY/ui5ijzMLw==;EndpointSuffix=core.windows.net");
        var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
        table = cloudTableClient.GetTableReference("MultiHostedEndpointsOutput");
        table.CreateIfNotExists();
    }

    public static void Write(string endpoint, string message)
    {
        lock (locker)
        {
            var tableOperation = TableOperation.Insert(new LogEntry(endpoint, message));
            table.Execute(tableOperation);
        }
    }

}