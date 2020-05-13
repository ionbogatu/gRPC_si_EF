using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcService;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);

            var reply2 = await client.GetFirstPostDescriptionAsync(new EmptyRequest {});
            Console.WriteLine("First Post Description: " + reply2.Message);

            var reply3 = await client.GetAllPostsAsync(new EmptyRequest { });
            Console.WriteLine("Posts: " + reply3.Message);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
