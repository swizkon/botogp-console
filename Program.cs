using System;
using Microsoft.AspNetCore.SignalR.Client;

using System.Threading.Tasks;

namespace BotoGP.ConsoleClient
{

    class Program
    {
        static void Main(string[] args)
        {
            var name = ReadInput("Enter racer name:");
            Console.WriteLine($"Hello {name}!");

            var tour = ReadInput("Enter tour name:");
            Console.WriteLine($"Hello {name}!");


            IRaceBot bot = new ConsoleBot();
            var t = Test(bot, name, tour).Result;


            Console.ReadKey();
        }

        static async Task<string> Test(IRaceBot raceBot, string name, string tour)
        {
            var connection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:5002/race")
                            .WithConsoleLogger()
                            .Build();

            connection.On<string>("Send", data =>
            {
                Console.WriteLine($"Received message Send: {data}");
            });

            connection.On<string, int, int>("Move", (racer, x, y) =>
            {
                raceBot.RacerMove(racer, x, y);
            });
            
            /*
            connection.On<string, int, int>("Move", (a, b, c) =>
            {
                Console.WriteLine($"Received message Move: {a} {b} {c}");
            });
            */

            await connection.StartAsync();

            await connection.InvokeAsync("Send", $"Hello! {name} joined the tour");

            var message = "";

            do
            {
                message = ReadInput("Write messages: (q to exit)");

                // if(message != "q")
                await connection.InvokeAsync("Send", message);
            } while (message != "q");



            return "";
        }


        static string ReadInput(string label)
        {
            var result = "";

            do
            {
                Console.WriteLine(label);
                result = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(result));

            return result;
        }
    }
}
