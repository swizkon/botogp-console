using System;

namespace BotoGP.Domain
{
    public class RaceState
    {
        public RaceState(int x, int y, int verticalVelocity, int horizontalVelocity)
        {
            this.x = x;
            this.y = y;
            this.VerticalVelocity = verticalVelocity;
            this.HorizontalVelocity = horizontalVelocity;
        }

        public int x { get; set; }

        public int y { get; set; }

        public int VerticalVelocity { get; set; }

        public int HorizontalVelocity { get; set; }
    }
}