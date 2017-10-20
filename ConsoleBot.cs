
namespace BotoGP.ConsoleClient
{
    using System;

    interface IRaceBot
    {
        void RaceStarted();

        void RacerMove(string racer, int x, int y);
    }

    public class ConsoleBot : IRaceBot
    {
        public void RaceStarted()
        {

        }

        public void RacerMove(string racer, int x, int y)
        {
            Console.WriteLine($"Received message Move: {racer} {x} {y}");
        }

    }
}