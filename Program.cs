using System;
using Microsoft.AspNetCore.SignalR.Client;

using System.Threading.Tasks;

namespace botogp_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var t = Test().Result;


            Console.ReadKey();
        }

        static async Task<string> Test()
        {
            var connection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:5001/race")
                            .WithConsoleLogger()
                            .Build();

            connection.On<string>("Send", data =>
            {
                Console.WriteLine($"Send: {data}");
            });

            /*
            connection.On<object>("Move", obj =>
            {
                Console.WriteLine($"Move: {obj}");
            });
            */
            
            connection.On<string, int, int>("Move", (a, b, c)  =>
            {
                Console.WriteLine($"Move: {a} {b} {c}");
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Send", "Hello");

            return "";
        }
    }
}
