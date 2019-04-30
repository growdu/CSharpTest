using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPTest.RabbitMq
{
    class ProducerTest
    {
        static void Main(string[] args)
        {
            //Publish();
            Consume();
        }

        public static void Publish()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "test-host";
            factory.UserName = "user";
            factory.Password = "password";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //channel.ExchangeDeclare(exchange: "test-exchange", type: "fanout");
                    //channel.QueueDeclare("test-queue", false, false, false, null);
                    string message = "Hello World";
                    var body = Encoding.UTF8.GetBytes(message);
                    //channel.BasicPublish("", "test-queue", null, body);
                    channel.BasicPublish(exchange: "test-exchange", routingKey: "", basicProperties: null, body: body);
                    Console.WriteLine(" set {0}", message);
                }
            }
        }

        public static void Consume()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "test-host";
            factory.UserName = "user";
            factory.Password = "password";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //channel.ExchangeDeclare(exchange: "test-exchange", type: "fanout");
                    //channel.QueueDeclare("test-queue", false, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("test-queue", true, consumer);
                    Console.WriteLine(" waiting for message.");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received {0}", message);

                    }
                }
            }
        }

    }
}
