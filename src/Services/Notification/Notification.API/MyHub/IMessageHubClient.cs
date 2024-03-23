namespace Notification.API.MyHub
{
    public interface IMessageHubClient
    {
        Task SendNewMessageCount( int count);
    }
}
