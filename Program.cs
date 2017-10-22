using System;
using Microsoft.AspNetCore.SignalR.Client;

using System.Threading.Tasks;
using BotoGP.Domain;

namespace BotoGP.ConsoleClient
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = Console.BackgroundColor;

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
                Console.WriteLine($"Received message Move: {racer}");
                // raceBot.RacerMove(racer, x, y);
            });

            connection.On<string, int, int, int, int>("RaceStateChange", (racer, x, y, verticalVelocity, horizontalVelocity) =>
            {
                var newState = new RaceState(x, y, verticalVelocity, horizontalVelocity);
                var next = raceBot.NextMove(racer, newState);
                
                 connection.InvokeAsync("NextMove", racer, next);
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Send", $"Hello! {name} joined {tour}");
            
            await connection.InvokeAsync("JoinTour", name, tour);


            /*
            var message = 0;

            do
            {
                message = raceBot.NextMove(name, null);
                // if(message != "q")
                await connection.InvokeAsync("NextMove", name, message);
            } while (message != 0);
            */
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
