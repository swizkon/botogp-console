
namespace BotoGP.ConsoleClient
{
    using System;
    using BotoGP.Domain;

    interface IRaceBot
    {
        void RaceStarted();

        void RacerMove(string racer, int x, int y);

        int NextMove(string racer, RaceState currentState);
    }

    public class ConsoleBot : IRaceBot
    {
        public void RaceStarted()
        {

        }

        public int NextMove(string racer, RaceState currentState)
        {
            Console.WriteLine($"{this.GetType().Name} NextMove");

            Console.WriteLine("PRESS 1-9 for your next move (0 to surrender)");
            var k = Console.ReadKey(false).KeyChar;

            if (k >= '0' && k <= '9')
                return int.Parse(k.ToString());

            return 0;
        }

        public void RacerMove(string racer, int x, int y)
        {
            Console.WriteLine($"Received message Move: {racer} {x} {y}");
        }
    }
}
