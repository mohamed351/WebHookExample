using AirLineProject.Dtos;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace AirLineProject.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        public void SendMessage(NotificationMessageDTO notificationMessage)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using(var connection = factory.CreateConnection())
            {
                using(var channcel = connection.CreateModel())
                {
                    channcel.ExchangeDeclare("trigger", ExchangeType.Fanout);
                    var message = JsonSerializer.Serialize(notificationMessage);
                    var body = Encoding.UTF8.GetBytes(message);
                    channcel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
                    Console.WriteLine("Message Published");

                }
            }
        }
    }
}
