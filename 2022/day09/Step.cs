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
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Coordinates otherCoord)
            {
                return otherCoord.X == X && otherCoord.Y == Y;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}