using System.Threading.Tasks;
using NServiceBus;
using MyShared;

#region AzureMultiHost_PingHandler

public class PingHandler :
    IHandleMessages<Ping>
{
    public Task Handle(MyShared.Ping message, IMessageHandlerContext context)
    {
        VerificationLogger.Write("Receiver", "Got Ping and will reply with Pong");
        return context.Reply(new Pong());
    }
}

#endregion
