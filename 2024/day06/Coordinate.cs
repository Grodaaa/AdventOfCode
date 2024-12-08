namespace AdventOfCode.day06
{
    internal class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Obstacle { get; set; }
        public string Guard { get; set; } = string.Empty;
        public int XDir { get; set; }
        public int YDir { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Coordinate other)
            {
                return X == other.X && Y == other.Y && XDir == other.XDir && YDir == other.YDir;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, XDir, YDir);
        }
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
