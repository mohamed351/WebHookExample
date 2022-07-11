using AirLineProject.Dtos;

namespace AirLineProject.MessageBus
{
    public interface IMessageBusClient
    {
        void SendMessage(NotificationMessageDTO notificationMessage);

    }
}
