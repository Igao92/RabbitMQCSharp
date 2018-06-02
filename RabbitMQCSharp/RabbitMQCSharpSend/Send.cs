using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCSharpSend
{
    class Send
    {
        public static void Main()
        {
            //Here we connect to a BROKER on the local machine - hence the localhost.
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"[X] Sent {message}");
                }

                Console.WriteLine(" Press [ENTER] to exit.");
                Console.ReadLine();
            }
        }
    }
}
