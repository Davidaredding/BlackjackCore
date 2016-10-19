using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

class Send
{
    public static void Main()
    {
       /* var message = string.Empty;
       while(true){
            Console.WriteLine("Type a message to put it on the queue, or just press enter to exit.");
            message = "Test";

            if(String.IsNullOrEmpty(message)){
                Console.WriteLine("Exiting");
                Environment.Exit(0);
            }
            else{
                SendMessage(message);
            }
            Thread.Sleep(1000);
       } */
       SendMessage("Testing");     
    }

    static ConnectionFactory factory = null;

    public static void SendMessage(string message){
        factory = factory??new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        {
            using(var channel = connection.CreateModel())
            {        
                //according to docs this is a idempotent so low cost.
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                
                while(true){
                    message = string.Format("{0:D5}",DateTime.Now.Ticks.ToString());
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    Thread.Sleep(100);
                }    
            }
        }
    }
}