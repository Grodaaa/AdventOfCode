namespace AdventOfCode.Day09
{
    class Step
    {
        public Direction Direction { get; set; }
        public int Steps { get; set; }
    }

    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    class Coordinates
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Coordinates otherCoord)
            {
                return otherCoord.Latitude == Latitude && otherCoord.Longitude == Longitude;
            }
            return false; ;
        }
    }
}