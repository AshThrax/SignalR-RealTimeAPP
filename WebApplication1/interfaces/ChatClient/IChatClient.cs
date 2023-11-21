namespace WebApplication1.interfaces.ChatClient
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
