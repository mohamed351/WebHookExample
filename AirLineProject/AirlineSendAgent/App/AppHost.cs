using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineSendAgent.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AirlineSendAgent.App
{
    public class AppHost : IAppHost
    {
        private readonly IWebHookClient client;
        private readonly ApplicationDbContext context;

        public AppHost(IWebHookClient client, ApplicationDbContext context)
        {
            this.client = client;
            this.context = context;
        }
        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare("trigger", type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;
                    Console.WriteLine(queueName);
                    channel.QueueBind(queueName, exchange: "trigger", routingKey: "");
                    var consumer = new EventingBasicConsumer(channel);
                    Console.WriteLine("listening to Message bus");
                    consumer.Received += async(e, c) =>
                    {
                        foreach (var item in context.WebHookSubscriptions.ToList())
                        {
                            var dto = JsonSerializer.Deserialize<FlightDetailUpdateDTO>(Encoding.UTF8.GetString(c.Body));
                            var webHook = context.WebHookSubscriptions.FirstOrDefault(a => a.Secret == dto.Secret);
                            dto.Url = item.WebhookUrl;
                            dto.Secret = item.Secret;
                            dto.WebhookType = item.WebhookType;
                            dto.Publisher = item.WebhookPublisher;
                            await client.SendWebHookNotification(dto);
                        }
                      
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    Console.ReadLine();
                }
            }
        }

      
    }
}
